<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\Library as Library;

	/**
	 * Class DataTableHelper
	 */
	class DataTableHelper
	{
		/**
		 * @param $linkQueryResult array
		 * @param $extraColumns array
		 * @return array
		 */
		public static function formatRegularData($linkQueryResult, $extraColumns = array())
		{
			$dataset = array();
			if (count($linkQueryResult) > 0)
			{
				$libraryManager = new LibraryManager();
				foreach ($linkQueryResult as $linkRecord)
				{
					if (array_key_exists('type', $linkRecord))
						$type = $linkRecord['type'];
					else
						$type = 9999;

					$record['id'] = $linkRecord['id'];
					$record['name'] = nl2br($linkRecord['name']);

					$record['file_name'] = $linkRecord['file_name'];
					$record['path'] = $linkRecord['path'];
					$record['lib_name'] = $linkRecord['lib_name'];

					$record['tooltip'] = isset($linkRecord['file_name']) && $linkRecord['file_name'] != '' ? $linkRecord['file_name'] : $linkRecord['name'];
					if (isset($linkRecord['format']) && $linkRecord['format'] != 'other')
						$formatKey = $linkRecord['format'];
					else
						$formatKey = $linkRecord['file_extension'];
					if (isset($formatKey) && array_key_exists($formatKey, \Yii::app()->params['tooltips']['wallbin']))
						$record['tooltip'] = $record['tooltip'] . PHP_EOL . \Yii::app()->params['tooltips']['wallbin'][$formatKey];

					$record['date'] = array(
						'display' => $type != 6 ? date(Yii::app()->params['outputDateFormat'], strtotime($linkRecord['link_date'])) : '',
						'value' => strtotime($linkRecord['link_date'])
					);

					/** @var $library Library */
					$library = $libraryManager->getLibraryById($linkRecord['id_library']);
					$record['library'] = array(
						'id' => $linkRecord['id_library'],
						'name' => isset($library) ? $library->name : ''
					);

					$record['tag'] = $linkRecord['tag'];

					$record['views'] = array(
						'value' => intval($linkRecord['total_views']),
						'display' => $linkRecord['total_views'] == 0 ? '' : $linkRecord['total_views']
					);

					$record['rate'] = array(
						'value' => $linkRecord['rate'],
						'image' => LinkRateRecord::getStarImage(floatval($linkRecord['rate']))
					);

					switch ($linkRecord['format'])
					{
						case 'video':
						case 'wmv':
						case 'mp4':
							$record['file_type'] = 'video';
							break;
						case 'quicksite':
							$record['file_type'] = $linkRecord['format'];
							break;
						default:
							if ($type == 5)
								$record['file_type'] = 'folder';
							if ($type == 8)
								$record['file_type'] = 'url';
							else
								$record['file_type'] = $linkRecord['format'];
							break;
					}

					$fileInfo = FileInfo::fromLinkData(
						$record['id'],
						$type,
						$linkRecord['name'],
						$linkRecord['path'],
						BaseLinkSettings::createByContent($linkRecord['extended_properties']),
						$library);
					$record['isFile'] = $fileInfo->isFile;

					$extendedProperties = BaseLinkSettings::createByContent($linkRecord['extended_properties']);
					$isHyperlink = \application\models\wallbin\models\web\LibraryLink::isOpenedAsHyperlink($type, $extendedProperties);
					$record['isHyperlink'] = $isHyperlink;
					if ($isHyperlink)
					{
						$record['url_header'] = 'URL';
						$record['url'] = $fileInfo->link;

						$currentDomainInfo = parse_url(\Yii::app()->getBaseUrl(true));
						$urlInfo = parse_url($fileInfo->link);

						$domainHost = $currentDomainInfo['host'];
						$urlHost = array_key_exists('host', $urlInfo) ? $urlInfo['host'] : null;
						$urlPath = array_key_exists('path', $urlInfo) ? strtolower($urlInfo['path']) : null;

						$record['isExternalHyperlink'] = $domainHost != $urlHost ||
							(isset($urlPath) &&
								(strpos($urlPath, 'qpage') ||
									strpos($urlPath, 'public_links') ||
									strpos($urlPath, 'getSinglePage')));
					}
					else if ($fileInfo->isFile)
					{
						$record['url_header'] = 'DownloadURL';
						$record['url'] = FileInfo::getFileMIME($linkRecord['format']) . ':' .
							(isset($fileInfo->name) ? $fileInfo->name : $linkRecord['file_name']) . ':' .
							str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', Yii::app()->getBaseUrl(true) . $fileInfo->link);
					}

					$record['isDraggable'] = $fileInfo->isFile || $isHyperlink;

					$record['extended_data'] = array();
					foreach ($extraColumns as $column)
						$record['extended_data'][$column] = $linkRecord[$column];

					$dataset[] = $record;
				}
			}
			return $dataset;
		}

		/**
		 * @param $linkQueryResult array
		 * @param $extraColumns array
		 * @return array
		 */
		public static function formatExtendedData($linkQueryResult, $extraColumns)
		{
			return self::formatRegularData($linkQueryResult, $extraColumns);
		}

		/**
		 * @param $from string
		 * @param array $customQueryFields
		 * @param array $customJoin
		 * @param array $customWhereConditions
		 * @param string $categoryWhereCondition
		 * @param array $groupFields
		 * @return CDbCommand
		 */
		public static function buildQuery(
			$from,
			$customQueryFields,
			$customJoin,
			$customWhereConditions,
			$categoryWhereCondition,
			$groupFields)
		{
			if (!isset($from))
				$from = 'tbl_link link';
			if (!isset($customQueryFields))
				$customQueryFields = array();
			if (!isset($customJoin))
				$customJoin = array();
			if (!isset($customWhereConditions))
				$customWhereConditions = array();
			if (!isset($categoryWhereCondition))
				$categoryWhereCondition = '1=1';
			if (!isset($groupFields))
				$groupFields = array('link.id');

			/** @var CDbCommand $dbCommand */
			$dbCommand = Yii::app()->db->createCommand();

			$queryFields = array(
				'id' => 'max(link.id) as id',
				'id_library' => 'max(link.id_library) as id_library',
				'name' => 'max(link.name) as name',
				'type' => 'max(link.type) as type',
				'lib_name' => 'max(lib.name) as lib_name',
				'path' => 'link.file_relative_path as path',
				'file_name' => 'link.file_name as file_name',
				'file_extension' => 'link.file_extension as file_extension',
				'link_date' => 'max(link.file_date) as link_date',
				'format' => 'max(link.search_format) as format',
				'extended_properties' => 'max(link.settings) as extended_properties',
				'rate' => '(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate',
				'tag' => 'glcat.tag as tag',
				'total_views' => '(select sum(aggr.link_views) from
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
				           ) aggr where aggr.link_id=link.id and link.type<>6) as total_views',
			);
			$queryFields = array_merge($queryFields, $customQueryFields);

			$joinParts = array(
				'tbl_library lib' => 'lib.id=link.id_library'
			);
			$joinParts = array_merge($customJoin, $joinParts);

			$whereConditions = array('AND',
				'link.is_preview_not_ready=0');
			$includeAppLinks = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
			if ($includeAppLinks)
				$whereConditions[] = 'link.type<>15';
			$whereConditions = array_merge($whereConditions, $customWhereConditions);

			$dbCommand = $dbCommand->select(array_values($queryFields));
			$dbCommand = $dbCommand->from($from);
			foreach ($joinParts as $table => $condition)
				$dbCommand = $dbCommand->join($table, $condition);
			$dbCommand = $dbCommand->leftJoin("(select lcat.id_link, group_concat(lcat.tag separator ', ') as tag from tbl_link_category lcat where " . $categoryWhereCondition . " group by lcat.id_link) glcat", 'glcat.id_link=link.id');
			$dbCommand = $dbCommand->where($whereConditions);
			$dbCommand = $dbCommand->group($groupFields);
			return $dbCommand;
		}
	}