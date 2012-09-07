<?php
class ViewDialogWidget extends CWidget
{
    public $link;
    public function run()
    {
        $this->render('application.views.wallbin.viewDialogWidget', array('link' => $this->link));
    }

}

?>
