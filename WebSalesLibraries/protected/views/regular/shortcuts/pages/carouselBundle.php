<?
	/** @var $shortcut CarouselBundleShortcut */
?>
<? if ($shortcut->searchBar->configured): ?>
	<style>
		#content .shortcuts-search-bar-container
		{
			width: 100%;
			padding: 20px 20px 20px;
		}
	</style>
	<div class="shortcuts-search-bar-container">
		<? echo $this->renderPartial('searchBar/bar', array('searchBar' => $shortcut->searchBar), true); ?>
	</div>
<? endif; ?>
<? if (count($shortcut->links) > 0): ?>
	<div id="shortcuts-links-carousel-view"
	     class="shortcuts-links-carousel-view<? if (Yii::app()->browser->isMobile()): ?> mobile<? endif; ?>"></div>
<? endif; ?>