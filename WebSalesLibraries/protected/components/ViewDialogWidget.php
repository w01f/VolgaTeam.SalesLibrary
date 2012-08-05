<?php
class ViewDialogWidget extends CWidget
{
    public $link;
    
    public function run()
    {
        $this->render('viewDialogWidget', array('link' => $this->link));
    }

}

?>
