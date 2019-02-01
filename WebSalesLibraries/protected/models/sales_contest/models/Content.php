<?

	namespace application\models\sales_contest\models;

	class Content
	{
		const NominationTypeShared = 'shared';
		const NominationTypeOriginal = 'original';
		const NominationTypeInitiative = 'initiative';

		const RevenueTypeNew = 'new';
		const RevenueTypeIncremental = 'incremental';

		const DigitalPlatformNetwork = 'network';
		const DigitalPlatformSite = 'site';
		const DigitalPlatformNativeAdvertorial = 'native';
		const DigitalPlatformMobile = 'mobile';
		const DigitalPlatformGeoTargeting = 'geo-targeting';
		const DigitalPlatformOtt = 'ott';
		const DigitalPlatformAdvancedWeb = 'advanced-web';

		public $nominationType;
		public $seller;
		public $market;
		public $revenueType;
		public $category;
		public $digitalRevenue;
		public $mediaRevenue;
		public $productionRevenue;
		public $otherRevenue;
		public $platforms;
		public $teamMembers;
		public $successStory;
		public $milestones;
		public $submittedByUserId;

		public function __construct()
		{
			$this->nominationType = self::NominationTypeShared;
			$this->revenueType = self::RevenueTypeNew;
			$this->platforms = array();
		}

		/**
		 * @param  $properties array
		 * @return Content
		 */
		public static function fromJson($properties)
		{
			$model = new self();
			foreach ($properties as $key => $value)
			{
				if (is_array($value))
				{
					switch ($key)
					{
						default:
							$model->{$key} = $value;
							break;
					}
				}
				else
				{
					switch ($key)
					{
						default:
							$model->{$key} = $value;
							break;
					}
				}
			}

			return $model;
		}
	}