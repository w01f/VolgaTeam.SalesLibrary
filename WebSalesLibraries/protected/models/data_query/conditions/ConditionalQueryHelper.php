<?

	namespace application\models\data_query\conditions;

	use application\models\data_query\data_table\DataTableQueryHelper;
	use application\models\data_query\data_table\DataTableQuerySettings;
	use application\models\data_query\data_table\DataTableFormatHelper;

	/**
	 * Class ConditionalQueryHelper
	 */
	class ConditionalQueryHelper
	{
		/**
		 * @param $condition string
		 * @param $exactMatch boolean
		 * @return array
		 */
		public static function prepareTextCondition($condition, $exactMatch)
		{
			$result = array();
			if (empty($condition)) return $result;

			$conditionArray = is_array($condition) ? $condition : array($condition);

			$configPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'search_config.xml';
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
						$config = new \DOMDocument();
						$config->loadXML($configContent);
						$xpath = new \DomXPath($config);

						$conditionPart = preg_replace('!\s+!', ' ', $conditionPart);
						$conditionToCompare = $conditionPart;

						/** @var $queryResult \DOMNodeList */
						$queryResult = $xpath->query('//Config/Ignore');
						foreach ($queryResult as $node)
						{
							/** @var $node \DomElement */
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
							/** @var $node \DomElement */
							$target = strtolower(trim($groupName = $node->getAttribute('Target')));
							$aliasNodes = $node->getElementsByTagName('Item');

							$useAsAlias = false;
							if ($target === strtolower($conditionPart))
							{
								$useAsAlias = true;
							}
							else
							{
								foreach ($aliasNodes as $aliasNode)
								{
									$aliasValue = strtolower(trim($aliasNode->nodeValue));
									if ($aliasValue === strtolower($conditionPart))
									{
										$useAsAlias = true;
										$result[] = sprintf('"%s"', $target);
										break;
									}
								}
							}

							if ($useAsAlias)
							{
								foreach ($aliasNodes as $aliasNode)
									$result[] = sprintf('"%s"', str_replace($conditionPart, trim($aliasNode->nodeValue), $conditionPart));
							}
						}
					}
				}
				catch (\Exception $e)
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
		 * @param $queryConditions BaseQueryConditions
		 * @param $datasetKey string
		 * @return array
		 */
		public static function getDatasetByCondition($queryConditions, $datasetKey)
		{
			$linksEncoded = \Yii::app()->session[$datasetKey];
			if (!isset($linksEncoded))
			{
				$queryRecords = self::queryLinksByCondition($queryConditions, true);
				$links = DataTableFormatHelper::formatRegularData($queryRecords, $queryConditions->columnSettings);
				\Yii::app()->session[$datasetKey] = \CJSON::encode($links);
			}
			else
				$links = \CJSON::decode($linksEncoded);
			return $links;
		}

		/**
		 * @param $queryConditions BaseQueryConditions
		 * @param boolean $usePermissionFilter
		 * @return array
		 */
		public static function queryLinksByCondition($queryConditions, $usePermissionFilter)
		{
			$baseLinksEncoded = !(\Yii::app() instanceof \CConsoleApplication) ? \Yii::app()->session[$queryConditions->baseDatasetKey] : null;
			$baseLinksCondition = '1=1';
			if (!empty($baseLinksEncoded))
			{
				$baseLinks = \CJSON::decode($baseLinksEncoded);
				$availableLinkIds = array();
				foreach ($baseLinks as $baseLink)
					$availableLinkIds[] = $baseLink['id'];
				if (count($availableLinkIds) > 0)
					$baseLinksCondition = sprintf("link.id in ('%s')",
						implode("','", $availableLinkIds));
			}

			$contentMatchCondition = 'link.name,link.file_name,link.tags,link.content';
			if ($queryConditions->onlyByName)
				$contentMatchCondition = 'link.name,link.file_name,link.tags';

			$folderCondition = '1 <> 1';
			$isAdmin = \UserIdentity::isUserAdmin();
			$notAuthorized = !\UserIdentity::isUserAuthorized();
			if (!$usePermissionFilter || $isAdmin || $notAuthorized)
				$folderCondition = '1 = 1';
			else
			{
				$userId = \UserIdentity::getCurrentUserId();
				$assignedPageIds = \UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);
				if (count($assignedPageIds) > 0)
				{
					$folderCondition = sprintf("link.id_folder in (select id from tbl_folder where id_page in ('%s'))",
						implode("', '", $assignedPageIds));
				}
			}

			$excludeLinkCondition = '1=1';
			if (isset($queryConditions->excludeQueryConditions) && count($queryConditions->excludeQueryConditions->linkConditions) > 0)
			{
				$linkConditions = array();
				foreach ($queryConditions->excludeQueryConditions->linkConditions as $linkCondition)
					$linkConditions[] = "(trim(link.file_name)='" . $linkCondition->linkName . "' or trim(link.name)='" . $linkCondition->linkName . "') and trim(f.name)='" . $linkCondition->folderName . "' and trim(p.name)='" . $linkCondition->pageName . "' and trim(lib.name)='" . $linkCondition->libraryName . "'";

				$excludeLinkCondition = sprintf("NOT (%s)", implode(' OR ', $linkConditions));
			}

			$excludeContentCondition = '1 = 1';
			if (isset($queryConditions->excludeQueryConditions) && !empty($queryConditions->excludeQueryConditions->text))
			{
				$textExcludeConditions = self::prepareTextCondition($queryConditions->excludeQueryConditions->text, $queryConditions->excludeQueryConditions->textExactMatch);
				$conditionParts = array();
				foreach ($textExcludeConditions as $contentConditionPart)
					$conditionParts[] = sprintf("(not match(%s) against('%s' in boolean mode))", $contentMatchCondition, str_replace("'", "\'", $contentConditionPart));
				$excludeContentCondition = implode(" and ", $conditionParts);
			}

			$excludeCategoryCondition = '1=1';
			if (isset($queryConditions->excludeQueryConditions) && count($queryConditions->excludeQueryConditions->categories) > 0)
			{
				$categoryConditions = array();
				foreach ($queryConditions->excludeQueryConditions->categories as $category)
					foreach ($category->items as $categoryItem)
						$categoryConditions[] = sprintf('(link.id in (select id_link from tbl_link_category where category = "%s" and tag = "%s"))',
							$category->name,
							$categoryItem);
				$excludeCategoryCondition = sprintf('NOT (%s)',
					implode(' or ', $categoryConditions));
			}

			$excludeLibraryCondition = '1 = 1';
			if (isset($queryConditions->excludeQueryConditions) && count($queryConditions->excludeQueryConditions->libraries) > 0)
			{
				$libraryIds = array();
				foreach ($queryConditions->excludeQueryConditions->libraries as $library)
					$libraryIds[] = $library->id;
				$excludeLibraryCondition = sprintf("link.id_library not in ('%s')",
					implode("','", $libraryIds));
			}

			$hideLinksWithinBundlesCondition = "1 = 1";
			if ($queryConditions->hideLinksWithinBundle)
				$hideLinksWithinBundlesCondition = "link.id not in (select hide_lb.id_link from tbl_link_bundle hide_lb)";

			$groupConditions = self::getQueryGroupItemCondition($queryConditions, $queryConditions->dateSettings);

			$whereConditions = array(
				'link.type not in (5,6)',
				$baseLinksCondition,
				$folderCondition,
				$hideLinksWithinBundlesCondition,
				$excludeLinkCondition,
				$excludeContentCondition,
				$excludeCategoryCondition,
				$excludeLibraryCondition,
				$groupConditions['where']
			);

			$querySettings = DataTableQuerySettings::prepareQuery(
				array(
					DataTableQuerySettings::SettingsTagWhere => $whereConditions,
					DataTableQuerySettings::SettingsTagCategoryWhere => $groupConditions['categoryJoin'],
					DataTableQuerySettings::SettingsTagColumns => $queryConditions->columnSettings,
					DataTableQuerySettings::SettingsTagDate => $queryConditions->dateSettings,
					DataTableQuerySettings::SettingsTagCategory => $queryConditions->categorySettings,
					DataTableQuerySettings::SettingsTagViewsCount => $queryConditions->viewCountSettings,
					DataTableQuerySettings::SettingsTagThumbnails => $queryConditions->thumbnailSettings,
					DataTableQuerySettings::SettingsTagSort => $queryConditions->sortSettings,
					DataTableQuerySettings::SettingsTagLimit => $queryConditions->limit,
				));
			/** @var \CDbCommand $dbCommand */
			$dbCommand = DataTableQueryHelper::buildQuery($querySettings);
			$queryRecords = $dbCommand->queryAll();

			return $queryRecords;
		}

		/**
		 * @param $queryConditions QueryConditionGroupItem
		 * @param $dateSettings DateQuerySettings
		 * @return array
		 */
		private static function getQueryGroupItemCondition($queryConditions, $dateSettings)
		{
			$contentMatchCondition = 'link.name,link.file_name,link.tags,link.content';
			if ($queryConditions->onlyByName)
				$contentMatchCondition = 'link.name,link.file_name,link.tags';

			$textConditions = self::prepareTextCondition($queryConditions->text, $queryConditions->textExactMatch);

			$libraryCondition = '1 = 1';
			if (isset($queryConditions->libraries) && count($queryConditions->libraries) > 0)
			{
				$libraryIds = array();
				foreach ($queryConditions->libraries as $library)
					$libraryIds[] = $library->id;
				$libraryCondition = sprintf("link.id_library in ('%s')",
					implode("','", $libraryIds));
			}

			if (count($queryConditions->fileTypesInclude) > 0)
				$fileTypeIncludeCondition = sprintf("link.search_format in ('%s')", implode("','", $queryConditions->fileTypesInclude));
			else
				$fileTypeIncludeCondition = '1 = 1';

			if (count($queryConditions->fileTypesExclude) > 0)
			{
				$fileTypeExcludeConditionParts = array();
				$fileTypeExcludeConditionParts[] = sprintf("link.search_format not in ('%s')", implode("','", $queryConditions->fileTypesExclude));
				$fileTypeExcludeConditionParts[] = sprintf("link.file_extension not in ('%s')", implode("','", $queryConditions->fileTypesExclude));
				$fileTypeExcludeCondition = implode(" and ", $fileTypeExcludeConditionParts);
			}
			else
				$fileTypeExcludeCondition = '1 = 1';

			$dateCondition = '1 = 1';
			$additionalDateCondition = '';
			if (!empty($queryConditions->startDate) && !empty($queryConditions->endDate))
			{
				$dateColumn = sprintf('link.%s', DateQuerySettings::getDateColumnName($dateSettings->dateMode));
				$dateCondition = sprintf('%1$s >= \'%2$s\' and %1$s <= \'%3$s\'',
					$dateColumn,
					date(\Yii::app()->params['mysqlDateFormat'], strtotime($queryConditions->startDate)),
					date(\Yii::app()->params['mysqlDateFormat'], strtotime($queryConditions->endDate) + 86400));
				if (count($textConditions) == 0)
					$additionalDateCondition = sprintf(" or (%s)",
						$dateCondition);;
			}

			$superFilterCondition = '1 = 1';
			$additionalSuperFilterCondition = '';
			if (isset($queryConditions->superFilters) && count($queryConditions->superFilters) > 0)
			{
				foreach ($queryConditions->superFilters as $superFilter)
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
			if (isset($queryConditions->categories) && count($queryConditions->categories) > 0)
			{
				$categoryConditions = array();
				$categoriesJoinSelector = array();
				foreach ($queryConditions->categories as $category)
				{
					foreach ($category->items as $categoryItem)
					{
						$categoryConditions[] = sprintf('(link.id in (select id_link from tbl_link_category where category = "%s" and tag = "%s"))',
							$category->name,
							$categoryItem);
						$categoriesJoinSelector[] = sprintf('(lcat.id in (select id from tbl_link_category where category = "%s" and tag = "%s"))',
							$category->name,
							$categoryItem);
					}
				}
				$categoryCondition = sprintf('(%s)',
					implode(' or ', $categoryConditions));
				$categoryJoinCondition = sprintf('(%s)',
					implode(' or ', $categoriesJoinSelector));
				if (count($textConditions) == 0)
					$additionalCategoryCondition = sprintf(' or %s',
						$categoryCondition);

			}
			else if (count($textConditions) > 0 && !$queryConditions->onlyByName)
			{
				$categoryConditions = array();
				$categoriesJoinSelector = array();
				foreach ($textConditions as $contentConditionPart)
				{
					$conditionToCompare = strtolower(trim(str_replace('"', '', $contentConditionPart)));
					if ($queryConditions->textExactMatch)
					{
						$categoryConditions[] = '(link.id in (select id_link from tbl_link_category where lower(category) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]" or lower(tag) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]"))';
						$categoriesJoinSelector[] = '(lcat.id in (select id from tbl_link_category where lower(category) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]" or lower(tag) regexp "[[:<:]]' . $conditionToCompare . '[[:>:]]"))';
					}
					else
					{
						$categoryConditions[] = '(link.id in (select id_link from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
						$categoriesJoinSelector[] = '(lcat.id in (select id from tbl_link_category where lower(category) like "%' . $conditionToCompare . '%" or lower(tag) like "%' . $conditionToCompare . '%"))';
					}
				}
				$categoryCondition = sprintf('(%s)', implode(' or ', $categoryConditions));
				$additionalCategoryCondition = sprintf(' or %s', $categoryCondition);
				$categoryCondition = '1=1';
			}

			$onlyWithCategoriesCondition = '1 = 1';
			$additionalOnlyWithCategoriesCondition = '';
			if ($queryConditions->onlyWithCategories)
			{
				$onlyWithCategoriesCondition = 'exists (select id_link from tbl_link_category where id_link = link.id)';
				if (count($textConditions) == 0)
					$additionalOnlyWithCategoriesCondition = ' or (exists (select id_link from tbl_link_category where id_link = link.id))';
			}

			$contentCondition = "1=1";
			if (count($textConditions) > 0)
			{
				$conditionParts = array();
				foreach ($textConditions as $contentConditionPart)
					$conditionParts[] = sprintf("(match(%s) against('%s' in boolean mode))", $contentMatchCondition, str_replace("'", "\'", $contentConditionPart));
				$contentCondition = implode(" or ", $conditionParts);
			}
			$contentCondition = sprintf("(%s%s%s%s%s)",
				$contentCondition,
				$additionalDateCondition,
				$additionalSuperFilterCondition,
				$additionalCategoryCondition,
				$additionalOnlyWithCategoriesCondition);

			$groupConditions = '1 = 1';
			$groupsCategoryJoinCondition = array($categoryJoinCondition);
			if (count($queryConditions->conditionGroups) > 0)
			{
				$groupWheres = array();
				foreach ($queryConditions->conditionGroups as $conditionGroup)
				{
					$whereConditions = array();
					$joinConditions = array();
					foreach ($conditionGroup->conditionItems as $conditionItem)
					{
						$conditionItemResult = self::getQueryGroupItemCondition($conditionItem, $dateSettings);
						$whereConditions[] = $conditionItemResult['where'];
						$joinConditions[] = $conditionItemResult['categoryJoin'];
					}
					$groupWheres[] = implode(sprintf(" %s ", $conditionGroup->operator), $whereConditions);
					$groupsCategoryJoinCondition[] = implode(sprintf(" %s ", $conditionGroup->operator), $joinConditions);
				}
				$groupConditions = implode(" and ", $groupWheres);
			}

			$whereConditions = array(
				$contentCondition,
				$libraryCondition,
				$fileTypeIncludeCondition,
				$fileTypeExcludeCondition,
				$dateCondition,
				$categoryCondition,
				$superFilterCondition,
				$onlyWithCategoriesCondition,
				$groupConditions
			);

			return array(
				'where' => implode(" and ", $whereConditions),
				'categoryJoin' => implode(" and ", $groupsCategoryJoinCondition)
			);
		}
	}