<?
	/** @var $shortcut LibraryPageShortcut */

	$libraryPage = $shortcut->getLibraryPage();
	if ($shortcut->pageViewType == 'accordion')
		$content = $this->renderPartial('../wallbin/accordionView', array('libraryPage' => $libraryPage), true);
	else
		$content = $libraryPage->getCache();
?>
<div class="wallbin-header">
	<div class="wallbin-logo-wrapper">
		<img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
	</div>
	<div class="page-selector-container"><h3><? echo $libraryPage->name; ?></h3></div>
</div>
<div class="wallbin-container"><? echo $content; ?></div>