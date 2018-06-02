<?php

	/**
	 * Class ShortcutDataQueryCacheRecord
	 * @property string id
	 * @property string id_block
	 * @property mixed expire
	 * @property string value
	 */
	class ShortcutDataQueryCacheRecord extends CActiveRecord
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
			return '{{shortcut_data_query_cache}}';
		}

		/**
		 * @param string $cacheId
		 * @param boolean $ignoreExpirationDate
		 * @return string
		 */
		public static function getCachedData($cacheId, $ignoreExpirationDate)
		{
			/** @var \CDbCommand $dbCommand */
			$dbCommand = \Yii::app()->db->createCommand();
			$dbCommand = $dbCommand->from('tbl_shortcut_data_query_cache qcache');
			$dbCommand = $dbCommand->select(array(
				'value' => 'qcache.value as value',
			));

			$whereConditions = array(
				'AND',
				sprintf("qcache.id_block='%s'", $cacheId),
			);

			if (!$ignoreExpirationDate)
				$whereConditions[] = "qcache.expire is null or qcache.expire > curdate()";

			$dbCommand = $dbCommand->where($whereConditions);

			try
			{
				$resultRecords = $dbCommand->queryAll();
			}
			catch (\Exception $ex)
			{
				$resultRecords = array();
			}
			if (count($resultRecords) > 0)
				return $resultRecords[0]['value'];
			else
				return null;
		}

		/**
		 * @param string $cacheId
		 * @param string $encodedData
		 * @param string $expirationDate
		 */
		public static function setCachedData($cacheId, $encodedData, $expirationDate)
		{
			/** @var  $cacheRecord ShortcutDataQueryCacheRecord */
			$cacheRecord = self::model()->find("id_block=?", array($cacheId));
			if (!isset($cacheRecord))
			{
				$cacheRecord = new ShortcutDataQueryCacheRecord();
				$cacheRecord->id = uniqid();
				$cacheRecord->id_block = $cacheId;
			}
			$cacheRecord->expire = $expirationDate;
			$cacheRecord->value = $encodedData;
			$cacheRecord->save();
		}
	}