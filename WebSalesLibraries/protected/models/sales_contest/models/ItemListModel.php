<?
	namespace application\models\sales_contest\models;

	class ItemListModel
	{
		public $id;
		public $idOwner;
		public $title;
		public $advertiser;
		public $revenue;
		public $dateSubmit;

		/**
		 * @param $itemRecord \SalesContestItemRecord
		 * @return ItemListModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->idOwner = $itemRecord->id_owner;
			$model->title = $itemRecord->title;
			$model->advertiser = $itemRecord->advertiser;
			$model->revenue = $itemRecord->revenue;
			if (isset($itemRecord->date_submit))
				$model->dateSubmit = $itemRecord->date_submit;

			return $model;
		}
	}