<?

	/**
	 * Class TableColumnSettings
	 */
	class TableColumnSettings
	{
		const ColumnTagCategory = 'tag';
		const ColumnTagLibrary = 'library';
		const ColumnTagFileType = 'type';
		const ColumnTagFileName = 'link';
		const ColumnTagThumbnail = 'thumbnail';
		const ColumnTagViewsCount = 'views';
		const ColumnTagRate = 'rate';
		const ColumnTagDate = 'date';

		private static $columnTags = array(
			self::ColumnTagCategory,
			self::ColumnTagLibrary,
			self::ColumnTagFileType,
			self::ColumnTagFileName,
			self::ColumnTagThumbnail,
			self::ColumnTagViewsCount,
			self::ColumnTagRate,
			self::ColumnTagDate,
		);

		public $enable;
		public $order;
		public $title;
		public $width;
		public $height;
		public $fullWidth;

		/** @param string $tag */
		public function __construct($tag)
		{
			$this->width = -1;
			$this->height = -1;
			$this->fullWidth = false;
			switch ($tag)
			{
				case self::ColumnTagCategory:
					$this->enable = Yii::app()->params['search_options']['hide_tag'] != true;
					$this->order = 1;
					$this->title = Yii::app()->params['tags']['column_name'];
					break;
				case self::ColumnTagLibrary:
					$this->enable = Yii::app()->params['search_options']['hide_libraries'] != true;
					$this->order = 2;
					$this->title = Yii::app()->params['stations']['column_name'];
					break;
				case self::ColumnTagFileType:
					$this->enable = true;
					$this->order = 3;
					$this->title = 'Type';
					break;
				case self::ColumnTagFileName:
					$this->enable = true;
					$this->order = 4;
					$this->title = 'Link';
					break;
				case self::ColumnTagThumbnail:
					$this->enable = false;
					$this->order = 9999;
					$this->title = 'Thumbnail';
					break;
				case self::ColumnTagViewsCount:
					$this->enable = true;
					$this->order = 6;
					$this->title = 'Views';
					break;
				case self::ColumnTagRate:
					$this->enable = true;
					$this->order = 7;
					$this->title = 'Rating';
					break;
				case self::ColumnTagDate:
					$this->enable = true;
					$this->order = 8;
					$this->title = 'Date';
					break;
				default:
					$this->order = 9999;
					break;
			}
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Enable', $contextNode);
			$this->enable = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->enable;

			$queryResult = $xpath->query('./Position', $contextNode);
			$this->order = $queryResult->length > 0 ? intval($queryResult->item(0)->nodeValue) : $this->order;

			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->title;

			$queryResult = $xpath->query('./MaxWidth', $contextNode);
			$this->width = $queryResult->length > 0 ? intval($queryResult->item(0)->nodeValue) : $this->width;

			$queryResult = $xpath->query('./MaxHeight', $contextNode);
			$this->height = $queryResult->length > 0 ? intval($queryResult->item(0)->nodeValue) : $this->width;

			$queryResult = $xpath->query('./FullWidth', $contextNode);
			$this->fullWidth = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->fullWidth;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return array()
		 */
		public static function loadColumnsFromXml($xpath, $contextNode)
		{
			$columnSettingsList = self::createEmpty();

			foreach ($columnSettingsList as $key => $value)
			{
				$queryResult = $xpath->query('./Column[@tag="' . $key . '"]', $contextNode);
				if ($queryResult->length > 0)
					$value->configureFromXml($xpath, $queryResult->item(0));
			}

			$orders = array();
			foreach ($columnSettingsList as $key => $value)
			{
				$orders[$key] = $value->order;
			}
			array_multisort($orders, SORT_ASC, $columnSettingsList);

			return $columnSettingsList;
		}

		/**
		 * @return array()
		 */
		public static function createEmpty()
		{
			$columnSettingsList = array();

			foreach (self::$columnTags as $columnTag)
			{
				$columnSettings = new self($columnTag);
				$columnSettingsList[$columnTag] = $columnSettings;
			}

			return $columnSettingsList;
		}

		/**
		 * @return array()
		 */
		public static function createLinkCartColumns()
		{
			$columnSettingsList = self::createEmpty();

			foreach ($columnSettingsList as $key => $value)
			{
				switch ($key)
				{
					case self::ColumnTagCategory:
					case self::ColumnTagLibrary:
					case self::ColumnTagViewsCount:
					case self::ColumnTagRate:
					case self::ColumnTagDate:
					case self::ColumnTagThumbnail:
						$value->enable = false;
						break;
				}
			}

			return $columnSettingsList;
		}
	}