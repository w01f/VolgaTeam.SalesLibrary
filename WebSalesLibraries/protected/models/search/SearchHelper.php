<?

	/**
	 * Class SearchHelper
	 */
	class SearchHelper
	{
		/**
		 * @param $condition
		 * @param $exactMatch boolean
		 * @return array
		 */
		public static function prepareTextCondition($condition, $exactMatch)
		{
			$result = array();
			if (!isset($condition) || $condition == '') return $result;

			$conditionArray = is_array($condition) ? $condition : array($condition);

			$configPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'search_config.xml';
			if (!file_exists($configPath))
			{
				if ($exactMatch)
				{
					foreach ($conditionArray as $conditionPart)
						$result[] = sprintf('"%s"', $conditionPart);
				}
				else
					$result = $conditionArray;
			}
			else
			{
				try
				{
					foreach ($conditionArray as $conditionArrayItem)
					{
						$conditionPart = $conditionArrayItem;

						$configContent = file_get_contents($configPath);
						$config = new DOMDocument();
						$config->loadXML($configContent);
						$xpath = new DomXPath($config);

						$conditionPart = preg_replace('!\s+!', ' ', $conditionPart);
						$conditionToCompare = $conditionPart;

						/** @var $queryResult DOMNodeList */
						$queryResult = $xpath->query('//Config/Ignore');
						foreach ($queryResult as $node)
						{
							/** @var $node DomElement */
							$ignoreValue = trim($node->nodeValue);
							if (strlen($conditionToCompare) == strlen($ignoreValue))
							{
								$conditionPart = str_replace($ignoreValue, '', $conditionPart);
								$conditionPart = str_replace(strtolower($ignoreValue), '', $conditionPart);
							}

							if (self::startsWith($conditionToCompare, $ignoreValue))
							{
								$conditionPart = str_replace($ignoreValue . ' ', '', $conditionPart);
								$conditionPart = str_replace(strtolower($ignoreValue) . ' ', '', $conditionPart);
							}

							if (self::endsWith($conditionToCompare, $ignoreValue))
							{
								$conditionPart = str_replace(' ' . $ignoreValue, '', $conditionPart);
								$conditionPart = str_replace(' ' . strtolower($ignoreValue), '', $conditionPart);
							}

							$ignoreValue = ' ' . trim($node->nodeValue) . ' ';
							$conditionPart = str_replace($ignoreValue, ' ', $conditionPart);
							$conditionPart = str_replace(strtolower($ignoreValue), ' ', $conditionPart);
						}

						if ($conditionPart != "")
							$result[] = $exactMatch ? sprintf('"%s"', $conditionPart) : $conditionPart;
						$queryResult = $xpath->query('//Config/AliasFor');
						foreach ($queryResult as $node)
						{
							/** @var $node DomElement */
							$target = strtolower(trim($groupName = $node->getAttribute('Target')));
							if ($target != strtolower($conditionPart)) continue;
							$aliasNodes = $node->getElementsByTagName('Item');
							foreach ($aliasNodes as $aliasNode)
								$result[] = sprintf('"%s"', str_replace($conditionPart, trim($aliasNode->nodeValue), $conditionPart));
						}
					}
				}
				catch (Exception $e)
				{
				}
			}
			return $result;
		}

		private static function startsWith($haystack, $needle)
		{
			// search backwards starting from haystack length characters from the end
			return $needle === "" || strrpos($haystack, $needle, -strlen($haystack)) !== FALSE;
		}

		private static function endsWith($haystack, $needle)
		{
			// search forward starting from end minus needle length characters
			return $needle === "" || (($temp = strlen($haystack) - strlen($needle)) >= 0 && strpos($haystack, $needle, $temp) !== FALSE);
		}

		/**
		 * @param $searchConditions SearchConditions
		 * @param $datasetKey string
		 * @return array
		 */
		public static function getDatasetByCondition($searchConditions, $datasetKey)
		{
			$linksEncoded = Yii::app()->session[$datasetKey];
			if (!isset($linksEncoded))
			{
				$queryRecords = self::queryLinksByCondition($searchConditions);
				$links = DataTableHelper::formatRegularData($queryRecords);
				Yii::app()->session[$datasetKey] = CJSON::encode($links);
			}
			else
				$links = CJSON::decode($linksEncoded);
			return $links;
		}

		/**
		 * @param $searchConditions SearchConditions
		 * @return array
		 */
		public static function queryLinksByCondition($searchConditions)
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

			$textConditions = self::prepareTextCondition($searchConditions->text, $searchConditions->textExactMatch);
			if (!(isset($baseLinks) ||
				(count($textConditions) > 0 || count($searchConditions->categories) > 0 || count($searchConditions->superFilters) > 0 || count($searchConditions->superFilters) > 0 || (isset($searchConditions->startDate) && isset($searchConditions->endDate))))
			)
				return array();

			$libraryCondition = '1 = 1';
			if (count($searchConditions->libraries) > 0)
			{
				$libraryIds = array();
				foreach ($searchConditions->libraries as $library)
					$libraryIds[] = $library->id;
				$libraryCondition = sprintf("link.id_library in ('%s')",
					implode("','", $libraryIds));
			}

			$count = count($searchConditions->fileTypes);
			switch ($count)
			{
				case 0:
					$fileTypeCondition = '1 = 1';
					break;
				default:
					$fileTypeCondition = sprintf("link.search_format in ('%s')",
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
			if (isset($searchConditions->categories) && count($searchConditions->categories) > 0)
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
			else if (count($textConditions) > 0 && !$searchConditions->onlyByName)
			{
				$categoriesSelector = array();
				$categoriesJoinSelector = array();
				foreach ($textConditions as $contentConditionPart)
				{
					$conditionToCompare = strtolower(trim(str_replace('"', '', $contentConditionPart)));
					if ($searchConditions->textExactMatch)
					{
						$categoriesSelector[] = '(link.id in (select id_link from tbl_link_category where lower(category) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]" or lower(tag) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]"))';
						$categoriesJoinSelector[] = '(lcat.id in (select id from tbl_link_category where lower(category) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]" or lower(tag) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]"))';
					}
					else
					{
						$categoriesSelector[] = '(link.id in (select id_link from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
						$categoriesJoinSelector[] = '(lcat.id in (select id from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
					}
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
			$isAdmin = UserIdentity::isUserAdmin();
			if ($isAdmin)
				$folderCondition = '1 = 1';
			else
			{
				$userId = UserIdentity::getCurrentUserId();
				$assignedPageIds = UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);
				if (count($assignedPageIds) > 0)
				{
					$folderCondition = sprintf("link.id_folder in (select id from tbl_folder where id_page in ('%s'))",
						implode("', '", $assignedPageIds));
				}
			}

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
				$matchCondition = 'link.name,link.file_name,link.tags';

			$includeAppLinks = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;

			$contentCondition = "1=1";
			if (count($textConditions) > 0)
			{
				$conditionParts = array();
				foreach ($textConditions as $contentConditionPart)
					$conditionParts[] = sprintf("(match(%s) against('%s' in boolean mode))", $matchCondition, str_replace("'", "\'", $contentConditionPart));
				$contentCondition = implode(" or ", $conditionParts);
			}
			$contentCondition = sprintf("(%s%s%s%s%s)",
				$contentCondition,
				$additionalDateCondition,
				$additionalSuperFilterCondition,
				$additionalCategoryCondition,
				$additionalOnlyWithCategoriesCondition);

			$selectText = 'max(link.id) as id,
							max(link.id_library) as id_library,
							max(link.name) as name,
							max(link.type) as type,
							max(lib.name) as lib_name,
							link.file_relative_path as path,
							link.file_name as file_name,
							link.file_extension as file_extension,
							max(link.file_date) as link_date,
							max(link.search_format) as format,
							max(link.settings) as extended_properties,
							(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate,
							glcat.tag as tag,
						   (select sum(aggr.link_views) from
				           (select
				              s_l.id_link as link_id,
				              count(s_l.id) as link_views
				            from tbl_statistic_link s_l
				            group by s_l.id_link
				            union
				            select
				              l_q.id_link as link_id,
				              count(s_q.id) as link_views
				            from tbl_statistic_qpage s_q
				              join tbl_link_qpage l_q on l_q.id_qpage = s_q.id_qpage
				            group by l_q.id_link
				           ) aggr where aggr.link_id=link.id) as total_views';

			$whereConditions = array(
				$contentCondition,
				$baseLinksCondition,
				$libraryCondition,
				$fileTypeCondition,
				$dateCondition,
				$categoryCondition,
				$superFilterCondition,
				$onlyWithCategoriesCondition,
				$folderCondition,
				$linkCondition
			);

			/** @var CDbCommand $dbCommnad */
			$dbCommnad = DataTableHelper::buildQuery(
				null,
				null,
				null,
				$whereConditions,
				$categoryJoinCondition,
				null);
			$queryRecords = $dbCommnad->queryAll();

			return $queryRecords;
		}
	}