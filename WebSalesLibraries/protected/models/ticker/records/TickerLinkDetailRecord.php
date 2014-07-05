<?php

	/**
	 * Class TickerLinkDetailRecord
	 */
	class TickerLinkDetailRecord extends CActiveRecord
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
			return '{{ticker_link_detail}}';
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}
