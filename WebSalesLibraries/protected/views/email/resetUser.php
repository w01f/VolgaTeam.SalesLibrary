<?
	/**
	 * @var $body string
	 * @var $login string
	 * @var $password string
	 */
?>
<? echo $body; ?>
	<br><br>
<?
	echo Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('auth/changePassword', array(
		'login' => $login,
		'password' => $password,
		'rememberMe' => false
	))
?>