<?php
	class WallbinUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";
			$rootFolderPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Libraries';
			$rootFolder = new DirectoryIterator($rootFolderPath);
			foreach ($rootFolder as $libraryFolder)
			{
				if ($libraryFolder->isDir() && !$libraryFolder->isDot())
				{
					$libraryName = $libraryFolder->getBasename();
					$storagePath = $libraryFolder->getPathname();
					$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCache.json');
					$storageLink = Yii::app()->baseUrl . '/' . Yii::app()->params['librariesRoot'] . '/Libraries/' . $libraryFolder->getBasename();
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
							$library = CJSON::decode($storageContent);
							$library['name'] = $libraryName;
							$libraryId = $library['id'];
							$libraryIds[] = $libraryId;
							$updated = LibraryStorage::updateData($library, $sourceDate, $storagePath);
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
							}
						}
					}
				}
			}

			if (isset($libraryIds))
			{
				$libraryRecords = LibraryStorage::model()->findAll();
				if (isset($libraryRecords))
					foreach ($libraryRecords as $libraryRecord)
						if (!in_array($libraryRecord->id, $libraryIds))
						{
							UserLibraryStorage::model()->deleteAll('id_library = ?', array($libraryRecord->id));
							LibraryStorage::clearData($libraryRecord->id);
							LinkStorage::clearByLibrary($libraryRecord->id);
							FolderStorage::clearByLibrary($libraryRecord->id);
							LibraryPageStorage::clearByLibrary($libraryRecord->id);
							$libraryRecord->delete();
							Yii::app()->cacheDB->flush();
						}
			}

			$categoriesPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'SDSearch.xml';
			if (file_exists($categoriesPath))
			{
				$categoriesContent = file_get_contents($categoriesPath);
				if ($categoriesContent)
				{
					$categories = new DOMDocument();
					$categories->loadXML($categoriesContent);
					$xpath = new DomXPath($categories);

					$queryResult = $xpath->query('//SDSearch/Category');
					foreach ($queryResult as $node)
					{
						$groupName = $node->getAttribute('Name');
						$tagNodes = $node->getElementsByTagName('Tag');
						foreach ($tagNodes as $tagNode)
						{
							$categoryRecord = new Category();
							$categoryRecord->category = $groupName;
							$categoryRecord->tag = trim($tagNode->getAttribute('Value'));
							$categoryRecords[] = $categoryRecord;
						}
					}
				}
			}
			CategoryStorage::clearData();
			if (isset($categoryRecords))
				CategoryStorage::loadData($categoryRecords);

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
			SuperFilterStorage::clearData();
			if (isset($superFilterRecords))
				SuperFilterStorage::loadData($superFilterRecords);

			LibraryGroupStorage::clearData();
			$libraryGroupFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'groups.txt';
			if (file_exists($libraryGroupFilePath))
				LibraryGroupStorage::updateData(file($libraryGroupFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES));

			GroupTemplateStorage::clearData();
			$groupTemplateFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'properties.txt';
			if (file_exists($groupTemplateFilePath))
				GroupTemplateStorage::updateData(file($groupTemplateFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES));

			$liveLinkRecords = LinkStorage::model()->findAll();
			if (isset($liveLinkRecords))
				foreach ($liveLinkRecords as $linkRecord)
					$liveLinkIds[] = $linkRecord->id;
			if (isset($liveLinkIds))
			{
				FavoritesLinkStorage::clearByLinkIds($liveLinkIds);
				UserLinkCartStorage::clearByLinkIds($liveLinkIds);
				QPageLinkStorage::clearByLinkIds($liveLinkIds);
				LinkRateStorage::clearByLinkIds($liveLinkIds);
			}

			echo "Job completed.\n";
		}
	}