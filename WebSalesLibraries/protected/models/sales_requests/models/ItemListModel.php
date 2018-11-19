<?
	namespace application\models\sales_requests\models;

	class ItemListModel
	{
		public $id;
		public $idOwner;
		public $title;
		public $status;
		public $assignedTo;
		public $dateNeeded;
		public $dateCompleted;

		/**
		 * @param $itemRecord \SalesRequestItemRecord
		 * @return ItemListModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->idOwner = $itemRecord->id_owner;
			$model->title = $itemRecord->title;
			$model->status = $itemRecord->status;
			$model->assignedTo = $itemRecord->assigned_to;
			$model->dateNeeded = $itemRecord->date_needed;
			if (isset($itemRecord->date_completed))
				$model->dateCompleted = $itemRecord->date_completed;

			return $model;
		}
	}