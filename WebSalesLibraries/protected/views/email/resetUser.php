<?php echo $body; ?>
	<br><br>
<?php
	echo Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('site/changePassword', array(
		'login' => $login,
		'password' => $password,
		'rememberMe' => false
	))
?>