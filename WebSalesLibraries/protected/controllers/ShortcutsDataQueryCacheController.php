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
						'BaseServiceProfile' => 'BaseServiceProfile',
						'ShortcutDataQueryCacheServiceProfile' => 'ShortcutDataQueryCacheServiceProfile',
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
		 * @return ShortcutDataQueryCacheServiceProfile[]
		 * @soap
		 */
		public function getProfiles($sessionKey)
		{
			$profiles = array();
			if ($this->authenticateBySession($sessionKey))
				$profiles = ShortcutServiceProfileRecord::getProfiles(BaseServiceProfile::ServiceTypeDataQueryCache);
			return $profiles;
		}

		/**
		 * @param string $sessionKey
		 * @param ShortcutDataQueryCacheServiceProfile $profile
		 * @soap
		 */
		public function saveProfile($sessionKey, $profile)
		{
			if ($this->authenticateBySession($sessionKey))
				ShortcutServiceProfileRecord::saveProfile($profile, BaseServiceProfile::ServiceTypeDataQueryCache);
		}

		/**
		 * @param string $sessionKey
		 * @param ShortcutDataQueryCacheServiceProfile $profile
		 * @soap
		 */
		public function deleteProfile($sessionKey, $profile)
		{
			if ($this->authenticateBySession($sessionKey))
				ShortcutServiceProfileRecord::deleteProfile($profile);
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
