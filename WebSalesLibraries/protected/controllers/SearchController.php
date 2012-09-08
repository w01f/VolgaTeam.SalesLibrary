<?php
class SearchController extends CController
{
    public $defaultAction = 'getSearchView';
    public function actionGetSearchView()
    {
        $this->renderPartial('searchView', array(), false, true);
    }

    public function actionSearchByContent()
    {
        $searched = false;
        $fileTypes = Yii::app()->request->getPost('fileTypes');
        $condition = Yii::app()->request->getPost('condition');
        if (isset($fileTypes) && isset($condition))
        {
            $links = LinkStorage::searchByContent($condition);
            if (isset($links))
            {
                $dataProvider = new CArrayDataProvider($links, array(
                        'id' => 'id',
                        'sort' => array(
                            'attributes' => array(
                                'id', 'name', 'file_name', 'library'
                            ),
                        ),
                        'pagination' => array(
                            'pageSize' => 1000000,
                        ),
                    ));
                $searched = true;
                $this->renderPartial('searchResult', array('dataProvider' => $dataProvider), false, true);
            }
        }
        if (!$searched)
            $this->renderPartial('empty', array(), false, true);
    }
}

?>
