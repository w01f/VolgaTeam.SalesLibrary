<?
	namespace application\models\wallbin\models\cadmin\entities;
	/**
	 * Class VersionedObject
	 */
	class VersionedObject
	{
		const ObjectTypeLibrary = 1;
		const ObjectTypeColumn = 2;
		const ObjectTypePage = 3;
		const ObjectTypeFolder = 4;
		const ObjectTypeLink = 5;
		const ObjectTypePreviewContainer = 6;

		public $id;
		public $objectType;
		public $lastModified;
	}