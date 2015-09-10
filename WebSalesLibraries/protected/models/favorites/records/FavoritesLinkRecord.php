<?php

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
		 * @return array
		 */
		public static function getLinksByFolder($userId, $folderId)
		{
			$links = array();
			if (count($links) == 0)
			{
				if (isset($folderId))
					$favoriteLinkRecords = self::model()->findAll('id_user=? and id_folder=?', array($userId, $folderId));
				else
					$favoriteLinkRecords = self::model()->findAll('id_user=? and id_folder is null', array($userId));
				if (isset($favoriteLinkRecords) && count($favoriteLinkRecords) > 0)
				{
					/** @var $linkIds string[] */
					foreach ($favoriteLinkRecords as $favoriteLinkRecord)
						$linkIds[] = $favoriteLinkRecord->id_link;
					$dateField = 'link.file_date as link_date';
					$linkRecords = Yii::app()->db->createCommand()
						->select('link.id, link.id_library,
							link.name,
							link.file_name,
							link.file_relative_path as path,
							' . $dateField . ',
							(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate,
							link.format,
							link.properties as extended_properties,
							glcat.tag as tag')
						->from('tbl_link link')
						->leftJoin("(select lcat.id_link, group_concat(lcat.tag separator ', ') as tag from tbl_link_category lcat group by lcat.id_link) glcat", "glcat.id_link=link.id")
						->where("id in ('" . implode("', '", $linkIds) . "')")
						->queryAll();
					$links = DataTableHelper::formatRegularData($linkRecords);
					foreach ($favoriteLinkRecords as $favoriteLinkRecord)
						foreach ($links as $link)
							if ($link['id'] == $favoriteLinkRecord->id_link)
							{
								$link['name']['value'] = $favoriteLinkRecord->name;
								break;
							}
				}
			}
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

		/**
		 * @param $liveLinkIds
		 */
		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_favorites_link', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}

		/**
		 * @param $folderId
		 */
		public static function clearByFolder($folderId)
		{
			self::model()->deleteAll('id_folder=?', array($folderId));
		}
	}
