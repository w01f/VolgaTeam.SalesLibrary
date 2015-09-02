<? /** @var $shortcut BundleShortcut */ ?>
<? if (Yii::app()->browser->isMobile() && !($this->browser == Browser::BROWSER_IPHONE || $this->browser == Browser::BROWSER_ANDROID_MOBILE)): ?>
	<ul>
		<? foreach ($shortcut->links as $link): ?>
			<li class="cbp-item">
				<a class="shortcuts-link" href="<? echo $link->getSourceLink(); ?>" data-ajax="false" target="_blank">
					<img class="logo" src="<? echo $link->imageContent ?>" alt="" width="100%">
					<div class="service-data">
						<? echo $link->getMenuItemData(); ?>
					</div>
				</a>
			</li>
		<? endforeach; ?>
	</ul>
<? else: ?>
	<div class="ui-grid-solo">
		<? foreach ($shortcut->links as $link): ?>
			<a class="ui-block-a shortcuts-link" href="<? echo $link->getSourceLink(); ?>" target="_blank">
				<img src="<? echo $link->imageContent; ?>">
				<div class="service-data">
					<? echo $link->getMenuItemData(); ?>
				</div>
			</a>
		<? endforeach; ?>
	</div>
<?endif; ?>
