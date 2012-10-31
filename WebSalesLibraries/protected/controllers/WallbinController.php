<?php
class WallbinController extends CController
{
    public $browser;
    public function init()
    {
        $this->browser = Yii::app()->browser->getBrowser();
        //$this->browser = Browser::BROWSER_IPHONE;
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
                $this->layout = '/phone/layouts/main';
                break;
            default :
                $this->layout = '/regular/layouts/main';
                break;
        }
    }

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
        $libraryManager->setSelectedPageName(htmlspecialchars_decode(Yii::app()->request->getPost('selectedPage')));
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

    public function actionEmailDialog()
    {
        $this->renderPartial('emailDialog', array(), false, true);
    }

    public function actionEmailSend()
    {
        $filePath = Yii::app()->request->getPost('file');
        $emailTo = Yii::app()->request->getPost('emailTo');
        $emailFrom = Yii::app()->request->getPost('emailFrom');
        $emailSubject = Yii::app()->request->getPost('emailSubject');
        $emailBody = Yii::app()->request->getPost('emailBody');
        if (isset($filePath) && isset($emailTo) && isset($emailFrom) && isset($emailSubject) && isset($emailBody))
        {
            if ($emailTo != '' && $emailFrom != '')
            {
                $fileName = basename($filePath);
                $emailFolder = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['emailTemp'];
                $emailFolderLink = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['emailTemp'];
                if (!file_exists($emailFolder))
                    mkdir($emailFolder, 0, true);

                $destinationPath = $emailFolder . DIRECTORY_SEPARATOR . $fileName;
                $destinationLink = str_replace(' ', '%20', htmlspecialchars($emailFolderLink . '/' . $fileName));

                if (!file_exists($destinationPath))
                    copy($filePath, $destinationPath);

                $message = Yii::app()->email;
                $message->to = $emailTo;
                $message->subject = $emailSubject;
                $message->from = $emailFrom;
                $message->view = 'sendLink';
                $message->viewVars = array('body' => $emailBody, 'link' => $destinationLink);
                $message->send();
            }
        }
        Yii::app()->end();
    }

}

?>
