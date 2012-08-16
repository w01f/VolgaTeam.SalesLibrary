<?php
class SiteController extends CController
{
    public function actionLogin()
    {
        $model = new LoginForm;

        if (isset($_POST['LoginForm']))
        {
            $model->attributes = $_POST['LoginForm'];
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

}

?>