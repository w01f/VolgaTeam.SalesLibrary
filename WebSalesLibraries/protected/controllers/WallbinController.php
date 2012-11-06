<?php
class WallbinController extends IsdController
{
    public function getViewPath()
    {
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
                return YiiBase::getPathOfAlias('application.views.phone.wallbin');
            default :
                return YiiBase::getPathOfAlias('application.views.regular.wallbin');
        }
    }

    public function actionGetColumnsView()
    {
        $libraryManager = new LibraryManager();

        $libraryManager->setSelectedLibraryName(htmlspecialchars_decode(Yii::app()->request->getPost('selectedLibrary')));
        $libraryManager->setSelectedPageName(htmlspecialchars_decode(Yii::app()->request->getPost('selectedPage')));

        $selectedPage = $libraryManager->getSelectedPage();
        $this->renderPartial('columnsView', array('selectedPage' => $selectedPage), false, true);
    }

    public function actionGetLibraryDropDownList()
    {
        $libraryManager = new LibraryManager();
        $this->renderPartial('libraryDropDownList', array('libraryManager' => $libraryManager), false, true);
    }

    public function actionGetLibraryCollapsibleList()
    {
        $libraryManager = new LibraryManager();
        $this->renderPartial('libraryCollapsibleList', array('libraryManager' => $libraryManager), false, true);
    }

    public function actionGetPageDropDownList()
    {
        $libraryManager = new LibraryManager();
        $this->renderPartial('pageDropDownList', array('selectedLibrary' => $libraryManager->getSelectedLibrary(),
            'selectedPage' => $libraryManager->getSelectedPage()), false, true);
    }

    public function actionGetFolderLinksList()
    {
        $libraryManager = new LibraryManager();
        $libraryManager->setSelectedLibraryName(htmlspecialchars_decode(Yii::app()->request->getPost('selectedLibrary')));
        $libraryManager->setSelectedPageName(str_replace('----------', '/', htmlspecialchars_decode(Yii::app()->request->getPost('selectedPage'))));
        $folderId = Yii::app()->request->getPost('folderId');

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

    public function actionGetLinkPreviewList()
    {
        $libraryManager = new LibraryManager();
        $folderId = Yii::app()->request->getPost('folderId');
        $linkId = Yii::app()->request->getPost('linkId');

        $selectedPage = $libraryManager->getSelectedPage();
        foreach ($selectedPage->folders as $folder)
            if ($folder->id == $folderId)
            {
                foreach ($folder->files as $link)
                    if ($link->id == $linkId)
                    {
                        $selectedLink = $link;
                        break;
                    }
                break;
            }
        $this->renderPartial('linkPreview', array('link' => $selectedLink), false, true);
    }

    public function actionRunFullscreenGallery()
    {
        $selectedLinks = CJSON::decode(Yii::app()->request->getPost('selectedLinks'));
        $this->renderPartial('fullscreenGallery', array('selectedLinks' => $selectedLinks));
    }
}

?>
