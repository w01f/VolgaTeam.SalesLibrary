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
        if (isset($_POST['selectedLibrary']))
            if ($_POST['selectedLibrary'] != '')
                $libraryManager->setSelectedLibraryName($_POST['selectedLibrary']);
        if (isset($_POST['selectedPage']))
            if ($_POST['selectedPage'] != '')
                $libraryManager->setSelectedPageName($_POST['selectedPage']);
        $this->renderPartial('columnsViewAjax', array('selectedPage' => $libraryManager->getSelectedPage()), false, true);
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
