<?
	namespace application\models\sales_ideas\models;

	class ItemListModel
	{
		public $id;
		public $idOwner;
		public $title;
		public $advertiser;
		public $filesCount;
		public $revenue;
		public $dateSubmit;

		/**
		 * @param $itemRecord \SalesIdeaItemRecord
		 * @return ItemListModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->idOwner = $itemRecord->id_owner;
			$model->title = $itemRecord->title;
			$model->advertiser = $itemRecord->advertiser;
			$model->filesCount = \SalesContestFileRecord::model()->getCount($itemRecord->id, 'attachment');
			$model->revenue = $itemRecord->revenue;
			if (isset($itemRecord->date_submit))
				$model->dateSubmit = $itemRecord->date_submit;

			return $model;
		}
	}