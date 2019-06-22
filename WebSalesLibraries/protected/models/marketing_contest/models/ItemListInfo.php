<?

	namespace application\models\marketing_contest\models;

	class ItemListInfo
	{
		public $ownItemsCount;
		public $allItemsCount;
		public $archiveItemsCount;

		public function __construct()
		{
			$this->ownItemsCount = 0;
			$this->allItemsCount = 0;
			$this->archiveItemsCount = 0;
		}
	}