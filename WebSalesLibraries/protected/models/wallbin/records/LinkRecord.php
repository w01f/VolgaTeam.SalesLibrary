<?php

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
	 * @property mixed format
	 * @property mixed order
	 * @property mixed type
	 * @property int widget_type
	 * @property mixed widget
	 * @property mixed tags
	 * @property mixed is_dead
	 * @property mixed date_add
	 * @property mixed date_modify
	 * @property mixed id_preview
	 * @property mixed content
	 * @property mixed properties
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
			$linkDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($link['dateModify']));
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
				$linkRecord->file_date = $link['originalFormat'] == 'url' || $link['originalFormat'] == 'url365' ? date(Yii::app()->params['mysqlDateFormat'], strtotime($link['dateAdd'])) : date(Yii::app()->params['mysqlDateFormat'], strtotime($link['fileDate']));
				$linkRecord->file_size = $link['fileSize'];
				$linkRecord->format = $link['originalFormat'];
				$linkRecord->order = $link['order'];
				$linkRecord->type = $link['type'];
				$linkRecord->widget_type = $link['widgetType'];
				$linkRecord->widget = $link['widget'];
				if (array_key_exists('isPreviewNotReady', $link))
					$linkRecord->is_preview_not_ready = $link['isPreviewNotReady'];
				if (array_key_exists('isDead', $link))
					$linkRecord->is_dead = $link['isDead'];
				$linkRecord->date_add = date(Yii::app()->params['mysqlDateFormat'], strtotime($link['dateAdd']));
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

			if (array_key_exists('extendedProperties', $link) && isset($link['extendedProperties']))
				foreach ($link['extendedProperties'] as $key => $value)
				{
					if ($key == 'assignedUsers' && isset($value))
						LinkWhiteListRecord::updateData($link['id'], $link['libraryId'], $value);
					else if ($key == 'deniedUsers' && isset($value))
						LinkBlackListRecord::updateData($link['id'], $link['libraryId'], $value);
					else if ($key == 'isRestricted')
						$linkRecord->is_restricted = CJSON::decode($value);
					else if ($key == 'no_share')
						$linkRecord->no_share = CJSON::decode($value);
					else
						$linkRecord->properties = CJSON::encode($link['extendedProperties']);
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
		 * @param $libraryId
		 */
		public static function clearByLibrary($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

		/**
		 * @param $folderId
		 * @param $linkIds
		 */
		public static function clearByIds($folderId, $linkIds)
		{
			if (isset($linkIds))
				Yii::app()->db->createCommand()->delete('tbl_link', "id_folder = '" . $folderId . "' and id not in ('" . implode("','", $linkIds) . "')");
			else
				Yii::app()->db->createCommand()->delete('tbl_link', "id_folder = '" . $folderId . "'");
		}

		/**
		 * @param $linkId
		 * @return LinkRecord|null
		 */
		public static function getLinkById($linkId)
		{
			$linkRecord = self::model()->findByPk($linkId);
			if (isset($linkRecord) && $linkRecord->is_dead == false && $linkRecord->is_preview_not_ready == false)
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
			return self::model()->findAll('id_folder=? and id_parent_link is null and is_dead=0 and is_preview_not_ready=0', array($folderId));
		}

		/**
		 * @param $linkId
		 * @return array|null
		 */
		public static function getLinksByParent($linkId)
		{
			$linkRecords = self::model()->findAll('id_parent_link=? and is_dead=0 and is_preview_not_ready=0 order by name', array($linkId));
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
			return self::model()->findAll('id_library=? and is_restricted = 1', array($libraryId));
		}

		/**
		 * @param $links LinkRecord[]
		 * @return LinkRecord[]
		 */
		public static function applyPermissionsFilter($links)
		{
			$filteredLinks = array();
			$isAdmin =UserIdentity::isUserAdmin();
			$userId = UserIdentity::getCurrentUserId();
			if (!$isAdmin)
			{
				foreach ($links as $link)
				{
					$availableLinkIds = LinkWhiteListRecord::getAvailableLinks($userId);
					if (in_array($link->id, $availableLinkIds))
						$filteredLinks[] = $link;
					$deniedLinkIds = LinkBlackListRecord::getDeniedLinks($userId);
					if (!in_array($link->id, $deniedLinkIds))
						$filteredLinks[] = $link;
				}
				return $filteredLinks;
			}
			else
				return $links;
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
		 * @param $linkRecords
		 * @return array|null
		 */
		public static function getLinksGrid($linkRecords)
		{
			if (isset($linkRecords) && count($linkRecords) > 0)
			{
				$libraryManager = new LibraryManager();
				$logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'search';
				foreach ($linkRecords as $linkRecord)
				{
					if (array_key_exists('type', $linkRecord))
						$type = $linkRecord['type'];
					else
						$type = 9999;

					$link['id'] = $linkRecord['id'];
					$link['name'] = $type == 6 ? "LineBreak" : $linkRecord['name'];
					$link['file_name'] = $linkRecord['file_name'];
					if (array_key_exists('link_date', $linkRecord))
						$link['date_modify'] = $linkRecord['link_date'];

					/** @var $library Library */
					$library = $libraryManager->getLibraryById($linkRecord['id_library']);
					$link['id_library'] = $linkRecord['id_library'];
					if (isset($library))
						$link['library'] = $library->name;
					else
						$link['library'] = '';

					if (array_key_exists('tag', $linkRecord))
						$link['tag'] = $linkRecord['tag'];
					else
						$link['tag'] = '';

					if (array_key_exists('rate', $linkRecord))
					{
						$link['rate'] = $linkRecord['rate'];
						$link['rate_image'] = LinkRateRecord::getStarImage(floatval($linkRecord['rate']));
					}
					else
					{
						$link['rate'] = '';
						$link['rate_image'] = '';
					}

					switch ($linkRecord['format'])
					{
						case 'ppt':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-powerpoint.png'));
							break;
						case 'doc':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-word.png'));
							break;
						case 'xls':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-excel.png'));
							break;
						case 'pdf':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-pdf.png'));
							break;
						case 'video':
						case 'wmv':
						case 'mp4':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-video.png'));
							break;
						case 'key':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-keynote.png'));
							break;
						case 'url':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-url.png'));
							break;
						case 'url365':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-url365.png'));
							break;
						case 'mp3':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-mp3.png'));
							break;
						case 'png':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-png.png'));
							break;
						case 'jpeg':
							$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-jpeg.png'));
							break;
						default:
							if ($type == 5)
								$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-folder.png'));
							else if ($type == 6)
								$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-line-break.png'));
							else if ($type == 8)
								$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-url.png'));
							else
								$link['file_type'] = base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'search-undefined-type.png'));
							break;
					}
					$links[] = $link;
				}
			}
			if (isset($links))
				return $links;
			else
				return null;
		}

		/**
		 * @param $libraryId
		 * @return int
		 */
		public static function getLinksCountByLibrary($libraryId)
		{
			return self::model()->count('id_library=? and type<>6', array($libraryId));
		}
	}
