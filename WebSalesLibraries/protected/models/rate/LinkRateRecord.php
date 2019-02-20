<?php

	/**
	 * Class LinkRateRecord
	 * @property mixed id
	 * @property string id_link
	 * @property int id_user
	 * @property float value
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
		 * @param $userId int|null
		 * @return float
		 */
		public function getRate($linkId, $userId)
		{
			if (isset($userId))
				$rate = Yii::app()->db->createCommand()
					->select('round(avg(value)*2)/2 as rate')
					->from('tbl_link_rate')
					->where("id_link = '" . $linkId . "' and id_user = " . $userId)
					->queryScalar();
			else
				$rate = Yii::app()->db->createCommand()
					->select('round(avg(value)*2)/2 as rate')
					->from('tbl_link_rate')
					->where("id_link = '" . $linkId . "'")
					->queryScalar();
			return isset($rate) ? $rate : 0;
		}

		/**
		 * @param $linkId string
		 * @param $userId int
		 * @param $value float
		 */
		public static function setRate($linkId, $userId, $value)
		{
			$record = self::model()->find('id_link=? and id_user=?', array($linkId, $userId));
			if (!isset($record))
			{
				$record = new LinkRateRecord();
				$record->id = uniqid();
				$record->id_link = $linkId;
				$record->id_user = $userId;
			}
			$record->value = $value;
			$record->save();
		}

		/**
		 * @param $userId int
		 */
		public static function clearByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}

		public static function clearByLinkIds()
		{
			Yii::app()->db->createCommand()->delete('tbl_link_rate', "id_link not in (select l.id from tbl_link l )");
		}

		/**
		 * @param $linkId string
		 * @param $userId int
		 * @return array
		 */
		public static function getRateData($linkId, $userId)
		{
			$totalRate = self::model()->getRate($linkId, null);
			$userRate = self::model()->getRate($linkId, $userId);
			$totalRateImage = self::getStarImage($totalRate);
			return array(
				'linkId' => $linkId,
				'linkName' => $linkId,
				'linkFile' => $linkId,
				'linkFormat' => $linkId,
				'totalRate' => $totalRate,
				'userRate' => $userRate,
				'totalRateImage' => $totalRateImage);
		}

		/**
		 * @param $rateValue float
		 * @return string
		 */
		public static function getStarImage($rateValue)
		{
			$starImagePath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'rate' . DIRECTORY_SEPARATOR . 'stars' . DIRECTORY_SEPARATOR . number_format($rateValue, 1, '.', '') . '.png';
			if (file_exists($starImagePath))
				$starImage = 'data:image/png;base64,' . base64_encode(file_get_contents($starImagePath));
			else
				$starImage = '';
			return $starImage;
		}
	}
