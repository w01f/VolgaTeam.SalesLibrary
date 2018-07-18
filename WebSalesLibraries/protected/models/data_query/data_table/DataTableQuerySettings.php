<?

	namespace application\models\data_query\data_table;

	use application\models\data_query\common\QuerySortSettings;
	use application\models\data_query\conditions\CategoryQuerySettings;
	use application\models\data_query\conditions\DateQuerySettings;
	use application\models\data_query\conditions\ThumbnailQuerySettings;
	use application\models\data_query\conditions\ViewCountQuerySettings;

	/**
	 * Class DataTableQuerySettings
	 */
	class DataTableQuerySettings
	{
		const DataTagCategory = 'tag';
		const DataTagLibrary = 'library';
		const DataTagFileType = 'type';
		const DataTagFileName = 'link';
		const DataTagThumbnail = 'thumbnail';
		const DataTagViewsCount = 'views';
		const DataTagRate = 'rate';
		const DataTagDate = 'date';

		const SettingsTagFrom = 'from';
		const SettingsTagQueryFields = 'queryFields';
		const SettingsTagInnerJoin = 'innerJoin';
		const SettingsTagLeftJoin = 'leftJoin';
		const SettingsTagWhere = 'where';
		const SettingsTagCategoryWhere = 'categoryWhere';
		const SettingsTagGroup = 'group';
		const SettingsTagSort = 'sort';
		const SettingsTagLimit = 'limit';

		const SettingsTagColumns = 'columnsSettings';

		const SettingsTagDate = 'dateQuery';
		const SettingsTagCategory = 'categoryQuery';
		const SettingsTagViewsCount = 'viewsCountQuery';
		const SettingsTagThumbnails = 'thumbnailsQuery';

		/** @var string */
		public $from;
		/** @var array */
		public $baseQueryFields;
		/** @var array */
		public $customQueryFields;
		/** @var array */
		public $innerJoin;
		/** @var array */
		public $leftJoin;
		/** @var array */
		public $whereConditions;
		/** @var array */
		public $groupFields;
		/** @var int */
		public $limit;

		/** @var array */
		public $dateQuerySettings;
		/** @var array */
		public $categoryQuerySettings;
		/** @var array */
		public $viewCountQuerySettings;
		/** @var array */
		public $thumbnailQuerySettings;

		/** @var array */
		public $sortSettings;


		public function __construct()
		{
			$userId = \UserIdentity::getCurrentUserId();
			$isAdmin = \UserIdentity::isUserAdmin();

			$this->from = 'tbl_link link';

			$this->baseQueryFields = array(
				'id' => 'max(link.id) as id',
				'id_library' => 'max(link.id_library) as id_library',
				'library_name' => 'max(lib.name) as library_name',
				'name' => 'max(link.name) as name',
				'type' => 'max(link.type) as type',
				'path' => 'link.file_relative_path as path',
				'file_name' => 'link.file_name as file_name',
				'file_extension' => 'link.file_extension as file_extension',
				'original_format' => 'max(link.original_format) as original_format',
				'search_format' => 'max(link.search_format) as search_format',
				'extended_properties' => 'max(link.settings) as extended_properties',
				'one_drive' => 'max(link.one_drive) as one_drive',
			);

			$this->customQueryFields = array();

			$this->innerJoin = array(
				'tbl_folder f' => 'f.id = link.id_folder',
				'tbl_page p' => 'p.id = f.id_page',
				'tbl_library lib' => 'lib.id = p.id_library'
			);

			$this->leftJoin = array();

			$this->whereConditions = array('AND',
				'link.is_preview_not_ready=0');
			if (!$isAdmin)
			{
				$restrictedLinkConditions = array('AND');

				$availableLinkIds = \LinkWhiteListRecord::getAvailableLinks($userId);
				if (count($availableLinkIds) > 0)
					$restrictedLinkConditions[] = sprintf("link.id in ('%s')", implode("', '", $availableLinkIds));

				$deniedLinkIds = \LinkBlackListRecord::getDeniedLinks($userId);
				if (count($deniedLinkIds) > 0)
					$restrictedLinkConditions[] = sprintf("link.id not in ('%s')", implode("', '", $deniedLinkIds));

				if (count($restrictedLinkConditions) > 1)
					$this->whereConditions[] = array('OR', 'link.id not in (select wl.id_link from tbl_link_white_list wl)', $restrictedLinkConditions);
				else
					$this->whereConditions[] = 'link.id not in (select wl.id_link from tbl_link_white_list wl)';
			}

			$includeAppLinks = \Yii::app()->browser->getBrowser() == \Browser::BROWSER_EO;
			if ($includeAppLinks)
				$this->whereConditions[] = 'link.type<>15';

			$this->groupFields = array('link.id');
			$this->limit = 0;

			$defaultDateSettings = new DateQuerySettings();
			$this->dateQuerySettings = array(
				'field' => DateQuerySettings::getDateColumnName($defaultDateSettings->dateMode)
			);

			$defaultCategorySettings = new CategoryQuerySettings();
			$this->categoryQuerySettings = array(
				'field' => $defaultCategorySettings->fieldName,
				'filter' => '1=1',
				'maxRows' => $defaultCategorySettings->maxRows
			);

			$defaultViewCountSettings = new ViewCountQuerySettings();
			$this->viewCountQuerySettings = array(
				'startDate' => $defaultViewCountSettings->startDate,
				'endDate' => $defaultViewCountSettings->endDate
			);

			$defaultThumbnailSettings = new ThumbnailQuerySettings();
			$this->thumbnailQuerySettings = array(
				'mode' => $defaultThumbnailSettings->mode
			);

			$this->sortSettings = array();
		}

		private function configureQueryFromDateSettings()
		{
			$this->baseQueryFields['link_date'] = sprintf('max(link.%s) as link_date', $this->dateQuerySettings['field']);
		}

		/** @param  array $columnSettingsList */
		private function configureQueryFromColumnSettings($columnSettingsList)
		{
			foreach ($columnSettingsList as $key => $value)
			{
				switch ($key)
				{
					case self::DataTagCategory:
						if ($value->enable)
						{
							$this->baseQueryFields['tag'] = 'glcat.tag as tag';
							$this->leftJoin["(select lcat.id_link, substring_index(group_concat(distinct lcat." . $this->categoryQuerySettings['field'] . " order by lcat.tag separator ', '),', '," . $this->categoryQuerySettings['maxRows'] . ") as tag from tbl_link_category lcat where " . $this->categoryQuerySettings['filter'] . " group by lcat.id_link) glcat"] =
								'glcat.id_link=link.id';
						}
						break;
					case self::DataTagRate:
						if ($value->enable)
							$this->baseQueryFields['rate'] = '(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate';
						break;
					case self::DataTagViewsCount:
						if ($value->enable)
						{
							if (!empty($this->viewCountQuerySettings['startDate']) && !empty($this->viewCountQuerySettings['endDate']))
							{
								$this->baseQueryFields['total_views'] = 'link_views_set.link_views as total_views';
								$this->innerJoin['(select aggr.id_link as id_link, sum(aggr.link_views) as link_views from
														           (select
														              s_l.id_link as id_link,
														              count(s_l.id) as link_views
														            from tbl_statistic_link s_l ' .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'join tbl_statistic_activity sa on s_l.id_activity=sa.id ' : '') .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'where sa.date_time>=\'' . $this->viewCountQuerySettings['startDate'] . '\' and sa.date_time<=\'' . $this->viewCountQuerySettings['endDate'] . '\' ' : '')
								. 'group by s_l.id_link
														            union
														            select
														              l_b.id_bundle as id_link,
														              count(s_l.id) as link_views
														            from tbl_statistic_link s_l 
														              join tbl_link_bundle l_b on l_b.id_link = s_l.id_link ' .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'join tbl_statistic_activity sa on s_l.id_activity=sa.id ' : '') .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'where sa.date_time>=\'' . $this->viewCountQuerySettings['startDate'] . '\' and sa.date_time<=\'' . $this->viewCountQuerySettings['endDate'] . '\' ' : '')
								. 'group by l_b.id_bundle								
														            union
														            select
														              l_q.id_link as id_link,
														              count(s_q.id) as link_views
														            from tbl_statistic_qpage s_q
														              join tbl_link_qpage l_q on l_q.id_qpage = s_q.id_qpage ' .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'join tbl_statistic_activity sa on s_q.id_activity=sa.id ' : '') .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'where sa.date_time>=\'' . $this->viewCountQuerySettings['startDate'] . '\' and sa.date_time<=\'' . $this->viewCountQuerySettings['endDate'] . '\' ' : '')
								. 'group by l_q.id_link) aggr group by aggr.id_link) link_views_set'] = 'link_views_set.id_link=link.id';
							}
							else
							{
								$this->baseQueryFields['total_views'] = '(select sum(aggr.link_views) from
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
																) as total_views';
							}
						}
						break;
					case self::DataTagThumbnail:
						switch ($this->thumbnailQuerySettings['mode'])
						{
							case ThumbnailQuerySettings::ThumbnailModeRandom:
								$thumbnailCondition = 'order by rand() limit 1';
								break;
							default:
								$thumbnailCondition = 'order by pv.relative_path limit 1';
								break;
						}
						$this->baseQueryFields['thumbnail'] = "case 
							when link.original_format='jpeg' or link.original_format='gif' or link.original_format='png' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs_datatable' " . $thumbnailCondition . ")
							when link.original_format='url' or link.original_format='html5' or link.original_format='youtube' or link.original_format='vimeo' or link.original_format='quicksite' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs_datatable' " . $thumbnailCondition . ")								
							when link.original_format='video' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs_datatable' " . $thumbnailCondition . ")										
							when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' or link.original_format='xls' then
								(select concat(lib.path,'/',pv.relative_path) from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs_datatable' " . $thumbnailCondition . ")
							when link.original_format='internal link' then
								(select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join tbl_link_internal_link l_i_l on l_i_l.id_original = child_link.id where l_i_l.id_internal=link.id and pv.type='thumbs_datatable' " . $thumbnailCondition . ")
							when link.original_format='link bundle' then
								(select concat(pv_lib.path,'/',pv.relative_path) from tbl_preview pv join tbl_library pv_lib on pv_lib.id=pv.id_library join tbl_link child_link on child_link.id_preview=pv.id_container join (select lb.id_link as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb union select l_i_l.id_original as id_link, lb.id_bundle as id_bundle, lb.use_as_thumbnail as use_as_thumbnail from tbl_link_bundle lb join tbl_link_internal_link l_i_l on l_i_l.id_internal = lb.id_link) link_set on link_set.id_link=child_link.id where link_set.id_bundle=link.id and link_set.use_as_thumbnail=1 and pv.type='thumbs_datatable' " . $thumbnailCondition . ")
							end as thumbnail";
						break;
				}
			}
		}

		/**
		 * @param array $params
		 * @return DataTableQuerySettings
		 */
		public static function prepareQuery($params)
		{
			/** @var DataTableQuerySettings $instance */
			$instance = new self();

			$columnSettingsList = DataTableColumnSettings::createEmpty();

			foreach ($params as $key => $value)
			{
				switch ($key)
				{
					case self::SettingsTagFrom:
						$instance->from = $value;
						break;
					case self::SettingsTagQueryFields:
						$instance->customQueryFields = $value;
						break;
					case self::SettingsTagInnerJoin:
						if (count($value) > 0)
							$instance->innerJoin = array_merge($value, $instance->innerJoin);
						break;
					case self::SettingsTagLeftJoin:
						if (isset($value) && is_array($value))
							$instance->leftJoin = array_merge($instance->leftJoin, $value);
						break;
					case self::SettingsTagWhere:
						if (isset($value) && is_array($value))
							$instance->whereConditions = array_merge($instance->whereConditions, $value);
						break;
					case self::SettingsTagCategoryWhere:
						$instance->categoryQuerySettings['filter'] = $value;
						break;
					case self::SettingsTagGroup:
						$instance->groupFields = $value;
						break;

					case self::SettingsTagColumns:
						$columnSettingsList = $value;
						break;

					case self::SettingsTagDate:
						/** @var DateQuerySettings $dateSettings */
						$dateSettings = $value;
						$instance->dateQuerySettings['field'] = DateQuerySettings::getDateColumnName($dateSettings->dateMode);
						break;

					case self::SettingsTagCategory:
						/** @var CategoryQuerySettings $categorySettings */
						$categorySettings = $value;
						$instance->categoryQuerySettings['field'] = $categorySettings->fieldName;
						$instance->categoryQuerySettings['maxRows'] = $categorySettings->maxRows;
						break;

					case self::SettingsTagViewsCount:
						/** @var ViewCountQuerySettings $viewCountSettings */
						$viewCountSettings = $value;
						$instance->viewCountQuerySettings['startDate'] = $viewCountSettings->startDate;
						$instance->viewCountQuerySettings['endDate'] = $viewCountSettings->endDate;
						break;

					case self::SettingsTagThumbnails:
						/** @var ThumbnailQuerySettings $thumbnailSettings */
						$thumbnailSettings = $value;
						$instance->thumbnailQuerySettings['mode'] = $thumbnailSettings->mode;
						break;

					case self::SettingsTagLimit:
						$instance->limit = $value;
						break;

					case self::SettingsTagSort:
						/** @var QuerySortSettings $sortSettings */
						$sortSettings = $value;
						if ($sortSettings->isConfigured)
						{
							switch ($sortSettings->columnTag)
							{
								case self::DataTagCategory:
									$instance->sortSettings['field'] = 'tag';
									break;
								case self::DataTagDate:
									$instance->sortSettings['field'] = 'link_date';
									break;
								case self::DataTagFileName:
									$instance->sortSettings['field'] = 'name';
									break;
								case self::DataTagFileType:
									$instance->sortSettings['field'] = 'type';
									break;
								case self::DataTagLibrary:
									$instance->sortSettings['field'] = 'library_name';
									break;
								case self::DataTagRate:
									$instance->sortSettings['field'] = 'rate';
									break;
								case self::DataTagThumbnail:
									$instance->sortSettings['field'] = 'thumbnail';
									break;
								case self::DataTagViewsCount:
									$instance->sortSettings['field'] = 'total_views';
									break;
							}
							$instance->sortSettings['order'] = $sortSettings->order;
						}
						break;

				}
			}

			$instance->configureQueryFromDateSettings();

			$instance->configureQueryFromColumnSettings($columnSettingsList);

			return $instance;
		}
	}