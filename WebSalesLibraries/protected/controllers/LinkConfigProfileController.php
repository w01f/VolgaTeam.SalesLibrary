<?

	/**
	 * Class LinkConfigProfileController
	 */
	class LinkConfigProfileController extends LocalAppDataController
	{
		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'LinkConfigProfileModel' => 'LinkConfigProfileModel',
						'LinkProfileSettings' => 'LinkProfileSettings',
						'LibraryReference' => 'LibraryReference',
						'LibraryLinkReference' => 'LibraryLinkReference',
						'SecurityGroupReference' => 'SecurityGroupReference',
					),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @return LibraryReference[]
		 * @soap
		 */
		public function getLibraries($sessionKey)
		{
			$libraryReferences = array();
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (LibraryRecord::model()->findAll(array('order' => 'name')) as $libraryRecord)
				{
					/** @var LibraryRecord $libraryRecord */
					$libraryReference = new LibraryReference();
					$libraryReference->id = $libraryRecord->id;
					$libraryReference->name = $libraryRecord->name;
					$libraryReferences[] = $libraryReference;
				}
			}
			return $libraryReferences;
		}

		/**
		 * @param string $sessionKey
		 * @return SecurityGroupReference[]
		 * @soap
		 */
		public function getSecurityGroups($sessionKey)
		{
			$securityGroupReferences = array();
			if ($this->authenticateBySession($sessionKey))
			{
				foreach (GroupRecord::model()->findAll(array('order' => 'name')) as $groupRecord)
				{
					/** @var GroupRecord $groupRecord */
					$securityGroupReference = new SecurityGroupReference();
					$securityGroupReference->id = $groupRecord->id;
					$securityGroupReference->name = $groupRecord->name;
					$securityGroupReferences[] = $securityGroupReference;
				}
			}
			return $securityGroupReferences;
		}

		/**
		 * @param string $sessionKey
		 * @return LinkConfigProfileModel[]
		 * @soap
		 */
		public function getProfiles($sessionKey)
		{
			$profiles = array();
			if ($this->authenticateBySession($sessionKey))
				$profiles = LinkConfigProfileRecord::getProfiles();
			return $profiles;
		}

		/**
		 * @param string $sessionKey
		 * @param LinkConfigProfileModel $profile
		 * @soap
		 */
		public function saveProfile($sessionKey, $profile)
		{
			if ($this->authenticateBySession($sessionKey))
				LinkConfigProfileRecord::saveProfile($profile);
		}

		/**
		 * @param string $sessionKey
		 * @param LinkConfigProfileModel $profile
		 * @soap
		 */
		public function deleteProfile($sessionKey, $profile)
		{
			if ($this->authenticateBySession($sessionKey))
				LinkConfigProfileRecord::deleteProfile($profile);
		}

		/**
		 * @param string $sessionKey
		 * @param LinkConfigProfileModel $profile
		 * @return LibraryLinkReference[]
		 * @soap
		 */
		public function getAffectedLinks($sessionKey, $profile)
		{
			$affectedLinks = array();
			if ($this->authenticateBySession($sessionKey))
				$affectedLinks = $profile->config->getAffectedLinks();
			return $affectedLinks;
		}
	}