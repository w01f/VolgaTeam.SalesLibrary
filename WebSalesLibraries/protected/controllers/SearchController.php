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
                                'library', 'file_type', 'name',
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

    public function actionViewLink()
    {
        $rendered = false;
        $linkId = Yii::app()->request->getPost('linkId');
        if (isset($linkId))
        {
            $linkRecord = LinkStorage::getLinkById($linkId);
            if (isset($linkRecord))
            {
                $libraryManager = new LibraryManager();
                $library = $libraryManager->getLibraryById($linkRecord->id_library);
                $link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
                if (Yii::app()->browser->isMobile())
                {
                    $link->browser = 'mobile';
                }
                else
                {
                    $browser = Yii::app()->browser->getBrowser();
                    switch ($browser)
                    {
                        case 'Internet Explorer':
                            $link->browser = 'ie';
                            break;
                        case 'Chrome':
                        case 'Safari':
                            $link->browser = 'webkit';
                            break;
                        case 'Firefox':
                            $link->browser = 'firefox';
                            break;
                        case 'Opera':
                            $link->browser = 'opera';
                            break;
                        default:
                            $link->browser = 'webkit';
                            break;
                    }
                }
                $link->load($linkRecord);
                $this->renderPartial('application.views.widgets.viewDialogWidget', array('link' => $link), false, true);
                $rendered = true;
            }
        }
        if (!$rendered)
            $this->renderPartial('empty', array(), false, true);
    }

}

?>
