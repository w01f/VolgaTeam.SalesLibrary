<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class LinkConfig
	 */
	class LinkConfig
	{
		/**
		 * @var boolean disablePreview
		 * @soap
		 */
		public $disablePreview;
		/**
		 * @var boolean disableDownload
		 * @soap
		 */
		public $disableDownload;
		/**
		 * @var boolean disableQuickSite
		 * @soap
		 */
		public $disableQuickSite;
		/**
		 * @var boolean disableFavorites
		 * @soap
		 */
		public $disableFavorites;
		/**
		 * @var boolean disableSave
		 * @soap
		 */
		public $disableSave;
		/**
		 * @var boolean disableEmail
		 * @soap
		 */
		public $disableEmail;
		/**
		 * @var boolean disablePdf
		 * @soap
		 */
		public $disablePdf;
		/**
		 * @var string[] libraryLinkTags
		 * @soap
		 */
		public $libraryLinkTags;
		/**
		 * @var LibraryReference[] libraryReferences
		 * @soap
		 */
		public $libraryReferences;
		/**
		 * @var SecurityGroupReference[] securityGroupReferences
		 * @soap
		 */
		public $securityGroupReferences;
		/**
		 * @var LibraryLinkReference[] ignoredLinkReferences
		 * @soap
		 */
		public $ignoredLinkReferences;

		public function __construct(Array $properties = array())
		{
			foreach ($properties as $key => $value)
			{
				if (is_array($value))
					$this->{$key} = CJSON::decode(CJSON::encode($value), false);
				else
					$this->{$key} = $value;
			}
		}

		/**
		 * @param  $libraryLink LibraryLink
		 * @return boolean
		 */
		public function isAffectToLibraryLink($libraryLink)
		{
			$isIgnored = false;
			foreach ($this->ignoredLinkReferences as $linkReference)
			{
				$isIgnored = $linkReference->id == $libraryLink->id;
				if ($isIgnored)
					break;
			}
			if ($isIgnored)
				return false;

			$isAffected = false;
			$userGroups = UserIdentity::getCurrentUserGroups();
			foreach ($this->securityGroupReferences as $securityGroupReference)
			{
				if (in_array($securityGroupReference->name, $userGroups))
				{
					$isAffected = true;
					break;
				}
			}

			if ($isAffected)
			{
				$isAffected = false;
				$affectedLinks = $this->getAffectedLinks();
				foreach ($affectedLinks as $linkReference)
				{
					if ($linkReference->id == $libraryLink->id)
					{
						$isAffected = true;
						break;
					}
				}
			}
			return $isAffected;
		}

		/**
		 * @return LibraryLinkReference[]
		 */
		public function getAffectedLinks()
		{
			$searchConditions = new SearchConditions();
			$searchConditions->text = $this->libraryLinkTags;
			$searchConditions->textExactMatch = true;
			$searchConditions->libraries = isset($this->libraryReferences) ? $this->libraryReferences : array();

			$queryRecords = SearchHelper::getDatasetByCondition($searchConditions, uniqid());

			$affectedLinkReferences = array();
			foreach ($queryRecords as $queryRecord)
			{
				$linkReference = new LibraryLinkReference();
				$linkReference->id = $queryRecord['id'];
				$linkReference->name = $queryRecord['name'];
				$linkReference->fileName = $queryRecord['file_name'];
				$linkReference->filePath = $queryRecord['path'];
				$linkReference->libraryName = $queryRecord['lib_name'];
				$affectedLinkReferences[] = $linkReference;
			}
			return $affectedLinkReferences;
		}
	}