<?
	/**
	 * @var $data InternalLinkPreviewData
	 */

	/** @var InternalLibraryPagePreviewInfo $previewInfo */
	$previewInfo = $data->previewInfo;
	$libraryPage = $previewInfo->getLibraryPage();
	if ($previewInfo->pageViewType == 'accordion')
		$content = $this->renderPartial(
			'../wallbin/accordionView',
			array(
				'libraryPage' => $libraryPage
			), true);
	else
		$content = $this->renderPartial(
			'../wallbin/columnsView',
			array(
				'libraryPage' => $libraryPage,
				'showWindowHeaders' => $previewInfo->showWindowHeaders
			), true);
?>
<div
	class="wallbin-header<? if (!$previewInfo->showText && !$previewInfo->showLogo): ?> single-page-no-text-no-logo<? endif; ?>"
	<? if ($previewInfo->showText || $previewInfo->showLogo): ?>style="background-color: <? echo $previewInfo->backColor; ?> !important;" <? endif; ?>>
	<div class="wallbin-logo-wrapper">
		<? if ($previewInfo->showLogo): ?>
			<img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
		<? endif; ?>
	</div>
	<div class="single-page-header<? if (!$previewInfo->showLogo): ?> single-page-header-no-logo<? endif; ?>">
		<? if ($previewInfo->showText): ?>
			<? if ($previewInfo->showLogo): ?>
				<h3 class="header-text"
				    style="color: <? echo $previewInfo->textColor; ?> !important;"><? echo $libraryPage->name; ?></h3>
			<? else: ?>
				<h4 class="header-text"
				    style="color: <? echo $previewInfo->textColor; ?> !important;"><? echo $libraryPage->name; ?></h4>
			<? endif; ?>
		<? endif; ?>
	</div>
</div>
<div class="wallbin-container"><? echo $content; ?></div>