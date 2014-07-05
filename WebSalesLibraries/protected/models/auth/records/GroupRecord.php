<?php

	/**
	 * Class GroupRecord
	 * @property mixed id
	 * @property mixed name
	 */
	class GroupRecord extends CActiveRecord
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
			return '{{group}}';
		}
	}