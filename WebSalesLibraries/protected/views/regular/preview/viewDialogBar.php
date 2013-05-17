<?
if (isset($format) && $format == 'ppt')
{
	$wholeFile = 'PowerPoint File';
	$partFile = 'Slide';
	$oldFormatImage = Yii::app()->baseUrl . '/images/viewDialogBar/ppt.png';
	$newFormatImage = Yii::app()->baseUrl . '/images/viewDialogBar/pptx.png';
	$oldFormatTooltip = "Use PPT-format for single slides";
	$newFormatTooltip = "Use PPTX-format for single slides";
}
else
{
	$wholeFile = 'Word File';
	$partFile = 'Page';
	$oldFormatImage = Yii::app()->baseUrl . '/images/viewDialogBar/doc.png';
	$newFormatImage = Yii::app()->baseUrl . '/images/viewDialogBar/docx.png';
	$oldFormatTooltip = "Use DOC-format for single pages";
	$newFormatTooltip = "Use DOCX-format for single pages";
}
?>
<div class="view-dialog-bar">
	<div class="back">
		<img src="<?php echo Yii::app()->baseUrl . '/images/viewDialogBar/back.png'; ?>" rel="tooltip" title="Back to File Options">
	</div>
	<div class="format old" style="display: none;">
		<img src="<?php echo $oldFormatImage; ?>" rel="tooltip" title="<? echo $oldFormatTooltip; ?>"></div>
	<div class="format new" style="display: none;">
		<img src="<?php echo $newFormatImage; ?>" rel="tooltip" title="<? echo $newFormatTooltip; ?>"></div>
	<div class="download-all">
		<img src="<?php echo Yii::app()->baseUrl . '/images/viewDialogBar/download-all.png'; ?>" rel="tooltip" title="Download this <? echo $wholeFile; ?>">
	</div>
	<div class="download-pdf">
		<img src="<?php echo Yii::app()->baseUrl . '/images/viewDialogBar/download-pdf.png'; ?>" rel="tooltip" title="Download this this entire file as PDF to your machine">
	</div>
	<div class="download">
		<img src="<?php echo Yii::app()->baseUrl . '/images/viewDialogBar/download.png'; ?>" rel="tooltip" title="Download Only this <? echo $partFile; ?>">
	</div>
	<div class="email-all">
		<img src="<?php echo Yii::app()->baseUrl . '/images/viewDialogBar/email-all.png'; ?>" rel="tooltip" title="Email a Link to this File">
	</div>
</div>