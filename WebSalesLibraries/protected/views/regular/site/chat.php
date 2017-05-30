<?
	if (\Yii::app()->params['comet_chat']['enabled'])
	{
		$_SESSION['cometchat_userid'] = UserIdentity::getCurrentUserId();
		$_SESSION['cometchat_baseurl'] = Yii::app()->getBaseUrl(true);

		$cs = Yii::app()->clientScript;
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cometchat/cometchatcss.php');
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cometchat/cometchatjs.php', CClientScript::POS_END);
	}
?>