<?php

	/**
	 * Class MetaDataRecord
	 * @property mixed id
	 * @property string data_tag
	 * @property string data_content
	 */
	class MetaDataRecord extends CActiveRecord
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
			return '{{local_app_meta_data}}';
		}


		/**
		 * @param $tag string
		 * @param $propertyName string
		 * @param $propertyValue mixed
		 */
		public static function setData($tag, $propertyName, $propertyValue)
		{
			$record = self::model()->find('data_tag=?', array($tag));
			if (!isset($record))
			{
				$record = new MetaDataRecord();
				$record->id = uniqid();
				$record->data_tag = $tag;
			}
			$dataContent = $record->data_content;
			if (isset($dataContent))
				$dataArray = CJSON::decode($dataContent, true);
			else
				$dataArray = array();
			$dataArray[$propertyName] = $propertyValue;
			$record->data_content = CJSON::encode($dataArray);
			$record->save();
		}

		/**
		 * @param $tag string
		 * @param $propertyName string
		 * @return mixed
		 */
		public static function getData($tag, $propertyName)
		{
			/** @var  $record MetaDataRecord */
			$record = self::model()->find('data_tag=?', array($tag));
			if (!isset($record)) return null;
			$dataContent = $record->data_content;
			if (!isset($dataContent)) return null;
			$dataArray = CJSON::decode($dataContent, true);
			if (!array_key_exists($propertyName, $dataArray)) return null;
			return $dataArray[$propertyName];
		}
	}