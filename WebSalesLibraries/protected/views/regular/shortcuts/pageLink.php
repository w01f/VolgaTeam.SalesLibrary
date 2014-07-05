<?
	/** @var $link PageShortcut*/
?>
<a class="cbp-caption shortcuts-link library-page" href="#">
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
		<div class="library-name"><? echo $link->libraryName; ?></div>
		<div class="page-name"><? echo $link->pageName; ?></div>
	</div>
</a>