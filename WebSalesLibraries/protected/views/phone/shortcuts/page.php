<? /** @var $page PageModel */ ?>
<? if (Yii::app()->browser->isMobile() && !($this->browser == Browser::BROWSER_IPHONE || $this->browser == Browser::BROWSER_ANDROID_MOBILE)): ?>
	<ul>
		<? foreach ($page->links as $link): ?>
			<? echo $this->renderPartial('grid/' . $link->viewPath, array('link' => $link), true); ?>
		<? endforeach; ?>
	</ul>
<? else: ?>
	<div class="ui-grid-solo">
		<? foreach ($page->links as $link): ?>
			<? echo $this->renderPartial('list/' . $link->viewPath, array('link' => $link), true); ?>
		<? endforeach; ?>
	</div>
<?endif; ?>
