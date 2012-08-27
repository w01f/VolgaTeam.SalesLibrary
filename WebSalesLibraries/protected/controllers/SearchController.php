<?php
class SearchController extends CController
{
    public $defaultAction = 'getSearchRegular';

    public function actionGetSearchRegular()
    {
        $this->layout = '/layouts/ribbon';
        $this->render('empty');
    }
}

?>
