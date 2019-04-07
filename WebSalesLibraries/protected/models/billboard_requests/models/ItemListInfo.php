<?

	namespace application\models\billboard_requests\models;

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