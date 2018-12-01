<?

	namespace application\models\sales_requests\models;

	class Content
	{
		public $submittedByUserId;
		public $advertiser;
		public $agency;
		public $category;
		public $meetingWith;
		public $demos;
		public $requestReason;
		public $details;

		public function __construct()
		{
			$this->requestReason = array();
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