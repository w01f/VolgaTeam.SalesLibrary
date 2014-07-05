<?php

	/**
	 * Class MainMenuLinkRecord
	 * @property string id
	 * @property string id_object
	 * @property string type
	 * @property int order
	 * @property string name
	 * @property string image_path
	 * @property string config
	 */
	class MainMenuLinkRecord extends CActiveRecord
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
			return '{{main_menu_link}}';
		}
	}