<?php
class LibraryManager
{
    public function getLibraries()
    {
        $librariesCache = Yii::app()->cacheDB->get('libraries' . '\\' . Yii::app()->browser->getBrowser());
        if ($librariesCache !== FALSE)
        {
            if (isset(Yii::app()->session['libraries']))
                $libraries = Yii::app()->session['libraries'];
        }
        else
            Yii::app()->cacheDB->flush();

        if (!isset($libraries))
        {
            $rootFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..' . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Libraries');
            $rootFolder = new DirectoryIterator($rootFolderPath);

            $librariesDependency = new CDirectoryCacheDependency();
            $librariesDependency->directory = $rootFolderPath;
            $librariesDependency->recursiveLevel = 2;

            foreach ($rootFolder as $libraryFolder)
            {
                if ($libraryFolder->isDir() && !$libraryFolder->isDot())
                {
                    $libraryName = $libraryFolder->getBasename();
                    $library = Yii::app()->cacheDB->get($libraryName . '\\' . Yii::app()->browser->getBrowser());
                    if ($library === false)
                    {
                        $library = new Library();
                        $library->name = $libraryName;

                        $storagePath = realpath($libraryFolder->getPathname() . DIRECTORY_SEPARATOR . "Primary Root");
                        if (file_exists($storagePath))
                        {
                            $library->storagePath = $storagePath;
                            $library->storageFile = $library->storagePath . DIRECTORY_SEPARATOR . "SalesDepotCache.xml";
                            $library->storageLink = Yii::app()->baseUrl . '/' . Yii::app()->params['librariesRoot'] . '/' . "Libraries" . '/' . $libraryFolder->getBasename() . '/' . "Primary Root";
                        }
                        else
                        {
                            $library->storagePath = realpath($libraryFolder->getPathname());
                            $library->storageLink = Yii::app()->baseUrl . '/' . Yii::app()->params['librariesRoot'] . '/' . "Libraries" . '/' . $libraryFolder->getBasename();
                        }
                        $library->storageFile = realpath($library->storagePath . DIRECTORY_SEPARATOR . 'SalesDepotCache.xml');

                        $library->presentationPreviewContainerPath = $library->storagePath . DIRECTORY_SEPARATOR . '!QV';
                        $library->universalPreviewContainerPath = $library->storagePath . DIRECTORY_SEPARATOR . '!WV';

                        $library->presentationPreviewContainerLink = $library->storageLink . '/!QV';
                        $library->universalPreviewContainerLink = $library->storageLink . '/!WV';
                        $library->logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $libraryFolder->getBasename() . "/no_logo.png";
                        $library->load();

                        $dependency = new CFileCacheDependency($library->storageFile);
                        Yii::app()->cacheDB->set($libraryName . '\\' . Yii::app()->browser->getBrowser(), $library, (60 * 60 * 24 * 7), $dependency);
                    }
                    $libraries[] = $library;
                }
            }
            if (isset($libraries))
            {
                Yii::app()->session['libraries'] = $libraries;
                Yii::app()->cacheDB->set('libraries' . '\\' . Yii::app()->browser->getBrowser(), 'libraries', (60 * 60 * 24 * 7), $librariesDependency);
            }
        }
        else
        {
            $libraries = Yii::app()->session['libraries'];
        }
        return $libraries;
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
            $libraries = LibraryManager::getLibraries();
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