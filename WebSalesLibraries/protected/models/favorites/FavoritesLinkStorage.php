<?php
	class FavoritesLinkStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{favorites_link}}';
		}

		public static function addLink($userId, $linkId, $linkName, $folderName, $libraryId)
		{
			if (isset($folderName))
			{
				$folderRecord = FavoritesFolderStorage::getFolderByName($userId, $folderName);
				$linkRecord = self::model()->find('id_user=? and id_link=? and id_folder=? and LOWER(name)=?', array($userId, $linkId, $folderRecord->id, strtolower($linkName)));
			}
			else
				$linkRecord = self::model()->find('id_user=? and id_link=? and id_folder is null and LOWER(name)=?', array($userId, $linkId, strtolower($linkName)));
			if (!isset($linkRecord))
			{
				$linkRecord = new FavoritesLinkStorage();
				$linkRecord->id = uniqid();
				$linkRecord->id_link = $linkId;
				$linkRecord->id_library = $libraryId;
				$linkRecord->id_folder = isset($folderRecord) ? $folderRecord->id : null;
				$linkRecord->id_user = $userId;
			}
			$linkRecord->name = $linkName;
			$linkRecord->save();
		}

		public static function getLinksByFolder($userId, $folderId, $isSort, $sortColumn, $sortDirection)
		{
			$sessionId = 'favoriteLinks';
			if ($isSort == 1)
				$links = Yii::app()->session[$sessionId];
			if (!isset($links))
			{
				if (isset($folderId))
					$favoriteLinkRecords = self::model()->findAll('id_user=? and id_folder=?', array($userId, $folderId));
				else
					$favoriteLinkRecords = self::model()->findAll('id_user=? and id_folder is null', array($userId));
				if (isset($favoriteLinkRecords) && count($favoriteLinkRecords) > 0)
				{
					foreach ($favoriteLinkRecords as $favoriteLinkRecord)
						$linkIds[] = $favoriteLinkRecord->id_link;
					$dateField = 'link.file_date as link_date';
					$linkRecords = Yii::app()->db->createCommand()
						->select('link.id, link.id_library,
							link.name, link.file_name,
							' . $dateField . ',
							link.enable_attachments,
							link.enable_file_card,
							link.format')
						->from('tbl_link link')
						->where("id in ('" . implode("', '", $linkIds) . "')")
						->queryAll();
					$preLinks = LinkStorage::getLinksGrid($linkRecords);
					if (isset($preLinks))
						foreach ($favoriteLinkRecords as $favoriteLinkRecord)
							foreach ($preLinks as $link)
								if ($link['id'] == $favoriteLinkRecord->id_link)
								{
									$link['name'] = $favoriteLinkRecord->name;
									$links[] = $link;
									break;
								}
				}
				if (isset($links))
					Yii::app()->session[$sessionId] = $links;
				else
					Yii::app()->session[$sessionId] = null;
			}
			if (isset($links))
				if (count($links) > 0)
				{
					$sortHelper = new ArraySortHelper($sortColumn, $sortDirection);
					usort($links, array($sortHelper, 'sort'));
					return $links;
				}
			return null;
		}

		public static function putLinkToFolder($linkId, $parentId, $oldParentId)
		{
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

		public static function clearByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}

		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_favorites_link', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}

		public static function clearByFolder($folderId)
		{
			self::model()->deleteAll('id_folder=?', array($folderId));
		}
	}
