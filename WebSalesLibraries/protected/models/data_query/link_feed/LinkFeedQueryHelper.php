<?

	namespace application\models\data_query\link_feed;

	use application\models\data_query\conditions\ConditionalQueryHelper;
	use application\models\data_query\conditions\QueryCacheSettings;
	use application\models\data_query\conditions\ThumbnailQuerySettings;
	use application\models\data_query\data_table\DataTableQuerySettings;
	use application\models\wallbin\models\web\LibraryLink;
	use application\models\wallbin\models\web\LibraryManager;

	/**
	 * Class LinkFeedManager
	 */
	class LinkFeedQueryHelper
	{
		/**
		 * @param LinkFeedQuerySettings $feedSettings
		 * @param bool $allowAnonymousRequest
		 * @return LinkFeedItem[]
		 */
		public static function queryFeedItems($feedSettings, $allowAnonymousRequest = false)
		{
			$feedItems = null;

			$cacheSettings = isset($feedSettings->cacheSettings) ? $feedSettings->cacheSettings : new QueryCacheSettings();
			if ($cacheSettings->enableCache)
			{
				$encodedData = \ShortcutDataQueryCacheRecord::getCachedData($cacheSettings->cacheId, true);
				if (!empty($encodedData))
					$feedItems = \CJSON::decode($encodedData, false);
			}

			if (!isset($feedItems))
			{
				switch ($feedSettings->feedType)
				{
					case LinkFeedQuerySettings::FeedTypeTrending:
						/**@var TrendingFeedQuerySettings $feedSettings */
						$feedItems = self::queryTrendingItems($feedSettings, !$allowAnonymousRequest);
						break;
					case LinkFeedQuerySettings::FeedTypeSearch:
						/**@var SearchFeedQuerySettings $feedSettings */
						$feedItems = self::querySearchItems($feedSettings, !$allowAnonymousRequest);
						break;
					case LinkFeedQuerySettings::FeedTypeSpecificLinks:
						/**@var SpecificLinkFeedQuerySettings $feedSettings */
						$feedItems = self::querySpecificLinkItems($feedSettings, !$allowAnonymousRequest);
						break;
				}
			}

			return isset($feedItems) ? $feedItems : array();
		}

		/**
		 * @param TrendingFeedQuerySettings $feedSettings
		 * @param boolean $usePermissionFilter
		 * @return LinkFeedItem[]
		 */
		private static function queryTrendingItems($feedSettings, $usePermissionFilter)
		{
			$feedItems = array();

			$queryFormats = array();
			foreach ($feedSettings->linkFormatsInclude as $linkFormat)
				switch ($linkFormat)
				{
					case LinkFeedQuerySettings::LinkFormatPowerPoint:
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
					case LinkFeedQuerySettings::LinkFormatExcel:
					case LinkFeedQuerySettings::LinkFormatUrl:
					case LinkFeedQuerySettings::LinkFormatHtml5:
					case LinkFeedQuerySettings::LinkFormatQuicksite:
					case LinkFeedQuerySettings::LinkFormatYouTube:
					case LinkFeedQuerySettings::LinkFormatVimeo:
					case LinkFeedQuerySettings::LinkFormatInternalLibrary:
					case LinkFeedQuerySettings::LinkFormatInternalPage:
					case LinkFeedQuerySettings::LinkFormatInternalWindow:
					case LinkFeedQuerySettings::LinkFormatInternalShortcut:
					case LinkFeedQuerySettings::LinkFormatInternalLink:
						$queryFormats[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatWord;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatPdf;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatExcel;
						break;
					case LinkFeedQuerySettings::LinkFormatVideo:
						$queryFormats[] = $linkFormat;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVimeo;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVideo;
						break;
					case LinkFeedQuerySettings::LinkFormatHyperlink:
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatUrl;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatHtml5;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatQuicksite;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVimeo;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalLibrary;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalPage;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalWindow;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalShortcut;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalLink;
						break;
				}

			$statisticRangeCondition = '';
			switch ($feedSettings->dateRangeType)
			{
				case TrendingFeedQuerySettings::DataRangeTypeToday:
					$statisticRangeCondition = sprintf("sa.date_time>='%s'", date(\Yii::app()->params['mysqlDateFormat']));
					break;
				case TrendingFeedQuerySettings::DataRangeTypeWeek:
					$statisticRangeCondition = "yearweek(sa.date_time, 1) = yearweek(curdate(), 1)";
					break;
				case TrendingFeedQuerySettings::DataRangeTypeMonth:
					$statisticRangeCondition = "year(sa.date_time) = year(curdate()) and month(sa.date_time) = month(curdate())";
					break;
				case TrendingFeedQuerySettings::DataRangeTypeAllTime:
					$statisticRangeCondition = "1=1";
					break;
			}

			$textConditions = ConditionalQueryHelper::prepareTextCondition($feedSettings->text, $feedSettings->textExactMatch);

			/** @var \CDbCommand $dbCommand */
			$dbCommand = \Yii::app()->db->createCommand();

			$dbCommand = $dbCommand->from('tbl_link link');

			$dbCommand = $dbCommand->select(array(
				'id' => 'link.id as id',
				'id_library' => 'link.id_library as id_library',
				'library_name' => 'lib.name as library_name',
				'name' => 'link.name as name',
				'type' => 'link.type as type',
				'path' => 'link.file_relative_path as path',
				'file_name' => 'link.file_name as file_name',
				'original_format' => 'link.original_format as original_format',
				'search_format' => 'link.search_format as search_format',
				'settings' => 'link.settings as settings',
				'total_views' => 'link_views_set.link_views as total_views',
				'thumbnail' => sprintf("case when '%s' = 0
							        then (case
							              when link.original_format='jpeg' or link.original_format='gif' or link.original_format='png' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by pv.relative_path limit 1)
							              when link.original_format='url' or link.original_format='html5' or link.original_format='youtube' or link.original_format='vimeo' or link.original_format='quicksite' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by pv.relative_path limit 1)
							              when link.original_format='video' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' order by pv.relative_path limit 1)
							              when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='png_phone' order by pv.relative_path limit 1)
							              when link.original_format='xls' then
							                (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by pv.relative_path limit 1)
							              when link.original_format='internal link' then
							                (select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join tbl_link_internal_link l_i_l on l_i_l.id_original = child_link.id where l_i_l.id_internal=link.id and (pv.type='png_phone' or pv.type='mp4 thumb') order by pv.relative_path limit 1)
							              when link.original_format='link bundle' then
							                (select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join (select lb.id_link as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb union select l_i_l.id_original as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb join tbl_link_internal_link l_i_l on l_i_l.id_internal = lb.id_link) link_set on link_set.id_link=child_link.id where link_set.id_bundle=link.id and link_set.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') order by pv.relative_path limit 1)
							              end)
							      else (case
							            when link.original_format='jpeg' or link.original_format='gif' or link.original_format='png' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by rand() limit 1)
										when link.original_format='url' or link.original_format='html5' or link.original_format='youtube' or link.original_format='vimeo' or link.original_format='quicksite' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by rand() limit 1)							              
							            when link.original_format='video' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' order by rand() limit 1)
							            when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='png_phone' order by rand() limit 1)
										when link.original_format='xls' then
							              (select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' order by rand() limit 1)							              
							            when link.original_format='internal link' then
							              (select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join tbl_link_internal_link l_i_l on l_i_l.id_original = child_link.id where l_i_l.id_internal=link.id and (pv.type='png_phone' or pv.type='mp4 thumb') order by rand() limit 1)  
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

			if (count($feedSettings->linkFormatsExclude) > 0)
			{
				$whereConditions[] = sprintf('link.search_format not in (\'%s\')', implode("','", $feedSettings->linkFormatsExclude));
				$whereConditions[] = sprintf('link.file_extension not in (\'%s\')', implode("','", $feedSettings->linkFormatsExclude));
			}

			if (count($textConditions) > 0)
			{
				$matchCondition = 'link.name,link.file_name,link.tags';
				$conditionParts = array();
				foreach ($textConditions as $contentConditionPart)
					$conditionParts[] = sprintf("(match(%s) against('%s' in boolean mode))", $matchCondition, str_replace("'", "\'", $contentConditionPart));
				$whereConditions[] = implode(" or ", $conditionParts);
			}

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

			if ($feedSettings->hideLinksWithinBundle)
			{
				$whereConditions[] = sprintf("link.id not in (select hide_lb.id_link from tbl_link_bundle hide_lb)");
			}

			$isAdmin = \UserIdentity::isUserAdmin();
			if ($usePermissionFilter && !$isAdmin)
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

			try
			{
				$resultRecords = $dbCommand->queryAll();
			}
			catch (\Exception $ex)
			{
				$resultRecords = array();
			}

			$libraryManager = new LibraryManager();

			foreach ($resultRecords as $resultRecord)
			{
				$feedItem = new LinkFeedItem();
				$feedItem->linkId = $resultRecord['id'];
				$feedItem->linkName = $resultRecord['name'];
				$feedItem->format = $resultRecord['original_format'];
				$feedItem->libraryName = $resultRecord['library_name'];
				$feedItem->viewsCount = $resultRecord['total_views'];

				$library = $libraryManager->getLibraryById($resultRecord['id_library']);
				$settings = \BaseLinkSettings::createByContent($resultRecord['settings']);
				$fileInfo = \FileInfo::fromLinkData(
					$resultRecord['id'],
					$resultRecord['type'],
					$resultRecord['name'],
					$resultRecord['path'],
					$settings,
					$library);
				$isHyperlink = LibraryLink::isOpenedAsHyperlink($resultRecord['type'], $settings);
				$isLinkBundle = $resultRecord['original_format'] === 'link bundle';

				$feedItem->isDraggable = $fileInfo->isFile || $isHyperlink || $isLinkBundle;
				if ($isHyperlink)
				{
					$feedItem->dragHeader = 'URL';
					$feedItem->url = $fileInfo->link;
					$feedItem->isDirectUrl = true;
				}
				else if ($fileInfo->isFile || $isLinkBundle)
				{
					$feedItem->dragHeader = 'DownloadURL';
					$feedItem->url = \FileInfo::getFileMIME($resultRecord['original_format']) . ':' .
						$fileInfo->dragDownloadName . ':' .
						str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $fileInfo->link);
					$feedItem->isDirectUrl = false;
				}

				if (!empty($resultRecord['thumbnail']))
					$resultRecord['thumbnail'] = str_replace("/", DIRECTORY_SEPARATOR, str_replace("\\", DIRECTORY_SEPARATOR, $resultRecord['thumbnail']));
				switch ($resultRecord['search_format'])
				{
					case LinkFeedQuerySettings::LinkFormatYouTube:
						if (empty($resultRecord['thumbnail']))
						{
							if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $resultRecord['path'], $match))
							{
								$youTubeId = $match[1];
								$feedItem->thumbnail = sprintf("https://img.youtube.com/vi/%s/0.jpg", $youTubeId);
							}
						}
						else
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
					case LinkFeedQuerySettings::LinkFormatVimeo:
						if (empty($resultRecord['thumbnail']))
						{

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
						}
						else
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
					case LinkFeedQuerySettings::LinkFormatUrl:
						if (!empty($resultRecord['thumbnail']))
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/url.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalLibrary:
						if (!isset($internalLibraryThumbnailFiles))
						{
							$internalLibraryThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'library';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalLibraryThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalLibraryThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/library/' . $internalLibraryThumbnailFiles[array_rand($internalLibraryThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal library.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalPage:
						if (!isset($internalPageThumbnailFiles))
						{
							$internalPageThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'page';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalPageThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalPageThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/page/' . $internalPageThumbnailFiles[array_rand($internalPageThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal page.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalWindow:
						if (!isset($internalWindowThumbnailFiles))
						{
							$internalWindowThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'window';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalWindowThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalWindowThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/window/' . $internalWindowThumbnailFiles[array_rand($internalWindowThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal window.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalShortcut:
						if (!isset($internalShortcutThumbnailFiles))
						{
							$internalShortcutThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'shortcut';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalShortcutThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalShortcutThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/shortcut/' . $internalShortcutThumbnailFiles[array_rand($internalShortcutThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal shortcut.png');
						break;
					default:
						if (!empty($resultRecord['thumbnail']))
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
				}
				if (empty($feedItem->thumbnail))
					$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/missing_image.jpg');
				$feedItems[] = $feedItem;
			}

			return $feedItems;
		}

		/**
		 * @param SearchFeedQuerySettings $feedSettings
		 * @param boolean $usePermissionFilter
		 * @return LinkFeedItem[]
		 */
		private static function querySearchItems($feedSettings, $usePermissionFilter)
		{
			$feedItems = array();

			$feedSettings->conditions->fileTypesInclude = array();
			foreach ($feedSettings->linkFormatsInclude as $linkFormat)
				switch ($linkFormat)
				{
					case LinkFeedQuerySettings::LinkFormatPowerPoint:
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
					case LinkFeedQuerySettings::LinkFormatExcel:
					case LinkFeedQuerySettings::LinkFormatUrl:
					case LinkFeedQuerySettings::LinkFormatHtml5:
					case LinkFeedQuerySettings::LinkFormatQuicksite:
					case LinkFeedQuerySettings::LinkFormatYouTube:
					case LinkFeedQuerySettings::LinkFormatVimeo:
					case LinkFeedQuerySettings::LinkFormatInternalLibrary:
					case LinkFeedQuerySettings::LinkFormatInternalPage:
					case LinkFeedQuerySettings::LinkFormatInternalWindow:
					case LinkFeedQuerySettings::LinkFormatInternalShortcut:
					case LinkFeedQuerySettings::LinkFormatInternalLink:
						$feedSettings->conditions->fileTypesInclude[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatVideo:
						$feedSettings->conditions->fileTypesInclude[] = $linkFormat;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatVimeo;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatWord;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatPdf;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatExcel;
						break;
					case LinkFeedQuerySettings::LinkFormatHyperlink:
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatUrl;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatHtml5;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatQuicksite;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatVimeo;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatInternalLibrary;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatInternalPage;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatInternalWindow;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatInternalShortcut;
						$feedSettings->conditions->fileTypesInclude[] = LinkFeedQuerySettings::LinkFormatInternalLink;
						break;
				}
			$feedSettings->conditions->fileTypesExclude = $feedSettings->linkFormatsExclude;

			$feedSettings->conditions->limit = $feedSettings->maxLinks;
			$feedSettings->conditions->hideLinksWithinBundle = $feedSettings->hideLinksWithinBundle;

			$resultRecords = ConditionalQueryHelper::queryLinksByCondition($feedSettings->conditions, $usePermissionFilter);

			$libraryManager = new LibraryManager();

			foreach ($resultRecords as $resultRecord)
			{
				$feedItem = new LinkFeedItem();
				$feedItem->linkId = $resultRecord['id'];
				$feedItem->linkName = $resultRecord['name'];
				$feedItem->format = $resultRecord['original_format'];
				$feedItem->libraryName = $resultRecord['library_name'];
				$feedItem->viewsCount = $resultRecord['total_views'];

				$library = $libraryManager->getLibraryById($resultRecord['id_library']);
				$settings = \BaseLinkSettings::createByContent($resultRecord['extended_properties']);
				$fileInfo = \FileInfo::fromLinkData(
					$resultRecord['id'],
					$resultRecord['type'],
					$resultRecord['name'],
					$resultRecord['path'],
					$settings,
					$library);
				$isHyperlink = LibraryLink::isOpenedAsHyperlink($resultRecord['type'], $settings);
				$isLinkBundle = $resultRecord['original_format'] === 'link bundle';

				$feedItem->isDraggable = $fileInfo->isFile || $isHyperlink || $isLinkBundle;
				if ($isHyperlink)
				{
					$feedItem->dragHeader = 'URL';
					$feedItem->url = $fileInfo->link;
					$feedItem->isDirectUrl = true;
				}
				else if ($fileInfo->isFile || $isLinkBundle)
				{
					$feedItem->dragHeader = 'DownloadURL';
					$feedItem->url = \FileInfo::getFileMIME($resultRecord['original_format']) . ':' .
						$fileInfo->dragDownloadName . ':' .
						str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $fileInfo->link);
					$feedItem->isDirectUrl = false;
				}

				if (!empty($resultRecord['thumbnail']))
					$resultRecord['thumbnail'] = str_replace("/", DIRECTORY_SEPARATOR, str_replace("\\", DIRECTORY_SEPARATOR, $resultRecord['thumbnail']));
				switch ($resultRecord['original_format'])
				{
					case LinkFeedQuerySettings::LinkFormatYouTube:
						if (empty($resultRecord['thumbnail']))
						{
							if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $resultRecord['path'], $match))
							{
								$youTubeId = $match[1];
								$feedItem->thumbnail = sprintf("https://img.youtube.com/vi/%s/0.jpg", $youTubeId);
							}
						}
						else
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
					case LinkFeedQuerySettings::LinkFormatVimeo:
						if (empty($resultRecord['thumbnail']))
						{

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
						}
						else
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
					case LinkFeedQuerySettings::LinkFormatUrl:
						if (!empty($resultRecord['thumbnail']))
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/url.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalLibrary:
						if (!isset($internalLibraryThumbnailFiles))
						{
							$internalLibraryThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'library';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalLibraryThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalLibraryThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/library/' . $internalLibraryThumbnailFiles[array_rand($internalLibraryThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal library.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalPage:
						if (!isset($internalPageThumbnailFiles))
						{
							$internalPageThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'page';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalPageThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalPageThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/page/' . $internalPageThumbnailFiles[array_rand($internalPageThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal page.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalWindow:
						if (!isset($internalWindowThumbnailFiles))
						{
							$internalWindowThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'window';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalWindowThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalWindowThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/window/' . $internalWindowThumbnailFiles[array_rand($internalWindowThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal window.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalShortcut:
						if (!isset($internalShortcutThumbnailFiles))
						{
							$internalShortcutThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'shortcut';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalShortcutThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalShortcutThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/shortcut/' . $internalShortcutThumbnailFiles[array_rand($internalShortcutThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal shortcut.png');
						break;
					default:
						if (!empty($resultRecord['thumbnail']))
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
				}
				if (empty($feedItem->thumbnail))
					$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/missing_image.jpg');
				$feedItems[] = $feedItem;
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
		 * @param boolean $usePermissionFilter
		 * @return LinkFeedItem[]
		 */
		private static function querySpecificLinkItems($feedSettings, $usePermissionFilter)
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
				'type' => 'link.type as type',
				'path' => 'link.file_relative_path as path',
				'original_format' => 'link.original_format as original_format',
				'search_format' => 'link.search_format as search_format',
				'settings' => 'link.settings as settings',
				'link_alias' => 'link.link_alias as link_alias',
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
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' " . $thumbnailCondition . ")
							when link.original_format='url' or link.original_format='html5' or link.original_format='youtube' or link.original_format='vimeo' or link.original_format='quicksite' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' " . $thumbnailCondition . ")								
							when link.original_format='video' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' " . $thumbnailCondition . ")										
							when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='png_phone' " . $thumbnailCondition . ")
							when link.original_format='xls' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs' " . $thumbnailCondition . ")
							when link.original_format='internal link' then
								(select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join tbl_link_internal_link l_i_l on l_i_l.id_original = child_link.id where l_i_l.id_internal=link.id and (pv.type='png_phone' or pv.type='mp4 thumb') " . $thumbnailCondition . ")								
							when link.original_format='link bundle' then
								(select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join (select lb.id_link as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb union select l_i_l.id_original as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb join tbl_link_internal_link l_i_l on l_i_l.id_internal = lb.id_link) link_set on link_set.id_link=child_link.id where link_set.id_bundle=link.id and link_set.use_as_thumbnail=1 and (pv.type='png_phone' or pv.type='mp4 thumb') " . $thumbnailCondition . ")
							end as thumbnail"
			));

			$dbCommand = $dbCommand->join('tbl_folder f', 'f.id=link.id_folder');
			$dbCommand = $dbCommand->join('tbl_page p', 'p.id=f.id_page');
			$dbCommand = $dbCommand->join('tbl_library lib', 'lib.id=p.id_library');

			$queryFormats = array();
			foreach ($feedSettings->linkFormatsInclude as $linkFormat)
				switch ($linkFormat)
				{
					case LinkFeedQuerySettings::LinkFormatPowerPoint:
					case LinkFeedQuerySettings::LinkFormatPdf:
					case LinkFeedQuerySettings::LinkFormatWord:
					case LinkFeedQuerySettings::LinkFormatExcel:
					case LinkFeedQuerySettings::LinkFormatUrl:
					case LinkFeedQuerySettings::LinkFormatHtml5:
					case LinkFeedQuerySettings::LinkFormatQuicksite:
					case LinkFeedQuerySettings::LinkFormatYouTube:
					case LinkFeedQuerySettings::LinkFormatVimeo:
					case LinkFeedQuerySettings::LinkFormatInternalLibrary:
					case LinkFeedQuerySettings::LinkFormatInternalPage:
					case LinkFeedQuerySettings::LinkFormatInternalWindow:
					case LinkFeedQuerySettings::LinkFormatInternalShortcut:
					case LinkFeedQuerySettings::LinkFormatInternalLink:
						$queryFormats[] = $linkFormat;
						break;
					case LinkFeedQuerySettings::LinkFormatDocument:
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatWord;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatPdf;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatExcel;
						break;
					case LinkFeedQuerySettings::LinkFormatVideo:
						$queryFormats[] = $linkFormat;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVimeo;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVideo;
						break;
					case LinkFeedQuerySettings::LinkFormatHyperlink:
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatUrl;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatHtml5;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatQuicksite;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatYouTube;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatVimeo;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalLibrary;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalPage;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalWindow;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalShortcut;
						$queryFormats[] = LinkFeedQuerySettings::LinkFormatInternalLink;
						break;
				}

			$whereConditions = array(
				'AND',
				sprintf('link.search_format in (\'%s\')', implode("','", $queryFormats)),
			);

			if (count($feedSettings->linkFormatsExclude) > 0)
			{
				$whereConditions[] = sprintf('link.search_format not in (\'%s\')', implode("','", $feedSettings->linkFormatsExclude));
				$whereConditions[] = sprintf('link.file_extension not in (\'%s\')', implode("','", $feedSettings->linkFormatsExclude));
			}

			$isAdmin = \UserIdentity::isUserAdmin();
			if ($usePermissionFilter && !$isAdmin)
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

			try
			{
				$resultRecords = $dbCommand->queryAll();
			}
			catch (\Exception $ex)
			{
				$resultRecords = array();
			}

			$libraryManager = new LibraryManager();

			foreach ($resultRecords as $resultRecord)
			{
				$feedItem = new LinkFeedItem();
				$feedItem->linkId = $resultRecord['id'];
				$feedItem->linkName = !empty($resultRecord['link_alias']) ? $resultRecord['link_alias'] : $resultRecord['name'];
				$feedItem->format = $resultRecord['original_format'];
				$feedItem->libraryName = $resultRecord['library_name'];
				$feedItem->viewsCount = $resultRecord['total_views'];

				$library = $libraryManager->getLibraryById($resultRecord['id_library']);
				$settings = \BaseLinkSettings::createByContent($resultRecord['settings']);
				$fileInfo = \FileInfo::fromLinkData(
					$resultRecord['id'],
					$resultRecord['type'],
					$resultRecord['name'],
					$resultRecord['path'],
					$settings,
					$library);
				$isHyperlink = LibraryLink::isOpenedAsHyperlink($resultRecord['type'], $settings);
				$isLinkBundle = $resultRecord['original_format'] === 'link bundle';

				$feedItem->isDraggable = $fileInfo->isFile || $isHyperlink || $isLinkBundle;
				if ($isHyperlink)
				{
					$feedItem->dragHeader = 'URL';
					$feedItem->url = $fileInfo->link;
					$feedItem->isDirectUrl = true;
				}
				else if ($fileInfo->isFile || $isLinkBundle)
				{
					$feedItem->dragHeader = 'DownloadURL';
					$feedItem->url = \FileInfo::getFileMIME($resultRecord['original_format']) . ':' .
						$fileInfo->dragDownloadName . ':' .
						str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $fileInfo->link);
					$feedItem->isDirectUrl = false;
				}

				if (!empty($resultRecord['thumbnail']))
					$resultRecord['thumbnail'] = str_replace("/", DIRECTORY_SEPARATOR, str_replace("\\", DIRECTORY_SEPARATOR, $resultRecord['thumbnail']));
				switch ($resultRecord['original_format'])
				{
					case LinkFeedQuerySettings::LinkFormatYouTube:
						if (empty($resultRecord['thumbnail']))
						{
							if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $resultRecord['path'], $match))
							{
								$youTubeId = $match[1];
								$feedItem->thumbnail = sprintf("https://img.youtube.com/vi/%s/0.jpg", $youTubeId);
							}
						}
						else
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
					case LinkFeedQuerySettings::LinkFormatVimeo:
						if (empty($resultRecord['thumbnail']))
						{
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
						}
						else
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
					case LinkFeedQuerySettings::LinkFormatUrl:
						if (!empty($resultRecord['thumbnail']))
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/url.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalLibrary:
						if (!isset($internalLibraryThumbnailFiles))
						{
							$internalLibraryThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'library';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalLibraryThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalLibraryThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/library/' . $internalLibraryThumbnailFiles[array_rand($internalLibraryThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal library.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalPage:
						if (!isset($internalPageThumbnailFiles))
						{
							$internalPageThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'page';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalPageThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalPageThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/page/' . $internalPageThumbnailFiles[array_rand($internalPageThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal page.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalWindow:
						if (!isset($internalWindowThumbnailFiles))
						{
							$internalWindowThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'window';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalWindowThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalWindowThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/window/' . $internalWindowThumbnailFiles[array_rand($internalWindowThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal window.png');
						break;
					case LinkFeedQuerySettings::LinkFormatInternalShortcut:
						if (!isset($internalShortcutThumbnailFiles))
						{
							$internalShortcutThumbnailFiles = array();
							$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'blocks' . DIRECTORY_SEPARATOR . 'shortcut';
							if (is_dir($folderPath))
							{
								if ($dh = opendir($folderPath))
								{
									while (($file = readdir($dh)) !== false)
									{
										if ($file !== '.' && $file !== '..')
											$internalShortcutThumbnailFiles[] = $file;
									}
									closedir($dh);
								}
							}
						}
						if (count($internalShortcutThumbnailFiles) > 0)
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/blocks/shortcut/' . $internalShortcutThumbnailFiles[array_rand($internalShortcutThumbnailFiles)]);
						else
							$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/internal shortcut.png');
						break;
					default:
						if (!empty($resultRecord['thumbnail']))
						{
							$thumbnailFile = realpath(LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . $resultRecord['thumbnail']);
							if (file_exists($thumbnailFile))
								$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . \Yii::app()->params['librariesRoot'] . '//' . $resultRecord['thumbnail']);
						}
						break;
				}
				if (empty($feedItem->thumbnail))
					$feedItem->thumbnail = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/missing_image.jpg');
				$feedItems[] = $feedItem;
			}

			return $feedItems;
		}

		/**
		 * @param LinkFeedQuerySettings $querySettings
		 * @param boolean $ignoreExpirationDate
		 */
		public static function prepareDataQueryCache($querySettings, $ignoreExpirationDate)
		{
			$cacheSettings = isset($querySettings->cacheSettings) ? $querySettings->cacheSettings : new QueryCacheSettings();

			if (!$cacheSettings->enableCache)
			{
				echo sprintf("Snapshot disabled - %s", $cacheSettings->cacheId);
				echo PHP_EOL;
				return;
			}

			if (!$ignoreExpirationDate)
			{
				if ($cacheSettings->enableCache)
				{
					$encodedData = \ShortcutDataQueryCacheRecord::getCachedData($cacheSettings->cacheId, false);
					if (!empty($encodedData))
					{
						echo sprintf("Snapshot not expired. Skip update - %s", $cacheSettings->cacheId);
						echo PHP_EOL;
						return;
					}
				}
			}

			$feedItems = null;
			switch ($querySettings->feedType)
			{
				case LinkFeedQuerySettings::FeedTypeTrending:
					/**@var TrendingFeedQuerySettings $querySettings */
					$feedItems = self::queryTrendingItems($querySettings, false);
					break;
				case LinkFeedQuerySettings::FeedTypeSearch:
					/**@var SearchFeedQuerySettings $querySettings */
					$feedItems = self::querySearchItems($querySettings, false);
					break;
				case LinkFeedQuerySettings::FeedTypeSpecificLinks:
					/**@var SpecificLinkFeedQuerySettings $querySettings */
					$feedItems = self::querySpecificLinkItems($querySettings, false);
					break;
			}

			if (isset($feedItems))
			{
				$encodedData = \CJSON::encode($feedItems);

				if ($cacheSettings->expireInHours > 0)
					$expirationDate = $cacheSettings->expireInHours > 0 ? date("Y-m-d H:59:00", strtotime('+' . ($cacheSettings->expireInHours - 1) . ' hours')) : null;
				else
					$expirationDate = null;

				\ShortcutDataQueryCacheRecord::setCachedData($cacheSettings->cacheId, $encodedData, $expirationDate);
				echo sprintf("Snapshot updated - %s", $cacheSettings->cacheId);
				echo PHP_EOL;
			}
		}
	}