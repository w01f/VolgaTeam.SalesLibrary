<?

	namespace application\models\star_steals\models;

	class DetailsData
	{
		const DigitalPlatformNetwork = 'network';
		const DigitalPlatformSite = 'site';
		const DigitalPlatformNativeAdvertorial = 'native';
		const DigitalPlatformMobile = 'mobile';
		const DigitalPlatformGeoTargeting = 'geo-targeting';
		const DigitalPlatformOtt = 'ott';
		const DigitalPlatformAdvancedWeb = 'advanced-web';

		public $advertiserCategory;
		public $digitalRevenue;
		public $mediaRevenue;
		public $digitalPlatforms;

		public function __construct()
		{
			$this->digitalPlatforms = array();
		}

		/**
		 * @param  $properties array
		 * @return DetailsData
		 */
		public static function fromJson($properties)
		{
			$model = new self();
			foreach ($properties as $key => $value)
				$model->{$key} = $value;
			return $model;
		}
	}