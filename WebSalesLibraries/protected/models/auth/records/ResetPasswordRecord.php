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
		 * @param $sendEmail
		 * @param $emailSubject
		 * @param $emailView
		 */
		public static function resetPasswordForUser($login, $password, $sendEmail, $emailSubject, $emailView)
		{
			/** @var UserRecord $user */
			$user = UserRecord::model()->find('LOWER(login)=? or LOWER(email)=?', array(strtolower($login),strtolower($login)));
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

				if ($sendEmail)
				{
					$message = Yii::app()->email;
					$message->to = $user->email;
					if (Yii::app()->params['email']['copy_enabled'])
						$message->cc = Yii::app()->params['email']['copy'];
					$message->subject = $emailSubject;
					$message->from = Yii::app()->params['email']['from'];
					$message->viewVars = array('fullName' => $user->first_name, 'login' => $user->login, 'password' => $password, 'site' => Yii::app()->name);
					$message->view = $emailView;
					$message->send();
				}
			}
		}
	}