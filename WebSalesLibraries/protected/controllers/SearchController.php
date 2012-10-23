<?php
class SearchController extends CController
{
    public function getViewPath()
    {
        return YiiBase::getPathOfAlias('application.views.regular.search');
    }

    public function actionGetSearchView()
    {
        $this->renderPartial('searchView', array(), false, true);
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

        if (isset($fileTypes) && isset($condition) && isset($isSort))
            $links = LinkStorage::searchByContent($condition, $fileTypes, $startDate, $endDate, $checkedLibraryIds, $onlyFileCards, $isSort);

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

    public function actionGetLinkDetails()
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
                $this->renderPartial('application.views.regular.wallbin.linkDetails', array('link' => $link), false, true);
            }
        }
    }

}

?>
