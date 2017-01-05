<?
	namespace application\models\services_data\common\dictionaries;

	/**
	 * Class LibraryFolder
	 */
	class LibraryFolder
	{
		public $id;
		public $name;
		public $order;

		/** @var  LibraryLink[] */
		public $links;
	}