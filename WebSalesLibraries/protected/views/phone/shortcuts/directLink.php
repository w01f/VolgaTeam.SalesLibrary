<?
	/** @var $link FileShortcut|
	 * LibraryLinkShortcut|
	 * PageShortcut|
	 * QuickListShortcut|
	 * SearchShortcut|
	 * UrlShortcut|
	 * VideoShortcut|
	 * WindowShortcut*/
?>
<a class="ui-block-a shortcuts-link direct<? echo isset($link->samePage) && $link->samePage ? ' embedded' : ''; ?> <? echo $link->type; ?>" href="<? echo $link->sourceLink ?>" target="_blank"><img src="<? echo $link->imagePath; ?>"></a>