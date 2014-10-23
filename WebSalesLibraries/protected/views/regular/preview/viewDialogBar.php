<?
	if (!isset($format))
		$format = 'ppt';
	switch ($format)
	{
		case 'ppt':
			$wholeFile = 'PowerPoint File';
			$partFile = 'Slide';
			$oldFormatImage = Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/ppt.png';
			$newFormatImage = Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/pptx.png';
			$oldFormatTooltip = "Use PPT-format for single slides";
			$newFormatTooltip = "Use PPTX-format for single slides";
			break;
		case 'doc':
			$wholeFile = 'Word File';
			$partFile = 'Page';
			$oldFormatImage = Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/doc.png';
			$newFormatImage = Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/docx.png';
			$oldFormatTooltip = "Use DOC-format for single pages";
			$newFormatTooltip = "Use DOCX-format for single pages";
			break;
		case 'pdf':
			$wholeFile = 'PDF File';
			$partFile = 'Page';
			break;
	};
?>
<div class="view-dialog-bar">
	<div class="back">
		<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/back.png'; ?>" rel="tooltip" title="Back to File Options">
	</div>
	<? if ($format != 'pdf'): ?>
		<div class="format old" style="display: none;">
			<img src="<?php echo $oldFormatImage; ?>" rel="tooltip" title="<? echo $oldFormatTooltip; ?>"></div>
		<div class="format new" style="display: none;">
			<img src="<?php echo $newFormatImage; ?>" rel="tooltip" title="<? echo $newFormatTooltip; ?>"></div>
	<? endif; ?>
	<div class="download-all">
		<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/' . ($format != 'pdf' ? 'download-all.png' : 'download-pdf.png'); ?>" rel="tooltip" title="Download this <? echo $wholeFile; ?>">
	</div>
	<? if ($format != 'pdf'): ?>
		<div class="download-pdf">
			<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/download-pdf.png'; ?>" rel="tooltip" title="Download this this entire file as PDF to your machine">
		</div>
		<div class="download">
			<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/download.png'; ?>" rel="tooltip" title="Download Only this <? echo $partFile; ?>">
		</div>
	<? endif; ?>
	<div class="email-all">
		<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/viewDialogBar/email-all.png'; ?>" rel="tooltip" title="Email a Link to this File">
	</div>
</div>