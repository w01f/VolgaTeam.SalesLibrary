<?
	/** @var $link VideoShortcut */
?>
<a class="cbp-caption shortcuts-link preview" href="#">
	<div class="cbp-caption-defaultWrap">
		<img src="<? echo $link->imagePath ?>" alt="" width="100%">
	</div>
	<div class="cbp-caption-activeWrap">
		<div class="cbp-l-caption-alignCenter">
			<div class="cbp-l-caption-body">
				<div class="cbp-l-caption-title"><? echo $link->name; ?></div>
				<div class="cbp-l-caption-desc"><? echo $link->tooltip; ?></div>
			</div>
		</div>
	</div>
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
</a>