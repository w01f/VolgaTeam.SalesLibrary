<?php

	class AuthController extends IsdController
	{
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

		public function actionSiteHelpDialog()
		{
			$this->renderPartial('siteHelp', array(), false, true);
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

		public function actionChangePassword()
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
					$this->render('changePassword', array('formData' => $changePasswordModel));
				}
			}
		}

		public function actionRecoverPasswordDialog()
		{
			$this->renderPartial('recoverPassword', array(), false, true);
		}

		public function actionRecoverPasswordDialogSuccess()
		{
			$this->renderPartial('successDialog', array('header' => 'Password Recovery', 'content' => 'A temporary password has been sent<br>Check your inbox of junk mail filter'), false, true);
		}

		public function actionValidateUserByEmail()
		{
			$login = Yii::app()->request->getPost('login');
			$email = Yii::app()->request->getPost('email');
			$result = 'Error while validating user. Try again or contact to technical support';
			if (isset($login) && isset($email))
				$result = UserRecord::validateUserByEmail($login, $email);
			echo $result;
		}

		public function actionRecoverPassword()
		{
			$login = Yii::app()->request->getPost('login');
			if (isset($login))
			{
				$password = UserRecord::generatePassword();
				UserRecord::changePassword($login, $password);
				ResetPasswordRecord::resetPasswordForUser($login, $password, false, true);
			}
		}
	}
