<?

	namespace application\models\marketing_contest\models;


	class ItemEditModel
	{
		public $id;
		public $title;
		public $createDate;
		public $dateSubmit;

		public $allowEdit;

		/** @var Content */
		public $content;

		/** @var FileModel[] */
		public $attachments;

		public function __construct()
		{
			$this->content = new Content();
			$this->attachments = array();
		}

		/**
		 * @param $itemRecord \MarketingContestItemRecord
		 * @return ItemEditModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->title = $itemRecord->title;
			$model->createDate = date(\Yii::app()->params['outputDateFormat'], strtotime($itemRecord->create_date)) . ' ' . date(\Yii::app()->params['outputTimeFormat'], strtotime($itemRecord->create_date));
			if (isset($itemRecord->date_submit))
				$model->dateSubmit = $itemRecord->date_submit;

			$userId = \UserIdentity::getCurrentUserId();
			$model->allowEdit = $userId == $itemRecord->id_owner;

			if (!empty($itemRecord->content))
				$model->content = Content::fromJson(\CJSON::decode($itemRecord->content, true));

			$model->attachments = \MarketingContestFileRecord::model()->getModels($itemRecord->id, 'attachment');

			return $model;
		}
	}