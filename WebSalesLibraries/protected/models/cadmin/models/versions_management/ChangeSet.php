<?
	use application\models\wallbin\models\cadmin\entities\VersionedObject;

	/**
	 * Class ChangeSet
	 */
	class ChangeSet
	{
		const ChangeTypeAdd = 1;
		const ChangeTypeUpdate = 2;
		const ChangeTypeDelete = 3;

		public $changeType;
		/** @var  VersionedObject $changedObject */
		public $changedObject;
	}