<?
	namespace application\models\services_data\common\dictionaries;

	/**
	 * Class LibraryPage
	 */
	class LibraryPage
	{
		public $id;
		public $name;
		public $order;

		/** @var  LibraryFolder[] */
		public $folders;
	}