<?
	use application\models\wallbin\models\web\Library as Library;
	use application\models\wallbin\models\web\Category as Category;

	/**
	 * Class WallbinUpdateAction
	 */
	class WallbinUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";
			$rootFolderPath = \application\models\wallbin\models\web\LibraryManager::getLibrariesRootPath();
			$libraryIds = array();
			foreach (Yii::app()->params['stations']['locations'] as $libraryLocation)
			{
				$librariesLocationPath = $rootFolderPath . DIRECTORY_SEPARATOR . $libraryLocation;
				/** @var $libraryRootFolder DirectoryIterator[] */
				$libraryRootFolder = new DirectoryIterator($librariesLocationPath);
				foreach ($libraryRootFolder as $libraryFolder)
				{
					if ($libraryFolder->isDir() && !$libraryFolder->isDot())
					{
						$libraryName = $libraryFolder->getBasename();
						$originalStoragePath = $libraryFolder->getPathname();
						$originalStorageLink = \application\models\wallbin\models\web\LibraryManager::getLibrariesRootLink() . '/' . str_replace('\\', '/', $libraryLocation) . '/' . $libraryFolder->getBasename();
						$storagePath = $originalStoragePath;
						$storageLink = $originalStorageLink;
						$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'z_library_data_cloud.json');
						if (!file_exists($storageFile))
						{
							$storagePath .= DIRECTORY_SEPARATOR . 'Primary Root';
							$storageLink .= '/Primary Root';
							$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'z_library_data_cloud.json');
						}
						if (!file_exists($storageFile))
						{
							$storagePath = $originalStoragePath;
							$storageLink = $originalStorageLink;
							$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCache.json');
						}
						if (!file_exists($storageFile))
						{
							$storagePath .= DIRECTORY_SEPARATOR . 'Primary Root';
							$storageLink .= '/Primary Root';
							$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCache.json');
						}
						if (file_exists($storageFile))
						{
							$sourceDate = filemtime($storageFile);
							$storageContent = file_get_contents($storageFile);
							if ($storageContent)
							{
								$library = CJSON::decode($storageContent, true);
								$library['name'] = $libraryName;
								$libraryId = $library['id'];
								$libraryIds[] = $libraryId;
								$updated = LibraryRecord::updateDataFromSoap($library, $sourceDate, $storagePath);
								if ($updated)
								{
									echo "Updating HTML cache for " . $libraryName . "...\n";
									$library = new Library();
									$library->name = $libraryName;
									$library->id = $libraryId;
									$library->storagePath = $storagePath;
									$library->storageLink = $storageLink;
									$library->logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $libraryFolder->getBasename() . "/no_logo.png";
									$library->load();
									$library->buildCache($this->controller);
									echo "HTML cache for " . $libraryName . " updated.\n";
									unset($library);

									MetaDataRecord::setData('external-library-links', 'last-update', date(Yii::app()->params['sourceDateFormat'], time()));

									Yii::app()->cacheDB->flush();
								}
							}
						}
					}
				}
			}

			/** @var $libraryRecords LinkRecord[] */
			$libraryRecords = LibraryRecord::model()->findAll();
			foreach ($libraryRecords as $libraryRecord)
				if (!in_array($libraryRecord->id, $libraryIds))
				{
					UserLibraryRecord::model()->deleteAll('id_library = ?', array($libraryRecord->id));
					LibraryRecord::clearData($libraryRecord->id);
					LinkRecord::clearByLibrary($libraryRecord->id);
					FolderRecord::clearByLibrary($libraryRecord->id);
					LibraryPageRecord::clearByLibrary($libraryRecord->id);
					$libraryRecord->delete();
					Yii::app()->cacheDB->flush();
				}

			$categoriesPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'SDSearch.xml';
			$maxTags = 10;
			$doTagCount = true;
			if (file_exists($categoriesPath))
			{
				$categoriesContent = file_get_contents($categoriesPath);
				if ($categoriesContent)
				{
					$categories = new DOMDocument();
					$categories->loadXML($categoriesContent);
					$xpath = new DomXPath($categories);

					/** @var $queryResult DOMNodeList */
					$queryResult = $xpath->query('//SDSearch/Category');
					foreach ($queryResult as $node)
					{
						/** @var $node DOMElement */
						$groupName = $node->getAttribute('Name');
						$groupDescription = $node->getAttribute('Description');
						/** @var $tagNodes DOMElement[] */
						$tagNodes = $node->getElementsByTagName('Tag');
						foreach ($tagNodes as $tagNode)
						{
							$categoryRecord = new Category();
							$categoryRecord->category = $groupName;
							$categoryRecord->description = $groupDescription;
							$categoryRecord->tag = trim($tagNode->getAttribute('Value'));
							$categoryRecords[] = $categoryRecord;
						}
					}

					$queryResult = $xpath->query('//SDSearch/MaxTags');
					$maxTags = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

					$queryResult = $xpath->query('//SDSearch/TagCount');
					$doTagCount = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
				}
			}
			CategoryRecord::clearData();
			if (isset($categoryRecords))
			{
				CategoryRecord::loadData($categoryRecords);
				MetaDataRecord::setData('link-categories', 'last-update', date(Yii::app()->params['sourceDateFormat'], filemtime($categoriesPath)));
				MetaDataRecord::setData('link-categories', 'max-tags', $maxTags);
				MetaDataRecord::setData('link-categories', 'tag-count', $doTagCount);
			}
			else
				MetaDataRecord::setData('link-categories', 'last-update', date(Yii::app()->params['sourceDateFormat'], time()));

			$superFiltersPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'superfilter.xml';
			if (file_exists($superFiltersPath))
			{
				$superFiltersContent = file_get_contents($superFiltersPath);
				if ($superFiltersContent)
				{
					$superFilters = new DOMDocument();
					$superFilters->loadXML($superFiltersContent);
					$xpath = new DomXPath($superFilters);

					$queryResult = $xpath->query('//superfilters/filter');
					foreach ($queryResult as $node)
						$superFilterRecords[] = trim($node->nodeValue);
				}
			}
			SuperFilterRecord::clearData();
			if (isset($superFilterRecords))
			{
				SuperFilterRecord::loadData($superFilterRecords);
				MetaDataRecord::setData('link-super-filters', 'last-update', date(Yii::app()->params['sourceDateFormat'], filemtime($superFiltersPath)));
			}
			else
				MetaDataRecord::setData('link-super-filters', 'last-update', date(Yii::app()->params['sourceDateFormat'], time()));

			LibraryGroupRecord::clearData();
			$libraryGroupFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'groups.txt';
			if (file_exists($libraryGroupFilePath))
				LibraryGroupRecord::updateData(file($libraryGroupFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES));

			GroupTemplateRecord::clearData();
			$groupTemplateFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'properties.txt';
			if (file_exists($groupTemplateFilePath))
				GroupTemplateRecord::updateData(file($groupTemplateFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES));

			LinkRecord::clearDeadLinkData();

			echo "Job completed.\n";
		}
	}