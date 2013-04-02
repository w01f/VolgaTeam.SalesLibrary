<?php
	class TickerController extends IsdController
	{
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'TickerLink' => 'TickerLink',
						'KeyValuePair' => 'KeyValuePair',
					),
				),
			);
		}

		protected function authenticateBySession($sessionKey)
		{
			$data = Yii::app()->cacheDB->get($sessionKey);
			if ($data !== FALSE)
				return TRUE;
			else
				return FALSE;
		}

		/**
		 * @param string $login
		 * @param string $password
		 * @return string session key
		 * @soap
		 */
		public function getSessionKey($login, $password)
		{
			$identity = new UserIdentity($login, $password);
			$identity->authenticate();
			if ($identity->errorCode === UserIdentity::ERROR_NONE)
			{
				$sessionKey = strval(md5(mt_rand()));
				Yii::app()->cacheDB->set($sessionKey, $login, (60 * 60 * 24 * 7));
				return $sessionKey;
			}
			else
				return '';
		}

		/**
		 * @param string Session Key
		 * @return TickerLink[]
		 * @soap
		 */
		public function getTickerLinks($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
				$tickerLinks = TickerLinkStorage::getLinks();
			if (isset($tickerLinks))
				return $tickerLinks;
			else
				return null;
		}

		/**
		 * @param string Session Key
		 * @param TickerLink[] Ticker Links
		 * @soap
		 */
		public function setTickerLinks($sessionKey, $tickerLinks)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				TickerLinkStorage::clearData();
				TickerLinkDetailStorage::clearData();
				foreach ($tickerLinks as $tickerLink)
				{
					$tickerLinkRecord = new TickerLinkStorage();
					$tickerLinkId = uniqid();
					$tickerLinkRecord->id = $tickerLinkId;
					$tickerLinkRecord->type = $tickerLink->type;
					$tickerLinkRecord->link_order = $tickerLink->order;
					$tickerLinkRecord->text = $tickerLink->text;
					$tickerLinkRecord->save();

					if (isset($tickerLink->details))
						foreach ($tickerLink->details as $tickerLinkDetail)
						{
							$tickerLinkDetailRecord = new TickerLinkDetailStorage();
							$tickerLinkDetailRecord->id = uniqid();
							$tickerLinkDetailRecord->id_ticker = $tickerLinkId;
							$tickerLinkDetailRecord->tag = $tickerLinkDetail->tag;
							$tickerLinkDetailRecord->data = $tickerLinkDetail->data;
							$tickerLinkDetailRecord->save();
						}
				}
			}
		}
	}
