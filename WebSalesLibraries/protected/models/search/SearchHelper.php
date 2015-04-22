<?php

	/**
	 * Class SearchHelper
	 */
	class SearchHelper
	{
		/**
		 * @param $condition
		 * @return array
		 */
		public static function prepareTextCondition($condition)
		{
			$result = array();
			if (!isset($condition)) return $result;

			$configPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'search_config.xml';
			if (!file_exists($configPath))
			{
				if ($condition != "" && $condition != '""')
					$result[] = $condition;
			}
			else
			{
				try
				{
					$configContent = file_get_contents($configPath);
					$config = new DOMDocument();
					$config->loadXML($configContent);
					$xpath = new DomXPath($config);

					$condition = preg_replace('!\s+!', ' ', $condition);
					$conditionToCompare = trim(str_replace('"', '', $condition));

					/** @var $queryResult DOMNodeList */
					$queryResult = $xpath->query('//Config/Ignore');
					foreach ($queryResult as $node)
					{
						/** @var $node DomElement */
						$ignoreValue = trim($node->nodeValue);
						if (strlen($conditionToCompare) == strlen($ignoreValue))
						{
							$condition = str_replace($ignoreValue, '', $condition);
							$condition = str_replace(strtolower($ignoreValue), '', $condition);
						}

						$ignoreValue = trim($node->nodeValue) . ' ';
						$condition = str_replace($ignoreValue, '', $condition);
						$condition = str_replace(strtolower($ignoreValue), '', $condition);

						$ignoreValue = ' ' . trim($node->nodeValue);
						$condition = str_replace($ignoreValue, '', $condition);
						$condition = str_replace(strtolower($ignoreValue), '', $condition);
					}

					if ($condition != "" && $condition != '""')
						$result[] = $condition;
					$conditionToCompare = trim(str_replace('"', '', $condition));
					$queryResult = $xpath->query('//Config/AliasFor');
					foreach ($queryResult as $node)
					{
						/** @var $node DomElement */
						$target = strtolower(trim($groupName = $node->getAttribute('Target')));
						if ($target != strtolower($conditionToCompare)) continue;
						$aliasNodes = $node->getElementsByTagName('Item');
						foreach ($aliasNodes as $aliasNode)
							$result[] = str_replace($conditionToCompare, trim($aliasNode->nodeValue), $condition);
					}
				} catch (Exception $e)
				{
				}
			}
			return $result;
		}

		/**
		 * @param $searchConditions SearchConditions
		 * @param $datasetKey string
		 * @return array
		 */
		public static function queryLinksByCondition($searchConditions, $datasetKey)
		{
			$linksEncoded = Yii::app()->session[$datasetKey];
			if (!isset($linksEncoded))
			{
				$baseLinksEncoded = Yii::app()->session[$searchConditions->baseDatasetKey];
				$baseLinksCondition = '1=1';
				if (isset($baseLinksEncoded))
				{
					$baseLinks = CJSON::decode($baseLinksEncoded);
					$availableLinkIds = array();
					foreach ($baseLinks as $baseLink)
						$availableLinkIds[] = $baseLink['id'];
					if (count($availableLinkIds) > 0)
						$baseLinksCondition = sprintf("link.id in ('%s')",
							implode("','", $availableLinkIds));
				}

				$originalTextCondition = $searchConditions->textExactMatch ? sprintf('"%s"', $searchConditions->text) : $searchConditions->text;
				$textConditions = self::prepareTextCondition($originalTextCondition);
				if (!(isset($baseLinks) ||
					(count($searchConditions->fileTypes) > 0 &&
						(count($textConditions) > 0 || count($searchConditions->categories) > 0 || count($searchConditions->superFilters) > 0 || count($searchConditions->superFilters) > 0 || (isset($searchConditions->startDate) && isset($searchConditions->endDate)))))
				)
					return array();

				$count = count($searchConditions->libraries);
				switch ($count)
				{
					case 0:
						$libraryCondition = '1 = 1';
						break;
					default:
						$libraryIds = array();
						foreach ($searchConditions->libraries as $library)
							$libraryIds[] = $library->id;
						$libraryCondition = sprintf("link.id_library in ('%s')",
							implode("','", $libraryIds));
						break;
				}

				$count = count($searchConditions->fileTypes);
				switch ($count)
				{
					case 0:
						$fileTypeCondition = '1 = 1';
						break;
					default:
						$fileTypeCondition = sprintf("link.format in ('%s')",
							implode("','", $searchConditions->fileTypes));;
						break;
				}

				$dateCondition = '1 = 1';
				$additionalDateCondition = '';
				if (isset($searchConditions->startDate) && isset($searchConditions->endDate) && $searchConditions->startDate != '' && $searchConditions->endDate != '')
				{
					$dateColumn = 'link.file_date';
					$dateCondition = sprintf('%1$s >= \'%2$s\' and %1$s <= \'%3$s\'',
						$dateColumn,
						date(Yii::app()->params['mysqlDateFormat'], strtotime($searchConditions->startDate)),
						date(Yii::app()->params['mysqlDateFormat'], strtotime($searchConditions->endDate) + 86400));
					if (count($textConditions) == 0)
						$additionalDateCondition = sprintf(" or (%s)",
							$dateCondition);;
				}

				$superFilterCondition = '1 = 1';
				$additionalSuperFilterCondition = '';
				if (count($searchConditions->superFilters) > 0)
				{
					foreach ($searchConditions->superFilters as $superFilter)
						$superFilterSelector[] = sprintf("(link.id in (select id_link from tbl_link_super_filter where value = '%s'))", $superFilter);
					if (isset($superFilterSelector))
					{
						$superFilterCondition = sprintf("(%s)",
							implode(' or ', $superFilterSelector));
						if (count($textConditions) == 0)
							$additionalSuperFilterCondition = sprintf(' or %s',
								$superFilterCondition);
					}
				}

				$categoryCondition = '1=1';
				$categoryJoinCondition = '1 = 1';
				$additionalCategoryCondition = '';
				if (count($searchConditions->categories) > 0)
				{
					$categoriesSelector = array();
					$categoriesJoinSelector = array();
					foreach ($searchConditions->categories as $category)
					{
						foreach ($category->items as $categoryItem)
						{
							$categoriesSelector[] = sprintf('(link.id in (select id_link from tbl_link_category where category = "%s" and tag = "%s"))',
								$category->name,
								$categoryItem);
							$categoriesJoinSelector[] = sprintf('(lcat.id in (select id from tbl_link_category where category = "%s" and tag = "%s"))',
								$category->name,
								$categoryItem);
						}
					}
					$categoryCondition = sprintf('(%s)',
						implode(' or ', $categoriesSelector));
					$categoryJoinCondition = sprintf('(%s)',
						implode(' or ', $categoriesJoinSelector));
					if (count($textConditions) == 0)
						$additionalCategoryCondition = sprintf(' or %s',
							$categoryCondition);

				}
				else if (count($textConditions) > 0)
				{
					$categoriesSelector = array();
					$categoriesJoinSelector = array();
					foreach ($textConditions as $contentConditionPart)
					{
						$conditionToCompare = strtolower(trim(str_replace('"', '', $contentConditionPart)));
						$categoriesSelector[] = '(link.id in (select id_link from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
						$categoriesJoinSelector[] = '(lcat.id in (select id from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
					}
					$categoryCondition = sprintf('(%s)',
						implode(' or ', $categoriesSelector));
					$additionalCategoryCondition = sprintf(' or %s',
						$categoryCondition);
					$categoryCondition = '1=1';
				}

				$onlyWithCategoriesCondition = '1 = 1';
				$additionalOnlyWithCategoriesCondition = '';
				if ($searchConditions->onlyWithCategories)
				{
					$onlyWithCategoriesCondition = 'exists (select id_link from tbl_link_category where id_link = link.id)';
					if (count($textConditions) == 0)
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
					$folderCondition = sprintf("link.id_folder in (select id from tbl_folder where id_page in ('%s'))",
						implode("', '", $assignedPageIds));
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
						$restrictedLinkConditions[] = sprintf("link.id in ('%s')",
							implode("', '", $availableLinkIds));

					$deniedLinkIds = LinkBlackListRecord::getDeniedLinks($userId);
					if (count($deniedLinkIds) > 0)
						$restrictedLinkConditions[] = sprintf("link.id not in ('%s')",
							implode("', '", $deniedLinkIds));

					$linkCondition = "link.is_restricted <> 1";
					if (count($restrictedLinkConditions) > 0)
						$linkCondition = sprintf("link.is_restricted <> 1 or (%s)",
							implode(" and ", $restrictedLinkConditions));
				}

				$matchCondition = 'link.name,link.file_name,link.tags,link.content';
				if ($searchConditions->onlyByName)
					$matchCondition = 'link.name,link.file_name';

				$contentCondition = "1=1";
				if (count($textConditions) > 0)
				{
					$conditionParts = array();
					foreach ($textConditions as $contentConditionPart)
						$conditionParts[] = sprintf("(match(%s) against('%s' in boolean mode))", $matchCondition, $contentConditionPart);
					$contentCondition = implode(" or ", $conditionParts);
				}
				$contentCondition = sprintf("(%s%s%s%s%s)",
					$contentCondition,
					$additionalDateCondition,
					$additionalSuperFilterCondition,
					$additionalCategoryCondition,
					$additionalOnlyWithCategoriesCondition);

				$dateField = 'max(link.file_date) as link_date';
				$selectText = 'max(link.id) as id,
							max(link.id_library) as id_library,
							max(link.name) as name,
							link.file_name,
							' . $dateField . ',
							max(link.format) as format,
							(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate,
							glcat.tag as tag';
				$joinText = "glcat.id_link=link.id";
				$whereText = $contentCondition .
					" and (" . $baseLinksCondition .
					") and (" . $libraryCondition .
					") and (" . $fileTypeCondition .
					") and (" . $dateCondition .
					") and (" . $categoryCondition .
					") and (" . $superFilterCondition .
					") and (" . $onlyWithCategoriesCondition .
					") and (" . $folderCondition .
					") and (" . $linkCondition .
					") and link.is_dead=0 and link.is_preview_not_ready=0";
				$queryRecords = Yii::app()->db->createCommand()
					->select($selectText)
					->from('tbl_link link')
					->leftJoin("(select lcat.id_link, group_concat(lcat.tag separator ', ') as tag from tbl_link_category lcat where " . $categoryJoinCondition . " group by lcat.id_link) glcat", $joinText)
					->where($whereText)
					->group('link.file_relative_path, glcat.tag')
					->queryAll();
				$links = self::formatLinksInfo($queryRecords);
				Yii::app()->session[$datasetKey] = CJSON::encode($links);
			}
			else
				$links = CJSON::decode($linksEncoded);
			return $links;
		}

		/**
		 * @param $linkQueryRecords array
		 * @return array
		 */
		private static function formatLinksInfo($linkQueryRecords)
		{
			$links = array();
			if (count($linkQueryRecords) > 0)
			{
				$libraryManager = new LibraryManager();
				foreach ($linkQueryRecords as $linkRecord)
				{
					if (array_key_exists('type', $linkRecord))
						$type = $linkRecord['type'];
					else
						$type = 9999;

					$link['id'] = $linkRecord['id'];
					$link['name'] = $linkRecord['name'];
					$link['file_name'] = $linkRecord['file_name'];
					$link['date'] = array(
						'display' => date(Yii::app()->params['outputDateFormat'], strtotime($linkRecord['link_date'])),
						'value' => strtotime($linkRecord['link_date'])
					);

					/** @var $library Library */
					$library = $libraryManager->getLibraryById($linkRecord['id_library']);
					$link['library'] = array(
						'id' => $linkRecord['id_library'],
						'name' => isset($library) ? $library->name : ''
					);

					$link['tag'] = $linkRecord['tag'];

					$link['rate'] = array(
						'value' => $linkRecord['rate'],
						'image' => LinkRateRecord::getStarImage(floatval($linkRecord['rate']))
					);

					switch ($linkRecord['format'])
					{
						case 'video':
						case 'wmv':
						case 'mp4':
							$link['file_type'] = 'video';
							break;
						default:
							if ($type == 5)
								$link['file_type'] = 'folder';
							else if ($type == 8)
								$link['file_type'] = 'url';
							else
								$link['file_type'] = $linkRecord['format'];
							break;
					}
					$links[] = $link;
				}
			}
			return $links;
		}
	}