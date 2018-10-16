<?

	namespace application\models\star_steals\models;

	class ItemListModel
	{
		public $id;
		public $title;
		public $order;

		/**
		 * @param $itemRecord \StarStealsItemRecord
		 * @return ItemListModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->title = $itemRecord->title;
			$model->order = $itemRecord->list_order;

			return $model;
		}
	}