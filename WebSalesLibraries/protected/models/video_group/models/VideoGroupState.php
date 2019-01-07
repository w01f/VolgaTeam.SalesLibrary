<?
	namespace application\models\video_group\models;

	class VideoGroupState
	{
		/** @var VideoItemState[] */
		public $itemStates;

		public function __construct()
		{
			$this->itemStates = array();
		}
	}