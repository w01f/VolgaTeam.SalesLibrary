<div class="shortcuts-link preview">
	<img src="<?php echo $link->imagePath ?>"
		<? if (isset($link->tooltip) && !Yii::app()->browser->isMobile()): ?>
			rel=" tooltip" title="<? echo $link->tooltip; ?>"
		<? endif; ?>>
	<div class="service-data">
		<div class="file-type">mp4</div>
		<div class="view-type">mp4</div>
		<div class="links"><?php echo json_encode(array(array(
				'src' => $link->sourceLink,
				'href' => $link->sourceLink,
				'title' => $link->name,
				'type' => 'video/mp4',
				'swf' => $link->playerLink))); ?></div>
	</div>
</div>