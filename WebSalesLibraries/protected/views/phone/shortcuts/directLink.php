<?
	/** @var $link FileShortcut|
	 * DownloadShortcut|
	 * LibraryLinkShortcut|
	 * PageShortcut|
	 * QuickListShortcut|
	 * SearchShortcut|
	 * UrlShortcut|
	 * VideoShortcut|
	 * WindowShortcut*/
?>
<a class="ui-block-a shortcuts-link direct<? echo isset($link->samePage) && $link->samePage ? ' embedded' : ''; ?> <? echo $link->type; ?>" href="<? echo $link->sourceLink ?>" target="_blank">
	<img src="<? echo $link->imagePath; ?>">
	<div class="service-data">
		<? if (isset($link->ribbonLogoPath) && @getimagesize($link->ribbonLogoPath)): ?>
			<div class="ribbon-logo-path"><? echo $link->ribbonLogoPath; ?></div>
		<? endif; ?>
		<div class="link-id"><? echo $link->id; ?></div>
		<div class="link-name"><? echo $link->name . ' - ' . $link->tooltip; ?></div>
	</div>
</a>