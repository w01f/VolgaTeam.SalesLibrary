<?
	/**
	 * @var $data InternalLinkPreviewData
	 */

	/** @var IsdController $controller */
	$controller = $this;

	/** @var InternalShortcutPreviewInfo $previewInfo */
	$previewInfo = $data->previewInfo;
?>
<div class="service-data">
	<? echo $previewInfo->getShortcutData($controller->isPhone); ?>
</div>
