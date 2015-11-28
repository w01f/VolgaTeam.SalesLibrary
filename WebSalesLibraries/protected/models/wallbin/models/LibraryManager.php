<?php

	/**
	 * Class LibraryManager
	 */
	class LibraryManager
	{
		/**
		 * @return Library[]
		 */
		public function getLibraries()
		{
			$libraries = null;
			if (!isset(Yii::app()->session['sessionKey']))
				Yii::app()->session['sessionKey'] = uniqid();
			$librariesCache = Yii::app()->cacheDB->get(Yii::app()->session['sessionKey']);
			if (isset($librariesCache))
			{
				if (isset(Yii::app()->session['libraries']))
					$libraries = Yii::app()->session['libraries'];
			}
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				if (isset(Yii::app()->user->role))
					$isAdmin = Yii::app()->user->role == 2;
				else
					$isAdmin = true;
				if (isset($userId) && !$isAdmin)
					$availableLibraryIds = UserLibraryRecord::getLibraryIdsByUserAngHisGroups($userId);
			}
			if (!is_array($libraries))
			{
				$rootFolderPath = realpath(Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Libraries');
				$rootFolder = new DirectoryIterator($rootFolderPath);

				$aliases = $this->getLibraryAliases();

				/** @var $libraryFolder DirectoryIterator */
				foreach ($rootFolder as $libraryFolder)
				{
					if ($libraryFolder->isDir() && !$libraryFolder->isDot())
					{
						$libraryName = $libraryFolder->getBasename();

						$originalStoragePath = $libraryFolder->getPathname();
						$originalStorageLink =Yii::app()->baseUrl . '/' . Yii::app()->params['librariesRoot'] . '/Libraries/' . $libraryFolder->getBasename();

						$storagePath = $originalStoragePath;
						$storageLink = $originalStorageLink;
						$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'z_library_data_info.xml');
						if (!file_exists($storageFile))
						{
							$storagePath .= DIRECTORY_SEPARATOR . 'Primary Root';
							$storageLink .= '/Primary Root';
							$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'z_library_data_info.xml');
						}
						if (!file_exists($storageFile))
						{
							$storagePath = $originalStoragePath;
							$storageLink = $originalStorageLink;
							$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCacheLight.xml');
						}
						if (!file_exists($storageFile))
						{
							$storagePath .= DIRECTORY_SEPARATOR . 'Primary Root';
							$storageLink .= '/Primary Root';
							$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCacheLight.xml');
						}
						if (file_exists($storageFile))
						{
							$doc = new DOMDocument();
							$doc->load($storageFile);
							$libraryId = trim($doc->getElementsByTagName("Identifier")->item(0)->nodeValue);
							$libraryRecord = LibraryRecord::model()->findByPk($libraryId);
							if (isset($libraryRecord))
							{
								if ((isset($availableLibraryIds) && in_array($libraryId, $availableLibraryIds)) || (!isset($userId) || (isset($isAdmin) && $isAdmin)))
								{
									//$library = Yii::app()->cacheDB->get($libraryId);
									//if (!isset($library))
									//{
										$library = new Library();
										$library->name = $libraryName;
										$library->id = $libraryId;
										$library->groupId = $libraryRecord->id_group;
										$library->order = $libraryRecord->order;
										$library->storagePath = $storagePath;
										$library->storageLink = $storageLink;
										$library->logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $libraryFolder->getBasename() . "/no_logo.png";

										$library->alias = $libraryName;
										if (array_key_exists($libraryName, $aliases))
											$library->alias = $aliases[$libraryName];

										$library->load();
										//Yii::app()->cacheDB->set($library->id, $library, (60 * 60 * 24 * 7));
									//}
									$libraries[] = $library;
								}
							}
						}
					}
				}
				if (is_array($libraries) && count($libraries) > 0)
				{
					usort($libraries, "Library::libraryComparerByName");
					Yii::app()->session['libraries'] = $libraries;
					Yii::app()->cacheDB->set(Yii::app()->session['sessionKey'], 'libraries', (60 * 60 * 24 * 7));
				}
				else
					unset(Yii::app()->session['libraries']);
			}
			if (is_array($libraries))
				return $libraries;
			return array();
		}

		/**
		 * @return LibraryGroup[]
		 */
		public function getLibraryGroups()
		{
			$libraryGroups = array();
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				if (isset(Yii::app()->user->role))
					$isAdmin = Yii::app()->user->role == 2;
				else
					$isAdmin = true;
				if (isset($userId) && !$isAdmin)
					$availableLibraryIds = UserLibraryRecord::getLibraryIdsByUserAngHisGroups($userId);
			}
			$libraryGroupRecords = LibraryGroupRecord::model()->findAll();

			if (isset($libraryGroupRecords) && count($libraryGroupRecords) > 0)
			{
				foreach ($libraryGroupRecords as $libraryGroupRecord)
				{
					$libraryGroup = new LibraryGroup();
					$libraryGroup->id = $libraryGroupRecord->id;
					$libraryGroup->order = $libraryGroupRecord->order;
					$libraryGroup->name = $libraryGroupRecord->name;
					$libraryRecords = LibraryRecord::model()->findAll('id_group=?', array($libraryGroupRecord->id));
					if (isset($libraryRecords) && count($libraryRecords) > 0)
					{
						foreach ($libraryRecords as $libraryRecord)
						{
							if ((isset($availableLibraryIds) && in_array($libraryRecord->id, $availableLibraryIds)) || (!isset($userId) || (isset($isAdmin) && $isAdmin)))
							{
								$library = new Library();
								$library->id = $libraryRecord->id;
								$library->groupId = $libraryRecord->id_group;
								$library->name = $libraryRecord->name;
								$library->order = $libraryRecord->order;
								$libraryGroup->libraries[] = $library;
							}
						}
					}
					if (isset($libraryGroup->libraries) && count($libraryGroup->libraries) > 0)
					{
						usort($libraryGroup->libraries, "Library::libraryComparerByGroup");
						$libraryGroups[] = $libraryGroup;
					}
				}
			}
			else
			{
				$libraryGroup = new LibraryGroup();
				$libraryGroup->order = 0;
				$libraryGroup->name = Yii::app()->params['stations']['tab_name'];
				$libraryRecords = LibraryRecord::model()->findAll();
				if (isset($libraryRecords) && count($libraryRecords) > 0)
				{
					foreach ($libraryRecords as $libraryRecord)
					{
						if ((isset($availableLibraryIds) && in_array($libraryRecord->id, $availableLibraryIds)) || (!isset($userId) || (isset($isAdmin) && $isAdmin)))
						{
							$library = new Library();
							$library->id = $libraryRecord->id;
							$library->name = $libraryRecord->name;
							$libraryGroup->libraries[] = $library;
						}
					}
				}
				if (isset($libraryGroup->libraries) && count($libraryGroup->libraries) > 0)
				{
					usort($libraryGroup->libraries, "Library::libraryComparerByName");
					$libraryGroups[] = $libraryGroup;
				}
			}
			usort($libraryGroups, "LibraryGroup::libraryGroupComparer");
			return $libraryGroups;
		}

		/**
		 * @param $libraryId
		 * @return Library
		 */
		public function getLibraryById($libraryId)
		{
			/** @var $selectedLibrary Library */
			$libraries = $this->getLibraries();
			if (isset($libraries))
			{
				foreach ($libraries as $library)
				{
					if ($library->id == $libraryId)
					{
						$selectedLibrary = $library;
						break;
					}
				}
			}
			if (isset($selectedLibrary))
				return $selectedLibrary;
			return null;
		}

		/**
		 * @param $libraryName
		 * @return Library
		 */
		public function getLibraryByName($libraryName)
		{
			/** @var $selectedLibrary Library */
			$libraries = $this->getLibraries();
			if (isset($libraries))
			{
				foreach ($libraries as $library)
				{
					if ($library->name == $libraryName)
					{
						$selectedLibrary = $library;
						break;
					}
				}
			}
			if (isset($selectedLibrary))
				return $selectedLibrary;
			return null;
		}

		/**
		 * @return array
		 */
		private function getLibraryAliases()
		{
			$aliases = array();
			try
			{
				$aliasesFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'jqmlibraryalias.xml';
				if (file_exists($aliasesFilePath))
				{
					$aliasesContent = file_get_contents($aliasesFilePath);
					if ($aliasesContent)
					{
						$aliasesDom = new DOMDocument();
						$aliasesDom->loadXML($aliasesContent);
						$xpath = new DomXPath($aliasesDom);

						/** @var $queryResult DOMElement[] */
						$queryResult = $xpath->query('//LibraryAlias/Library');
						foreach ($queryResult as $node)
						{
							$libraryName = $node->getAttribute('Name');
							$libraryAlias = $node->getAttribute('Alias');
							$aliases[$libraryName] = $libraryAlias;
						}
					}
				}
			} catch (Exception $e)
			{
			}
			return $aliases;
		}
	}
