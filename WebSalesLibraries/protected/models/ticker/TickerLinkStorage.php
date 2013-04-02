<?php
	class TickerLinkStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{ticker_link}}';
		}

		public static function getLinks()
		{
			$tickerLinkRecords = TickerLinkStorage::model()->findAll(array('order'=>'link_order'));
			foreach ($tickerLinkRecords as $tickerLinkRecord)
			{
				$tickerLink = new TickerLink();
				$tickerLink->id = $tickerLinkRecord->id;
				$tickerLink->type = $tickerLinkRecord->type;
				$tickerLink->order = $tickerLinkRecord->link_order;
				$tickerLink->text = $tickerLinkRecord->text;

				$tickerLinkDetailRecords = TickerLinkDetailStorage::model()->findAll('id_ticker=?', array($tickerLinkRecord->id));
				foreach ($tickerLinkDetailRecords as $tickerLinkDetailRecord)
				{
					$detail = new KeyValuePair();
					$detail->tag = $tickerLinkDetailRecord->tag;
					$detail->data = $tickerLinkDetailRecord->data;
					$tickerLink->details[] = $detail;
				}

				$tickerLinks[] = $tickerLink;
			}
			if (isset($tickerLinks))
				return $tickerLinks;
			else
				return null;
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}
