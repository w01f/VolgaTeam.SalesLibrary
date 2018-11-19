<?

	/**
	 * Class SalesRequestFileRecord
	 * @property mixed id
	 * @property mixed id_item
	 * @property mixed list_order
	 * @property string file_name
	 * @property mixed file_type
	 * @property mixed file_format
	 * @property mixed upload_date
	 * @property mixed content
	 */
	class SalesRequestFileRecord extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{sales_request_file}}';
		}

		/**
		 * @param $itemId
		 * @param $fileType
		 * @return \application\models\sales_requests\models\FileModel[]
		 */
		public function getModels($itemId, $fileType)
		{
			$models = array();

			$criteria = new CDbCriteria();
			$criteria->addCondition("id_item='" . $itemId . "'");
			$criteria->addCondition("file_type='" . $fileType . "'");
			$criteria->order = 'list_order';

			$fileRecords = $this->findAll($criteria);
			foreach ($fileRecords as $fileRecord)
				$models[] = \application\models\sales_requests\models\FileModel::fromRecord($fileRecord);
			return $models;
		}

		/**
		 * @param $fileId
		 * @return \application\models\sales_requests\models\FileModel
		 */
		public function getModel($fileId)
		{
			$itemRecord = $this->findByPk($fileId);
			if (isset($itemRecord))
				return \application\models\sales_requests\models\FileModel::fromRecord($itemRecord);
			return null;
		}

		/**
		 * @param $fileId
		 * @return mixed
		 */
		public function getFileContent($fileId)
		{
			$itemRecord = $this->findByPk($fileId);
			if (isset($itemRecord))
				return $itemRecord->content;
			return null;
		}

		/**
		 * @param $itemId string
		 * @param $fileName string
		 * @param $fileType string
		 * @param $tempPath string
		 */
		public static function addItem($itemId, $fileName, $fileType, $tempPath)
		{
			$fileRecord = new self();

			$fileRecord->id = uniqid();
			$fileRecord->id_item = $itemId;
			$fileRecord->list_order = self::getMaxItemIndex($itemId, $fileType) + 1;
			$fileRecord->file_name = $fileName;
			$fileRecord->file_type = $fileType;
			$fileRecord->file_format = pathinfo($fileName, PATHINFO_EXTENSION);
			$fileRecord->upload_date = date(Yii::app()->params['mysqlDateTimeFormat']);
			$fileRecord->content = @file_get_contents($tempPath);

			$fileRecord->save();
		}

		/**
		 * @param $itemId
		 * @param $sourceItemId
		 * @param $sourceItemId
		 */
		public static function cloneFiles($itemId, $sourceItemId)
		{
			$criteria = new CDbCriteria();
			$criteria->addCondition("id_item='" . $sourceItemId . "'");
			/** @var $sourceFileRecords SalesRequestFileRecord[] */
			$sourceFileRecords = self::model()->findAll($criteria);
			foreach ($sourceFileRecords as $sourceFileRecord)
			{
				$fileRecord = new self();

				$fileRecord->id = uniqid();
				$fileRecord->id_item = $itemId;
				$fileRecord->list_order = $sourceFileRecord->list_order;
				$fileRecord->file_name = $sourceFileRecord->file_name;
				$fileRecord->file_type = $sourceFileRecord->file_type;
				$fileRecord->file_format = $sourceFileRecord->file_format;
				$fileRecord->upload_date = date(Yii::app()->params['mysqlDateTimeFormat']);
				$fileRecord->content = $sourceFileRecord->content;

				$fileRecord->save();
			}
		}

		/**
		 * @param $fileId
		 */
		public static function deleteFile($fileId)
		{
			/** @var $fileRecord SalesRequestFileRecord */
			$fileRecord = self::model()->findByPk($fileId);
			$itemId = $fileRecord->id_item;
			$fileType = $fileRecord->file_type;
			self::model()->deleteByPk($fileId);
			self::rebuildItemList($itemId, $fileType, -1);
		}

		/**
		 * @param $itemId
		 */
		public static function deleteFilesByItem($itemId)
		{
			/** @var SalesRequestFileRecord $fileRecords */
			$fileRecords = self::model()->findAll('id_item=?', array($itemId));
			foreach ($fileRecords as $fileRecord)
				self::deleteFile($fileRecord->id);
		}

		/**
		 * @param $itemId
		 * @param $fileType
		 * @param $shiftIndex
		 */
		private static function rebuildItemList($itemId, $fileType, $shiftIndex)
		{
			$i = 0;
			foreach (self::model()->findAll('id_item=? and file_type=? order by list_order', array($itemId, $fileType)) as $fileRecord)
			{
				if ($i == $shiftIndex)
					$i++;
				/** @var $fileRecord SalesRequestFileRecord */
				$fileRecord->list_order = $i;
				$fileRecord->save();
				$i++;
			}
		}

		/**
		 * @param $itemId
		 * @param $fileType
		 * @return int
		 */
		private static function getMaxItemIndex($itemId, $fileType)
		{
			return Yii::app()->db->createCommand()
				->select('max(list_order)')
				->from('tbl_sales_request_file')
				->where("id_item='" . $itemId . "' and file_type='" . $fileType . "'")
				->queryScalar();
		}
	}