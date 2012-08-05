<?php
class RequireLogin extends CBehavior
{
    public function attach($owner)
    {
        $owner->attachEventHandler('onBeginRequest', array($this, 'handleBeginRequest'));
    }

    public function handleBeginRequest($event)
    {
        if (Yii::app()->user->isGuest && !strstr(Yii::app()->request->getUrl(),'site/login'))
        {
            Yii::app()->user->loginRequired();
        }
    }
}

?>