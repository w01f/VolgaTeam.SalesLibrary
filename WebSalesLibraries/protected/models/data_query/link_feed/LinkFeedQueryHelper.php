<?

	namespace application\models\data_query\link_feed;

	use application\models\data_query\conditions\ConditionalQueryHelper;
	use application\models\data_query\conditions\ThumbnailQuerySettings;
	use application\models\data_query\data_table\DataTableQuerySettings;

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

			$queryFormats = array();
			foreach ($feedSettings->linkFormats as $linkFormat)
				switch ($linkFormat)
				{
					case LinkFeedQuerySettings::LinkFormatPowerPoint:
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
					case LinkFeedQuerySettings::LinkFormatUrl:
						$queryFormats[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatWord;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatPdf;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatUrl;
						break;
					case LinkFeedQuerySettings::LinkFormatVideo:
						$queryFormats[] = $linkFormat;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVimeo;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVideo;
						break;
				}

			$statisticRangeCondition = '';
			switch ($feedSettings->dateRangeType)
			{
				case "today":
					$statisticRangeCondition = sprintf("sa.date_time>='%s'", date(\Yii::app()->params['mysqlDateFormat']));
					break;
				case "week":
					$statisticRangeCondition = "yearweek(sa.date_time, 1) = yearweek(curdate(), 1)";
					break;
				case "month":
					$statisticRangeCondition = "year(sa.date_time) = year(curdate()) and month(sa.date_time) = month(curdate())";
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
				'path' => 'link.file_relative_path as path',
				'file_name' => 'link.file_name as file_name',
				'search_format' => 'link.search_format as search_format',
				'total_views' => 'link_views_set.link_views as total_views',
				'thumbnail' => sprintf("case when '%s' = 0
							        then (case
							              when link.original_format='jpeg' or link.original_format='gif' or link.original_format='png' then
							                concat(lib.path,'/',link.file_relative_path)
							              when link.original_format='url' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by pv.relative_path limit 1)
							              when link.original_format='video' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' order by pv.relative_path limit 1)
							              when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='png_phone' order by pv.relative_path limit 1)
							              when link.original_format='link bundle' then
							                (select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join (select lb.id_link as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb union select l_i_l.id_original as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb join tbl_link_internal_link l_i_l on l_i_l.id_internal = lb.id_link) link_set on link_set.id_link=child_link.id where link_set.id_bundle=link.id and link_set.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') order by pv.relative_path limit 1)
							              end)
							      else (case
							            when link.original_format='jpeg' or link.original_format='gif' or link.original_format='png' then
							              concat(lib.path,'/',link.file_relative_path)
										when link.original_format='url' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by rand() limit 1)							              
							            when link.original_format='video' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' order by rand() limit 1)
							            when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='png_phone' order by rand() limit 1)
							            when link.original_format='link bundle' then
							              (select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join (select lb.id_link as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb union select l_i_l.id_original as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb join tbl_link_internal_link l_i_l on l_i_l.id_internal = lb.id_link) link_set on link_set.id_link=child_link.id where link_set.id_bundle=link.id and link_set.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') order by rand() limit 1)
							            end)
							      end as thumbnail", $feedSettings->thumbnailSettings->mode)
			));

			$dbCommand = $dbCommand->join('tbl_folder f', 'f.id = link.id_folder');
			$dbCommand = $dbCommand->join('tbl_page p', 'p.id = f.id_page');
			$dbCommand = $dbCommand->join('tbl_library lib', 'lib.id = p.id_library');
			$dbCommand = $dbCommand->join('(select aggr.id_link as id_link, sum(aggr.link_views) as link_views from
														           (select
														              s_l.id_link as id_link,
														              count(s_l.id) as link_views
														            from tbl_statistic_link s_l ' .
				'join tbl_statistic_activity sa on s_l.id_activity=sa.id ' .
				'where ' . $statisticRangeCondition . ' ' .
				'group by s_l.id_link
																	union
																	select
														              l_i_l.id_internal as link_id,
														              count(s_l.id) as link_views
														            from tbl_statistic_link s_l
														              join tbl_link_internal_link l_i_l on l_i_l.id_original = s_l.id_link ' .
				'join tbl_statistic_activity sa on s_l.id_activity=sa.id ' .
				'where ' . $statisticRangeCondition . ' ' .
				'group by l_i_l.id_internal
														            union
														            select
														              l_b.id_bundle as id_link,
														              count(s_l.id) as link_views
														            from tbl_statistic_link s_l 
														              join tbl_link_bundle l_b on l_b.id_link = s_l.id_link ' .
				'join tbl_statistic_activity sa on s_l.id_activity=sa.id ' .
				'where ' . $statisticRangeCondition . ' ' .
				'group by l_b.id_bundle								
														            union
														            select
														              l_b.id_bundle as link_id,
														              count(s_l.id) as link_views
														            from tbl_statistic_link s_l
														              join tbl_link_internal_link l_i_l on l_i_l.id_original = s_l.id_link
														              join tbl_link_bundle l_b on l_b.id_link = l_i_l.id_internal ' .
				'join tbl_statistic_activity sa on s_l.id_activity=sa.id ' .
				'where ' . $statisticRangeCondition . ' ' .
				'group by l_b.id_bundle
														            union
														            select
														              l_q.id_link as id_link,
														              count(s_q.id) as link_views
														            from tbl_statistic_qpage s_q
														              join tbl_link_qpage l_q on l_q.id_qpage = s_q.id_qpage ' .
				'join tbl_statistic_activity sa on s_q.id_activity=sa.id ' .
				'where ' . $statisticRangeCondition . ' ' .
				'group by l_q.id_link) aggr group by aggr.id_link) link_views_set', "link_views_set.id_link=link.id");

			$whereConditions = array(
				'AND',
				sprintf('link.search_format in (\'%s\')', implode("','", $queryFormats)),
			);

			if (count($feedSettings->libraries) > 0)
				$whereConditions[] = sprintf('lib.name in (\'%s\')', implode("','", $feedSettings->libraries));

			if (count($feedSettings->categories) > 0)
			{
				$categoriesConditions = array('OR');
				foreach ($feedSettings->categories as $category)
					foreach ($category->items as $categoryItem)
						$categoriesConditions[] = sprintf('link.id in (select id_link from tbl_link_category where category = "%s" and tag = "%s")',
							$category->name,
							$categoryItem);
				$whereConditions[] = $categoriesConditions;
			}

			if (count($feedSettings->excludeQueryConditions->linkConditions) > 0)
			{
				$linkConditions = array();
				foreach ($feedSettings->excludeQueryConditions->linkConditions as $linkCondition)
					$linkConditions[] = "(trim(link.file_name)='" . $linkCondition->linkName . "' or trim(link.name)='" . $linkCondition->linkName . "') and trim(f.name)='" . $linkCondition->folderName . "' and trim(p.name)='" . $linkCondition->pageName . "' and trim(lib.name)='" . $linkCondition->libraryName . "'";

				$whereConditions[] = sprintf("NOT (%s)", implode(' OR ', $linkConditions));
			}

			if (count($feedSettings->excludeQueryConditions->categories) > 0)
			{
				$categoryConditions = array();
				foreach ($feedSettings->excludeQueryConditions->categories as $category)
					foreach ($category->items as $categoryItem)
						$categoryConditions[] = sprintf('(link.id in (select id_link from tbl_link_category where category = "%s" and tag = "%s"))',
							$category->name,
							$categoryItem);
				$whereConditions[] = sprintf('NOT (%s)', implode(' or ', $categoryConditions));
			}

			if (count($feedSettings->excludeQueryConditions->libraries) > 0)
			{
				$libraryIds = array();
				foreach ($feedSettings->excludeQueryConditions->libraries as $library)
					$libraryIds[] = $library->id;
				$whereConditions[] = sprintf("link.id_library not in ('%s')", implode("','", $libraryIds));
			}

			$isAdmin = \UserIdentity::isUserAdmin();
			if (!$isAdmin)
			{
				$userId = \UserIdentity::getCurrentUserId();

				$restrictedLinkConditions = array('AND');

				$availableLinkIds = \LinkWhiteListRecord::getAvailableLinks($userId);
				if (count($availableLinkIds) > 0)
					$restrictedLinkConditions[] = sprintf("link.id in ('%s')", implode("', '", $availableLinkIds));

				$deniedLinkIds = \LinkBlackListRecord::getDeniedLinks($userId);
				if (count($deniedLinkIds) > 0)
					$restrictedLinkConditions[] = sprintf("link.id not in ('%s')", implode("', '", $deniedLinkIds));

				if (count($restrictedLinkConditions) > 1)
					$whereConditions[] = array('OR', 'link.id not in (select wl.id_link from tbl_link_white_list wl)', $restrictedLinkConditions);
				else
					$whereConditions[] = 'link.id not in (select wl.id_link from tbl_link_white_list wl)';

				$assignedPageIds = \UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);
				if (count($assignedPageIds) > 0)
					$whereConditions[] = sprintf("p.id in ('%s')", implode("', '", $assignedPageIds));
				else
					$whereConditions[] = '1<>1';
			}

			$dbCommand = $dbCommand->where($whereConditions);

			$dbCommand = $dbCommand->group(array(
				'link.id',
				'link.name',
				'link.file_name',
				'link.search_format',
				'lib.name',
			));

			$dbCommand = $dbCommand->order('total_views desc');

			$dbCommand = $dbCommand->limit($feedSettings->maxLinks);

			$resultRecords = $dbCommand->queryAll();

			foreach ($resultRecords as $resultRecord)
			{
				$feedItem = new LinkFeedItem();
				$feedItem->linkId = $resultRecord['id'];
				$feedItem->linkName = $resultRecord['name'];
				$feedItem->format = $resultRecord['search_format'];
				$feedItem->libraryName = $resultRecord['library_name'];
				$feedItem->viewsCount = $resultRecord['total_views'];

				switch ($resultRecord['search_format'])
				{
					case LinkFeedQuerySettings::LinkFormatYouTube:
						if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $resultRecord['path'], $match))
						{
							$youTubeId = $match[1];
							$feedItem->thumbnail = sprintf("https://img.youtube.com/vi/%s/0.jpg", $youTubeId);
						}
						break;
					case LinkFeedQuerySettings::LinkFormatVimeo:
						if (preg_match('/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|(\w*\/)*review\/|)(\d+)(?:$|\/|\?)/', $resultRecord['path'], $match))
						{
							try
							{
								$vimeoId = $match[4];
								$vimeoInfo = \CJSON::decode(@file_get_contents(sprintf("http://vimeo.com/api/v2/video/%s.json", $vimeoId)), true);
								$feedItem->thumbnail = $vimeoInfo[0]["thumbnail_large"];
							}
							catch (\Exception $ex)
							{
							}
						}
						if (empty($feedItem->thumbnail))
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/vimeo.png');
						break;
					case LinkFeedQuerySettings::LinkFormatUrl:
						if (!empty($resultRecord['thumbnail']))
						{
							$thumbnailRelativePath = $resultRecord['thumbnail'];
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . 'sd_cache' . '//' . $thumbnailRelativePath);
						}
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/url.png');
						break;
					default:
						$thumbnailRelativePath = $resultRecord['thumbnail'];
						$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . 'sd_cache' . '//' . $thumbnailRelativePath);
						break;
				}

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
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
					case LinkFeedQuerySettings::LinkFormatUrl:
						$feedSettings->conditions->fileTypes[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatVideo:
						$feedSettings->conditions->fileTypes[] = $linkFormat;
						$feedSettings->conditions->fileTypes[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$feedSettings->conditions->fileTypes[] = LinkFeedQuerySettings::LinkFormatVimeo;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$feedSettings->conditions->fileTypes[] = LinkFeedQuerySettings::LinkFormatWord;
						$feedSettings->conditions->fileTypes[] = LinkFeedQuerySettings::LinkFormatPdf;
						$feedSettings->conditions->fileTypes[] = LinkFeedQuerySettings::LinkFormatUrl;
						break;
				}

			$feedSettings->conditions->limit = $feedSettings->maxLinks;

			$resultRecords = ConditionalQueryHelper::queryLinksByCondition($feedSettings->conditions);

			foreach ($resultRecords as $resultRecord)
			{
				if (!empty($resultRecord['thumbnail']))
				{
					$feedItem = new LinkFeedItem();
					$feedItem->linkId = $resultRecord['id'];
					$feedItem->linkName = $resultRecord['name'];
					$feedItem->format = $resultRecord['original_format'];
					$feedItem->libraryName = $resultRecord['library_name'];
					$feedItem->viewsCount = $resultRecord['total_views'];

					switch ($resultRecord['original_format'])
					{
						case LinkFeedQuerySettings::LinkFormatYouTube:
							if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $resultRecord['path'], $match))
							{
								$youTubeId = $match[1];
								$feedItem->thumbnail = sprintf("https://img.youtube.com/vi/%s/0.jpg", $youTubeId);
							}
							break;
						case LinkFeedQuerySettings::LinkFormatVimeo:
							if (preg_match('/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|(\w*\/)*review\/|)(\d+)(?:$|\/|\?)/', $resultRecord['path'], $match))
							{
								try
								{
									$vimeoId = $match[4];
									$vimeoInfo = \CJSON::decode(@file_get_contents(sprintf("http://vimeo.com/api/v2/video/%s.json", $vimeoId)), true);
									$feedItem->thumbnail = $vimeoInfo[0]["thumbnail_large"];
								}
								catch (\Exception $ex)
								{
								}
							}
							if (empty($feedItem->thumbnail))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/vimeo.png');
							break;
						case LinkFeedQuerySettings::LinkFormatUrl:
							if (!empty($resultRecord['thumbnail']))
							{
								$thumbnailRelativePath = $resultRecord['thumbnail'];
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . 'sd_cache' . '//' . $thumbnailRelativePath);
							}
							else
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/url.png');
							break;
						default:
							$thumbnailRelativePath = $resultRecord['thumbnail'];
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . 'sd_cache' . '//' . $thumbnailRelativePath);
							break;
					}

					$feedItems[] = $feedItem;
				}
			}

			if ($feedSettings->conditions->sortSettings->columnTag === DataTableQuerySettings::DataTagFileName)
			{
				$sortHelper = new \ObjectSortHelper('linkName', $feedSettings->conditions->sortSettings->order);
				usort($feedItems, array($sortHelper, 'sort'));
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
			switch ($feedSettings->thumbnailSettings->mode)
			{
				case ThumbnailQuerySettings::ThumbnailModeRandom:
					$thumbnailCondition = 'order by rand() limit 1';
					break;
				default:
					$thumbnailCondition = 'order by pv.relative_path limit 1';
					break;
			}

			/** @var \CDbCommand $dbCommand */
			$dbCommand = \Yii::app()->db->createCommand();

			$fromClause = array();
			foreach ($feedSettings->linkConditions as $linkCondition)
				$fromClause[] = "select sub_link.*, '" . (!empty($linkCondition->linkAlias) ? $linkCondition->linkAlias : "") . "' as link_alias from tbl_link sub_link join tbl_folder sub_folder on sub_folder.id=sub_link.id_folder join tbl_page sub_page on sub_page.id=sub_folder.id_page join tbl_library sub_lib on sub_lib.id=sub_page.id_library where (trim(sub_link.file_name)='" . $linkCondition->linkName . "' or trim(sub_link.name)='" . $linkCondition->linkName . "') and trim(sub_folder.name)='" . $linkCondition->folderName . "' and trim(sub_page.name)='" . $linkCondition->pageName . "' and trim(sub_lib.name)='" . $linkCondition->libraryName . "'";

			$dbCommand = $dbCommand->from('(' . implode(" union ", $fromClause) . ') link');

			$dbCommand = $dbCommand->select(array(
				'id' => 'link.id as id',
				'id_library' => 'link.id_library as id_library',
				'library_name' => 'lib.name as library_name',
				'name' => 'link.name as name',
				'link_alias' => 'link.link_alias as link_alias',
				'path' => 'link.file_relative_path as path',
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
						              l_i_l.id_internal as link_id,
						              count(s_l.id) as link_views
						            from tbl_statistic_link s_l
						              join tbl_link_internal_link l_i_l on l_i_l.id_original = s_l.id_link
						            group by l_i_l.id_internal
						            union
						            select
						              l_b.id_bundle as link_id,
						              count(s_l.id) as link_views
						            from tbl_statistic_link s_l
						              join tbl_link_bundle l_b on l_b.id_link = s_l.id_link  
						              group by l_b.id_bundle
						            union
						            select
						              l_b.id_bundle as link_id,
						              count(s_l.id) as link_views
						            from tbl_statistic_link s_l
						              join tbl_link_internal_link l_i_l on l_i_l.id_original = s_l.id_link
						              join tbl_link_bundle l_b on l_b.id_link = l_i_l.id_internal  
						              group by l_b.id_bundle
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
								concat(lib.path,'/',link.file_relative_path)
							when link.original_format='url' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' " . $thumbnailCondition . ")								
							when link.original_format='video' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' " . $thumbnailCondition . ")										
							when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='png_phone' " . $thumbnailCondition . ")
							when link.original_format='link bundle' then
								(select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join (select lb.id_link as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb union select l_i_l.id_original as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb join tbl_link_internal_link l_i_l on l_i_l.id_internal = lb.id_link) link_set on link_set.id_link=child_link.id where link_set.id_bundle=link.id and link_set.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') " . $thumbnailCondition . ")
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
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
					case LinkFeedQuerySettings::LinkFormatUrl:
						$queryFormats[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatWord;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatPdf;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatUrl;
						break;
					case LinkFeedQuerySettings::LinkFormatVideo:
						$queryFormats[] = $linkFormat;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVimeo;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVideo;
						break;
				}

			$whereConditions = array(
				'AND',
				sprintf('link.search_format in (\'%s\')', implode("','", $queryFormats)),
			);

			$isAdmin = \UserIdentity::isUserAdmin();
			if (!$isAdmin)
			{
				$userId = \UserIdentity::getCurrentUserId();

				$restrictedLinkConditions = array();

				$availableLinkIds = \LinkWhiteListRecord::getAvailableLinks($userId);
				if (count($availableLinkIds) > 0)
					$restrictedLinkConditions[] = sprintf("link.id in ('%s')", implode("', '", $availableLinkIds));

				$deniedLinkIds = \LinkBlackListRecord::getDeniedLinks($userId);
				if (count($deniedLinkIds) > 0)
					$restrictedLinkConditions[] = sprintf("link.id not in ('%s')", implode("', '", $deniedLinkIds));

				if (count($restrictedLinkConditions) > 0)
					$whereConditions[] = array('OR', 'link.is_restricted <> 1', array('AND', $restrictedLinkConditions));
				else
					$whereConditions[] = array('link.is_restricted <> 1');

				$assignedPageIds = \UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);
				if (count($assignedPageIds) > 0)
					$whereConditions[] = sprintf("p.id in ('%s')", implode("', '", $assignedPageIds));
				else
					$whereConditions[] = '1<>1';
			}

			$dbCommand = $dbCommand->where($whereConditions);

			$sortFiled = 'link.name';
			switch ($feedSettings->sortSettings->columnTag)
			{
				case DataTableQuerySettings::DataTagDate:
					$sortFiled = 'link_date';
					break;
				case DataTableQuerySettings::DataTagFileName:
					$sortFiled = 'name';
					break;
				case DataTableQuerySettings::DataTagViewsCount:
					$sortFiled = 'total_views';
					break;
			}
			$dbCommand = $dbCommand->order(sprintf("%s %s", $sortFiled, $feedSettings->sortSettings->order));
			$resultRecords = $dbCommand->queryAll();

			foreach ($resultRecords as $resultRecord)
			{
				if (!empty($resultRecord['thumbnail']))
				{
					$feedItem = new LinkFeedItem();
					$feedItem->linkId = $resultRecord['id'];
					$feedItem->linkName = !empty($resultRecord['link_alias']) ? $resultRecord['link_alias'] : $resultRecord['name'];
					$feedItem->format = $resultRecord['original_format'];
					$feedItem->libraryName = $resultRecord['library_name'];
					$feedItem->viewsCount = $resultRecord['total_views'];

					switch ($resultRecord['original_format'])
					{
						case LinkFeedQuerySettings::LinkFormatYouTube:
							if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $resultRecord['path'], $match))
							{
								$youTubeId = $match[1];
								$feedItem->thumbnail = sprintf("https://img.youtube.com/vi/%s/0.jpg", $youTubeId);
							}
							break;
						case LinkFeedQuerySettings::LinkFormatVimeo:
							if (preg_match('/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|(\w*\/)*review\/|)(\d+)(?:$|\/|\?)/', $resultRecord['path'], $match))
							{
								try
								{
									$vimeoId = $match[4];
									$vimeoInfo = \CJSON::decode(@file_get_contents(sprintf("http://vimeo.com/api/v2/video/%s.json", $vimeoId)), true);
									$feedItem->thumbnail = $vimeoInfo[0]["thumbnail_large"];
								}
								catch (\Exception $ex)
								{
								}
							}
							if (empty($feedItem->thumbnail))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/vimeo.png');
							break;
						case LinkFeedQuerySettings::LinkFormatUrl:
							if (!empty($resultRecord['thumbnail']))
							{
								$thumbnailRelativePath = $resultRecord['thumbnail'];
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . 'sd_cache' . '//' . $thumbnailRelativePath);
							}
							else
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/url.png');
							break;
						default:
							$thumbnailRelativePath = $resultRecord['thumbnail'];
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . 'sd_cache' . '//' . $thumbnailRelativePath);
							break;
					}

					$feedItems[] = $feedItem;
				}
			}

			return $feedItems;
		}
	}