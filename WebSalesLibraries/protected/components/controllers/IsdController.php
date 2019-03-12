<?

	/**
	 * Class IsdController
	 */
	abstract class IsdController extends CController
	{
		public $browser;
		public $pathPrefix;
		public $isPhone;
		public $isIOSDevice;
		public $isTokenAuthenticated;

		/** return boolean */
		protected function getIsPublicController()
		{
			return false;
		}

		/** return array */
		protected function getPublicActionIds()
		{
			return array();
		}

		public function init()
		{
			//Yii::app()->browser->setUserAgent('Mozilla/5.0 (iPhone; CPU iPhone OS 5_0 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9A334 Safari/7534.48.3');
			//Yii::app()->browser->setUserAgent('Mozilla/5.0 (Linux; U; Android 4.2.2; es-us; GT-P5210 Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30/1.05v.3406.d7');
			$this->browser = Yii::app()->browser->getBrowser();
			$this->isIOSDevice = $this->browser == Browser::BROWSER_IPAD;

			if (Yii::app()->params['jqm_theme']['jqm_enabled'] === true)
			{
				switch ($this->browser)
				{
					case Browser::BROWSER_IPHONE:
					case Browser::BROWSER_ANDROID_MOBILE:
						$this->layout = '/phone/layouts/main';
						$this->pathPrefix = 'application.views.phone.';
						$this->isPhone = true;
						break;
					default :
						$this->layout = '/regular/layouts/main';
						$this->pathPrefix = 'application.views.regular.';
						$this->isPhone = false;
						break;
				}
			}
			else
			{
				$this->layout = '/regular/layouts/main';
				$this->pathPrefix = 'application.views.regular.';
				$this->isPhone = false;
			}
		}

		protected function beforeAction($action)
		{
			/** @var CHttpRequest $request */
			$request = Yii::app()->request;

			if ($request->getIsAjaxRequest())
				return true;

			if (Yii::app()->params['login']['use_token_connection'])
			{
				$tokenRequestData = \application\models\auth\models\TokenRequestData::tryExtractTokenData($request);
				//var_dump($tokenRequestData);
				if ($tokenRequestData->configured)
				{
					if ($this->authenticateWithToken($tokenRequestData))
					{
						$this->isTokenAuthenticated = true;
						return true;
					}
				}
			}

			if (\UserIdentity::isUserAuthorized())
				return true;

			if ($this->getIsPublicController())
				return true;

			if (!(in_array($action->id, $this->getPublicActionIds())))
			{
				Yii::app()->user->loginRequired();
				return false;
			}

			return true;
		}

		/**
		 * @param $tokenRequestData \application\models\auth\models\TokenRequestData
		 * @return bool
		 */
		private function authenticateWithToken($tokenRequestData)
		{
			$email = $tokenRequestData->email;
			$token = $tokenRequestData->token;
			$secretKey = Yii::app()->params['login']['secret_key'];

			$currentTime = time();
			$allowedTime = $currentTime - 10;

			$currentTz = $script_tz = date_default_timezone_get();
			date_default_timezone_set("UTC");

			for ($t = $allowedTime; $t <= $currentTime; $t++)
			{
				$hashToCheck = md5($email . $t . $secretKey);
				if ($hashToCheck == $token)
				{
					/** @var  $userRecord UserRecord */
					$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($tokenRequestData->login)));
					if (!isset($userRecord))
					{
						/** @var GroupRecord $groupRecord */
						$groupRecord = GroupRecord::model()->find('LOWER(name)=?', array(strtolower($tokenRequestData->group)));
						if (isset($groupRecord))
						{
							$userRecord = new UserRecord();
							$userRecord->login = $tokenRequestData->login;
							$userRecord->password = UserRecord::hashPassword($tokenRequestData->password);
							$userRecord->first_name = $tokenRequestData->firstName;
							$userRecord->last_name = $tokenRequestData->lastName;
							$userRecord->email = $tokenRequestData->email;
							$userRecord->phone = $tokenRequestData->phone;
							$userRecord->role = 0;
							$userRecord->date_add = date(Yii::app()->params['mysqlDateTimeFormat']);
							$userRecord->save();

							$userGroupRecord = new UserGroupRecord();
							$userGroupRecord->id = uniqid();
							$userGroupRecord->id_user = $userRecord->id;
							$userGroupRecord->id_group = $groupRecord->id;
							$userGroupRecord->save();

							ResetPasswordRecord::resetPasswordForUser(
								$tokenRequestData->login,
								$tokenRequestData->password,
								true,
								'Your New GraySales.tv Account is Activated!',
								'newTokenUser');
						}
					}

					if (isset($userRecord))
					{
						date_default_timezone_set($currentTz);

						$identity = new \UserIdentity($userRecord->login, $userRecord->password);
						if ($identity->authenticate())
						{
							$duration = 3600 * 24 * 30; // 30 days
							\Yii::app()->user->login($identity, $duration);
							StatisticActivityRecord::writeCommonActivity('System', 'Login (GC)', null);
							return true;
						}
					}
					break;
				}
			}
			return false;
		}
	}
