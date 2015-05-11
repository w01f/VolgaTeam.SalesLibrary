<?
	if (!isset($format))
		$format = 'ppt';
	switch ($format)
	{
		case 'ppt':
			$wholeFile = 'PowerPoint File';
			$partFile = 'Slide';
			break;
		case 'doc':
			$wholeFile = 'Word File';
			$partFile = 'Page';
			break;
		case 'pdf':
			$wholeFile = 'PDF File';
			$partFile = 'Page';
			break;
	};
?>
<div class="link-viewer-bar">
	<div class="back">
		<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/preview/bar/back.png'; ?>" rel="tooltip" title="Back to File Options">
	</div>
	<div class="download-all">
		<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/preview/bar/' . ($format != 'pdf' ? 'download-all.png' : 'download-pdf.png'); ?>" rel="tooltip" title="Download this <? echo $wholeFile; ?>">
	</div>
	<? if ($format != 'pdf'): ?>
		<div class="download-pdf">
			<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/preview/bar/download-pdf.png'; ?>" rel="tooltip" title="Download this this entire file as PDF to your machine">
		</div>
		<div class="download">
			<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/preview/bar/download.png'; ?>" rel="tooltip" title="Download Only this <? echo $partFile; ?>">
		</div>
	<? endif; ?>
	<div class="email-all">
		<img src="<?php echo Yii::app()->getBaseUrl(true) . '/images/preview/bar/email-all.png'; ?>" rel="tooltip" title="Email a Link to this File">
	</div>
</div>