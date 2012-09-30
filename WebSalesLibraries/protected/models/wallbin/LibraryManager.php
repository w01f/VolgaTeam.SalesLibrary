<?php
class LibraryManager
{
    public function getLibraries()
    {
        if (!isset(Yii::app()->session['sessionKey']))
            Yii::app()->session['sessionKey'] = uniqid();
        $librariesCache = Yii::app()->cacheDB->get(Yii::app()->session['sessionKey']);
        if ($librariesCache !== FALSE)
        {
            if (isset(Yii::app()->session['libraries']))
                $libraries = Yii::app()->session['libraries'];
        }

        if (!isset($libraries))
        {
            $rootFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..' . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Libraries');
            $rootFolder = new DirectoryIterator($rootFolderPath);

            foreach ($rootFolder as $libraryFolder)
            {
                if ($libraryFolder->isDir() && !$libraryFolder->isDot())
                {
                    $libraryName = $libraryFolder->getBasename();
                    $storagePath = $libraryFolder->getPathname();
                    $storageLink = Yii::app()->baseUrl . '/' . Yii::app()->params['librariesRoot'] . '/Libraries/' . $libraryFolder->getBasename();
                    $storageFile = realpath($storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCacheLight.xml');
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
                        $libraryRecord = LibraryStorage::model()->findByPk($libraryId);
                        if ($libraryRecord !== null)
                        {
                            $library = Yii::app()->cacheDB->get($libraryId);
                            if ($library === false)
                            {
                                $library = new Library();
                                $library->name = $libraryName;
                                $library->id = $libraryId;
                                $library->storagePath = $storagePath;
                                $library->storageLink = $storageLink;
                                $library->logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $libraryFolder->getBasename() . "/no_logo.png";
                                $library->load();
                                Yii::app()->cacheDB->set($library->id, $library, (60 * 60 * 24 * 7));
                            }
                            $libraries[] = $library;
                        }
                    }
                }
            }
            if (isset($libraries))
            {
                usort($libraries, "Library::libraryComparer");
                Yii::app()->session['libraries'] = $libraries;
                Yii::app()->cacheDB->set(Yii::app()->session['sessionKey'], 'libraries', (60 * 60 * 24 * 7));
            }
        }
        else
        {
            $libraries = Yii::app()->session['libraries'];
        }
        return $libraries;
    }

    public function getLibraryById($libraryId)
    {
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
    }

    public function getSelectedLibrary()
    {
        if (isset(Yii::app()->request->cookies['selectedLibraryName']->value))
        {
            $selectedLibraryName = Yii::app()->request->cookies['selectedLibraryName']->value;
            $libraries = $this->getLibraries();
            foreach ($libraries as $library)
            {
                if ($library->name == $selectedLibraryName)
                {
                    $selectedLibrary = $library;
                    break;
                }
            }
        }
        if (!isset($selectedLibrary))
        {
            $libraries = $this->getLibraries();
            $selectedLibrary = $libraries[0];
            $this->setSelectedLibraryName($selectedLibrary->name);
        }
        return $selectedLibrary;
    }

    public function setSelectedLibraryName($libraryName)
    {
        if (isset($libraryName))
        {
            $cookie = new CHttpCookie('selectedLibraryName', $libraryName);
            $cookie->expire = time() + (60 * 60 * 24 * 7);
            Yii::app()->request->cookies['selectedLibraryName'] = $cookie;
        }
    }

    public function getSelectedPage()
    {
        $selectedLibrary = $this->getSelectedLibrary();
        if (isset(Yii::app()->request->cookies['selectedPageName']->value))
        {
            $selectedPageName = Yii::app()->request->cookies['selectedPageName']->value;
            foreach ($selectedLibrary->pages as $page)
            {
                if ($page->name == $selectedPageName)
                {
                    $selectedPage = $page;
                    break;
                }
            }
        }
        if (!isset($selectedPage))
        {
            $selectedPage = count($selectedLibrary->pages) > 0 ? $selectedLibrary->pages[0] : NULL;
            $this->setSelectedPageName($selectedPage->name);
        }
        return $selectedPage;
    }

    public function setSelectedPageName($pageName)
    {
        if (isset($pageName))
        {
            $cookie = new CHttpCookie('selectedPageName', $pageName);
            $cookie->expire = time() + (60 * 60 * 24 * 7);
            Yii::app()->request->cookies['selectedPageName'] = $cookie;
        }
    }
}

?>