<?php

	/**
	 * Class ResetPasswordRecord
	 * @property mixed login
	 * @property mixed initial_date
	 */
	class ResetPasswordRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{reset_password}}';
		}

		/**
		 * @param $login
		 * @param $password
		 * @param $newUser
		 * @param $email
		 */
		public static function resetPasswordForUser($login, $password, $newUser, $email)
		{
			/** @var UserRecord $user */
			$user = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($user))
			{
				$resetPassword = ResetPasswordRecord::model()->findByPk($user->login);
				if (!isset($resetPassword))
				{
					$resetPassword = new ResetPasswordRecord();
					$resetPassword->login = $user->login;
				}
				$resetPassword->initial_date = date(Yii::app()->params['mysqlDateFormat']);
				$resetPassword->save();

				if ($email)
				{
					$message = Yii::app()->email;
					$message->to = $user->email;
					if (Yii::app()->params['email']['copy_enabled'])
						$message->cc = Yii::app()->params['email']['copy'];
					$message->subject = $newUser ? Yii::app()->params['email']['new_user']['subject'] : ('Password Reset for ' . Yii::app()->getBaseUrl(true));
					$message->from = Yii::app()->params['email']['from'];
					$message->viewVars = array('fullName' => ($user->first_name . ' ' . $user->last_name), 'login' => $user->login, 'password' => $password, 'site' => Yii::app()->name);
					$message->view = $newUser ? 'newUser' : 'existedUser';
					$message->send();
				}
			}
		}
	}