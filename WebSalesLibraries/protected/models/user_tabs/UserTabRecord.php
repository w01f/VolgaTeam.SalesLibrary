<?php

	/**
	 * Class UserTabRecord
	 * @property mixed id
	 * @property int id_user
	 * @property string id_object
	 * @property int object_type
	 * @property int order
	 */
	class UserTabRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return UserTabRecord
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
			return '{{user_tab}}';
		}

		/**
		 * @param $userId int
		 * @return UserTabModel[]
		 */
		public static function getLibraryTabs($userId)
		{
			$libraryTabs = array();
			$records = self::model()->findAll(array('order' => "'order'", 'condition' => 'id_user=:id_user and object_type=1', 'params' => array(':id_user' => $userId)));
			foreach ($records as $record)
			{
				$libraryTab = new LibraryTab($record);
				$libraryTabs[] = $libraryTab;
			}
			return $libraryTabs;
		}

		/**
		 * @param $userId int
		 * @param $objectId string
		 * @return boolean
		 */
		public static function isUserTabExists($userId, $objectId)
		{
			return self::model()->count('id_user=? and id_object=?', array($userId, $objectId)) > 0;
		}

		/**
		 * @param $userId int
		 * @param $libraryId string
		 */
		public static function addLibraryTab($userId, $libraryId)
		{
			$userTabRecord = new UserTabRecord();
			$userTabRecord->id = uniqid();
			$userTabRecord->id_user = $userId;
			$userTabRecord->id_object = $libraryId;
			$userTabRecord->object_type = 1;
			$userTabRecord->order = self::getMaxTabOrder($userId, 1) + 1;
			$userTabRecord->save();
		}

		/**
		 * @param $userId int
		 * @param $objectId string
		 */
		public static function deleteTab($userId, $objectId)
		{
			self::model()->deleteAll('id_user=? and id_object=?', array($userId, $objectId));
		}

		/**
		 * @param $userId int
		 * @param $objectType int
		 * @return int
		 */
		private static function getMaxTabOrder($userId, $objectType)
		{
			$criteria = new CDbCriteria;
			$criteria->select = "max('order') as maxOrder";
			$criteria->condition = 't.id_user =:userId and t.object_type = :objectType';
			$criteria->params = array(':userId' => $userId, ':objectType' => $objectType);
			$row = self::model()->find($criteria);
			return isset($row['maxOrder']) ? $row['maxOrder'] : 0;
		}
	}
