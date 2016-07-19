<?

	/**
	 * Class ChangeSetRecord
	 * @property string id
	 * @property string id_library
	 * @property string user
	 * @property string change_date
	 * @property int change_type
	 * @property int object_type
	 * @property string object_data
	 */
	class ChangeSetRecord extends CActiveRecord
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
			return '{{cadmin_changeset}}';
		}

		/**
		 * @return ChangeSet
		 */
		public function getModel()
		{
			$model = new ChangeSet();
			$model->changeType = $this->change_type;
			$model->changedObject = CJSON::decode($this->change_data, false);
			return $model;
		}

		/**
		 * @param ChangeSet $changeSetModel
		 * @param string $idLibrary
		 * @param string $userName
		 */
		public static function saveChangeSet($changeSetModel, $idLibrary, $userName)
		{
			$changeSetRecord = new ChangeSetRecord();
			$changeSetRecord->id = uniqid();
			$changeSetRecord->id_library = $idLibrary;
			$changeSetRecord->user = $userName;
			$changeSetRecord->change_date = date(Yii::app()->params['mysqlDateFormat'], strtotime($changeSetModel->changedObject->lastModified));
			$changeSetRecord->change_type = $changeSetModel->changeType;
			$changeSetRecord->object_type = $changeSetModel->changedObject->objectType;
			$changeSetRecord->object_data = CJSON::encode($changeSetModel->changedObject);
			$changeSetRecord->save();
		}
	}