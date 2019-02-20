<?
	/**
	 * @var $fullName string
	 * @var $login string
	 * @var $password string
	 */
?>
Hello <? echo $fullName ?>
<br>
Welcome to GraySales.tv, your source for over $1 BILLION DOLLARS of Local sales ideas!
<br><br>
Your GraySales.tv Account is activated through your Gray Connect Dashboard, and you can access GSTV anytime by logging into your Gray Connect account.
<br><br>
**If you want to access GraySales.tv directly (without Gray Connect) you will need to create your account password.
<br><br>
**Your username is ALWAYS your email address.
<br><br>
**Set up your GSTV password by clicking link below:
<br>
<?
	echo Yii::app()->getBaseUrl(true) .Yii::app()->createUrl('auth/changePassword', array(
			'login' => $login,
			'password' => $password,
			'rememberMe' => false
		))
?>