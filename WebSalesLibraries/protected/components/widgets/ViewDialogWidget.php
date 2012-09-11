<?php
class ViewDialogWidget extends CWidget
{
    public $link;
    public function run()
    {
        $this->render('application.views.widgets.viewDialogWidget', array('link' => $this->link));
    }

}

?>
