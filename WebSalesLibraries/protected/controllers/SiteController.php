<?php
class SiteController extends IsdController
{
    public $defaultAction = 'index';
    public function getViewPath()
    {
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
            case Browser::BROWSER_ANDROID_MOBILE:
                return YiiBase::getPathOfAlias('application.views.phone.site');
            default :
                return YiiBase::getPathOfAlias('application.views.regular.site');
        }
    }

    public function actionIndex()
    {
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
            case Browser::BROWSER_ANDROID_MOBILE:
                $this->layout = '/phone/layouts/ribbon';
                break;
            default :
                $this->layout = '/regular/layouts/ribbon';
                break;
        }
        $this->render('index');
    }

    public function actionLogin()
    {
        $loginModel = new LoginForm();

        $attributes = Yii::app()->request->getPost('LoginForm');
        if (isset($attributes))
        {
            $loginModel->attributes = $attributes;
            if ($loginModel->validate() && $loginModel->login())
            {
                if ($loginModel->needToResetPassword)
                {
                    $this->redirect($this->createUrl('site/changePassword', array(
                            'login' => $loginModel->login,
                            'password' => $loginModel->password,
                            'rememberMe' => $loginModel->rememberMe
                        )));
                }
                else
                    $this->redirect(Yii::app()->user->returnUrl);
            }
        }
        $this->render('login', array('formData' => $loginModel));
    }

    public function actionLogout()
    {
        Yii::app()->user->logout();
        Yii::app()->end();
    }

    public function actionChangePassword()
    {
        $changePasswordModel = new ChangePasswordForm();
        $attributes = Yii::app()->request->getPost('ChangePasswordForm');
        if (isset($attributes))
        {
            $changePasswordModel->attributes = $attributes;
            $changePasswordModel->login = $attributes['login'];
            $changePasswordModel->oldPassword = $attributes['oldPassword'];
            $changePasswordModel->rememberMe = $attributes['rememberMe'];
            if ($changePasswordModel->validate() && $changePasswordModel->changePassword())
                $this->redirect(Yii::app()->user->returnUrl);
            else
                $this->render('changePassword', array('formData' => $changePasswordModel));
        }
        else
        {
            $login = Yii::app()->request->getQuery('login');
            $oldPassword = Yii::app()->request->getQuery('password');
            $rememberMe = Yii::app()->request->getQuery('rememberMe', false);
            if (isset($login) && isset($oldPassword))
            {
                $changePasswordModel->login = $login;
                $changePasswordModel->oldPassword = $oldPassword;
                $changePasswordModel->rememberMe = $rememberMe;
                $this->render('changePassword', array('formData' => $changePasswordModel));
            }
        }
    }

    public function actionRecoverPasswordDialog()
    {
        $this->renderPartial('recoverPassword', array(), false, true);
    }

    public function actionRecoverPasswordDialogSuccess()
    {
        $this->renderPartial('recoverPasswordSuccess', array(), false, true);
    }

    public function actionValidateUserByEmail()
    {
        $login = Yii::app()->request->getPost('login');
        $email = Yii::app()->request->getPost('email');
        $result = 'Error while validating user. Try again or contact to technical support';
        if (isset($login) && isset($email))
            $result = UserStorage::validateUserByEmail($login, $email);
        echo $result;
    }

    public function actionRecoverPassword()
    {
        $login = Yii::app()->request->getPost('login');
        if (isset($login))
        {
            $password = UserStorage::generatePassword();
            UserStorage::changePassword($login, $password);
            ResetPasswordStorage::resetPasswordForUser($login, $password, $newUser);
        }
    }

    public function actionError()
    {
        if ($error = Yii::app()->errorHandler->error)
        {
            if (Yii::app()->request->isAjaxRequest)
                echo $error['message'];
            else
                $this->render('errorMessage', $error);
        }
    }

    public function actionDownloadDialog()
    {
        $linkId = Yii::app()->request->getPost('linkId');
        if (isset($linkId))
        {
            $linkRecord = LinkStorage::getLinkById($linkId);
            if (isset($linkRecord))
            {
                if ($linkRecord->format == 'video' || $linkRecord->format == 'mp4')
                    $this->renderPartial('downloadVideoDialog', array('format' => $linkRecord->format), false, true);
                else
                    $this->renderPartial('downloadDialog', array(), false, true);
            }
            else
            {
                $attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
                if (isset($attachmentRecord))
                {
                    if ($attachmentRecord->format == 'video' || $attachmentRecord->format == 'mp4')
                        $this->renderPartial('downloadVideoDialog', array('format' => $attachmentRecord->format), false, true);
                    else
                        $this->renderPartial('downloadDialog', array(), false, true);
                }
            }
        }
    }

    public function actionDownloadFile()
    {
        $linkId = Yii::app()->request->getQuery('linkId');
        $format = Yii::app()->request->getQuery('format');
        if (isset($linkId))
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
                    $link->load($linkRecord);
                    if ($linkRecord->format == 'video' || $linkRecord->format == 'mp4')
                    {
                        if ($format == 'wmv')
                            $path = $link->filePath;
                        else if ($format == 'mp4')
                        {
                            $previewRecord = PreviewStorage::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'mp4'));
                            if (isset($previewRecord))
                                $path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
                        }
                    }
                }
            }
            else
            {
                $attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
                if (isset($attachmentRecord))
                {
                    $libraryManager = new LibraryManager();
                    $libraryManager->getLibraries();
                    $library = $libraryManager->getLibraryById($attachmentRecord->id_library);
                    if (isset($library))
                    {
                        if ($attachmentRecord->format == 'video' || $attachmentRecord->format == 'mp4')
                        {
                            if ($format == 'wmv')
                                $path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $attachmentRecord->path);
                            else if ($format == 'mp4')
                            {
                                $previewRecord = PreviewStorage::model()->find('id_container =? and type=?', array($attachmentRecord->id_preview, 'mp4'));
                                if (isset($previewRecord))
                                    $path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
                            }
                        }
                    }
                }
            }
        }
        if (isset($path))
            return Yii::app()->getRequest()->sendFile(basename($path), @file_get_contents($path));
    }

    public function actionEmailLinkDialog()
    {
        $this->renderPartial('emailDialog', array(), false, true);
    }

    public function actionEmailLinkSend()
    {
        $linkId = Yii::app()->request->getPost('linkId');
        $emailTo = Yii::app()->request->getPost('emailTo');
        $emailFrom = Yii::app()->request->getPost('emailFrom');
        $emailSubject = Yii::app()->request->getPost('emailSubject');
        $emailBody = Yii::app()->request->getPost('emailBody');
        $expiresIn = Yii::app()->request->getPost('expiresIn');
        if (isset($expiresIn) && $expiresIn == '')
            unset($expiresIn);

        $linkRecord = LinkStorage::getLinkById($linkId);
        if (isset($linkRecord))
        {
            $libraryId = $linkRecord->id_library;
            $libraryManager = new LibraryManager();
            $library = $libraryManager->getLibraryById($libraryId);
            if (isset($library))
            {
                $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                $link->load($linkRecord);
            }
            else
            {
                echo 'Library was not found';
                echo $emailedLinkRecord->id_library;
            }
        }

        if (isset($link) && isset($emailTo) && isset($emailFrom) && isset($emailSubject) && isset($emailBody))
        {
            if ($emailTo != '' && $emailFrom != '')
            {
                $emailFolder = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['emailTemp'];
                $emailFolderLink = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['emailTemp'];
                if (!file_exists($emailFolder))
                    mkdir($emailFolder, 0, true);

                $id = uniqid();
                $destinationPath = $emailFolder . DIRECTORY_SEPARATOR . $id . $link->fileName;
                $destinationLink = str_replace(' ', '%20', htmlspecialchars($emailFolderLink . '/' . $id . $link->fileName));

                if (!file_exists($destinationPath))
                    copy($link->filePath, $destinationPath);

                EmailedLinkStorage::saveEmailedLink($id, $linkId, $libraryId, $destinationPath, $destinationLink, $expiresIn, Yii::app()->user->login, $emailFrom, $emailTo);

                if ($link->originalFormat == 'mp4' || $link->originalFormat == 'video')
                    $destinationLink = Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('site/emailLinkGet', array('emailId' => $id));

                $message = Yii::app()->email;
                $message->to = $emailTo;
                $message->subject = $emailSubject;
                $message->from = $emailFrom;
                $message->view = 'sendLink';
                $message->viewVars = array('body' => $emailBody, 'link' => $destinationLink, 'expiresIn' => $expiresIn);
                $message->send();
            }
        }
        Yii::app()->end();
    }

    public function actionEmailLinkSuccess()
    {
        $this->renderPartial('emailSuccess', array(), false, true);
    }

    public function actionEmailLinkGet()
    {
        $id = Yii::app()->request->getQuery('emailId');
        if (isset($id))
        {
            $emailedLinkRecord = EmailedLinkStorage::getEmailedLink($id);
            if (isset($emailedLinkRecord))
            {
                $linkRecord = LinkStorage::getLinkById($emailedLinkRecord->id_link);
                if (isset($linkRecord))
                {
                    $libraryManager = new LibraryManager();
                    $libraryManager->getLibraries();
                    $library = $libraryManager->getLibraryById($emailedLinkRecord->id_library);
                    if (isset($library))
                    {
                        $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                        $link->load($linkRecord);
                        $this->render('emailVideo', array('link' => $link, 'expiresIn' => $emailedLinkRecord->expires_in));
                    }
                    else
                    {
                        echo 'Library was not found';
                        echo $emailedLinkRecord->id_library;
                    }
                }
                else
                {
                    echo 'Link was not found';
                    echo $emailedLinkRecord->id_link;
                }
            }
        }
        Yii::app()->end();
    }

}

?>