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
	#content .wallbin-header-container {
		padding-left: <? echo $shortcut->style->header->paddingLeft;?>px;
	}

	#content .wallbin-header .wallbin-header-cell {
		padding-left: 0;
	}
	<?endif;?>

	#content .wallbin-header {
		background-color: <? echo '#'.$shortcut->style->header->backColor; ?> !important;
	}

	#content .wallbin-header .wallbin-header-cell {
		border-bottom: 1px <? echo '#'.$shortcut->style->header->headerBorderColor; ?> solid !important;
	}

	#content .wallbin-header .single-page-header .header-text {
		color: <? echo '#'.$shortcut->style->header->textColor; ?> !important;
	}

	<? if ($shortcut->searchBar->configured): ?>
	#content .wallbin-header .shortcuts-search-bar-container {
		padding-right: 10px;
		padding-top: 25px;
		padding-bottom: 0;
		vertical-align: top;
	}

	<? if ($shortcut->style->header->showLogo && $shortcut->style->header->showText): ?>
	#content .wallbin-header .shortcuts-search-bar-container {
		padding-bottom: 25px;
		width: 100%;
	}

	<? else: ?>
	#content .wallbin-header .shortcuts-search-bar-container {
		width: 70%;
	}

	#content .wallbin-header .wallbin-logo-wrapper {
		width: 30%;
		vertical-align: top;
	}

	#content .wallbin-header .single-page-header.single-page-header-no-logo {
		width: 30%;
		padding-top: 30px;
		vertical-align: top;
	}

	#content .wallbin-header .single-page-header.single-page-header-no-logo h4 {
		margin: 0;
	}

	<? endif; ?>
	<? endif; ?>
</style>
<div class="wallbin-header-container">
	<table class="wallbin-header">
		<? if ($shortcut->style->header->showLogo && $shortcut->style->header->showText): ?>
			<tr>
				<td class="wallbin-logo-wrapper<? if (!$shortcut->searchBar->configured): ?> wallbin-header-cell<? endif; ?>">
					<img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
				</td>
				<td class="single-page-header<? if (!$shortcut->searchBar->configured): ?> wallbin-header-cell<? endif; ?>">
					<h3 class="header-text"><? echo $libraryPage->name; ?></h3>
				</td>
			</tr>
			<? if ($shortcut->searchBar->configured): ?>
				<tr>
					<td class="wallbin-header-cell shortcuts-search-bar-container" colspan="2">
						<? echo $this->renderPartial('searchBar/bar', array('searchBar' => $shortcut->searchBar), true); ?>
					</td>
				</tr>
			<? endif; ?>
		<? else: ?>
			<tr>
				<? if ($shortcut->style->header->showLogo): ?>
					<td class="wallbin-header-cell wallbin-logo-wrapper">
						<img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
					</td>
				<? endif; ?>
				<? if ($shortcut->style->header->showText): ?>
					<td class="wallbin-header-cell single-page-header single-page-header-no-logo">
						<h4 class="header-text"><? echo $libraryPage->name; ?></h4>
					</td>
				<? endif; ?>
				<? if ($shortcut->searchBar->configured): ?>
					<td class="wallbin-header-cell shortcuts-search-bar-container">
						<? echo $this->renderPartial('searchBar/bar', array('searchBar' => $shortcut->searchBar), true); ?>
					</td>
				<? endif; ?>
			</tr>
		<? endif; ?>
	</table>
</div>
<div class="wallbin-container"><? echo $content; ?></div>