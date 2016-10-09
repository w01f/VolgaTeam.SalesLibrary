<?
	/** @var $shortcut LibraryPageShortcut */

	$libraryPage = $shortcut->getLibraryPage();
	if ($shortcut->pageViewType == 'accordion')
		$content = $this->renderPartial('../wallbin/accordionView', array('libraryPage' => $libraryPage), true);
	else
		$content = $this->renderPartial('../wallbin/columnsView', array('libraryPage' => $libraryPage, 'showWindowHeaders' => $shortcut->showWindowHeaders), true);
?>
<div
	class="wallbin-header<? if (!$shortcut->showText && !$shortcut->showLogo): ?> single-page-no-text-no-logo<? endif; ?>"
	<? if ($shortcut->showText || $shortcut->showLogo): ?>style="background-color: #<? echo $shortcut->backColor; ?> !important;" <? endif; ?>>
	<div class="wallbin-logo-wrapper">
		<? if ($shortcut->showLogo): ?>
			<img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
		<? endif; ?>
	</div>
	<div class="single-page-header<? if (!$shortcut->showLogo): ?> single-page-header-no-logo<? endif; ?>">
		<? if ($shortcut->showText): ?>
			<? if ($shortcut->showLogo): ?>
				<h3 class="header-text"
				    style="color: #<? echo $shortcut->textColor; ?> !important;"><? echo $libraryPage->name; ?></h3>
			<? else: ?>
				<h4 class="header-text"
				    style="color: #<? echo $shortcut->textColor; ?> !important;"><? echo $libraryPage->name; ?></h4>
			<? endif; ?>
		<? endif; ?>
	</div>
</div>
<div class="wallbin-container"><? echo $content; ?></div>