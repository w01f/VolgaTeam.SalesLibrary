<?php
class HelpController extends IsdController
{
    public function getViewPath()
    {
        return YiiBase::getPathOfAlias($this->pathPrefix . 'help');
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
