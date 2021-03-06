<?php
	use application\models\data_query\data_table\DataTableQueryHelper;
	use application\models\data_query\data_table\DataTableQuerySettings;
	use application\models\data_query\data_table\DataTableFormatHelper;

	/**
	 * Class FavoritesLinkRecord
	 * @property mixed id
	 * @property mixed id_link
	 * @property mixed id_library
	 * @property mixed id_user
	 * @property mixed id_folder
	 * @property mixed name
	 */
	class FavoritesLinkRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{favorites_link}}';
		}

		/**
		 * @param $userId
		 * @param $linkId
		 * @param $linkName
		 * @param $folderName
		 * @param $libraryId
		 */
		public static function addLink($userId, $linkId, $linkName, $folderName, $libraryId)
		{
			/** @var $linkRecord FavoritesLinkRecord */
			if (isset($folderName))
			{
				$folderRecord = FavoritesFolderRecord::getFolderByName($userId, $folderName);
				$linkRecord = self::model()->find('id_user=? and id_link=? and id_folder=? and LOWER(name)=?', array($userId, $linkId, $folderRecord->id, strtolower($linkName)));
			}
			else
				$linkRecord = self::model()->find('id_user=? and id_link=? and id_folder is null and LOWER(name)=?', array($userId, $linkId, strtolower($linkName)));
			if (!isset($linkRecord))
			{
				$linkRecord = new FavoritesLinkRecord();
				$linkRecord->id = uniqid();
				$linkRecord->id_link = $linkId;
				$linkRecord->id_library = $libraryId;
				$linkRecord->id_folder = isset($folderRecord) ? $folderRecord->id : null;
				$linkRecord->id_user = $userId;
			}
			$linkRecord->name = $linkName;
			$linkRecord->save();
		}

		/**
		 * @param $userId
		 * @param $folderId
		 * @param $columnSettings
		 * @return array
		 */
		public static function getLinksByFolder($userId, $folderId, $columnSettings)
		{
			$querySettings = DataTableQuerySettings::prepareQuery(
				array(
					DataTableQuerySettings::SettingsTagFrom => 'tbl_favorites_link flink',
					DataTableQuerySettings::SettingsTagQueryFields => array('name' => 'flink.name as name'),
					DataTableQuerySettings::SettingsTagInnerJoin => array('tbl_link link' => 'flink.id_link=link.id'),
					DataTableQuerySettings::SettingsTagWhere => array(
						sprintf('flink.id_user=%s', $userId),
						isset($folderId) ? sprintf("flink.id_folder='%s'", $folderId) : "flink.id_folder is null"
					),
					DataTableQuerySettings::SettingsTagGroup => array('flink.id'),
					DataTableQuerySettings::SettingsTagColumns => $columnSettings
				));
			/** @var CDbCommand $dbCommand */
			$dbCommand = DataTableQueryHelper::buildQuery($querySettings);
			$linkRecords = $dbCommand->queryAll();

			$links = DataTableFormatHelper::formatRegularData($linkRecords, $columnSettings);
			return $links;
		}

		/**
		 * @param $linkId
		 * @param $parentId
		 * @param $oldParentId
		 */
		public static function putLinkToFolder($linkId, $parentId, $oldParentId)
		{
			/** @var $linkRecord FavoritesLinkRecord */
			if (isset($oldParentId))
				$linkRecord = self::model()->find('id_link=? and id_folder=?', array($linkId, $oldParentId));
			else
				$linkRecord = self::model()->find('id_link=? and id_folder is null', array($linkId));
			if (isset($linkRecord))
			{
				$linkRecord->id_folder = $parentId;
				$linkRecord->save();
			}
		}

		/**
		 * @param $linkId
		 * @param $parentId
		 */
		public static function deleteLink($linkId, $parentId)
		{
			if (isset($parentId))
				self::model()->deleteAll('id_link=? and id_folder=?', array($linkId, $parentId));
			else
				self::model()->deleteAll('id_link=? and id_folder is null', array($linkId));
		}

		public static function clearAll()
		{
			self::model()->deleteAll();
		}

		/**
		 * @param $userId
		 */
		public static function clearByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}

		public static function clearByLinkIds()
		{
			Yii::app()->db->createCommand()->delete('tbl_favorites_link', "id_link not in (select l.id from tbl_link l )");
		}

		/**
		 * @param $folderId
		 */
		public static function clearByFolder($folderId)
		{
			self::model()->deleteAll('id_folder=?', array($folderId));
		}
	}
