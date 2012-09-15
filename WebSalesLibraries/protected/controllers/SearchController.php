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

        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
            $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
        else
            $checkedLibraryIds = array();

        if (isset(Yii::app()->request->cookies['sortColumn']->value))
        {
            switch (Yii::app()->request->cookies['sortColumn']->value)
            {
                case 'library':
                    $sortColumn = 'library';
                    break;
                case 'link-type':
                    $sortColumn = 'file_type';
                    break;
                case 'link-name':
                    $sortColumn = 'name';
                    break;
                case 'link-date':
                    break;
            }
        }
        else
            $sortColumn = 'name';

        if (isset(Yii::app()->request->cookies['sortDirection']->value))
            $sortDirection = Yii::app()->request->cookies['sortDirection']->value;
        else
            $sortDirection = 'asc';

        if (isset($fileTypes) && isset($condition))
        {
            $links = LinkStorage::searchByContent($condition, $fileTypes, $checkedLibraryIds);
            if (!isset($links))
            {
                $link['id'] = null;
                $link['name'] = null;
                $link['file_name'] = null;
                $link['file_type'] = null;
                $link['library'] = null;
                $links[] = $link;
            }
            else
            {
                if (isset($sortColumn) && isset($sortDirection))
                {
                    usort($links, function($a, $b) use($sortColumn, $sortDirection)
                        {
                            if ($sortDirection == 'asc')
                                return strnatcmp($a[$sortColumn], $b[$sortColumn]);
                            else
                                return strnatcmp($b[$sortColumn], $a[$sortColumn]);
                        });
                }
            }
            $searched = true;
            $this->renderPartial('searchResult', array('links' => $links), false, true);
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

    public function actionGetLibraryCheckboxList()
    {
        $libraryManager = new LibraryManager();
        $libraryObjects = $libraryManager->getLibraries();

        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
            $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
        foreach ($libraryObjects as $libraryObject)
        {
            $library['id'] = $libraryObject->id;
            $library['name'] = $libraryObject->name;
            if (isset($checkedLibraryIds))
                $library['selected'] = in_array($libraryObject->id, $checkedLibraryIds);
            else
                $library['selected'] = true;
            $libraries[] = $library;
        }
        if (isset($libraries))
            $this->renderPartial('application.views.search.librarySelector', array('libraries' => $libraries), false, true);
    }

    public function actionGetSelectedLibraries()
    {
        $libraryManager = new LibraryManager();
        $libraryObjects = $libraryManager->getLibraries();
        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
            $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
        foreach ($libraryObjects as $libraryObject)
        {
            if (isset($checkedLibraryIds))
            {
                if (in_array($libraryObject->id, $checkedLibraryIds))
                    $libraries[] = $libraryObject->name;
            }
            else
                $libraries[] = $libraryObject->name;
        }
        if (isset($libraries))
        {
            if (count($libraries) == count($libraryObjects))
                echo 'All';
            else
                echo implode(', ', $libraries);
        }
        else
            echo 'None';
        Yii::app()->end();
    }

}

?>
