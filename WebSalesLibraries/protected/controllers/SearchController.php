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
        $libraryGroups = $libraryManager->getLibraryGroups();

        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
            $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);

        foreach ($libraryGroups as $libraryGroup)
            foreach ($libraryGroup->libraries as $library)
                if (isset($checkedLibraryIds))
                    $library->selected = in_array($library->id, $checkedLibraryIds);
                else
                    $library->selected = true;
        $this->renderPartial('librariesView', array('libraryGroups' => $libraryGroups), false, true);                
    }

    public function actionSearchByContent()
    {
        $fileTypes = Yii::app()->request->getPost('fileTypes');
        $condition = Yii::app()->request->getPost('condition');
        $startDate = Yii::app()->request->getPost('startDate');
        $endDate = Yii::app()->request->getPost('endDate');
        $dateFile = Yii::app()->request->getPost('dateFile');
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
            $links = LinkStorage::searchByContent($condition, $fileTypes, $startDate, $endDate, $dateFile ,$checkedLibraryIds, $onlyFileCards, $categories, $categoriesExactMatch, $isSort);

        if (!isset($links))
            $links = null;

        if (isset($links))
            $searchInfo['count'] = 'Files: ' . count($links);
        else
            $searchInfo['count'] = 'No Files Meet your Criteria';
        if (isset($condition) && !($condition == '""' || $condition == ''))
            $searchInfo['condition'] = '<b>Keyword (' . (strstr($condition, '"') ? 'Exact match' : 'Partial match') . '):</b> ' . str_replace('"', '', $condition);
        if (isset($fileTypes))
            $searchInfo['file_types'] = '<b>File Types:</b> ' . implode(', ', $fileTypes);
        else
            $searchInfo['file_types'] = '<b>File Types:</b> None';
        if (isset($startDate) && isset($endDate))
            $searchInfo['dates'] = '<b>Dates:</b> ' . $startDate . ' - ' . $endDate;
        else
            $searchInfo['dates'] = '<b>Dates:</b> ALL';
        if (isset($categories))
        {
            foreach ($categories as $category)
                $groups[] = $category['category'];
            if (isset($groups))
            {
                $groups = array_unique($groups);
                foreach ($groups as $group)
                {
                    foreach ($categories as $category)
                        if ($category['category'] == $group)
                            $tags[] = $category['tag'];
                    if (isset($tags))
                    {
                        $categoryTags[] = '<b>' . $group . ':</b> ' . implode(', ', $tags);
                        unset($tags);
                    }
                }
                if (isset($categoryTags))
                    $searchInfo['categories'] = implode('; ', $categoryTags);
            }
        }
        if (isset($checkedLibraryIds))
        {
            $allLibraryRecords = LibraryStorage::model()->findAll();
            foreach ($checkedLibraryIds as $libraryId)
            {
                $libraryRecord = LibraryStorage::model()->findByPk($libraryId);
                if (isset($libraryRecord))
                    $libraries[] = $libraryRecord->name;
            }
            if (isset($libraries))
            {
                if (count($allLibraryRecords) == count($libraries))
                    $searchInfo['libraries'] = '<b>Libraries:</b> ALL';
                else
                    $searchInfo['libraries'] = '<b>Libraries:</b> ' . implode(", ", $libraries);
            }
            else
                $searchInfo['libraries'] = '<b>Libraries:</b> Not selected';
        }
        else
            $searchInfo['libraries'] = 'Libraries: Not selected';


        if (!isset($searchInfo))
            $searchInfo = null;



        $this->renderPartial('searchResult', array('searchInfo' => $searchInfo, 'links' => $links), false, true);
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
