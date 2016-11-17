<?
	/** @var $shortcut CarouselBundleShortcut */
	echo $this->renderPartial('searchBar/bar', array('searchBar' => $shortcut->searchBar, 'linkId' => $shortcut->id), true);
?>
<? if (count($shortcut->links) > 0): ?>
	<div id="shortcuts-links-carousel-view"
	     class="shortcuts-links-carousel-view<? if (Yii::app()->browser->isMobile()): ?> mobile<? endif; ?>"></div>
<? endif; ?>