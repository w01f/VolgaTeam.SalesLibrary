<?

	namespace application\models\video_group\models;

	class VideoItemState
	{
		public $index;
		public $fullyViewed;
		public $lastViewPosition;

		public function __construct()
		{
			$this->fullyViewed = false;
			$this->lastViewPosition = 0;
		}
	}