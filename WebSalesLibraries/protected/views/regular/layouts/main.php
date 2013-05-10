<!DOCTYPE html>
<html>
<head>
	<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0"/>
	<?php Yii::app()->clientScript->registerCoreScript('jquery'); ?>
	<title><?php echo $this->pageTitle; ?></title>
</head>
<body <?if (isset(Yii::app()->browser) && Yii::app()->browser->isMobile()): ?> class="mobile-body"<? endif;?>>
	<?php echo $content; ?>
</body>
</html>