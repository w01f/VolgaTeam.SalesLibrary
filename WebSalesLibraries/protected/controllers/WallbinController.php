<?php
class WallbinController extends IsdController
{
    public function getViewPath()
    {
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
            case Browser::BROWSER_ANDROID_MOBILE:
                return YiiBase::getPathOfAlias('application.views.phone.wallbin');
            default :
                return YiiBase::getPathOfAlias('application.views.regular.wallbin');
        }
    }

    public function actionGetColumnsView()
    {
        $libraryManager = new LibraryManager();
        $selectedPage = $libraryManager->getSelectedPage();
        $this->renderPartial('columnsView', array('selectedPage' => $selectedPage), false, true);
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
        foreach ($selectedPage->folders as $folder)
            if ($folder->id == $folderId)
            {
                $selectedFolder = $folder;
                $selectedFolder->loadFiles();
                break;
            }
        $this->renderPartial('folderLinks', array('folder' => $selectedFolder), false, true);
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
                        if (Yii::app()->browser->isMobile())
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
        $selectedLinks = CJSON::decode(Yii::app()->request->getPost('selectedLinks'));
        $this->renderPartial('fullscreenGallery', array('selectedLinks' => $selectedLinks));
    }

}

?>
