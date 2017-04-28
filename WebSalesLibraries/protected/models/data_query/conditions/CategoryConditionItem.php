<?
	namespace application\models\data_query\conditions;

	/**
	 * Class CategoryQueryItem
	 */
	class CategoryConditionItem
	{
		public $name;
		public $items;

		public function __construct()
		{
			$this->items = array();
		}
	}