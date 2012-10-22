<?php
class SiteController extends CController
{
    public $defaultAction = 'index';
    public function actionIndex()
    {
        $this->layout = '/layouts/ribbon';
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

    public function actionValidateUserByEmail()
    {
        $login = Yii::app()->request->getPost('login');
        $email = Yii::app()->request->getPost('email');
        $result = 'Error while validating user. Try again or contact to technical support';
        if (isset($login) && isset($email))
            $result = UserStorage::validateUserByEmail ($login, $email);
        echo $result;
    }
    
    public function actionRecoverPassword()
    {
        $login = Yii::app()->request->getPost('login');
        if(isset($login))
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

    public function actionDownloadFile()
    {
        //$url = Yii::app()->request->getPost('url');
        $url = Yii::app()->request->getQuery('url', '');
        if (isset($url))
        {
            $path = realpath(str_replace(Yii::app()->baseUrl, '', $url));
            Yii::app()->request->xSendFile($path, array(
                'forceDownload' => false,
                'terminate' => false,
            ));
        }
        Yii::app()->end();
    }

}

?>