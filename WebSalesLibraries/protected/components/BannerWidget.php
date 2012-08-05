<?php
class BannerWidget extends CWidget
{
    public $banner;
    public $isLinkBanner;
    public function run()
    {
        $this->render('bannerWidget', array('banner' => $this->banner, 'isLinkBanner' => $this->isLinkBanner));
    }
}

?>
