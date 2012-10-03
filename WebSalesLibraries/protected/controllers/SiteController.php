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
        $model = new LoginForm;

        $attributes = Yii::app()->request->getPost('LoginForm');
        if (isset($attributes))
        {
            $model->attributes = $attributes;
            if ($model->validate() && $model->login())
                $this->redirect(Yii::app()->user->returnUrl);
        }

        $this->render('loginPage', array('loginData' => $model));
    }

    public function actionRecoverPassword()
    {
        $this->render('recoverPasswordPage');
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