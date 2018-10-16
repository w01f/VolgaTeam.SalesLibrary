<?

	namespace application\models\star_steals\models;

	class ItemEditModel
	{
		public $id;
		public $title;
		public $order;
		public $createDate;

		/** @var Content */
		public $content;

		public function __construct()
		{
			$this->content = new Content();
		}

		/**
		 * @param $itemRecord \StarStealsItemRecord
		 * @return ItemEditModel
		 */
		public static function fromRecord($itemRecord)
		{
			$model = new self();

			$model->id = $itemRecord->id;
			$model->title = $itemRecord->title;
			$model->order = $itemRecord->list_order;
			$model->createDate = date(\Yii::app()->params['outputDateFormat'], strtotime($itemRecord->create_date)) . ' ' . date(\Yii::app()->params['outputTimeFormat'], strtotime($itemRecord->create_date));

			if (!empty($itemRecord->content))
				$model->content = Content::fromJson(\CJSON::decode($itemRecord->content, true));

			return $model;
		}
	}