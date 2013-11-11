<div class="shortcuts-link library-file">
	<img src="<?php echo $link->imagePath ?>"
		<? if (isset($link->tooltip) && !Yii::app()->browser->isMobile()): ?>
			rel=" tooltip" title="<? echo $link->tooltip; ?>"
		<? endif; ?>>
	<div class="service-data">
		<div class="link-id"><?php echo $link->linkId ?></div>
	</div>
</div>