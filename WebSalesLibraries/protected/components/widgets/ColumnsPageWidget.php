<?php
class ColumnsPageWidget extends CWidget
{
    public $libraryPage;
    public function run()
    {
        $this->render('application.views.widgets.columnsPageWidget', array('libraryPage' => $this->libraryPage));
    }

}

?>
