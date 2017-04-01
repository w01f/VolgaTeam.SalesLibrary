<?

	/**
	 * Class QuerySettings
	 */
	class QuerySettings
	{
		const SettingsTagFrom = 'from';
		const SettingsTagQueryFields = 'queryFields';
		const SettingsTagInnerJoin = 'innerJoin';
		const SettingsTagLeftJoin = 'leftJoin';
		const SettingsTagWhere = 'where';
		const SettingsTagCategoryWhere = 'categoryWhere';
		const SettingsTagGroup = 'group';

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

		/** @var array */
		public $dateQuerySettings;
		/** @var array */
		public $categoryQuerySettings;
		/** @var array */
		public $viewCountQuerySettings;
		/** @var array */
		public $thumbnailQuerySettings;

		public function __construct()
		{
			$this->from = 'tbl_link link';

			$this->baseQueryFields = array(
				'id' => 'max(link.id) as id',
				'id_library' => 'max(link.id_library) as id_library',
				'name' => 'max(link.name) as name',
				'type' => 'max(link.type) as type',
				'path' => 'link.file_relative_path as path',
				'file_name' => 'link.file_name as file_name',
				'file_extension' => 'link.file_extension as file_extension',
				'format' => 'max(link.search_format) as format',
				'extended_properties' => 'max(link.settings) as extended_properties',
			);

			$this->customQueryFields = array();
			$this->innerJoin = array();
			$this->leftJoin = array();
			$this->whereConditions = array();
			$this->groupFields = array('link.id');

			$defaultDateSettings = new SearchDateSettings();
			$this->dateQuerySettings = array(
				'field' => SearchDateSettings::getDateColumnName($defaultDateSettings->dateMode)
			);

			$defaultCategorySettings = new SearchCategorySettings();
			$this->categoryQuerySettings = array(
				'field' => $defaultCategorySettings->fieldName,
				'filter' => '1=1',
				'maxRows' => $defaultCategorySettings->maxRows
			);

			$defaultViewCountSettings = new SearchViewCountSettings();
			$this->viewCountQuerySettings = array(
				'startDate' => $defaultViewCountSettings->startDate,
				'endDate' => $defaultViewCountSettings->endDate
			);

			$defaultThumbnailSettings = new SearchThumbnailSettings();
			$this->thumbnailQuerySettings = array(
				'mode' => $defaultThumbnailSettings->mode
			);
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
					case TableColumnSettings::ColumnTagCategory:
						if ($value->enable)
						{
							$this->baseQueryFields['tag'] = 'glcat.tag as tag';
							$this->leftJoin["(select lcat.id_link, substring_index(group_concat(distinct lcat." . $this->categoryQuerySettings['field'] . " order by lcat.tag separator ', '),', '," . $this->categoryQuerySettings['maxRows'] . ") as tag from tbl_link_category lcat where " . $this->categoryQuerySettings['filter'] . " group by lcat.id_link) glcat"] =
								'glcat.id_link=link.id';
						}
						break;
					case TableColumnSettings::ColumnTagRate:
						if ($value->enable)
							$this->baseQueryFields['rate'] = '(select (round(avg(lr.value)*2)/2) as value from tbl_link_rate lr where lr.id_link=link.id) as rate';
						break;
					case TableColumnSettings::ColumnTagViewsCount:
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
					case TableColumnSettings::ColumnTagThumbnail:
						switch ($this->thumbnailQuerySettings['mode'])
						{
							case SearchThumbnailSettings::ThumbnailModeRandom:
								$thumbnailCondition = 'order by rand() limit 1';
								break;
							default:
								$thumbnailCondition = 'order by pv.relative_path limit 1';
								break;
						}
						$this->baseQueryFields['thumbnail'] = "case 
							when link.original_format='jpeg' or link.original_format='gif' or link.original_format='png' then
								link.file_relative_path
							when link.original_format='video' then
								(select pv.relative_path from tbl_preview pv where pv.id_container=link.id_preview and pv.type='mp4 thumb' " . $thumbnailCondition . ")										
							when link.original_format='ppt' or link.original_format='doc' or link.original_format='pdf' then
								(select pv.relative_path from tbl_preview pv where pv.id_container=link.id_preview and pv.type='thumbs_phone' " . $thumbnailCondition . ")
							end as thumbnail";
						break;
				}
			}
		}

		/**
		 * @param array $params
		 * @return QuerySettings
		 */
		public static function prepareQuery($params)
		{
			/** @var QuerySettings $instance */
			$instance = new self();

			$columnSettingsList = TableColumnSettings::createEmpty();

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
						$instance->innerJoin = $value;
						break;
					case self::SettingsTagLeftJoin:
						$instance->leftJoin = $value;
						break;
					case self::SettingsTagWhere:
						$instance->whereConditions = $value;
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
						/** @var SearchDateSettings $dateSettings */
						$dateSettings = $value;
						$instance->dateQuerySettings['field'] = SearchDateSettings::getDateColumnName($dateSettings->dateMode);
						break;

					case self::SettingsTagCategory:
						/** @var SearchCategorySettings $categorySettings */
						$categorySettings = $value;
						$instance->categoryQuerySettings['field'] = $categorySettings->fieldName;
						$instance->categoryQuerySettings['maxRows'] = $categorySettings->maxRows;
						break;

					case self::SettingsTagViewsCount:
						/** @var SearchViewCountSettings $viewCountSettings */
						$viewCountSettings = $value;
						$instance->viewCountQuerySettings['startDate'] = $viewCountSettings->startDate;
						$instance->viewCountQuerySettings['endDate'] = $viewCountSettings->endDate;
						break;

					case self::SettingsTagThumbnails:
						/** @var SearchThumbnailSettings $thumbnailSettings */
						$thumbnailSettings = $value;
						$instance->thumbnailQuerySettings['mode'] = $thumbnailSettings->mode;
						break;
				}
			}

			$instance->configureQueryFromDateSettings();

			$instance->configureQueryFromColumnSettings($columnSettingsList);

			return $instance;
		}
	}