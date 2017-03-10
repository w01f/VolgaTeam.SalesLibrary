<?
	require_once(\Yii::app()->params['appRoot'] . '/vendor/iflychat/lib/iFlyChat.php');
	use Iflylabs\iFlyChat;

	if (\Yii::app()->params['ifly_chat']['enabled'])
	{
		$iflychat = new iFlyChat(\Yii::app()->params['ifly_chat']['app_id'], \Yii::app()->params['ifly_chat']['app_key']);

		$user = array(
			'user_name' => UserIdentity::getCurrentUserLogin(),
			'user_id' => UserIdentity::getCurrentUserId(),
			'is_admin' => UserIdentity::isUserAdmin(),
			'user_avatar_url' => null,
			'user_profile_url' => null,
		);

		$iflychat->setUser($user);

		$iflychat_code = $iflychat->getHtmlCode();

		echo $iflychat_code;
	}