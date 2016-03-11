<?php

	/**
	 * Class UserRecord
	 * @property mixed id
	 * @property mixed login
	 * @property mixed password
	 * @property mixed first_name
	 * @property mixed last_name
	 * @property mixed email
	 * @property mixed phone
	 * @property mixed role
	 * @property mixed date_add
	 * @property mixed date_modify
	 */
	class UserRecord extends CActiveRecord
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
			return '{{user}}';
		}

		/**
		 * @param $password
		 * @return bool
		 */
		public function validatePassword($password)
		{
			return self::hashPassword($password) === $this->password;
		}

		/**
		 * @param $password
		 * @return string
		 */
		public static function hashPassword($password)
		{
			return md5($password);
		}

		/**
		 * @param $login
		 * @param $password
		 */
		public static function changePassword($login, $password)
		{
			/** @var $user UserRecord */
			$user = self::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($user))
			{
				$user->password = self::hashPassword($password);
				$user->date_modify = date(Yii::app()->params['mysqlDateFormat'], strtotime(date("Y-m-d H:i:s")));
				$user->save();
			}
		}

		/**
		 * @return string
		 */
		public static function generatePassword()
		{
			$pass = '';
			if (Yii::app()->params['login']['complex_password'])
				$pass = self::generateStrongPassword();
			else
			{
				$alphabet = "abcdefghijklmnopqrstuwxyz0123456789";
				for ($i = 0; $i < 5; $i++)
				{
					$n = rand(0, strlen($alphabet) - 1);
					$pass[$i] = $alphabet[$n];
				}
				$pass = implode($pass);
			}
			return $pass;
		}

		/**
		 * @return string
		 */
		public static function generateStrongPassword()
		{
			$out = '';
			$l = 10;
			$c = 2;
			$n = 2;
			$s = 2;
			// get count of all required minimum special chars
			$count = $c + $n + $s;

			// change these strings if you want to include or exclude possible password characters
			$chars = "abcdefghijklmnopqrstuvwxyz";
			$caps = strtoupper($chars);
			$nums = "0123456789";
			$syms = "!@#$%^&*()-+?";

			// build the base password of all lower-case letters
			for ($i = 0; $i < $l; $i++)
			{
				$out .= substr($chars, mt_rand(0, strlen($chars) - 1), 1);
			}

			// create arrays if special character(s) required
			if ($count)
			{
				// split base password to array; create special chars array
				$tmp1 = str_split($out);
				$tmp2 = array();

				// add required special character(s) to second array
				for ($i = 0; $i < $c; $i++)
				{
					array_push($tmp2, substr($caps, mt_rand(0, strlen($caps) - 1), 1));
				}
				for ($i = 0; $i < $n; $i++)
				{
					array_push($tmp2, substr($nums, mt_rand(0, strlen($nums) - 1), 1));
				}
				for ($i = 0; $i < $s; $i++)
				{
					array_push($tmp2, substr($syms, mt_rand(0, strlen($syms) - 1), 1));
				}

				// hack off a chunk of the base password array that's as big as the special chars array
				$tmp1 = array_slice($tmp1, 0, $l - $count);
				// merge special character(s) array with base password array
				$tmp1 = array_merge($tmp1, $tmp2);
				// mix the characters up
				shuffle($tmp1);
				// convert to string for output
				$out = implode('', $tmp1);
			}
			return $out;
		}

		/**
		 * @param $login
		 * @param $email
		 * @return string
		 */
		public static function validateUserByEmail($login, $email)
		{
			/** @var $result string */
			/** @var $user UserRecord */
			$user = self::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($user))
			{
				if (strtolower($user->email) == strtolower($email))
					$result = '';
				else
					$result = 'Email address is not correct';
			}
			else
				$result = 'User with name "' . $login . '" is not registered';
			return $result;
		}

		/**
		 * @return array
		 */
		public static function getAdminUserIds()
		{
			$userIds = array();
			foreach (self::model()->findAll('role=2') as $user)
				$userIds[] = $user->id;
			return $userIds;
		}

		/**
		 * @param $login
		 */
		public static function deleteUserByLogin($login)
		{
			/** @var $userRecord UserRecord */
			$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($userRecord))
			{
				UserLibraryRecord::clearObjectsByUser($userRecord->id);
				UserGroupRecord::clearObjectsByUser($userRecord->id);
				UserRecord::model()->deleteByPk($userRecord->id);
				FavoritesLinkRecord::clearByUser($userRecord->id);
				FavoritesFolderRecord::clearByUser($userRecord->id);
				QPageRecord::deletePagesByOwner($userRecord->id);
				UserLinkCartRecord::deleteLinksByUser($userRecord->id);
				ResetPasswordRecord::model()->deleteAll('LOWER(login)=?', array(strtolower($login)));
			}
		}

		/**
		 * @param $userId
		 * @return string[]
		 */
		public static function getGroupNames($userId)
		{
			$groups = array();
			$userGroupIds = UserGroupRecord::getGroupIdsByUser($userId);
			if (isset($userGroupIds))
				foreach ($userGroupIds as $groupId)
				{
					/** @var $groupRecord GroupRecord */
					$groupRecord = GroupRecord::model()->findByPk($groupId);
					if (isset($groupRecord))
						$groups[] = $groupRecord->name;
				}
			return $groups;
		}

	}