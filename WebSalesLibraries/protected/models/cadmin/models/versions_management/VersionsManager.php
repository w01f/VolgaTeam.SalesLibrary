<?
	use \application\models\wallbin\models\cadmin\entities\VersionedObject as VersionedObject;
	use \application\models\wallbin\models\cadmin\entities\Library as Library;
	use \application\models\wallbin\models\cadmin\entities\LibraryPage as LibraryPage;

	/**
	 * Class VersionsManager
	 */
	class VersionsManager
	{
		/**
		 * @param ChangesGetRequestData $changesRequestData
		 * @return RestResponse
		 */
		public static function getChanges($changesRequestData)
		{
			/** @var ChangeSetRecord[] $availableChangeSetRecords */
			$availableChangeSetRecords = ChangeSetRecord::model()->findAll('id_library=? and change_date>?', array(
				$changesRequestData->libraryId,
				date(Yii::app()->params['mysqlDateFormat'], strtotime($changesRequestData->lastUpdate))));

			$availableChangeSets = array();
			foreach ($availableChangeSetRecords as $changeSetRecord)
			{
				$changeSet = new ChangeSet();
				$changeSet->changeType = $changeSetRecord->change_type;
				$changeSet->changedObject = CJSON::decode($changeSetRecord->object_data, false);
				$availableChangeSets[] = $changeSet;
			}
			return RestResponse::success($availableChangeSets);
		}

		/**
		 * @param ChangesSetRequestData $changesRequestData
		 * @return RestResponse
		 */
		public static function applyChanges($changesRequestData)
		{
			foreach ($changesRequestData->pendingChanges as $changeSet)
			{
				/** @var ChangeSet $changeSet */
				switch ($changeSet->changedObject->objectType)
				{
					case VersionedObject::ObjectTypeLibrary:
						/** @var Library $library */
						$library = $changeSet->changedObject;
						LibraryRecord::updateDataFromChangeSet($library, $changeSet->changeType);
						break;
					case VersionedObject::ObjectTypeColumn:
						break;
					case VersionedObject::ObjectTypePage:
						/** @var LibraryPage $libraryPage */
						$libraryPage = $changeSet->changedObject;
						LibraryPageRecord::updateDataFromChangeSet($libraryPage, $changeSet->changeType);
						break;
				}
				ChangeSetRecord::saveChangeSet($changeSet, $changesRequestData->libraryId, $changesRequestData->user);
			}
			return RestResponse::success(null);
		}
	}