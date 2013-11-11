<a class="shortcuts-link direct<? echo isset($link->samePage) && $link->samePage ? ' embedded' : ''; ?> <? echo $link->type; ?>" href="<? echo $link->sourceLink ?>" target="_blank" onfocus="this.blur();this.hideFocus=true;">
	<img src="<? echo $link->imagePath ?>"
		<? if (isset($link->tooltip) && !Yii::app()->browser->isMobile()): ?>
			rel="tooltip"
			title="<? echo $link->tooltip; ?>"
		<? endif; ?>
		>
	<div class="service-data">
		<? if (isset($link->ribbonLogoPath) && @getimagesize($link->ribbonLogoPath)): ?>
			<div class="ribbon-logo-path"><?php echo $link->ribbonLogoPath ?></div>
		<? endif; ?>
	</div>
</a>