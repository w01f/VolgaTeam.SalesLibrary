<?php
	class TickerLinkDetailStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{ticker_link_detail}}';
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}
