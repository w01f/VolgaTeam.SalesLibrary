<?php
class SearchController extends CController
{
    public $defaultAction = 'getSearchView';
    
    public function actionGetSearchView()
    {
        $this->renderPartial('searchView', array(), false, true);
    }

}

?>
