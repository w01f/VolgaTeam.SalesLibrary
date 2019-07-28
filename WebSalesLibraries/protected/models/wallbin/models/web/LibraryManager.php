<?php

	namespace application\models\wallbin\models\web;
	/**
	 * Class LibraryManager
	 */
	class LibraryManager
	{
		public $loadedCache;

		/**
		 * @return array[]
		 */
		public function getAvailableLibraries()
		{
			$libraries = array();
			$availableLibraryIds = array();
			$useLibraryByUserFilter = \UserIdentity::isUserAuthorized() && !\UserIdentity::isUserAdmin();
			if ($useLibraryByUserFilter)
			{
				$userId = \UserIdentity::getCurrentUserId();
				$availableLibraryIds = \UserLibraryRecord::getLibraryIdsByUserAngHisGroups($userId);
			}

			$libraryAliases = $this->getLibraryAliases();

			/** @var \LibraryRecord[] $libraryRecords */
			$libraryRecords = \LibraryRecord::model()->findAll();
			foreach ($libraryRecords as $libraryRecord)
			{
				if (count($availableLibraryIds) > 0 && !in_array($libraryRecord->id, $availableLibraryIds))
					continue;

				$library = $this->loadLibraryFromDatabase($libraryRecord, false);

				if (array_key_exists($library->name, $libraryAliases))
					$library->alias = $libraryAliases[$library->name];

				$libraries[] = $this->loadLibraryFromDatabase($libraryRecord, false);
			}

			return $libraries;
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
							if (!$useLibraryByUserFilter || in_array($libraryRecord->id, $availableLibraryIds))
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
		 * @param bool $loadLibraryContent
		 * @return Library
		 */
		public function getLibraryById($libraryId, $loadLibraryContent = false)
		{
			/** @var $selectedLibrary Library */
			$libraries = $this->getLibraryCache();
			if (!isset($libraries))
				$libraries = array();

			if (array_key_exists($libraryId, $libraries))
			{
				$libraryCache = $libraries[$libraryId];
				if (!$loadLibraryContent || $libraryCache['contentLoaded'] == true)
					$selectedLibrary = $libraryCache['library'];
			}

			if (!isset($selectedLibrary))
			{
				$selectedLibrary = $this->loadLibraryFromDatabaseById($libraryId, $loadLibraryContent);
				$libraries[$libraryId] = array(
					'library' => $selectedLibrary,
					'contentLoaded' => $loadLibraryContent
				);
				$this->saveLibraryCache($libraries);
			}

			if (isset($selectedLibrary))
				return $selectedLibrary;
			return null;
		}

		/**
		 * @param $libraryName
		 * @param bool $loadLibraryContent
		 * @return Library
		 */
		public function getLibraryByName($libraryName, $loadLibraryContent = false)
		{
			/** @var \LibraryRecord $libraryRecord */
			$libraryRecord = \LibraryRecord::model()->find('name=?', array($libraryName));
			if (!isset($libraryRecord))
				return null;
			return $this->getLibraryById($libraryRecord->id, $loadLibraryContent);
		}

		private function saveLibraryCache($libraries)
		{
			if (!(\Yii::app() instanceof \CConsoleApplication))
			{
				unset(\Yii::app()->session['all-libraries']);
				unset($this->loadedCache);
				if (is_array($libraries) && count($libraries) > 0)
				{
					\Yii::app()->session['all-libraries'] = $libraries;
					\Yii::app()->cacheDB->set(\Yii::app()->session['sessionKey'], 'all-libraries', (60 * 60 * 24 * 7));
				}
			}
		}

		private function getLibraryCache()
		{
			$libraries = null;
			if (!(\Yii::app() instanceof \CConsoleApplication))
			{
				if (isset($this->loadedCache))
					$libraries = $this->loadedCache;
				else
				{
					if (!isset(\Yii::app()->session['sessionKey']))
						\Yii::app()->session['sessionKey'] = uniqid();
					$librariesCache = \Yii::app()->cacheDB->get(\Yii::app()->session['sessionKey']);
					if ($librariesCache !== false)
					{
						if (isset(\Yii::app()->session['all-libraries']))
						{
							$libraries = \Yii::app()->session['all-libraries'];
							$this->loadedCache = $libraries;
						}
					}
				}
			}
			return $libraries;
		}

		private function loadLibraryFromDatabaseById($libraryId, $loadLibraryContent)
		{
			/** @var \LibraryRecord $libraryRecord */
			$libraryRecord = \LibraryRecord::model()->findByPk($libraryId);
			if (isset($libraryRecord))
				return $this->loadLibraryFromDatabase($libraryRecord, $loadLibraryContent);
			return null;
		}

		private function loadLibraryFromDatabase($libraryRecord, $loadLibraryContent)
		{
			/** @var \LibraryRecord $libraryRecord */
			if (isset($libraryRecord))
			{
				$library = new Library();
				$library->name = $libraryRecord->name;
				$library->id = $libraryRecord->id;
				$library->groupId = $libraryRecord->id_group;
				$library->order = $libraryRecord->order;
				$library->lastUpdate = $libraryRecord->last_update;
				$library->storagePath = self::resolveLibraryStoragePath($libraryRecord->path);
				$library->storageLink = self::resolveLibraryStorageLink($libraryRecord->path);
				$library->alias = $libraryRecord->name;
				if ($loadLibraryContent)
					$library->load();
				return $library;
			}
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

		public static function resolveLibraryStoragePath($libraryRelativePath)
		{
			return \application\models\wallbin\models\web\LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $libraryRelativePath;
		}

		public static function resolveLibraryStorageLink($libraryRelativePath)
		{
			return self::getLibrariesRootLink() . '/' . str_replace('\\', '/', $libraryRelativePath);
		}
	}
