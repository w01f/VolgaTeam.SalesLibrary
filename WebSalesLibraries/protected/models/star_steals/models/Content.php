<?

	namespace application\models\star_steals\models;

	class Content
	{
		const SaleTypeStar = 'star';
		const SaleTypeSteal = 'steal';
		const SaleTypeIdea = 'idea';
		const SaleTypeWeekStar = 'week-star';

		const RevenueTypeNew = 'new';
		const RevenueTypeIncremental = 'incremental';

		public $revenue;
		public $client;
		public $seller;
		public $closedDate;
		public $market;
		public $station;
		public $saleType;
		public $revenueType;
		public $teamMates;

		/** @var DetailsData */
		public $details;

		/** @var SuccessData */
		public $success;

		public function __construct()
		{
			$this->saleType = self::SaleTypeStar;
			$this->revenueType = self::RevenueTypeNew;
			$this->closedDate = date("d/m/y");
			$this->teamMates = array();

			$this->details = new DetailsData();
			$this->success = new SuccessData();
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
						case 'details':
							$model->{$key} = DetailsData::fromJson($value);
							break;
						case 'success':
							$model->{$key} = SuccessData::fromJson($value);
							break;
						default:
							$model->{$key} = $value;
							break;
					}
				}
				else
					$model->{$key} = $value;
			}

			return $model;
		}
	}