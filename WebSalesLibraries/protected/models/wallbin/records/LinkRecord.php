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
	 * @property mixed note
	 * @property mixed format
	 * @property mixed is_bold
	 * @property mixed order
	 * @property mixed type
	 * @property mixed enable_widget
	 * @property mixed widget
	 * @property mixed tags
	 * @property mixed is_dead
	 * @property mixed is_preview_not_ready
	 * @property mixed force_preview
	 * @property mixed no_share
	 * @property mixed is_restricted
	 * @property mixed date_add
	 * @property bool|string date_modify
	 * @property mixed id_preview
	 * @property mixed content
	 * @property mixed id_banner
	 * @property mixed id_line_break
	 * @property mixed enable_file_card
	 * @property mixed id_file_card
	 * @property mixed enable_attachments
	 * @property mixed id
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
				$linkRecord->file_date = $link['originalFormat'] == 'url' ? date(Yii::app()->params['mysqlDateFormat'], strtotime($link['dateAdd'])) : date(Yii::app()->params['mysqlDateFormat'], strtotime($link['fileDate']));
				$linkRecord->file_size = $link['fileSize'];
				$linkRecord->note = $link['note'];
				$linkRecord->format = $link['originalFormat'];
				$linkRecord->is_bold = $link['isBold'];
				$linkRecord->order = $link['order'];
				$linkRecord->type = $link['type'];
				$linkRecord->enable_widget = $link['enableWidget'];
				$linkRecord->widget = $link['widget'];
				if (array_key_exists('isDead', $link))
					$linkRecord->is_dead = $link['isDead'];
				if (array_key_exists('isPreviewNotReady', $link))
					$linkRecord->is_preview_not_ready = $link['isPreviewNotReady'];
				if (array_key_exists('forcePreview', $link))
					$linkRecord->force_preview = $link['forcePreview'];
				if (array_key_exists('isRestricted', $link))
					$linkRecord->is_restricted = $link['isRestricted'];
				else
					$linkRecord->is_restricted = false;
				if (array_key_exists('noShare', $link))
					$linkRecord->no_share = $link['noShare'];
				else
					$linkRecord->no_share = false;
				$linkRecord->date_add = date(Yii::app()->params['mysqlDateFormat'], strtotime($link['dateAdd']));
				$linkRecord->date_modify = $linkDate;

				if (array_key_exists('previewId', $link) && isset($link['previewId']))
					$linkRecord->id_preview = $link['previewId'];

				$contentPath = str_replace('\\', '/', $libraryRootPath . DIRECTORY_SEPARATOR . $link['contentPath']);
				if (file_exists($contentPath) && is_file($contentPath))
					$linkRecord->content = file_get_contents($contentPath);

				echo 'Link ' . ($needToCreate ? 'created' : 'updated') . ': ' . $link['name'] . ' (' . $link['fileName'] . ')' . "\n";
			}

			if (array_key_exists('banner', $link) && isset($link['banner']))
			{
				$linkRecord->id_banner = $link['banner']['id'];
				BannerRecord::updateData($link['banner']);
			}

			if (array_key_exists('lineBreakProperties', $link) && isset($link['lineBreakProperties']))
			{
				$linkRecord->id_line_break = $link['lineBreakProperties']['id'];
				LineBreakRecord::updateData($link['lineBreakProperties']);
			}

			if (array_key_exists('fileCard', $link) && isset($link['fileCard']))
			{
				$linkRecord->enable_file_card = $link['enableFileCard'];
				$linkRecord->id_file_card = $link['fileCard']['id'];
				FileCardRecord::updateData($link['fileCard']);
			}
			else
				$linkRecord->enable_file_card = false;

			if (array_key_exists('attachments', $link) && isset($link['attachments']))
			{
				$linkRecord->enable_attachments = $link['enableAttachments'];
				foreach ($link['attachments'] as $attachment)
					AttachmentRecord::updateData($attachment);
			}
			else
				$linkRecord->enable_attachments = false;

			$linkRecord->tags = $link['tags'];
			if (array_key_exists('superFilters', $link))
				if (isset($link['superFilters']))
					foreach ($link['superFilters'] as $superFilter)
						LinkSuperFilterRecord::updateData($superFilter);
			if (array_key_exists('categories', $link))
				if (isset($link['categories']))
					foreach ($link['categories'] as $category)
						LinkCategoryRecord::updateData($category);

			if (array_key_exists('assignedUsers', $link) && isset($link['assignedUsers']))
				LinkWhiteListRecord::updateData($link['id'], $link['libraryId'], $link['assignedUsers']);

			if (array_key_exists('deniedUsers', $link) && isset($link['deniedUsers']))
				LinkBlackListRecord::updateData($link['id'], $link['libraryId'], $link['deniedUsers']);

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
		 * @param $contentConditions string[]
		 * @param $fileTypes string[]
		 * @param $startDate string
		 * @param $endDate string
		 * @param $dateFile boolean
		 * @param $checkedLibraryIds string[]
		 * @param $onlyFileCards int
		 * @param $superFilters string[]
		 * @param $categories Category[]
		 * @param $categoriesExactMatch string
		 * @param $onlyWithCategories boolean
		 * @param $hideDuplicated boolean
		 * @param $onlyByName boolean
		 * @param $onlyByContent boolean
		 * @param $datasetKey string
		 * @param $baseDatasetKey string
		 * @param $sortColumn string
		 * @param $sortDirection string
		 * @return array|null
		 */
		public static function searchByContent($contentConditions, $fileTypes, $startDate, $endDate, $dateFile, $checkedLibraryIds, $onlyFileCards, $superFilters, $categories, $categoriesExactMatch, $onlyWithCategories, $hideDuplicated, $onlyByName, $onlyByContent, $datasetKey, $baseDatasetKey, $sortColumn, $sortDirection)
		{
			$baseLinks = Yii::app()->session[$baseDatasetKey];
			$baseLinksCondition = '1=1';
			if (isset($baseLinks))
			{
				$availableLinkIds = array();
				foreach ($baseLinks as $baseLink)
					$availableLinkIds[] = $baseLink['id'];
				if (count($availableLinkIds) > 0)
					$baseLinksCondition = "link.id in ('" . implode("','", $availableLinkIds) . "')";
			}
			$links = Yii::app()->session[$datasetKey];
			if (!isset($links))
			{
				if (!((isset($fileTypes) && (count($contentConditions) > 0 || (isset($categories) && count($categories) > 0) || isset($superFilters) || (isset($startDate) && isset($endDate))) || isset($baseLinks))))
					return null;
				$libraryCondition = '1 = 1';
				if (isset($checkedLibraryIds))
				{
					$count = count($checkedLibraryIds);
					switch ($count)
					{
						case 0:
							$libraryCondition = '1 = 1';
							break;
						default:
							$libraryCondition = "link.id_library in ('" . implode("','", $checkedLibraryIds) . "')";
							break;
					}
				}

				$fileTypeCondition = '1 = 1';
				if (isset($fileTypes))
				{
					$count = count($fileTypes);
					switch ($count)
					{
						case 0:
							$fileTypeCondition = '1 = 1';
							break;
						default:
							$fileTypeCondition = "link.format in ('" . implode("','", $fileTypes) . "')";
							break;
					}
				}

				$dateCondition = '1 = 1';
				$additionalDateCondition = '';
				if (isset($startDate) && isset($endDate))
					if ($startDate != '' && $endDate != '')
					{
						$dateColumn = 'link.date_modify';
						if ($dateFile)
							$dateColumn = 'link.file_date';
						$dateCondition = $dateColumn . " >= '" . date(Yii::app()->params['mysqlDateFormat'], strtotime($startDate)) . "' and " . $dateColumn . " <= '" . date(Yii::app()->params['mysqlDateFormat'], strtotime($endDate) + 86400) . "'";
						if (count($contentConditions) == 0)
							$additionalDateCondition = " or (" . $dateCondition . ")";
					}

				$fileCardsCondition = '1 = 1';
				$additionalFileCardsCondition = '';
				if (isset($onlyFileCards))
				{
					if ($onlyFileCards == 1)
					{
						$fileCardsCondition = 'link.enable_file_card = true or link.enable_attachments = true ';
						if (count($contentConditions) == 0)
							$additionalFileCardsCondition = ' or (link.enable_file_card = true or link.enable_attachments = true)';
					}
				}

				$superFilterCondition = '1 = 1';
				$additionalSuperFilterCondition = '';
				if (isset($superFilters))
				{
					foreach ($superFilters as $superFilter)
						$superFilterSelector[] = '(link.id in (select id_link from tbl_link_super_filter where value = "' . $superFilter . '"))';
					if (isset($superFilterSelector))
					{
						$superFilterCondition = '(' . implode(($categoriesExactMatch == 'true' ? ' and ' : ' or '), $superFilterSelector) . ')';
						if (count($contentConditions) == 0)
							$additionalSuperFilterCondition = ' or ' . $superFilterCondition;
					}
				}

				$categoryCondition = '1=1';
				$categoryJoinCondition = '1 = 1';
				$additionalCategoryCondition = '';
				if (isset($categories) && count($categories) > 0)
				{
					$categoriesSelector = array();
					$categoriesJoinSelector = array();
					foreach ($categories as $category)
					{
						$categoriesSelector[] = '(link.id in (select id_link from tbl_link_category where category = "' . $category['category'] . '" and tag = "' . $category['tag'] . '"))';
						$categoriesJoinSelector[] = '(lcat.id in (select id from tbl_link_category where category = "' . $category['category'] . '" and tag = "' . $category['tag'] . '"))';
					}
					$categoryCondition = '(' . implode(($categoriesExactMatch == 'true' ? ' and ' : ' or '), $categoriesSelector) . ')';
					$categoryJoinCondition = '(' . implode(($categoriesExactMatch == 'true' ? ' and ' : ' or '), $categoriesJoinSelector) . ')';
					if (count($contentConditions) == 0)
						$additionalCategoryCondition = ' or ' . $categoryCondition;

				}
				else if (count($contentConditions) > 0)
				{
					$categoriesSelector = array();
					$categoriesJoinSelector = array();
					foreach ($contentConditions as $contentConditionPart)
					{
						$conditionToCompare = strtolower(trim(str_replace('"', '', $contentConditionPart)));
						$categoriesSelector[] = '(link.id in (select id_link from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
						$categoriesJoinSelector[] = '(lcat.id in (select id from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
					}
					$categoryCondition = '(' . implode(($categoriesExactMatch == 'true' ? ' and ' : ' or '), $categoriesSelector) . ')';
					$categoryJoinCondition = '(' . implode(($categoriesExactMatch == 'true' ? ' and ' : ' or '), $categoriesJoinSelector) . ')';
					$additionalCategoryCondition = ' or ' . $categoryCondition;
					$categoryCondition = '1=1';
				}

				$onlyWithCategoriesCondition = '1 = 1';
				$additionalOnlyWithCategoriesCondition = '';
				if ($onlyWithCategories)
				{
					$onlyWithCategoriesCondition = 'exists (select id_link from tbl_link_category where id_link = link.id)';
					if (count($contentConditions) == 0)
						$additionalOnlyWithCategoriesCondition = ' or (exists (select id_link from tbl_link_category where id_link = link.id))';
				}

				$folderCondition = '1 <> 1';
				$isAdmin = true;
				if (isset(Yii::app()->user))
				{
					$userId = Yii::app()->user->getId();
					if (isset(Yii::app()->user->role))
						$isAdmin = Yii::app()->user->role == 2;
					else
						$isAdmin = true;
					if (isset($userId) && !$isAdmin)
						$assignedPageIds = UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);
				}
				if (isset($assignedPageIds))
					$folderCondition = "link.id_folder in (select id from tbl_folder where id_page in ('" . implode("', '", $assignedPageIds) . "'))";
				else if (!isset($userId) || (isset($isAdmin) && $isAdmin))
					$folderCondition = '1 = 1';

				$linkCondition = '1 <> 1';
				if ($isAdmin)
					$linkCondition = '1 = 1';
				else if (isset($userId))
				{
					$restrictedLinkConditions = array();
					$availableLinkIds = LinkWhiteListRecord::getAvailableLinks($userId);
					if (count($availableLinkIds) > 0)
						$restrictedLinkConditions[] = "link.id in ('" . implode("', '", $availableLinkIds) . "')";

					$deniedLinkIds = LinkBlackListRecord::getDeniedLinks($userId);
					if (count($deniedLinkIds) > 0)
						$restrictedLinkConditions[] = "link.id not in ('" . implode("', '", $deniedLinkIds) . "')";

					$linkCondition = "link.is_restricted <> 1";
					if (count($restrictedLinkConditions) > 0)
						$linkCondition = "link.is_restricted <> 1 or (" . implode(" and ", $restrictedLinkConditions) . ")";
				}

				$matchCondition = 'link.name,link.file_name,link.tags,link.content';
				if (!($onlyByName && $onlyByContent))
				{
					if ($onlyByName)
						$matchCondition = 'link.name,link.file_name';
					else if ($onlyByContent)
						$matchCondition = 'link.content';
				}

				$contentCondition = "1=1";
				if (count($contentConditions) > 0)
				{
					$conditionParts = array();
					foreach ($contentConditions as $contentConditionPart)
						$conditionParts[] = "(match(" . $matchCondition . ") against('" . $contentConditionPart . "' in boolean mode))";
					$contentCondition = implode(" or ", $conditionParts);
				}
				$contentCondition = "(" . $contentCondition .
					$additionalFileCardsCondition .
					$additionalDateCondition .
					$additionalSuperFilterCondition .
					$additionalCategoryCondition .
					$additionalOnlyWithCategoriesCondition . ")";

				if ($hideDuplicated)
				{
					$dateField = 'max(' . ($dateFile ? 'link.file_date' : 'link.date_modify') . ') as link_date';
					$selectText = 'max(link.id) as id,
							max(link.id_library) as id_library,
							max(link.name) as name,
							link.file_name,
							' . $dateField . ',
							max(link.enable_attachments) as enable_attachments,
							max(link.enable_file_card) as enable_file_card,
							max(link.format) as format,
							(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate,
							lcat.tag as tag';
					$joinText = "lcat.id_link=link.id and " . $categoryJoinCondition;
					$whereText = $contentCondition .
						" and (" . $baseLinksCondition .
						") and (" . $libraryCondition .
						") and (" . $fileTypeCondition .
						") and (" . $fileCardsCondition .
						") and (" . $dateCondition .
						") and (" . $categoryCondition .
						") and (" . $superFilterCondition .
						") and (" . $onlyWithCategoriesCondition .
						") and (" . $folderCondition .
						") and (" . $linkCondition .
						") and link.is_dead=0 and link.is_preview_not_ready=0";
					Yii::trace(CVarDumper::dumpAsString($selectText));
					Yii::trace(CVarDumper::dumpAsString($joinText));
					Yii::trace(CVarDumper::dumpAsString($whereText));
					$linkRecords = Yii::app()->db->createCommand()
						->select($selectText)
						->from('tbl_link link')
						->leftJoin("tbl_link_category lcat", $joinText)
						->where($whereText)
						->group('link.file_name, lcat.tag')
						->queryAll();
				}
				else
				{
					$dateField = ($dateFile ? 'link.file_date' : 'link.date_modify') . ' as link_date';
					$selectText = 'link.id,
							link.id_library,
							link.name,
							link.file_name,
							' . $dateField . ',
							link.enable_attachments,
							link.enable_file_card,
							link.format,
							(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate,
							lcat.tag as tag';
					$joinText = "lcat.id_link=link.id and " . $categoryJoinCondition;
					$whereText = $contentCondition .
						" and (" . $baseLinksCondition .
						") and (" . $libraryCondition .
						") and (" . $fileTypeCondition .
						") and (" . $fileCardsCondition .
						") and (" . $dateCondition .
						") and (" . $categoryCondition .
						") and (" . $superFilterCondition .
						") and (" . $onlyWithCategoriesCondition .
						") and (" . $folderCondition .
						") and (" . $linkCondition .
						") and link.is_dead=0 and link.is_preview_not_ready=0";
					Yii::trace(CVarDumper::dumpAsString($selectText));
					Yii::trace(CVarDumper::dumpAsString($joinText));
					Yii::trace(CVarDumper::dumpAsString($whereText));
					$linkRecords = Yii::app()->db->createCommand()
						->select($selectText)
						->from('tbl_link link')
						->leftJoin("tbl_link_category lcat", $joinText)
						->where($whereText)
						->queryAll();
				}
				$links = self::getLinksGrid($linkRecords, $hideDuplicated);

				if (isset($links))
					Yii::app()->session[$datasetKey] = $links;
				else
					Yii::app()->session[$datasetKey] = null;
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
			$isAdmin = false;
			$userId = null;
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				if (isset(Yii::app()->user->role))
					$isAdmin = Yii::app()->user->role == 2;
				else
					$isAdmin = true;
			}
			if (!$isAdmin && isset($userId))
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
					if (array_key_exists('enable_attachments', $linkRecord) && array_key_exists('enable_file_card', $linkRecord))
						$link['hasDetails'] = $linkRecord['enable_attachments'] | $linkRecord['enable_file_card'];

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
	}
