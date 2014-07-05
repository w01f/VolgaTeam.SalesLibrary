<?
	/**
	 * @var $fullName string
	 * @var $login string
	 * @var $password string
	 */
?>
Hello <? echo $fullName ?>:
<br><br>
Your Account Password has been reset.
<br><br>
Account Username: <? echo $login ?><br><br>
Click the link below to create your NEW Password:<br><br>
<?
	echo Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('auth/changePassword', array(
			'login' => $login,
			'password' => $password,
			'rememberMe' => false
		))
?>