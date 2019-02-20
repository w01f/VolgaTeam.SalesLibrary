<?php

	class AuthController extends IsdController
	{
		public function init()
		{
			parent::init();
			if ($this->isPhone)
				$this->layout = '/phone/layouts/main';
			else
				$this->layout = '/regular/layouts/auth';
		}

		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'auth');
		}

		public function actionLogin()
		{
			if (Yii::app()->params['jqm_theme']['jqm_enabled'] === true)
			{
				if ($this->isPhone)
					$this->redirect($this->createAbsoluteUrl('auth/loginSeparate'));
				else
					$this->redirect($this->createAbsoluteUrl('auth/loginUniversal'));
			}
			else
				$this->redirect($this->createAbsoluteUrl('auth/loginUniversal'));
		}

		public function actionLoginSeparate()
		{
			$loginModel = new LoginForm();

			$attributes = Yii::app()->request->getPost('LoginForm');
			if (isset($attributes))
			{
				$loginModel->attributes = $attributes;
				if ($loginModel->validate() && $loginModel->login())
				{
					StatisticActivityRecord::writeCommonActivity('System', 'Login', null);
					if ($loginModel->needToResetPassword)
					{
						$this->redirect($this->createUrl('auth/changePassword', array(
							'login' => $loginModel->login,
							'password' => $loginModel->password,
							'rememberMe' => $loginModel->rememberMe
						)));
					}
					else
						$this->redirect(Yii::app()->user->returnUrl);
				}
			}
			$this->pageTitle = Yii::app()->name . ' - Login';
			$this->render('login', array('formData' => $loginModel));
		}

		public function actionLoginUniversal()
		{
			$this->pageTitle = Yii::app()->name . ' - Login';
			$this->render('login');
		}

		public function actionProcessUniversalLoginData()
		{
			$actionType = Yii::app()->request->getPost('actionType');
			$actionData = Yii::app()->request->getPost('actionData');
			if (!empty($actionType) && isset($actionData))
			{
				$actionDataEncoded = CJSON::encode($actionData);
				switch ($actionType)
				{
					case "login":
						$loginModel = \application\models\auth\models\LoginModel::fromJson($actionDataEncoded);
						$errors = $loginModel->validateCredentials();
						if ($loginModel->authenticated)
						{
							$loginModel->login();
							StatisticActivityRecord::writeCommonActivity('System', 'Login', null);

							if ($loginModel->needToResetPassword)
							{
								echo CJSON::encode(array(
									'nextAction' => 'change-password',
									'loginModel' => $loginModel
								));
							}
							else
								echo CJSON::encode(array(
									'nextAction' => 'login',
									'returnUrl' => Yii::app()->user->returnUrl
								));
						}
						else
						{
							echo CJSON::encode(array(
								'nextAction' => 'fix-errors',
								'errors' => $errors
							));
						}
						break;
					case "change-password":
						$changePasswordModel = \application\models\auth\models\ChangePasswordModel::fromJson($actionDataEncoded);
						$errors = $changePasswordModel->validatePassword();
						if (count($errors) > 0)
						{
							echo CJSON::encode(array(
								'nextAction' => 'fix-errors',
								'errors' => $errors
							));
						}
						else
						{
							$changePasswordModel->changePassword();
							StatisticActivityRecord::writeCommonActivity('System', 'Login', null);

							echo CJSON::encode(array(
								'nextAction' => 'login',
								'returnUrl' => Yii::app()->user->returnUrl
							));
						}
						break;
				}
			}
			Yii::app()->end();
		}

		public function actionProcessSuccessfulUniversalLogin()
		{
			$returnUrl = Yii::app()->request->getPost('returnUrl');
			$this->redirect($returnUrl);
		}

		public function actionLogout()
		{
			StatisticActivityRecord::writeCommonActivity('System', 'Logout', null);
			Yii::app()->user->logout();
			Yii::app()->end();
		}

		public function actionUnauthorized()
		{
			$this->render('unauthorized');
		}

		public function actionSendHelpRequest()
		{
			$email = Yii::app()->request->getPost('email');
			$name = Yii::app()->request->getPost('name');
			$station = Yii::app()->request->getPost('station');
			$text = Yii::app()->request->getPost('text');

			$message = Yii::app()->email;
			$to = array(Yii::app()->params['email']['help_request_address']);
			$message->to = $to;
			$message->subject = 'Site Help Request - ' . Yii::app()->request->serverName;
			$message->from = $email;
			$message->message = sprintf("%s<br><br>%s<br><br>%s", $name, $station, $text);
			$message->send();

			Yii::app()->end();
		}

		public function actionGetChangePasswordDialog()
		{
			$login = UserIdentity::getCurrentUserLogin();
			$this->renderPartial('changePasswordDialog', array('login' => $login), false, true);
		}

		public function actionChangePassword()
		{
			$login = Yii::app()->request->getQuery('login');
			$oldPassword = Yii::app()->request->getQuery('password');
			$rememberMe = Yii::app()->request->getQuery('rememberMe', false);
			if (Yii::app()->params['jqm_theme']['jqm_enabled'] === true)
			{
				if ($this->isPhone)
					$this->redirect($this->createAbsoluteUrl('auth/changePasswordSeparate', array(
						'login' => $login,
						'password' => $oldPassword,
						'rememberMe' => $rememberMe
					)));
				else
					$this->redirect($this->createAbsoluteUrl('auth/changePasswordUniversal', array(
						'login' => $login,
						'password' => $oldPassword,
						'rememberMe' => $rememberMe
					)));
			}
			else
				$this->redirect($this->createAbsoluteUrl('auth/changePasswordUniversal', array(
					'login' => $login,
					'password' => $oldPassword,
					'rememberMe' => $rememberMe
				)));
		}

		public function actionChangePasswordSeparate()
		{
			$changePasswordModel = new ChangePasswordForm();
			$attributes = Yii::app()->request->getPost('ChangePasswordForm');
			$this->pageTitle = Yii::app()->name . ' - Change Password';
			if (isset($attributes))
			{
				$changePasswordModel->attributes = $attributes;
				$changePasswordModel->login = $attributes['login'];
				$changePasswordModel->oldPassword = $attributes['oldPassword'];
				$changePasswordModel->rememberMe = $attributes['rememberMe'];
				if ($changePasswordModel->validate() && $changePasswordModel->changePassword())
				{
					StatisticActivityRecord::writeCommonActivity('System', 'Login', null);
					$this->redirect(Yii::app()->user->returnUrl);
				}
				else
					$this->render('changePassword', array('formData' => $changePasswordModel));
			}
			else
			{
				$login = Yii::app()->request->getQuery('login');
				$oldPassword = Yii::app()->request->getQuery('password');
				$rememberMe = Yii::app()->request->getQuery('rememberMe', false);
				if (isset($login) && isset($oldPassword))
				{
					$changePasswordModel->login = $login;
					$changePasswordModel->oldPassword = $oldPassword;
					$changePasswordModel->rememberMe = $rememberMe;
					$this->render('changePasswordPage', array('formData' => $changePasswordModel));
				}
			}
		}

		public function actionChangePasswordUniversal()
		{
			$login = Yii::app()->request->getQuery('login');
			$oldPassword = Yii::app()->request->getQuery('password');
			$rememberMe = Yii::app()->request->getQuery('rememberMe', false);
			if (empty(trim($rememberMe)))
				$rememberMe = false;
			$this->pageTitle = Yii::app()->name . ' - Change Password';
			$this->render('changePasswordPage', array(
				'login' => $login,
				'password' => $oldPassword,
				'rememberMe' => $rememberMe
			));
		}

		public function actionChangePasswordQuick()
		{
			$password = Yii::app()->request->getPost('password');
			$login = UserIdentity::getCurrentUserLogin();
			UserRecord::changePasswordByLogin($login, $password);
			ResetPasswordRecord::model()->deleteByPk($login);
			echo CJSON::encode(array("success" => true));
		}

		public function actionValidateUserByEmail()
		{
			$email = Yii::app()->request->getPost('email');
			$result = 'Error while validating user. Try again or contact to technical support';
			if (isset($email))
				$result = UserRecord::validateUserByEmail($email);
			echo $result;
		}

		public function actionRecoverPassword()
		{
			$email = Yii::app()->request->getPost('email');
			if (isset($email))
			{
				$password = UserRecord::generatePassword();
				UserRecord::changePasswordByEmail($email, $password);
				ResetPasswordRecord::resetPasswordForUser(
					$email,
					$password,
					true,
					'Password Reset for ' . Yii::app()->getBaseUrl(true),
					'existedUser');
			}
		}
	}
