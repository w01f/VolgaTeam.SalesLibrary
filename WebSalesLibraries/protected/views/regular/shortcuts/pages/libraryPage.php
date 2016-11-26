<?
	/** @var $shortcut LibraryPageShortcut */

	$libraryPage = $shortcut->getLibraryPage();
	if ($shortcut->pageViewType == 'accordion')
		$content = $this->renderPartial('../wallbin/accordionView',
			array(
				'libraryPage' => $libraryPage
			), true);
	else
		$content = $this->renderPartial('../wallbin/columnsView',
			array(
				'libraryPage' => $libraryPage,
				'style' => $shortcut->style->page
			), true);
?>
<style>
	<?if($shortcut->style->header->paddingLeft>0):?>
		#content .wallbin-header {
			margin-left: <? echo $shortcut->style->header->paddingLeft;?>px;
		}

		#content .wallbin-header > div {
			padding-left: 1px !important;
		}
	<?endif;?>

	#content .wallbin-header > div {
		border-bottom: 1px #<? echo $shortcut->style->header->headerBorderColor?> solid !important;
	}
</style>
<div
	class="wallbin-header<? if (!$shortcut->style->header->showText && !$shortcut->style->header->showLogo): ?> single-page-no-text-no-logo<? endif; ?>"
	<? if ($shortcut->style->header->showText || $shortcut->style->header->showLogo): ?>style="background-color: #<? echo $shortcut->style->header->backColor; ?> !important;" <? endif; ?>>
	<div class="wallbin-logo-wrapper">
		<? if ($shortcut->style->header->showLogo): ?>
			<img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
		<? endif; ?>
	</div>
	<div
		class="single-page-header<? if (!$shortcut->style->header->showLogo): ?> single-page-header-no-logo<? endif; ?>">
		<? if ($shortcut->style->header->showText): ?>
			<? if ($shortcut->style->header->showLogo): ?>
				<h3 class="header-text"
				    style="color: #<? echo $shortcut->style->header->textColor; ?> !important;"><? echo $libraryPage->name; ?></h3>
			<? else: ?>
				<h4 class="header-text"
				    style="color: #<? echo $shortcut->style->header->textColor; ?> !important;"><? echo $libraryPage->name; ?></h4>
			<? endif; ?>
		<? endif; ?>
	</div>
</div>
<div class="wallbin-container"><? echo $content; ?></div>