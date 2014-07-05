<?php

	/**
	 * Class TickerLinkRecord
	 */
	class TickerLinkRecord extends CActiveRecord
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
			return '{{ticker_link}}';
		}

		/**
		 * @return TickerLink[]
		 */
		public static function getLinks()
		{
			$tickerLinks = array();
			$tickerLinkRecords = TickerLinkRecord::model()->findAll(array('order' => 'link_order'));
			foreach ($tickerLinkRecords as $tickerLinkRecord)
			{
				$tickerLink = new TickerLink();
				$tickerLink->id = $tickerLinkRecord->id;
				$tickerLink->type = $tickerLinkRecord->type;
				$tickerLink->order = $tickerLinkRecord->link_order;
				$tickerLink->text = $tickerLinkRecord->text;

				$tickerLinkDetailRecords = TickerLinkDetailRecord::model()->findAll('id_ticker=?', array($tickerLinkRecord->id));
				foreach ($tickerLinkDetailRecords as $tickerLinkDetailRecord)
				{
					$detail = new KeyValuePair();
					$detail->tag = $tickerLinkDetailRecord->tag;
					$detail->data = $tickerLinkDetailRecord->data;
					$tickerLink->details[] = $detail;
				}

				$tickerLinks[] = $tickerLink;
			}
			return $tickerLinks;
		}

		/**
		 *
		 */
		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}
