<?
	/**
	 * @var $link LibraryLink | Attachment
	 * @var $senderName string
	 */
?>
<!DOCTYPE html>
<html>
<head>
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<?
		$cs = Yii::app()->clientScript;
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . Yii::app()->params['version']);
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
		$cs->registerCoreScript('jquery');
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewing.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	?>
	<script type="text/javascript">
		(function ($)
		{
			$(document).ready(function ()
			{
				$('a#view-dialog-link').fancybox();
				$('a.video-link').on('click', function ()
				{
					$.SalesPortal.LinkManager.viewSelectedFormat($(this), false, false);
				});
			});
		})(jQuery);
	</script>
</head>
<body>
<span><? echo $senderName; ?> Sent you this Video Link to preview.</span> <br>
<h2><? echo $link->name; ?></h2>
<br> <a class="video-link" href="#">
	<h3>MP4</h3>
            <span class="service-data" style="display: none;">
                <div class="link-id"><? echo $link->id; ?></div>
				<div class="link-name"><? echo $link->name; ?></div>
				<div class="file-name"><? echo isset($link->isAttachment) ? $link->name : $link->fileName; ?></div>
                <div class="file-type"><? echo $link->originalFormat; ?></div>
                <div class="view-type"><? echo 'tab'; ?></div>
				<? $viewLinks = $link->getViewSource('tab'); ?>
				<? if (isset($viewLinks)): ?>
					<div class="links"><? echo json_encode($viewLinks); ?></div>
				<? endif; ?>
            </span> </a>
<? if (isset($expiresIn)): ?>
	<br>
	<br>
	<span><i>This link will expire in <? echo $expiresIn; ?> days.</i></span>
<? endif; ?>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container" style="display: none;">View Options</a>
	<div id="view-dialog-wrapper" style="display: none;">
		<div id="view-dialog-container">
		</div>
	</div>
</div>
<!------------------------->
</body>
</html>