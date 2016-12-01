<?php
	namespace application\models\wallbin\models\web;
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
			if (!isset(\Yii::app()->session['sessionKey']))
				\Yii::app()->session['sessionKey'] = uniqid();
			$librariesCache = \Yii::app()->cacheDB->get(\Yii::app()->session['sessionKey']);
			if (isset($librariesCache))
			{
				if (isset(\Yii::app()->session['libraries']))
					$libraries = \Yii::app()->session['libraries'];
			}
			$useLibraryByUserFilter = \UserIdentity::isUserAuthorized() && !\UserIdentity::isUserAdmin();
			if ($useLibraryByUserFilter)
			{
				$userId = \UserIdentity::getCurrentUserId();
				$availableLibraryIds = \UserLibraryRecord::getLibraryIdsByUserAngHisGroups($userId);
			}
			else
				$availableLibraryIds = array();
			if (!is_array($libraries))
			{
				$aliases = $this->getLibraryAliases();
				$rootFolderPath = LibraryManager::getLibrariesRootPath();
				foreach (\Yii::app()->params['stations']['locations'] as $libraryLocation)
				{
					$librariesLocationPath = $rootFolderPath . DIRECTORY_SEPARATOR . $libraryLocation;
					/** @var $libraryRootFolder \DirectoryIterator[] */
					$libraryRootFolder = new \DirectoryIterator($librariesLocationPath);
					foreach ($libraryRootFolder as $libraryFolder)
					{
						/** @var $libraryFolder \DirectoryIterator */
						if ($libraryFolder->isDir() && !$libraryFolder->isDot())
						{
							$libraryName = $libraryFolder->getBasename();

							$originalStoragePath = $libraryFolder->getPathname();
							$originalStorageLink = LibraryManager::getLibrariesRootLink() . '/' . str_replace('\\', '/', $libraryLocation) . '/' . $libraryFolder->getBasename();

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
								$doc = new \DOMDocument();
								$doc->load($storageFile);
								$libraryId = trim($doc->getElementsByTagName("Identifier")->item(0)->nodeValue);
								/** @var \LibraryRecord $libraryRecord */
								$libraryRecord = \LibraryRecord::model()->findByPk($libraryId);
								if (isset($libraryRecord))
								{
									if (!$useLibraryByUserFilter || in_array($libraryId, $availableLibraryIds))
									{
										//$library = Yii::app()->cacheDB->get($libraryId);
										//if (!isset($library))
										//{
										$library = new Library();
										$library->name = $libraryName;
										$library->id = $libraryId;
										$library->groupId = $libraryRecord->id_group;
										$library->order = $libraryRecord->order;
										$library->lastUpdate = $libraryRecord->last_update;
										$library->storagePath = $storagePath;
										$library->storageLink = $storageLink;
										$library->logoPath = \Yii::app()->params['librariesRoot'] . "/Graphics/" . $libraryFolder->getBasename() . "/no_logo.png";

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
				}
				if (is_array($libraries) && count($libraries) > 0)
				{
					usort($libraries, "application\\models\\wallbin\\models\\web\\Library::libraryComparerByName");
					\Yii::app()->session['libraries'] = $libraries;
					\Yii::app()->cacheDB->set(\Yii::app()->session['sessionKey'], 'libraries', (60 * 60 * 24 * 7));
				}
				else
					unset(\Yii::app()->session['libraries']);
			}
			if (is_array($libraries))
				return $libraries;
			return array();
		}

		/**
		 * @return \LibraryGroup[]
		 */
		public function getLibraryGroups()
		{
			$libraryGroups = array();
			$useLibraryByUserFilter = \UserIdentity::isUserAuthorized() && !\UserIdentity::isUserAdmin();
			if ($useLibraryByUserFilter)
			{
				$userId = \UserIdentity::getCurrentUserId();
				$availableLibraryIds = \UserLibraryRecord::getLibraryIdsByUserAngHisGroups($userId);
			}
			else
				$availableLibraryIds = array();
			/** @var  $libraryGroupRecords \LibraryGroupRecord[] */
			$libraryGroupRecords = \LibraryGroupRecord::model()->findAll();
			if (isset($libraryGroupRecords) && count($libraryGroupRecords) > 0)
			{
				foreach ($libraryGroupRecords as $libraryGroupRecord)
				{
					$libraryGroup = new \LibraryGroup();
					$libraryGroup->id = $libraryGroupRecord->id;
					$libraryGroup->order = $libraryGroupRecord->order;
					$libraryGroup->name = $libraryGroupRecord->name;
					/** @var  $libraryRecords \LibraryRecord[] */
					$libraryRecords = \LibraryRecord::model()->findAll('id_group=?', array($libraryGroupRecord->id));
					if (isset($libraryRecords) && count($libraryRecords) > 0)
					{
						foreach ($libraryRecords as $libraryRecord)
						{
							if (!$useLibraryByUserFilter && in_array($libraryRecord->id, $availableLibraryIds))
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
						usort($libraryGroup->libraries, "application\\models\\wallbin\\models\\web\\Library::libraryComparerByGroup");
						$libraryGroups[] = $libraryGroup;
					}
				}
			}
			else
			{
				$libraryGroup = new \LibraryGroup();
				$libraryGroup->order = 0;
				$libraryGroup->name = \Yii::app()->params['stations']['tab_name'];
				/** @var  $libraryRecords \LibraryRecord[] */
				$libraryRecords = \LibraryRecord::model()->findAll();
				if (isset($libraryRecords) && count($libraryRecords) > 0)
				{
					foreach ($libraryRecords as $libraryRecord)
					{
						if (in_array($libraryRecord->id, $availableLibraryIds) || $useLibraryByUserFilter)
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
					usort($libraryGroup->libraries, "application\\models\\wallbin\\models\\web\\Library::libraryComparerByName");
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
				$aliasesFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'jqmlibraryalias.xml';
				if (file_exists($aliasesFilePath))
				{
					$aliasesContent = file_get_contents($aliasesFilePath);
					if ($aliasesContent)
					{
						$aliasesDom = new \DOMDocument();
						$aliasesDom->loadXML($aliasesContent);
						$xpath = new \DomXPath($aliasesDom);

						/** @var $queryResult \DOMElement[] */
						$queryResult = $xpath->query('//LibraryAlias/Library');
						foreach ($queryResult as $node)
						{
							$libraryName = $node->getAttribute('Name');
							$libraryAlias = $node->getAttribute('Alias');
							$aliases[$libraryName] = $libraryAlias;
						}
					}
				}
			}
			catch (\Exception $e)
			{
			}
			return $aliases;
		}

		/** @return string */
		public static function getLibrariesRootPath()
		{
			return \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . \Yii::app()->params['librariesRoot'];
		}

		/** @return string */
		public static function getLibrariesRootLink()
		{
			return \Yii::app()->getBaseUrl(true) . '/' . \Yii::app()->params['librariesRoot'];
		}
	}
