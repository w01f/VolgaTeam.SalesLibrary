<?php
class WallbinController extends CController
{
    public $defaultAction = 'getColumnsViewRegular';
    public function actionGetColumnsViewRegular()
    {
        $this->layout = '/layouts/ribbon';
        $this->render('columnsViewRegular');
    }

    public function actionGetColumnsViewAjax()
    {
        $libraryManager = new LibraryManager();

        $libraryManager->setSelectedLibraryName(Yii::app()->request->getPost('selectedLibrary'));
        $libraryManager->setSelectedPageName(Yii::app()->request->getPost('selectedPage'));

        $selectedPage = $libraryManager->getSelectedPage();
        $cacheKey = $selectedPage->parent->name . '\\' . $selectedPage->name;
        $cache = Yii::app()->cacheDB->get($cacheKey);

        $this->renderPartial('columnsViewAjax', array('selectedPage' => $selectedPage, 'cacheKey' => $cacheKey, 'cache' => $cache), false, true);
    }

    public function actionGetListView()
    {
        $this->layout = '/layouts/ribbon';
        $this->render('listViewRegular');
    }

    public function actionGetLibraryDropDownList()
    {
        $libraryManager = new LibraryManager();
        $this->renderPartial('libraryDropDownList', array('libraryManager' => $libraryManager), false, true);
    }

    public function actionGetPageDropDownList()
    {
        $libraryManager = new LibraryManager();
        $this->renderPartial('pageDropDownList', array('selectedLibrary' => $libraryManager->getSelectedLibrary(),
            'selectedPage' => $libraryManager->getSelectedPage()), false, true);
    }

}

?>
