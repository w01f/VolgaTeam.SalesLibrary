<?php
class BannerWidget extends CWidget
{
    public $banner;
    public $isLinkBanner;
    public function run()
    {
        $this->render('application.views.wallbin.bannerWidget', array('banner' => $this->banner, 'isLinkBanner' => $this->isLinkBanner));
    }

}

?>
