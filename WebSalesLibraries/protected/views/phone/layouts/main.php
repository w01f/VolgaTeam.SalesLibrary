<? /** @var $content string */ ?>
<!DOCTYPE html>
<html>
<head>
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<?
		$cs = Yii::app()->clientScript;
		$cs->registerCoreScript('jquery');
	?>
	<script type="text/javascript">
		window.BaseUrl = '<? echo Yii::app()->getBaseUrl(true); ?>' + '/';
	</script>
	<title><? echo $this->pageTitle; ?></title>
</head>
<body>
<? echo $content; ?>
</body>
</html>