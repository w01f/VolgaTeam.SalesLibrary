<?php

	/**
	 * Class ShortcutsTabRecord
	 * @property string id
	 * @property mixed name
	 * @property mixed order
	 * @property mixed enabled
	 * @property mixed image_path
	 */
	class ShortcutsTabRecord extends CActiveRecord
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
			return '{{shortcut_tab}}';
		}

		public static function clearData()
		{
			ShortcutsLinkRecord::clearData();
			ShortcutsPageRecord::clearData();
			self::model()->deleteAll();
		}

	}
