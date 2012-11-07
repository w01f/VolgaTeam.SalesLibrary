<?php
class IsdController extends CController
{
    public $browser;
    public function init()
    {
        //$this->browser = Yii::app()->browser->getBrowser();
        $this->browser = Browser::BROWSER_IPHONE;
        switch ($this->browser)
        {
            case Browser::BROWSER_IPHONE:
                $this->layout = '/phone/layouts/main';
                break;
            default :
                $this->layout = '/regular/layouts/main';
                break;
        }
    }
}
?>
