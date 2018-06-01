<?php

	/**
	 * Class ShortcutsDataQueryCacheController
	 */
	class ShortcutsDataQueryCacheController extends SoapController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'SoapShortcutModel' => 'SoapShortcutModel',
					),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @return SoapShortcutModel[]
		 * @soap
		 */
		public function getLandingPages($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$models = array();
				/** @var ShortcutLinkRecord[] $landingPageShortcutRecords */
				$landingPageShortcutRecords = ShortcutLinkRecord::model()->findAll('type=?', array('landing'));
				foreach ($landingPageShortcutRecords as $linkRecord)
					$models[] = SoapShortcutModel::fromLinkRecord($linkRecord);
				return $models;
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $landingPageId
		 * @soap
		 */
		public function resetDataQueryCache($sessionKey, $landingPageId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var ShortcutLinkRecord $landingPageRecord */
				$landingPageRecord = ShortcutLinkRecord::model()->findByPk($landingPageId);
				if (isset($landingPageRecord))
				{
					/** @var LandingPageShortcut $landingPageShortcut */
					$landingPageShortcut = $landingPageRecord->getServiceModel();
					$landingPageShortcut->prepareDataQueryCache(true);
				}
			}
		}
	}
