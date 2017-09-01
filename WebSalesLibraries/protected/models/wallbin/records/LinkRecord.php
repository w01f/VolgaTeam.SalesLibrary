<?

	/**
	 * Class LinkRecord
	 * @property mixed id_parent_link
	 * @property mixed id_folder
	 * @property mixed id_library
	 * @property mixed name
	 * @property mixed file_relative_path
	 * @property mixed file_name
	 * @property mixed file_extension
	 * @property mixed file_date
	 * @property mixed file_size
	 * @property mixed original_format
	 * @property mixed search_format
	 * @property mixed order
	 * @property mixed type
	 * @property int widget_type
	 * @property mixed widget
	 * @property mixed tags
	 * @property mixed date_add
	 * @property mixed date_modify
	 * @property mixed id_preview
	 * @property mixed content
	 * @property mixed settings
	 * @property mixed thumbnail
	 * @property mixed id_banner
	 * @property mixed id_line_break
	 * @property mixed id
	 * @property mixed is_preview_not_ready
	 * @property mixed is_restricted
	 * @property mixed no_share
	 */
	class LinkRecord extends CActiveRecord
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
			return '{{link}}';
		}

		/**
		 * @param $link
		 * @param $libraryRootPath
		 */
		public static function updateData($link, $libraryRootPath)
		{
			$needToUpdate = false;
			$needToCreate = false;
			/** @var $linkRecord LinkRecord */
			$linkRecord = LinkRecord::model()->findByPk($link['id']);
			$linkDate = date(Yii::app()->params['mysqlDateTimeFormat'], strtotime($link['dateModify']));
			if ($linkRecord !== null)
			{
				if ($linkRecord->date_modify != null)
					if ($linkRecord->date_modify != $linkDate)
						$needToUpdate = true;
			}
			else
			{
				$linkRecord = new LinkRecord();
				$needToCreate = true;
			}
			if ($needToCreate || $needToUpdate)
			{
				$linkRecord->id = $link['id'];
				if (array_key_exists('parentLinkId', $link))
					$linkRecord->id_parent_link = $link['parentLinkId'];
				$linkRecord->id_folder = $link['folderId'];
				$linkRecord->id_library = $link['libraryId'];
				$linkRecord->name = $link['name'];
				$linkRecord->file_relative_path = $link['fileRelativePath'];
				$linkRecord->file_name = $link['fileName'];
				$linkRecord->file_extension = $link['fileExtension'];
				$linkRecord->file_date = date(Yii::app()->params['mysqlDateTimeFormat'], strtotime($link['fileDate']));
				$linkRecord->file_size = $link['fileSize'];
				$linkRecord->original_format = $link['originalFormat'];
				if (array_key_exists('searchFormat', $link))
					$linkRecord->search_format = $link['searchFormat'];
				else
					$linkRecord->search_format = $link['originalFormat'];
				$linkRecord->order = $link['order'];
				$linkRecord->type = $link['type'];
				$linkRecord->widget_type = $link['widgetType'];
				$linkRecord->widget = $link['widget'];
				if (array_key_exists('isPreviewNotReady', $link))
					$linkRecord->is_preview_not_ready = $link['isPreviewNotReady'];
				$linkRecord->date_add = date(Yii::app()->params['mysqlDateTimeFormat'], strtotime($link['dateAdd']));
				$linkRecord->date_modify = $linkDate;

				$contentPath = str_replace('\\', '/', $libraryRootPath . DIRECTORY_SEPARATOR . $link['contentPath']);
				if (file_exists($contentPath) && is_file($contentPath))
					$linkRecord->content = file_get_contents($contentPath);

				echo 'Link ' . ($needToCreate ? 'created' : 'updated') . ': ' . $link['name'] . ' (' . $link['fileName'] . ')' . "\n";
			}

			if (array_key_exists('previewId', $link) && isset($link['previewId']))
				$linkRecord->id_preview = $link['previewId'];

			if (array_key_exists('banner', $link) && isset($link['banner']))
			{
				$linkRecord->id_banner = $link['banner']['id'];
				BannerRecord::updateData($link['banner']);
			}

			if (array_key_exists('thumbnail', $link) && !empty($link['thumbnail']))
				$linkRecord->thumbnail = CJSON::encode($link['thumbnail']);

			if (array_key_exists('extendedProperties', $link) && !empty($link['extendedProperties']))
			{
				foreach ($link['extendedProperties'] as $key => $value)
				{
					if (!isset($value)) continue;
					switch ($key)
					{
						case 'assignedUsers':
							LinkWhiteListRecord::updateData($link['id'], $link['libraryId'], $value);
							break;
						case 'deniedUsers':
							LinkBlackListRecord::updateData($link['id'], $link['libraryId'], $value);
							break;
						case 'isRestricted':
							$linkRecord->is_restricted = CJSON::decode($value);
							break;
						case 'no_share':
							$linkRecord->no_share = CJSON::decode($value);
							break;
						case 'qpageId':
							LinkQPageRecord::addLinkQPageRelation($linkRecord->id, $value);
							break;
						case 'bundleItems':
							LinkBundleRecord::updateData($link['id'], $link['libraryId'], $value);
							break;
					}
				}

				if ($link['type'] == 16)
					LinkInternalLinkRecord::updateData($link['id'], $link['libraryId'], $link['extendedProperties']);

				$linkRecord->settings = CJSON::encode($link['extendedProperties']);
			}

			if (array_key_exists('lineBreakProperties', $link) && isset($link['lineBreakProperties']))
			{
				$linkRecord->id_line_break = $link['lineBreakProperties']['id'];
				LineBreakRecord::updateData($link['lineBreakProperties']);
			}

			$linkRecord->tags = $link['tags'];
			if (array_key_exists('superFilters', $link))
				if (isset($link['superFilters']))
					foreach ($link['superFilters'] as $superFilter)
						LinkSuperFilterRecord::updateData($superFilter);
			if (array_key_exists('categories', $link))
				if (isset($link['categories']))
					foreach ($link['categories'] as $category)
						LinkCategoryRecord::updateData($category);

			$linkRecord->save();
		}

		/**
		 * @param $linkId
		 * @return LinkRecord|null
		 */
		public static function getLinkById($linkId)
		{
			/** @var  $linkRecord LinkRecord */
			$linkRecord = self::model()->findByPk($linkId);
			if (isset($linkRecord) && $linkRecord->is_preview_not_ready == false)
				return $linkRecord;
			return null;
		}

		/**
		 * @param $libraryName
		 * @param $pageName
		 * @param $linkName
		 * @return mixed
		 */
		public static function getLinkByLibraryAndPageAndName($libraryName, $pageName, $linkName)
		{
			return Yii::app()->db->createCommand()
				->select('link.id as link_id')
				->from('tbl_link link')
				->join('tbl_folder fol', 'fol.id = link.id_folder')
				->join('tbl_page pg', 'pg.id = fol.id_page')
				->join('tbl_library lib', 'lib.id = pg.id_library')
				->where('link.name = :linkName and pg.name = :pageName and lib.name = :libraryName', array(':linkName' => $linkName, ':pageName' => $pageName, ':libraryName' => $libraryName))
				->queryRow();
		}

		/**
		 * @param $folderId
		 * @return LinkRecord[]
		 */
		public static function getLinksByFolder($folderId)
		{
			/** @var LinkRecord[] $linkRecords */
			$linkRecords = self::model()->findAll('id_folder=? and id_parent_link is null', array($folderId));
			return $linkRecords;
		}

		/**
		 * @param $linkId
		 * @return array|null
		 */
		public static function getLinksByParent($linkId)
		{
			$linkRecords = self::model()->findAll('id_parent_link=? and is_preview_not_ready=0 order by name', array($linkId));
			if (isset($linkRecords))
				return $linkRecords;
			return null;
		}

		/**
		 * @param $libraryId
		 * @return LinkRecord[]
		 */
		public static function getRestrictedLinks($libraryId)
		{
			/** @var LinkRecord[] $linkRecords */
			$linkRecords = self::model()->findAll('id_library=? and is_restricted = 1', array($libraryId));
			return $linkRecords;
		}

		/**
		 * @param $links LinkRecord[]
		 * @return LinkRecord[]
		 */
		public static function applyPermissionsFilter($links)
		{
			if (\UserIdentity::isUserAuthorized() && !\UserIdentity::isUserAdmin())
			{
				$userId = UserIdentity::getCurrentUserId();
				$filteredLinks = array();
				$whiteList = LinkWhiteListRecord::getWhiteListLinkIds();
				foreach ($links as $link)
				{
					$linkAvailable = true;
					if (in_array($link->id, $whiteList))
					{
						$availableLinkIds = LinkWhiteListRecord::getAvailableLinks($userId);
						if (in_array($link->id, $availableLinkIds))
							$linkAvailable = true;
						else
							$linkAvailable = false;
					}
					if ($linkAvailable)
					{
						$deniedLinkIds = LinkBlackListRecord::getDeniedLinks($userId);
						if (!in_array($link->id, $deniedLinkIds))
							$filteredLinks[] = $link;
					}
				}
				return $filteredLinks;
			}
			else if (\UserIdentity::isUserAdmin())
				return $links;
			return array();
		}

		/**
		 * @param $linkId
		 * @return int
		 */
		public static function enumFolderContent($linkId)
		{
			$linksNumber = 0;
			$childLinks = self::getLinksByParent($linkId);
			if (isset($childLinks))
				foreach ($childLinks as $linkRecord)
					switch ($linkRecord->type)
					{
						case 6:
							break;
						case 5:
							$linksNumber += self::enumFolderContent($linkRecord->id);
							break;
						default:
							$linksNumber++;
							break;
					}
			return $linksNumber;
		}

		/**
		 * @param $pageIds string[]
		 * @return int
		 */
		public static function getLinksCountByPageIds($pageIds)
		{
			return self::model()->count(sprintf("id_folder in (select f.id from tbl_folder f where f.id_page in ('%s')) and type<>6", implode("','", $pageIds)));
		}

		/**
		 * @param $libraryId
		 */
		public static function clearByLibrary($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

		/**
		 * @param string $folderId
		 * @param array $excludeLinkIds
		 */
		public static function clearByIds($folderId, $excludeLinkIds)
		{
			if (count($excludeLinkIds) > 0)
				Yii::app()->db->createCommand()->delete('tbl_link', "id_folder = '" . $folderId . "' and id not in ('" . implode("','", $excludeLinkIds) . "')");
			else
				Yii::app()->db->createCommand()->delete('tbl_link', "id_folder = '" . $folderId . "'");
		}

		public static function clearDeadLinkData()
		{
			/** @var $liveLinkRecords LinkRecord[] */
			$liveLinkRecords = self::model()->findAll();
			$liveLinkIds = array();
			if (isset($liveLinkRecords))
				foreach ($liveLinkRecords as $linkRecord)
					$liveLinkIds[] = $linkRecord->id;
			if (count($liveLinkIds) > 0)
			{
				FavoritesLinkRecord::clearByLinkIds($liveLinkIds);
				UserLinkCartRecord::clearByLinkIds($liveLinkIds);
				QPageLinkRecord::clearByLinkIds($liveLinkIds);
				LinkRateRecord::clearByLinkIds($liveLinkIds);
				LinkQPageRecord::clearByLinkIds($liveLinkIds);
			}
		}
	}
