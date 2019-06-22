<?

	namespace application\models\marketing_contest\models;

	class Content
	{
		public $contactName;
		public $contactEmail;
		public $market;
		public $strategy;
		public $info;
		public $submittedByUserId;

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