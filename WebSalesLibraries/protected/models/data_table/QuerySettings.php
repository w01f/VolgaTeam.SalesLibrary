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
				'link_date' => 'max(link.file_date) as link_date',
				'format' => 'max(link.search_format) as format',
				'extended_properties' => 'max(link.settings) as extended_properties',
			);

			$this->customQueryFields = array();
			$this->innerJoin = array();
			$this->leftJoin = array();
			$this->whereConditions = array();
			$this->groupFields = array('link.id');

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

		/** @param  array $columnSettingsList */
		private function configureFromColumnSettings($columnSettingsList)
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
							$this->baseQueryFields['total_views'] = '(select sum(aggr.link_views) from
														           (select
														              s_l.id_link as link_id,
														              count(s_l.id) as link_views
														            from tbl_statistic_link s_l ' .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'join tbl_statistic_activity sa on s_l.id_activity=sa.id ' : '') .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'where sa.date_time>=\'' . $this->viewCountQuerySettings['startDate'] . '\' and sa.date_time<=\'' . $this->viewCountQuerySettings['endDate'] . '\' ' : '')
								. 'group by s_l.id_link
														            union
														            select
														              l_q.id_link as link_id,
														              count(s_q.id) as link_views
														            from tbl_statistic_qpage s_q
														              join tbl_link_qpage l_q on l_q.id_qpage = s_q.id_qpage ' .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'join tbl_statistic_activity sa on s_q.id_activity=sa.id ' : '') .
								(!empty($this->viewCountQuerySettings['startDate']) ? 'where sa.date_time>=\'' . $this->viewCountQuerySettings['startDate'] . '\' and sa.date_time<=\'' . $this->viewCountQuerySettings['endDate'] . '\' ' : '')
								. 'group by l_q.id_link
														           ) aggr where aggr.link_id=link.id and link.type<>6
																) as total_views';
						if (!empty($this->viewCountQuerySettings['startDate']) && !empty($this->viewCountQuerySettings['endDate']))
						{
							$this->leftJoin['tbl_statistic_link s_l'] = 's_l.id_link=link.id';
							$this->leftJoin['tbl_statistic_activity sa_l'] = 'sa_l.id=s_l.id_activity and sa_l.date_time>=\'' . $this->viewCountQuerySettings['startDate'] . '\' and sa_l.date_time<=\'' . $this->viewCountQuerySettings['endDate'] . '\'';
							$this->whereConditions[] = 'link.original_format= \'quicksite\' or sa_l.id is not null';

							$this->leftJoin['tbl_statistic_qpage s_q'] = 's_l.id_link=link.id';
							$this->leftJoin['tbl_link_qpage l_q'] = 'l_q.id_qpage = s_q.id_qpage';
							$this->leftJoin['tbl_statistic_activity sa_q'] = 'sa_q.id=s_q.id_activity and sa_q.date_time>=\'' . $this->viewCountQuerySettings['startDate'] . '\' and sa_q.date_time<=\'' . $this->viewCountQuerySettings['endDate'] . '\'';
							$this->whereConditions[] = 'link.original_format<> \'quicksite\' or sa_q.id is not null';
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
						$this->baseQueryFields['thumbnail'] = "(select pv.relative_path from tbl_preview pv where pv.id_container=link.id_preview and ((link.original_format<>'video' and pv.type='thumbs_phone') or (link.original_format='video' and pv.type='mp4 thumb')) " . $thumbnailCondition . ") as thumbnail";
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

			$instance->configureFromColumnSettings($columnSettingsList);

			return $instance;
		}
	}