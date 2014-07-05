<? /** @var $content string */ ?>
<!DOCTYPE html>
<html>
<head>
	<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0"/>
	<? Yii::app()->clientScript->registerCoreScript('jquery'); ?>
	<script type="text/javascript">
		window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/';
	</script>
	<title><?php echo $this->pageTitle; ?></title>
</head>
<body <? if (isset(Yii::app()->browser) && Yii::app()->browser->isMobile()): ?> class="mobile-body"<? endif; ?>>
<? echo $content; ?>
</body>
</html>