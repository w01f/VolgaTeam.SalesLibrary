<?php

	/**
	 * Class AutoWidgetRecord
	 * @property mixed id_library
	 * @property mixed extension
	 * @property mixed widget
	 */
	class AutoWidgetRecord extends CActiveRecord
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
			return '{{autowidget}}';
		}

		/**
		 * @param $autoWidget
		 */
		public static function updateData($autoWidget)
		{
			$autoWidgetRecord = new AutoWidgetRecord();
			$autoWidgetRecord->id_library = $autoWidget['libraryId'];
			$autoWidgetRecord->extension = $autoWidget['extension'];
			$autoWidgetRecord->widget = $autoWidget['widget'];
			$autoWidgetRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

	}

