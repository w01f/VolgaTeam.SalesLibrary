<?php
class SearchController extends IsdController
{
    public function getViewPath()
    {
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
            case Browser::BROWSER_ANDROID_MOBILE:
                return YiiBase::getPathOfAlias('application.views.phone.search');
            default :
                return YiiBase::getPathOfAlias('application.views.regular.search');
        }
    }

    public function actionGetSearchView()
    {
        $this->renderPartial('searchView', array(), false, true);
    }

    public function actionGetTagsView()
    {
        $categories = new CategoryManager();
        $categories->loadCategories();

        $this->renderPartial('tagsView', array('categories' => $categories), false, true);
    }

    public function actionGetLibrariesView()
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
        if (!isset($libraries))
            $libraries[] = 'All';

        $this->renderPartial('librariesView', array('libraries' => $libraries), false, true);
    }

    public function actionSearchByContent()
    {
        $fileTypes = Yii::app()->request->getPost('fileTypes');
        $condition = Yii::app()->request->getPost('condition');
        $startDate = Yii::app()->request->getPost('startDate');
        $endDate = Yii::app()->request->getPost('endDate');
        $onlyFileCards = intval(Yii::app()->request->getPost('onlyFileCards'));
        $isSort = intval(Yii::app()->request->getPost('isSort'));

        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
            $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
        else
            $checkedLibraryIds = array();

        $categories = Yii::app()->request->getPost('categories');
        if (isset($categories))
            $categories = CJSON::decode($categories);
        $categoriesExactMatch = Yii::app()->request->getPost('categoriesExactMatch');
        if (!isset($categoriesExactMatch))
            $categoriesExactMatch = true;

        if (isset($fileTypes) && isset($condition) && isset($isSort))
            $links = LinkStorage::searchByContent($condition, $fileTypes, $startDate, $endDate, $checkedLibraryIds, $onlyFileCards, $categories, $categoriesExactMatch, $isSort);

        if (!isset($links))
            $links = null;
        $this->renderPartial('searchResult', array('links' => $links), false, true);
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
                $this->renderPartial('application.views.regular.wallbin.viewDialog', array('link' => $link), false, true);
                $rendered = true;
            }
        }
        if (!$rendered)
            $this->renderPartial('empty', array(), false, true);
    }

}

?>
