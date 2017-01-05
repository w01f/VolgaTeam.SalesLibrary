<?
	namespace application\models\services_data\cadmin\models\versions_management;

	/**
	 * Class ChangesSetRequestData
	 */
	class ChangesSetRequestData
	{
		public $libraryId;
		public $user;
		/** @var  ChangeSet[] $pendingChanges */
		public $pendingChanges;
	}