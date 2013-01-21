<?php
class WallbinController extends IsdController
{
    public function getViewPath()
    {
        return YiiBase::getPathOfAlias($this->pathPrefix . 'wallbin');
    }

    public function actionGetColumnsView()
    {
        $libraryManager = new LibraryManager();
        $selectedPage = $libraryManager->getSelectedPage();
        $this->renderPartial('columnsView', array('selectedPage' => $selectedPage), false, true);
    }

    public function actionGetAccordionView()
    {
        $libraryManager = new LibraryManager();
        $selectedPage = $libraryManager->getSelectedPage();
        $selectedPage->loadData(null);
        $this->renderPartial('accordionView', array('libraryPage' => $selectedPage), false, true);
    }

    public function actionGetLibraryDropDownList()
    {
        $libraryManager = new LibraryManager();
        $this->renderPartial('libraryDropDownList', array('libraryManager' => $libraryManager), false, true);
    }

    public function actionGetPageDropDownList()
    {
        $libraryManager = new LibraryManager();
        $this->renderPartial('pageDropDownList', array('selectedLibrary' => $libraryManager->getSelectedLibrary(),
            'selectedPage' => $libraryManager->getSelectedPage()), false, true);
    }

    public function actionGetFoldersList()
    {
        $libraryManager = new LibraryManager();
        $selectedPage = $libraryManager->getSelectedPage();
        $selectedPage->loadData('phone');
        $this->renderPartial('folders', array('page' => $selectedPage), false, true);
    }

    public function actionGetFolderLinksList()
    {
        $folderId = Yii::app()->request->getPost('folderId');

        $libraryManager = new LibraryManager();
        $selectedPage = $libraryManager->getSelectedPage();
        if (isset($selectedPage->folders))
        {
            foreach ($selectedPage->folders as $folder)
                if ($folder->id == $folderId)
                {
                    $selectedFolder = $folder;
                    $selectedFolder->loadFiles();
                    break;
                }
            $this->renderPartial('folderLinks', array('folder' => $selectedFolder), false, true);
        }
    }

    public function actionGetLinkDetails()
    {
        $linkId = Yii::app()->request->getPost('linkId');
        if (isset($linkId))
        {
            $linkRecord = LinkStorage::getLinkById($linkId);
            if (isset($linkRecord))
            {
                $libraryManager = new LibraryManager();
                $library = $libraryManager->getLibraryById($linkRecord->id_library);
                $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                switch ($this->browser)
                {
                    case Browser::BROWSER_IPHONE:
                    case Browser::BROWSER_ANDROID_MOBILE:
                        $link->browser = 'phone';
                        break;
                    default :
                        if ($this->isTabletMobileView)
                            $link->browser = 'phone';
                        else if (Yii::app()->browser->isMobile())
                        {
                            $link->browser = 'mobile';
                        }
                        else
                        {
                            $browser = Yii::app()->browser->getBrowser();
                            switch ($browser)
                            {
                                case 'Internet Explorer':
                                    $link->browser = 'ie';
                                    break;
                                case 'Chrome':
                                case 'Safari':
                                    $link->browser = 'webkit';
                                    break;
                                case 'Firefox':
                                    $link->browser = 'firefox';
                                    break;
                                case 'Opera':
                                    $link->browser = 'opera';
                                    break;
                                default:
                                    $link->browser = 'webkit';
                                    break;
                            }
                        }
                        break;
                }
                $link->load($linkRecord);
                $this->renderPartial('linkDetails', array('link' => $link), false, true);
            }
        }
    }

    public function actionGetLinkPreviewList()
    {
        $linkId = Yii::app()->request->getPost('linkId');
        if (isset($linkId))
        {
            $linkRecord = LinkStorage::getLinkById($linkId);
            if (isset($linkRecord))
            {
                $libraryManager = new LibraryManager();
                $library = $libraryManager->getLibraryById($linkRecord->id_library);
                $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                $link->browser = 'phone';
                $link->load($linkRecord);
                $this->renderPartial('linkPreview', array('link' => $link), false, true);
            }
        }
    }

    public function actionGetAttachmentPreviewList()
    {
        $attachmnetId = Yii::app()->request->getPost('linkId');
        if (isset($attachmnetId))
        {
            $attachmentRecord = AttachmentStorage::getAttachmentById($attachmnetId);
            if (isset($attachmentRecord))
            {
                $linkRecord = LinkStorage::getLinkById($attachmentRecord->id_link);
                if (isset($linkRecord))
                {
                    $libraryManager = new LibraryManager();
                    $library = $libraryManager->getLibraryById($linkRecord->id_library);

                    $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                    $link->browser = 'phone';
                    $link->load($linkRecord);

                    $attachment = new Attachment($link);
                    $attachment->browser = $link->browser;
                    $attachment->load($attachmentRecord);

                    $this->renderPartial('linkPreview', array('link' => $attachment), false, true);
                }
            }
        }
    }

    public function actionGetFileCard()
    {
        $linkId = Yii::app()->request->getPost('linkId');
        if (isset($linkId))
        {
            $linkRecord = LinkStorage::getLinkById($linkId);
            if (isset($linkRecord))
            {
                $libraryManager = new LibraryManager();
                $library = $libraryManager->getLibraryById($linkRecord->id_library);
                $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                $link->browser = 'phone';
                $link->load($linkRecord);
                $this->renderPartial('fileCard', array('link' => $link), false, true);
            }
        }
    }

    public function actionRunFullscreenGallery()
    {
        $linkId = Yii::app()->request->getQuery('linkId');
        $format = Yii::app()->request->getQuery('format');
        if (isset($linkId) && isset($format))
        {
            $linkRecord = LinkStorage::getLinkById($linkId);
            if (isset($linkRecord))
            {
                $libraryManager = new LibraryManager();
                $libraryManager->getLibraries();
                $library = $libraryManager->getLibraryById($linkRecord->id_library);
                if (isset($library))
                {
                    $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                    $link->browser = 'default';
                    $link->load($linkRecord);
                    $selectedLinks = $link->getViewSource($format);
                    $selectedThumbs = $link->getViewSource('thumbs');
                }
            }
            else
            {
                $attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
                if (isset($attachmentRecord))
                {
                    $linkRecord = LinkStorage::getLinkById($attachmentRecord->id_link);
                    $libraryManager = new LibraryManager();
                    $libraryManager->getLibraries();
                    $library = $libraryManager->getLibraryById($attachmentRecord->id_library);
                    if (isset($library) && isset($linkRecord))
                    {
                        $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                        $link->browser = 'default';
                        $link->load($linkRecord);

                        $attachment = new Attachment($link);
                        $attachment->browser = 'default';
                        $attachment->load($attachmentRecord);
                        $selectedLinks = $attachment->getViewSource($format);
                        $selectedThumbs = $attachment->getViewSource('thumbs');
                    }
                }
            }
        }
        if (isset($selectedLinks) && isset($selectedThumbs))
        {
            for ($i = 0; $i < count($selectedLinks); $i++)
            {
                $galleryLink = array('image' => $selectedLinks[$i]['href'],
                    'title' => $selectedLinks[$i]['title'],
                    'thumb' => $selectedThumbs[$i]['href']);
                $galleryLinks[] = $galleryLink;
            }
            if (isset($galleryLinks))
                $this->render('fullscreenGallery', array('selectedLinks' => $galleryLinks));
        }
    }

}
