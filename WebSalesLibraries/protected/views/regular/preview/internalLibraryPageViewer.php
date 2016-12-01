<?
	/**
	 * @var $data InternalLinkPreviewData
	 */

	/** @var InternalLibraryPagePreviewInfo $previewInfo */
	$previewInfo = $data->previewInfo;
	$libraryPage = $previewInfo->getLibraryPage();

	$style = \application\models\wallbin\models\web\style\WallbinPageStyle::createDefault();
	if (!$previewInfo->showWindowHeaders)
	{
		$style->enabled = true;
		$style->showWindowHeaders = false;
	}

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
				'style' => $style
			), true);
?>
<style>
	#content .wallbin-header {
		background-color: <? echo '#'.$previewInfo->backColor; ?> !important;
	}

	#content .wallbin-header .wallbin-header-cell {
		border-bottom: 1px #999 solid !important;
	}

	#content .wallbin-header .single-page-header .header-text {
		color: <? echo '#'.$previewInfo->textColor; ?> !important;
	}
</style>
<div class="wallbin-header-container">
	<table class="wallbin-header">
		<tr>
			<? if ($previewInfo->showLogo): ?>
				<td class="wallbin-header-cell wallbin-logo-wrapper">
					<img class="wallbin-logo" src="<? echo $libraryPage->logoContent; ?>">
				</td>
			<? endif; ?>
			<? if ($previewInfo->showText): ?>
				<td class="wallbin-header-cell single-page-header<? if (!$previewInfo->showLogo): ?> single-page-header-no-logo<? endif; ?>">
					<h4 class="header-text"><? echo $libraryPage->name; ?></h4>
				</td>
			<? endif; ?>
		</tr>
	</table>
</div>
<div class="wallbin-container"><? echo $content; ?></div>