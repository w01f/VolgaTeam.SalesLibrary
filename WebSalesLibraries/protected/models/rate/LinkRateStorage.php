<?php
	class LinkRateStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{link_rate}}';
		}

		public function getLinkRate($linkId)
		{
			return count($this->findAll('id_link=?', array($linkId)));
		}

		public function isRated($linkId, $userId)
		{
			return count($this->findAll('id_link=? and id_user=?', array($linkId, $userId))) > 0;
		}

		public static function addRate($linkId, $userId)
		{
			$record = new LinkRateStorage();
			$record->id = uniqid();
			$record->id_link = $linkId;
			$record->id_user = $userId;
			$record->save();
		}

		public static function clearByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}

		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_link_rate', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}
