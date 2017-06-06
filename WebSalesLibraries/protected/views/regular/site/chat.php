<?
	use Iflylabs\iFlyChat;

	if (\Yii::app()->params['comet_chat']['enabled'])
	{
		$_SESSION['cometchat_userid'] = UserIdentity::getCurrentUserId();
		$_SESSION['cometchat_baseurl'] = Yii::app()->getBaseUrl(true);

		$cs = Yii::app()->clientScript;
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cometchat/cometchatcss.php');
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cometchat/cometchatjs.php', CClientScript::POS_END);
	}
	else if (\Yii::app()->params['ifly_chat']['enabled'])
	{
		require_once(\Yii::app()->params['appRoot'] . '/vendor/iflychat/lib/iFlyChat.php');

		$iflychat = new iFlyChat(\Yii::app()->params['ifly_chat']['app_id'], \Yii::app()->params['ifly_chat']['app_key']);

		$avatarFolderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'avatars';
		$avatarUrlPrefix = Yii::app()->getBaseUrl(true) . '/images/avatars';
		$avatarUrl = null;
		if (UserIdentity::isUserAdmin())
		{
			$avatarFileName = null;
			switch (strtolower(UserIdentity::getCurrentUserLogin()))
			{
				case    'alex':
					$avatarFileName = 'alex.png';
					break;
				case 'billyb':
					$avatarFileName = 'billy.png';
					break;
			}
			if (isset($avatarFileName) && file_exists($avatarFolderPath . DIRECTORY_SEPARATOR . 'users' . DIRECTORY_SEPARATOR . $avatarFileName))
				$avatarUrl = $avatarUrlPrefix . '/users/' . $avatarFileName;
		}
		else
		{
			$groups = UserIdentity::getCurrentUserGroups();
			foreach ($groups as $group)
			{
				if (file_exists($avatarFolderPath . DIRECTORY_SEPARATOR . 'groups' . DIRECTORY_SEPARATOR . strtolower($group).'.png'))
				{
					$avatarUrl = $avatarUrlPrefix . '/groups/' . strtolower($group) . '.png';
					break;
				}
			}
		}

		$user = array(
			'user_name' => UserIdentity::getCurrentUserLogin(),
			'user_id' => UserIdentity::getCurrentUserId(),
			'is_admin' => UserIdentity::isUserAdmin(),
			'user_avatar_url' => $avatarUrl,
			'user_profile_url' => null,
		);

		$iflychat->setUser($user);

		$iflychat_code = $iflychat->getHtmlCode();

		echo $iflychat_code;
	}
?>