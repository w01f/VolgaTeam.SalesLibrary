<?
	namespace application\models\data_query\link_feed;

	use application\models\data_query\common\QuerySettings;
	use application\models\wallbin\models\web\LibraryManager;

	/**
	 * Class LinkFeedManager
	 */
	class LinkFeedQueryHelper
	{
		/**
		 * @param LinkFeedQuerySettings $feedSettings
		 * @return LinkFeedItem[]
		 * @throws \Exception
		 */
		public static function queryFeedItems($feedSettings)
		{
			switch ($feedSettings->feedType)
			{
				case LinkFeedQuerySettings::FeedTypeTrending:
					/**@var TrendingFeedQuerySettings $feedSettings */
					return self::queryTrendingItems($feedSettings);
				case LinkFeedQuerySettings::FeedTypeSearch:
					/**@var SearchFeedQuerySettings $feedSettings */
					return self::querySearchItems($feedSettings);
				case LinkFeedQuerySettings::FeedTypeSpecificLinks:
					/**@var SpecificLinkFeedQuerySettings $feedSettings */
					return self::querySpecificLinkItems($feedSettings);
				default:
					throw new \Exception('Undefined feed type');
			}
		}

		/**
		 * @param TrendingFeedQuerySettings $feedSettings
		 * @return LinkFeedItem[]
		 */
		private static function queryTrendingItems($feedSettings)
		{
			$feedItems = array();

			$today = date(\Yii::app()->params['mysqlDateFormat']);
			$startDate = $today;
			$endDate = date(\Yii::app()->params['mysqlDateFormat'], strtotime($today . ' + 1 days'));
			switch ($feedSettings->dateRangeType)
			{
				case "today":
					$startDate = $today;
					break;
				case "week":
					$startDate = date(\Yii::app()->params['mysqlDateFormat'], strtotime('last monday', strtotime('tomorrow')));
					break;
				case "month":
					$startDate = date(\Yii::app()->params['mysqlDateFormat'], strtotime(date('Y-m-1')));
					break;
			}

			$queryFormats = array();
			foreach ($feedSettings->linkFormats as $linkFormat)
				switch ($linkFormat)
				{
					case LinkFeedQuerySettings::LinkFormatPowerPoint:
					case LinkFeedQuerySettings::LinkFormatVideo:
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
						$queryFormats[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$queryFormats[] = implode(',', array(LinkFeedQuerySettings::LinkFormatWord, LinkFeedQuerySettings::LinkFormatPdf));
						break;
				}
			$formatsCondition = sprintf("%s", implode(",", $queryFormats));

			$librariesCondition = null;
			if (count($feedSettings->libraries) > 0)
				$librariesCondition = sprintf("%s", implode(",", $feedSettings->libraries));

			$command = \Yii::app()->db->createCommand("call sp_get_trending_links(:start_date,:end_date,:formats,:libraries,:max_links,:thumbnail_mode)");
			$command->bindValue(":start_date", $startDate, \PDO::PARAM_STR);
			$command->bindValue(":end_date", $endDate, \PDO::PARAM_STR);
			$command->bindValue(":formats", $formatsCondition, \PDO::PARAM_STR);
			$command->bindValue(":libraries", $librariesCondition, \PDO::PARAM_STR);
			$command->bindValue(":max_links", $feedSettings->maxLinks, \PDO::PARAM_INT);
			$command->bindValue(":thumbnail_mode", $feedSettings->thumbnailMode, \PDO::PARAM_STR);
			$resultRecords = $command->queryAll();

			$libraryManager = new LibraryManager();

			foreach ($resultRecords as $resultRecord)
			{
				$feedItem = new LinkFeedItem();
				$feedItem->linkId = $resultRecord['link_id'];
				$feedItem->linkName = $resultRecord['link_name'];
				$feedItem->format = $resultRecord['link_format'];
				$feedItem->libraryName = $resultRecord['lib_name'];
				$feedItem->viewsCount = $resultRecord['link_views'];

				$libraryId = $resultRecord['lib_id'];
				$library = $libraryManager->getLibraryById($libraryId);
				$thumbnailRelativePath = $resultRecord['thumbnail'];
				$feedItem->thumbnail = \Utils::formatUrl($library->storageLink . '//' . $thumbnailRelativePath);

				$feedItems[] = $feedItem;
			}

			return $feedItems;
		}


		/**
		 * @param SearchFeedQuerySettings $feedSettings
		 * @return LinkFeedItem[]
		 */
		private static function querySearchItems($feedSettings)
		{
			$feedItems = array();

			$feedSettings->conditions->fileTypes = array();
			foreach ($feedSettings->linkFormats as $linkFormat)
				switch ($linkFormat)
				{
					case LinkFeedQuerySettings::LinkFormatPowerPoint:
					case LinkFeedQuerySettings::LinkFormatVideo:
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
						$feedSettings->conditions->fileTypes[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$feedSettings->conditions->fileTypes[] = LinkFeedQuerySettings::LinkFormatWord;
						$feedSettings->conditions->fileTypes[] = LinkFeedQuerySettings::LinkFormatPdf;
						break;
				}

			$feedSettings->conditions->limit = $feedSettings->maxLinks;

			$resultRecords = \SearchHelper::queryLinksByCondition($feedSettings->conditions);

			$libraryManager = new LibraryManager();

			foreach ($resultRecords as $resultRecord)
			{
				if (!empty($resultRecord['thumbnail']))
				{
					$trendingLink = new LinkFeedItem();
					$trendingLink->linkId = $resultRecord['id'];
					$trendingLink->linkName = $resultRecord['name'];
					$trendingLink->format = $resultRecord['original_format'];
					$trendingLink->libraryName = $resultRecord['library_name'];
					$trendingLink->viewsCount = $resultRecord['total_views'];

					$libraryId = $resultRecord['id_library'];
					$library = $libraryManager->getLibraryById($libraryId);
					$thumbnailRelativePath = $resultRecord['thumbnail'];
					$trendingLink->thumbnail = \Utils::formatUrl($library->storageLink . '//' . $thumbnailRelativePath);

					$feedItems[] = $trendingLink;
				}
			}

			return $feedItems;
		}

		/**
		 * @param SpecificLinkFeedQuerySettings $feedSettings
		 * @return LinkFeedItem[]
		 */
		private static function querySpecificLinkItems($feedSettings)
		{
			$feedItems = array();

			$thumbnailCondition = null;
			switch ($feedSettings->thumbnailMode)
			{
				case LinkFeedQuerySettings::ThumbnailModeRandom:
					$thumbnailCondition = 'order by rand() limit 1';
					break;
				default:
					$thumbnailCondition = 'order by pv.relative_path limit 1';
					break;
			}

			/** @var \CDbCommand $dbCommand */
			$dbCommand = \Yii::app()->db->createCommand();

			$dbCommand = $dbCommand->from('tbl_link link');

			$dbCommand = $dbCommand->select(array(
				'id' => 'link.id as id',
				'id_library' => 'link.id_library as id_library',
				'library_name' => 'lib.name as library_name',
				'name' => 'link.name as name',
				'original_format' => 'link.original_format as original_format',
				'link_date' => 'link.file_date as link_date',
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
						           ) aggr where aggr.link_id=link.id and link.type<>6
								) as total_views',
				'thumbnail' => "case 
							when link.original_format='jpeg' or link.original_format='gif' or link.original_format='png' then
								link.file_relative_path
							when link.original_format='video' then
								(select pv.relative_path from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' " . $thumbnailCondition . ")										
							when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
								(select pv.relative_path from tbl_preview pv where pv.id_container=link.id_preview and pv.type='png_phone' " . $thumbnailCondition . ")
							when link.original_format='link bundle' then
								(select pv.relative_path from tbl_preview pv join tbl_link child_link on child_link.id_preview=pv.id_container join tbl_link_bundle lb on lb.id_link=child_link.id where lb.id_bundle=link.id and lb.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') " . $thumbnailCondition . ")
							end as thumbnail"
			));

			$dbCommand = $dbCommand->join('tbl_folder f', 'f.id=link.id_folder');
			$dbCommand = $dbCommand->join('tbl_page p', 'p.id=f.id_page');
			$dbCommand = $dbCommand->join('tbl_library lib', 'lib.id=p.id_library');

			$queryFormats = array();
			foreach ($feedSettings->linkFormats as $linkFormat)
				switch ($linkFormat)
				{
					case LinkFeedQuerySettings::LinkFormatPowerPoint:
					case LinkFeedQuerySettings::LinkFormatVideo:
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
						$queryFormats[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatWord;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatPdf;
						break;
				}

			$linkConditions = array();
			$linkConditions[] = 'or';
			foreach ($feedSettings->linkConditions as $linkCondition)
				$linkConditions[] = "(trim(link.file_name)='" . $linkCondition->linkName . "' or trim(link.name)='" . $linkCondition->linkName . "') and trim(f.name)='" . $linkCondition->folderName . "' and trim(p.name)='" . $linkCondition->pageName . "' and trim(lib.name)='" . $linkCondition->libraryName . "'";

			$dbCommand = $dbCommand->where(array(
				'and',
				sprintf('link.search_format in (\'%s\')', implode("','", $queryFormats)),
				$linkConditions
			));

			$sortFiled = 'link.name';
			switch ($feedSettings->sortSettings->columnTag)
			{
				case QuerySettings::DataTagDate:
					$sortFiled = 'link_date';
					break;
				case QuerySettings::DataTagFileName:
					$sortFiled = 'name';
					break;
				case QuerySettings::DataTagViewsCount:
					$sortFiled = 'total_views';
					break;
			}
			$dbCommand = $dbCommand->order(sprintf("%s %s", $sortFiled, $feedSettings->sortSettings->order));
			$resultRecords = $dbCommand->queryAll();

			$libraryManager = new LibraryManager();

			foreach ($resultRecords as $resultRecord)
			{
				if (!empty($resultRecord['thumbnail']))
				{
					$trendingLink = new LinkFeedItem();
					$trendingLink->linkId = $resultRecord['id'];
					$trendingLink->linkName = $resultRecord['name'];
					$trendingLink->format = $resultRecord['original_format'];
					$trendingLink->libraryName = $resultRecord['library_name'];
					$trendingLink->viewsCount = $resultRecord['total_views'];

					$libraryId = $resultRecord['id_library'];
					$library = $libraryManager->getLibraryById($libraryId);
					$thumbnailRelativePath = $resultRecord['thumbnail'];
					$trendingLink->thumbnail = \Utils::formatUrl($library->storageLink . '//' . $thumbnailRelativePath);

					$feedItems[] = $trendingLink;
				}
			}

			return $feedItems;
		}
	}