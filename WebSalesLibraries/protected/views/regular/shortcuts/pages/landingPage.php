<?
	/**
     * @var $shortcut LandingPageShortcut
	 * @var $screenSettings array
     */
?>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.5.2/animate.min.css">
<div class="shortcuts-page-content landing-page">
    <div class="container-fluid landing-page-markup">
	    <? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $shortcut->markupSettings->contentBlocks, 'screenSettings' => $screenSettings), true); ?>
    </div>
</div>
