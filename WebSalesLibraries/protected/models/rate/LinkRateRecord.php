<?php

	/**
	 * Class LinkRateRecord
	 * @property mixed id
	 * @property string id_link
	 * @property int id_user
	 */
	class LinkRateRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return LinkRateRecord
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
			return '{{link_rate}}';
		}

		/**
		 * @param $linkId
		 * @return int
		 */
		public function getLinkRate($linkId)
		{
			return count($this->findAll('id_link=?', array($linkId)));
		}

		/**
		 * @param $linkId
		 * @param $userId
		 * @return bool
		 */
		public function isRated($linkId, $userId)
		{
			return count($this->findAll('id_link=? and id_user=?', array($linkId, $userId))) > 0;
		}

		/**
		 * @param $linkId string
		 * @param $userId int
		 */
		public static function addRate($linkId, $userId)
		{
			$record = new LinkRateRecord();
			$record->id = uniqid();
			$record->id_link = $linkId;
			$record->id_user = $userId;
			$record->save();
		}

		/**
		 * @param $linkId
		 * @param $userId
		 */
		public static function deleteRate($linkId, $userId)
		{
			self::model()->deleteAll('id_user=? and id_link=?', array($userId, $linkId));
		}

		/**
		 * @param $userId
		 */
		public static function clearByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}

		/**
		 * @param $liveLinkIds
		 */
		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_link_rate', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}
