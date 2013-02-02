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
				$folderRecord = FavoritesFolderStorage::getFolder($userId, $folderName);
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

		public static function getLinksByFolder($userId, $folderId, $isSort)
		{
			$sessionId = 'favoriteLinks';
			if ($isSort == 1)
			{
				$links = Yii::app()->session[$sessionId];
			}
			else
			{
				if (isset($folderId))
					$favoriteLinkRecords = self::model()->findAll('id_user=? and id_folder=?', array($userId, $folderId));
				else
					$favoriteLinkRecords = self::model()->findAll('id_user=? and id_folder is null', array($userId));
				if (isset($favoriteLinkRecords) && count($favoriteLinkRecords) > 0)
				{
					foreach ($favoriteLinkRecords as $favoriteLinkRecord)
						$linkIds[] = $favoriteLinkRecord->id_link;
					$dateField = 'file_date as link_date';
					$linkRecords = Yii::app()->db->createCommand()
						->select('id, id_library, name, file_name, ' . $dateField . ', enable_attachments, enable_file_card, format')
						->from('tbl_link')
						->where("id in ('" . implode("', '", $linkIds) . "')")
						->queryAll();
					$preLinks = LinkStorage::getLinksGrid($linkRecords);
					if (isset($preLinks))
						foreach ($preLinks as $link)
							foreach ($favoriteLinkRecords as $favoriteLinkRecord)
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
					usort($links, 'LinkStorage::sortLinks');
					return $links;
				}
			return null;
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

		public static function clearByLibrary($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}
