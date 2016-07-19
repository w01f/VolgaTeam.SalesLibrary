<?

	/**
	 * Class ConnectionInfoRecord
	 * @property string id
	 * @property string user
	 * @property string last_update
	 */
	class ConnectionInfoRecord extends CActiveRecord
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
			return '{{cadmin_connection_info}}';
		}

		/**
		 * @return ConnectionInfoModel
		 */
		private function getModel()
		{
			$model = new ConnectionInfoModel();
			$model->user = $this->user;
			$model->connectionTime = date(Yii::app()->params['outputDateFormat'], strtotime($this->last_update));
			return $model;
		}

		/**
		 * @param  $libraryId string
		 * @return ConnectionInfoModel
		 */
		private static function getConnectionInfo($libraryId)
		{
			/** @var  $connectionRecord ConnectionInfoRecord */
			$connectionRecord = self::model()->findByPk($libraryId);
			if (!isset($connectionRecord))
				return null;

			$connectionInfo = $connectionRecord->getModel();
			$connectionInfo->libraryId = $libraryId;
			return $connectionInfo;
		}

		/**
		 * @param  $userName string
		 * @param  $libraryName string
		 * @return ConnectionInfoModel
		 */
		public static function addConnection($userName, $libraryName)
		{
			/** @var  $libraryRecord LibraryRecord */
			$libraryRecord = LibraryRecord::model()->find('name=?', array($libraryName));
			if (!isset($libraryRecord))
				$libraryRecord = LibraryRecord::createEmptyLibrary($libraryName);

			$connectionInfo = self::getConnectionInfo($libraryRecord->id);
			if (isset($connectionInfo))
			{
				$connectionInfo->state = $connectionInfo->user == $userName ?
					ConnectionInfoModel::ConnectionStateActive :
					ConnectionInfoModel::ConnectionStateBusy;
			}
			else
			{
				$connectionRecord = new ConnectionInfoRecord();
				$connectionRecord->id = $libraryRecord->id;
				$connectionRecord->user = $userName;
				$connectionRecord->last_update = date(Yii::app()->params['mysqlDateFormat']);
				$connectionRecord->save();

				$connectionInfo = $connectionRecord->getModel();
				$connectionInfo->libraryId = $libraryRecord->id;
				$connectionInfo->state = ConnectionInfoModel::ConnectionStateActive;
			}
			$connectionInfo->libraryName = $libraryName;
			return $connectionInfo;

		}

		/**
		 * @param  $userName string
		 * @param  $libraryName string
		 */
		public static function deleteConnection($userName, $libraryName)
		{
			/** @var  $libraryRecord LibraryRecord */
			$libraryRecord = LibraryRecord::model()->find('name=?', array($libraryName));
			if (!isset($libraryRecord))
				return;
			/** @var  $connectionRecord ConnectionInfoRecord */
			self::model()->deleteAll('id=? and user=?', array($libraryRecord->id, $userName));
		}
	}