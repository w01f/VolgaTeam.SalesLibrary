<div class="shortcuts-link library-page">
	<img src="<?php echo $link->imagePath ?>"
		<? if (isset($link->tooltip) && !Yii::app()->browser->isMobile()): ?>
			rel=" tooltip" title="<? echo $link->tooltip; ?>"
		<? endif; ?>>
	<div class="service-data">
		<div class="library-name"><? echo $link->libraryName; ?></div>
		<div class="page-name"><? echo $link->pageName; ?></div>
	</div>
</div>