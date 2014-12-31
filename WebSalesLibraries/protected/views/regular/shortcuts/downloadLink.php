<?
	/** @var $link DownloadShortcut*/
?>
<a class="cbp-caption shortcuts-link <? echo $link->type; ?>" href="<? echo $link->sourceLink ?>" target="_self">
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
		<div class="link-id"><? echo $link->id; ?></div>
	</div>
</a>