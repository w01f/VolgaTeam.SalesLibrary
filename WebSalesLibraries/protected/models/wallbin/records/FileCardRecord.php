<?php

	/**
	 * Class FileCardRecord
	 * @property mixed id
	 * @property mixed id_library
	 * @property mixed title
	 * @property mixed advertiser
	 * @property mixed date_sold
	 * @property mixed broadcast_closed
	 * @property mixed digital_closed
	 * @property mixed publishing_closed
	 * @property mixed sales_name
	 * @property mixed sales_email
	 * @property mixed sales_phone
	 * @property mixed sales_station
	 * @property mixed notes
	 */
	class FileCardRecord extends CActiveRecord
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
			return '{{file_card}}';
		}

		/**
		 * @param $fileCard
		 */
		public static function updateData($fileCard)
		{
			$existedItem = self::model()->findByPk($fileCard['id']);
			if (isset($existedItem))
				return;
			$fileCardRecord = new FileCardRecord();
			$fileCardRecord->id = $fileCard['id'];
			$fileCardRecord->id_library = $fileCard['libraryId'];
			$fileCardRecord->title = $fileCard['title'];
			$fileCardRecord->advertiser = $fileCard['advertiser'];
			$fileCardRecord->date_sold = $fileCard['dateSold'] != null ? date(Yii::app()->params['mysqlDateFormat'], strtotime($fileCard['dateSold'])) : null;
			$fileCardRecord->broadcast_closed = $fileCard['broadcastClosed'] > 0 ? $fileCard['broadcastClosed'] : null;
			$fileCardRecord->digital_closed = $fileCard['digitalClosed'] > 0 ? $fileCard['digitalClosed'] : null;
			$fileCardRecord->publishing_closed = $fileCard['publishingClosed'] > 0 ? $fileCard['publishingClosed'] : null;
			$fileCardRecord->sales_name = $fileCard['salesName'];
			$fileCardRecord->sales_email = $fileCard['salesEmail'];
			$fileCardRecord->sales_phone = $fileCard['salesPhone'];
			$fileCardRecord->sales_station = $fileCard['salesStation'];

			if (array_key_exists('notes', $fileCard))
				if (isset($fileCard['notes']))
					$fileCardRecord->notes = CJSON::encode($fileCard['notes']);

			$fileCardRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

	}
