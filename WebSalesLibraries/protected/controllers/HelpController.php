<?php
class HelpController extends IsdController
{
    public function getViewPath()
    {
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
            case Browser::BROWSER_ANDROID_MOBILE:
                return YiiBase::getPathOfAlias('application.views.phone.help');
            default :
                return YiiBase::getPathOfAlias('application.views.regular.help');
        }
    }

    public function actionGetPage()
    {
        $pageId = Yii::app()->request->getPost('pageId');
        if (isset($pageId))
        {
            $linkRecords = HelpLinkStorage::model()->findAll(array('order' => '`order`', 'condition' => 'id_page=:id_page', 'params' => array(':id_page' => $pageId)));
            if (isset($linkRecords))
                $this->render('page', array('linkRecords' => $linkRecords));
        }
    }

}

?>
