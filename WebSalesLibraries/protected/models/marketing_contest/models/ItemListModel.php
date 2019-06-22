<?
	namespace application\models\marketing_contest\models;

	class ItemListModel
	{
		public $id;
		public $idOwner;
		public $title;
		public $filesCount;
		public $dateSubmit;

		/**
		 * @param $itemRecord \MarketingContestItemRecord
		 * @return ItemListModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->idOwner = $itemRecord->id_owner;
			$model->title = $itemRecord->title;
			$model->filesCount = \MarketingContestFileRecord::model()->getCount($itemRecord->id, 'attachment');
			if (isset($itemRecord->date_submit))
				$model->dateSubmit = $itemRecord->date_submit;

			return $model;
		}
	}