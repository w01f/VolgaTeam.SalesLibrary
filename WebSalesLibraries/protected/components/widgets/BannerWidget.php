<?php
class BannerWidget extends CWidget
{
    public $banner;
    public $isLinkBanner;
    public function run()
    {
        $this->render('application.views.widgets.bannerWidget', array('banner' => $this->banner, 'isLinkBanner' => $this->isLinkBanner));
    }

}

?>
