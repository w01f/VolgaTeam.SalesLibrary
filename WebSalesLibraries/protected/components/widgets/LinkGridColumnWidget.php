<?php
class LinkGridColumnWidget extends CWidget
{
    public $linkName;
    public $fileName;
    
    public function run()
    {
        $this->render('application.views.search.linkGridColumnWidget', array('linkName' => $this->linkName,'fileName' => $this->fileName));
    }

}

?>
