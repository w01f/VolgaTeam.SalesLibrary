<?

	/**
	 * Class ShortcutAction
	 */
	class ShortcutAction
	{
		public $id;
		public $tag;
		public $group;
		public $order;
		public $enabled;
		public $title;
		public $iconClass;
		public $backColor;
		public $textColor;
		public $iconColor;

		/** @var $parentShortcut BaseShortcut */
		private $parentShortcut;

		public function __construct($tag)
		{
			$this->id = uniqid();
			$this->order = 0;
			$this->enabled = true;
			$this->tag = $tag;
			$this->group = '';
			$this->title = 'No Title';
			$this->backColor = Yii::app()->params['menu']['BarColor'];
			$this->textColor = 'FFF';
			$this->iconColor = 'FFF';
		}

		/**
		 * @param $instance ShortcutAction
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @return ShortcutAction
		 */
		public static function configureFromXml($instance, $xpath, $contextNode)
		{
			/** @var $queryResult DOMNodeList */
			$queryResult = $xpath->query('Order', $contextNode);
			if ($queryResult->length > 0)
				$instance->order = intval(trim($queryResult->item(0)->nodeValue));

			$queryResult = $xpath->query('Enabled', $contextNode);
			if ($queryResult->length > 0)
				$instance->enabled = filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);

			$queryResult = $xpath->query('Title', $contextNode);
			if ($queryResult->length > 0)
				$instance->title = trim($queryResult->item(0)->nodeValue);

			$queryResult = $xpath->query('Icon', $contextNode);
			if ($queryResult->length > 0)
				$instance->iconClass = trim($queryResult->item(0)->nodeValue);

			$queryResult = $xpath->query('BackColor', $contextNode);
			if ($queryResult->length > 0)
				$instance->backColor = trim($queryResult->item(0)->nodeValue);

			$queryResult = $xpath->query('TextColor', $contextNode);
			if ($queryResult->length > 0)
				$instance->textColor = trim($queryResult->item(0)->nodeValue);

			$queryResult = $xpath->query('IconColor', $contextNode);
			if ($queryResult->length > 0)
				$instance->iconColor = trim($queryResult->item(0)->nodeValue);

			return $instance;
		}

		/**
		 * @return string
		 */
		public function getActionData()
		{
			$result = '';
			if (!isset($this->parentShortcut))
				return $result;
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'shortcut' => $this->parentShortcut->getTypeForActivityTracker(),
					'file' => $this->parentShortcut->getTitleForActivityTracker(),
					'title' => $this->title
				)) .
				'</div>';
			return $result;
		}

		/**
		 * @param $shortcut BaseShortcut
		 * @return array[]
		 */
		public static function getShortcutActions($shortcut)
		{
			$customActions = self::getActionsByShortcutType($shortcut->type);
			foreach ($customActions as $tag => $action)
			{
				/** @var $action ShortcutAction */
				$action->parentShortcut = $shortcut;
			}
			return $customActions;
		}

		/**
		 * @param $shortcutType string
		 * @return array[]
		 */
		public static function getActionsByShortcutType($shortcutType)
		{
			$customActions = self::getCommonActions();
			switch ($shortcutType)
			{
				case 'gridbundle':
					$action = new ShortcutAction('carousel');
					$action->order = 10;
					$action->enabled = false;
					$action->title = 'Show Carousel';
					$action->iconClass = 'icon-view_carousel';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('grid');
					$action->enabled = false;
					$action->order = 20;
					$action->title = 'Show Grid';
					$action->iconClass = 'icon-grid2';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('show-search');
					$action->order = 30;
					$action->title = 'Show Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('hide-search');
					$action->order = 40;
					$action->title = 'Hide Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;
					break;
				case 'carouselbundle':
					$action = new ShortcutAction('carousel');
					$action->order = 10;
					$action->title = 'Show Carousel';
					$action->iconClass = 'icon-view_carousel';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('grid');
					$action->order = 20;
					$action->title = 'Show Grid';
					$action->iconClass = 'icon-grid2';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('show-search');
					$action->order = 30;
					$action->title = 'Show Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('hide-search');
					$action->order = 40;
					$action->title = 'Hide Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;
					break;
				case 'search':
					$action = new ShortcutAction('sub-search-all');
					$action->group = 'sub-search-action';
					$action->order = 10;
					$action->title = 'Show All';
					$action->iconClass = 'icon-list';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('sub-search-criteria');
					$action->group = 'sub-search-action';
					$action->order = 20;
					$action->title = 'Show Search';
					$action->iconClass = 'icon-search2';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('sub-search-links');
					$action->group = 'sub-search-action';
					$action->order = 30;
					$action->title = 'Show Links';
					$action->iconClass = 'icon-hyperlink';
					$customActions[$action->tag] = $action;
					break;

				case 'library':
					$action = new ShortcutAction('page-select-tabs');
					$action->order = 10;
					$action->title = 'Show Tabs';
					$action->iconClass = 'icon-tabs-outline';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-select-combo');
					$action->order = 20;
					$action->title = 'Show Combo';
					$action->iconClass = 'icon-list';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-view-columns');
					$action->order = 30;
					$action->title = 'Show Columns';
					$action->iconClass = 'icon-view_column';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-view-accordion');
					$action->order = 40;
					$action->title = 'Show Accordion';
					$action->iconClass = 'icon-grid';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-zoom-in');
					$action->order = 50;
					$action->title = 'Zoom In';
					$action->iconClass = 'icon-zoomin';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-zoom-out');
					$action->order = 60;
					$action->title = 'Zoom Out';
					$action->iconClass = 'icon-zoomout';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('show-search');
					$action->order = 70;
					$action->title = 'Show Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('hide-search');
					$action->order = 70;
					$action->title = 'Hide Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;
					break;
				case 'page':
					$action = new ShortcutAction('page-view-columns');
					$action->order = 10;
					$action->title = 'Show Columns';
					$action->iconClass = 'icon-view_column';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-view-accordion');
					$action->order = 20;
					$action->title = 'Show Accordion';
					$action->iconClass = 'icon-grid';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-zoom-in');
					$action->order = 30;
					$action->title = 'Zoom In';
					$action->iconClass = 'icon-zoomin';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-zoom-out');
					$action->order = 40;
					$action->title = 'Zoom Out';
					$action->iconClass = 'icon-zoomout';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('show-search');
					$action->order = 50;
					$action->title = 'Show Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('hide-search');
					$action->order = 60;
					$action->title = 'Hide Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;
					break;
				case 'window':
					$action = new ShortcutAction('page-zoom-in');
					$action->order = 10;
					$action->title = 'Zoom In';
					$action->iconClass = 'icon-zoomin';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('page-zoom-out');
					$action->order = 20;
					$action->title = 'Zoom Out';
					$action->iconClass = 'icon-zoomout';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('show-search');
					$action->order = 30;
					$action->title = 'Show Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('hide-search');
					$action->order = 40;
					$action->title = 'Hide Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;
					break;
				case 'searchapp':
					$action = new ShortcutAction('search-app-run');
					$action->order = 10;
					$action->title = 'Search';
					$action->iconClass = 'icon-search';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-clear');
					$action->order = 20;
					$action->title = 'Clear Page';
					$action->iconClass = 'icon-refresh';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-keyword');
					$action->order = 30;
					$action->title = 'Keyword';
					$action->iconClass = ' icon-key';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-date-range');
					$action->order = 40;
					$action->title = 'Date Range';
					$action->iconClass = 'icon-calendar';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-filters');
					$action->order = 50;
					$action->title = 'Filters';
					$action->iconClass = 'icon-filter';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-file-types');
					$action->order = 60;
					$action->title = 'File Types';
					$action->iconClass = 'icon-file-powerpoint-o';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-tags');
					$action->order = 70;
					$action->title = Yii::app()->params['tags']['tab_name'];
					$action->iconClass = 'icon-grid';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-super-tags');
					$action->order = 80;
					$action->title = 'Super Tags';
					$action->iconClass = 'icon-usd';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('search-app-libraries');
					$action->order = 90;
					$action->title = Yii::app()->params['stations']['tab_name'];
					$action->iconClass = 'icon-tv';
					$customActions[$action->tag] = $action;
					break;
				case 'qbuilder':
					$action = new ShortcutAction('qbuilder-panel-show');
					$action->order = 10;
					$action->group = 'qbuilder-panel';
					$action->title = 'Show Panel';
					$action->iconClass = 'icon-list4';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('qbuilder-panel-hide');
					$action->order = 10;
					$action->group = 'qbuilder-panel';
					$action->title = 'Hide Panel';
					$action->iconClass = 'icon-list4';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('qbuilder-qsite-add');
					$action->order = 20;
					$action->title = 'Add Site';
					$action->iconClass = 'icon-add';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('qbuilder-qsite-delete');
					$action->order = 30;
					$action->title = 'Delete Site';
					$action->iconClass = 'icon-delete';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('qbuilder-qsite-save');
					$action->order = 40;
					$action->title = 'Save Site';
					$action->iconClass = 'icon-save';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('qbuilder-qsite-preview');
					$action->order = 50;
					$action->title = 'Preview Site';
					$action->iconClass = 'icon-magnifying-glass';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('qbuilder-qsite-email');
					$action->order = 60;
					$action->title = 'Email';
					$action->iconClass = 'icon-email';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('qbuilder-qsite-delete-all-links');
					$action->order = 70;
					$action->title = 'Delete all Links';
					$action->iconClass = 'icon-delete';
					$customActions[$action->tag] = $action;
					break;
				case 'landing':
					$action = new ShortcutAction('show-search');
					$action->order = 10;
					$action->title = 'Show Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;

					$action = new ShortcutAction('hide-search');
					$action->order = 20;
					$action->title = 'Hide Search';
					$action->iconClass = 'icon-search32';
					$customActions[$action->tag] = $action;
					break;

			}
			return $customActions;
		}

		/**
		 * @return array[]
		 */
		public static function getCommonActions()
		{
			$commonActions = array();

			if (UserIdentity::isUserAuthorized())
			{
				$logoutAction = new ShortcutAction('logout');
				$logoutAction->order = 9999;
				$logoutAction->title = 'Logout';
				$logoutAction->iconClass = 'icon-exit2';
				$commonActions[$logoutAction->tag] = $logoutAction;
			}

			return $commonActions;
		}
	}