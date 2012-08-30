<?php
class ColumnsPageWidget extends CWidget
{
    public $libraryPage;
    public function run()
    {
        $this->render('application.views.wallbin.columnsPageWidget', array('libraryPage' => $this->libraryPage));
    }

}

?>
