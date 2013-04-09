<?php
class RequireLogin extends CBehavior
{
    public function attach($owner)
    {
        $owner->attachEventHandler('onBeginRequest', array($this, 'handleBeginRequest'));
    }

    public function handleBeginRequest($event)
    {
        if (Yii::app()->user->isGuest &&
            !strstr(Yii::app()->request->getUrl(), 'site/login') &&
            !strstr(Yii::app()->request->getUrl(), 'site/changePassword') &&
            !strstr(Yii::app()->request->getUrl(), 'site/recoverPasswordDialog') &&
            !strstr(Yii::app()->request->getUrl(), 'site/recoverPasswordDialogSuccess') &&
            !strstr(Yii::app()->request->getUrl(), 'site/recoverPassword') &&
            !strstr(Yii::app()->request->getUrl(), 'site/validateUserByEmail') &&
            !strstr(Yii::app()->request->getUrl(), 'site/emailLinkGet') &&
            !strstr(Yii::app()->request->getUrl(), 'site/switchVersion') &&
			!strstr(Yii::app()->request->getUrl(), 'site/disclaimerWarning') &&
            !strstr(Yii::app()->request->getUrl(), 'admin/') &&
			!strstr(Yii::app()->request->getUrl(), 'statistic/') &&
			!strstr(Yii::app()->request->getUrl(), 'ticker/') &&
			!strstr(Yii::app()->request->getUrl(), 'inactiveusers/') &&
            !strstr(Yii::app()->request->getUrl(), 'content/'))
        {
            Yii::app()->user->loginRequired();
        }
    }

}

