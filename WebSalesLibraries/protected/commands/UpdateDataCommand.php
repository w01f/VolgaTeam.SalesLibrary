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
                        $updated = LibraryStorage::updateData($library, $sourceDate, $storagePath);
                        if ($updated)
                        {
                            echo "Updating HTML cache for " . $libraryName . "...\n";
                            $libraryId = $library['id'];
                            $library = new Library();
                            $library->name = $libraryName;
                            $library->id = $libraryId;
                            $library->storagePath = $storagePath;
                            $library->storageLink = $storageLink;
                            $library->logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $libraryFolder->getBasename() . "/no_logo.png";
                            $library->load();
                            $library->buildCache($this);
                            echo "HTML cache for " . $libraryName . " updated.\n";
                        }
                    }
                }

                if (file_exists($referencesFile))
                {
                    $referencesContent = file_get_contents($referencesFile);
                    if ($referencesContent)
                    {
                        $categories = CJSON::decode($referencesContent);
                        CategoryStorage::updateData($categories);
                    }
                }
            }
        }

        LibraryGroupStorage::clearData();
        $libraryGroupFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'groups.txt';
        if (file_exists($libraryGroupFilePath))
            LibraryGroupStorage::updateData(file($libraryGroupFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES));

        echo "Job completed.\n";
    }

}

?>
