<?php
	class UpdateDataCommand extends CConsoleCommand
	{
		public function run($args)
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
					$referencesFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotReferences.json');
					$storageLink = Yii::app()->baseUrl . '/' . Yii::app()->params['librariesRoot'] . '/Libraries/' . $libraryFolder->getBasename();
					if (!file_exists($storageFile))
					{
						$storagePath .= DIRECTORY_SEPARATOR . 'Primary Root';
						$storageLink .= '/Primary Root';
						$storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCache.json');
						$referencesFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotReferences.json');
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
								$library->buildCache($this);
								echo "HTML cache for " . $libraryName . " updated.\n";
								unset($library);
							}
						}
					}

					if (file_exists($referencesFile))
					{
						$referencesContent = file_get_contents($referencesFile);
						if ($referencesContent)
						{
							$references = CJSON::decode($referencesContent);
							if (array_key_exists('categories', $references))
								if (isset($references['categories']))
									CategoryStorage::updateData($references['categories']);
							if (array_key_exists('superFilters', $references))
								if (isset($references['superFilters']))
									SuperFilterStorage::updateData($references['superFilters']);
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
