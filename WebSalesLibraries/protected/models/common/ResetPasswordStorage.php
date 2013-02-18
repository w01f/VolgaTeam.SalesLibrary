<?php
	class ResetPasswordStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{reset_password}}';
		}

		public static function resetPasswordForUser($login, $password, $newUser)
		{
			$user = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($user))
			{
				$resetPassword = ResetPasswordStorage::model()->findByPk($user->login);
				if (!isset($resetPassword))
				{
					$resetPassword = new ResetPasswordStorage();
					$resetPassword->login = $user->login;
				}
				$resetPassword->initial_date = date(Yii::app()->params['mysqlDateFormat'], strtotime(date('y:m:d')));
				$resetPassword->save();

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