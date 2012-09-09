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
            if (isset(Yii::app()->request->cookies['lastFyleTypes']->value))
                $fileTypes = json_decode(Yii::app()->request->cookies['lastFyleTypes']->value);
            if (isset(Yii::app()->request->cookies['lastContentCondition']->value))
                $condition = Yii::app()->request->cookies['lastContentCondition']->value;
        }
        else
        {
            $fileTypes = Yii::app()->request->getPost('fileTypes');
            if (isset($fileTypes))
            {
                $cookie = new CHttpCookie('lastFyleTypes', json_encode($fileTypes));
                $cookie->expire = time() + (60 * 60 * 24 * 7);
                Yii::app()->request->cookies['lastFyleTypes'] = $cookie;
            }

            $condition = Yii::app()->request->getPost('condition');
            if (isset($condition))
            {
                $cookie = new CHttpCookie('lastContentCondition', $condition);
                $cookie->expire = time() + (60 * 60 * 24 * 7);
                Yii::app()->request->cookies['lastContentCondition'] = $cookie;
            }
        }
        if (isset($fileTypes) && isset($condition))
        {
            $links = LinkStorage::searchByContent($condition, $fileTypes);
            if (isset($links))
            {
                $dataProvider = new CArrayDataProvider($links, array(
                        'id' => 'id',
                        'sort' => array(
                            'defaultOrder'=>'name',
                            'attributes' => array(
                                'library', 'name', 'file_name'
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
