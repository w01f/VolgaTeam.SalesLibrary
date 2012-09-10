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
        $sortFlag = Yii::app()->request->getQuery('id_sort');
        if (isset($sortFlag))
        {
            if (isset(Yii::app()->session['lastFyleTypes']))
                $fileTypes = Yii::app()->session['lastFyleTypes'];
            if (isset(Yii::app()->session['lastContentCondition']))
                $condition = Yii::app()->session['lastContentCondition'];
        }
        else
        {
            $fileTypes = Yii::app()->request->getPost('fileTypes');
            if (isset($fileTypes))
                Yii::app()->session['lastFyleTypes'] = $fileTypes;

            $condition = Yii::app()->request->getPost('condition');
            if (isset($condition))
                Yii::app()->session['lastContentCondition'] = $condition;
        }
        if (isset($fileTypes) && isset($condition))
        {
            $links = LinkStorage::searchByContent($condition, $fileTypes);
            if (isset($links))
            {
                $dataProvider = new CLowerCaseArrayDataProvider($links, array(
                        'id' => 'id',
                        'sort' => array(
                            'defaultOrder' => 'name',
                            'attributes' => array(
                                'library', 'name',
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
