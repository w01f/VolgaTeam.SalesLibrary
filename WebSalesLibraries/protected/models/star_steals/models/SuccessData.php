<?
	namespace application\models\star_steals\models;

	class SuccessData
	{
		public $why;
		public $what;

		/**
		 * @param  $properties array
		 * @return SuccessData
		 */
		public static function fromJson($properties)
		{
			$model = new self();
			foreach ($properties as $key => $value)
				$model->{$key} = $value;
			return $model;
		}
	}