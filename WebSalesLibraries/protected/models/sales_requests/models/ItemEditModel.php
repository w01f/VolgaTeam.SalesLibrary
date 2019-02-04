<?

	namespace application\models\sales_requests\models;


	use SalesRequestFileRecord;

	class ItemEditModel
	{
		public $id;
		public $title;
		public $status;
		public $assignedTo;
		public $createDate;
		public $dateSubmit;
		public $dateNeeded;
		public $dateCompleted;

		public $allowEdit;

		/** @var Content */
		public $content;

		/** @var FileModel[] */
		public $attachments;

		/** @var FileModel[] */
		public $deliverables;

		public function __construct()
		{
			$this->content = new Content();
			$this->attachments = array();
		}

		/**
		 * @param $itemRecord \SalesRequestItemRecord
		 * @return ItemEditModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->title = $itemRecord->title;
			$model->status = $itemRecord->status;
			$model->assignedTo = $itemRecord->assigned_to;
			$model->createDate = date(\Yii::app()->params['outputDateFormat'], strtotime($itemRecord->create_date)) . ' ' . date(\Yii::app()->params['outputTimeFormat'], strtotime($itemRecord->create_date));
			if (isset($itemRecord->date_submit))
				$model->dateSubmit = $itemRecord->date_submit;
			$model->dateNeeded = $itemRecord->date_needed;
			if (isset($itemRecord->date_completed))
				$model->dateCompleted = $itemRecord->date_completed;

			$userId = \UserIdentity::getCurrentUserId();
			$model->allowEdit = $userId == $itemRecord->id_owner;

			if (!empty($itemRecord->content))
				$model->content = Content::fromJson(\CJSON::decode($itemRecord->content, true));

			$model->attachments = SalesRequestFileRecord::model()->getModels($itemRecord->id, 'attachment');
			$model->deliverables = SalesRequestFileRecord::model()->getModels($itemRecord->id, 'deliverable');

			return $model;
		}
	}