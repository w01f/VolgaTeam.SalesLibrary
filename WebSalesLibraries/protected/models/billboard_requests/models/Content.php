<?

	namespace application\models\billboard_requests\models;

	class Content
	{
		public $submittedByUserId;
		public $advertiser;
		public $agency;
		public $length;
		public $property;
		public $details;

		public function __construct()
		{
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