<? /** @var $page PageModel */ ?>
<? if (count($page->links) > 0): ?>
	<div id="shortcuts-links-carousel-view" class="shortcuts-links-carousel-view<? if (Yii::app()->browser->isMobile()): ?> mobile<? endif; ?>">
	</div>
<? endif; ?>