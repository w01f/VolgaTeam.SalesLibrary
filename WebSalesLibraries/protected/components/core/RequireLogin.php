<?php
class RequireLogin extends CBehavior
{
    public function attach($owner)
    {
        $owner->attachEventHandler('onBeginRequest', array($this, 'handleBeginRequest'));
    }

    public function handleBeginRequest($event)
    {
        if (Yii::app()->user->isGuest && !strstr(Yii::app()->request->getUrl(), 'site/login') && !strstr(Yii::app()->request->getUrl(), 'admin/') && !strstr(Yii::app()->request->getUrl(), 'content/'))
        {
            Yii::app()->user->loginRequired();
        }
    }

}

?>