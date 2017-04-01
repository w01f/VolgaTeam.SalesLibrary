<?
	namespace application\models\shortcuts\models\landing_page\regular_markup;

	/**
	 * Class ContentBlock
	 */
	abstract class ContentBlock
	{
		/** @var \LandingPageShortcut  */
		protected $parentShortcut;

		/** @var BlockContainer  */
		protected $parentBlock;

		public $type;

		public $hoverText;

		/** @var  \Padding */
		public $padding;
		/** @var  \Padding */
		public $margin;
		/** @var  TextAppearance */
		public $textAppearance;
		/** @var  BorderStyle */
		public $border;

		public $isAccessGranted;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		protected function __construct($parentShortcut, $parentBlock)
		{
			$this->parentShortcut = $parentShortcut;
			$this->parentBlock = $parentBlock;

			$this->padding = new \Padding(0);
			$this->margin = new \Padding(0);
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./HoverTip', $contextNode);
			$this->hoverText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Padding', $contextNode);
			if ($queryResult->length > 0)
				$this->padding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Margin', $contextNode);
			if ($queryResult->length > 0)
				$this->margin = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./TextStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->textAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Border', $contextNode);
			if ($queryResult->length > 0)
				$this->border = BorderStyle::fromXml($xpath, $queryResult->item(0));

			$user = \Yii::app()->user;
			$userGroups = \UserIdentity::getCurrentUserGroups();

			$approvedUsers = array();
			$queryResult = $xpath->query('./ApprovedUsers/User', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedUsers[] = trim($groupNode->nodeValue);

			$excludedUsers = array();
			$queryResult = $xpath->query('./ExcludedUsers/User', $contextNode);
			foreach ($queryResult as $groupNode)
				$excludedUsers[] = trim($groupNode->nodeValue);

			$approvedGroups = array();
			$queryResult = $xpath->query('./ApprovedGroups/Group', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedGroups[] = trim($groupNode->nodeValue);

			$excludedGroups = array();
			$queryResult = $xpath->query('./ExcludedGroups/Group', $contextNode);
			foreach ($queryResult as $groupNode)
				$excludedGroups[] = trim($groupNode->nodeValue);

			$this->isAccessGranted = true;

			if (isset($user) && count($excludedUsers) > 0)
				$this->isAccessGranted &= !in_array($user->login, $excludedUsers);
			if (isset($user) && count($excludedGroups) > 0)
				$this->isAccessGranted &= !array_intersect($userGroups, $excludedGroups);

			if ($this->isAccessGranted && (count($approvedUsers) > 0 || count($approvedGroups) > 0))
			{
				$this->isAccessGranted = false;
				if (isset($user) && isset($user->login))
				{
					$this->isAccessGranted |= in_array($user->login, $approvedUsers);
					if (count($userGroups) > 0)
						$this->isAccessGranted |= array_intersect($userGroups, $approvedGroups);
				}
			}
		}

		/** @return TextAppearance */
		public function getTextAppearance()
		{
			if (isset($this->textAppearance))
				return $this->textAppearance;
			if (isset($this->parentBlock))
				return $this->parentBlock->textAppearance;
			return null;
		}

		/** @return string */
		public function getViewName()
		{
			switch ($this->type)
			{
				case 'row':
					return 'row';
				case 'column':
					return 'column';
				case 'image':
					return 'image';
				case 'text':
					return 'text';
				case 'list':
					return 'list';
				case 'list-item':
					return 'listItem';
				case 'url':
					return 'url';
				case 'shortcut':
					return 'shortcut';
				case 'search-bar':
					return 'searchBar';
				case 'slider':
					return 'slider';
				case 'trending':
					return 'trending';
				default:
					return 'undefinedBlock';
			}
		}

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return ContentBlock
		 */
		public static function fromXml($parentShortcut, $parentBlock, $xpath, $contextNode)
		{
			$typeAttribute = $contextNode->attributes->getNamedItem("Type");
			$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
			switch ($type)
			{
				case "row":
					$row = new Row($parentShortcut, $parentBlock);
					$row->configureFromXml($xpath, $contextNode);
					return $row;
				case "column":
					$column = new Column($parentShortcut, $parentBlock);
					$column->configureFromXml($xpath, $contextNode);
					return $column;
				case "image":
					$imageBlock = new ImageBlock($parentShortcut, $parentBlock);
					$imageBlock->configureFromXml($xpath, $contextNode);
					return $imageBlock;
				case "text":
					$textBlock = new TextBlock($parentShortcut, $parentBlock);
					$textBlock->configureFromXml($xpath, $contextNode);
					return $textBlock;
				case "list":
					$listBlock = new ListBlock($parentShortcut, $parentBlock);
					$listBlock->configureFromXml($xpath, $contextNode);
					return $listBlock;
				case "list-item":
					$listItem = new ListItem($parentShortcut, $parentBlock);
					$listItem->configureFromXml($xpath, $contextNode);
					return $listItem;
				case "url":
					$urlBlock = new UrlBlock($parentShortcut, $parentBlock);
					$urlBlock->configureFromXml($xpath, $contextNode);
					return $urlBlock;
				case "shortcut":
					$shortcutBlock = new ShortcutBlock($parentShortcut, $parentBlock);
					$shortcutBlock->configureFromXml($xpath, $contextNode);
					return $shortcutBlock;
				case "search-bar":
					$searchBarBlock = new SearchBarBlock($parentShortcut, $parentBlock);
					$searchBarBlock->configureFromXml($xpath, $contextNode);
					return $searchBarBlock;
				case "slider":
					$sliderBlock = new SliderBlock($parentShortcut, $parentBlock);
					$sliderBlock->configureFromXml($xpath, $contextNode);
					return $sliderBlock;
				case "slide":
					$slideBlock = new SlideBlock($parentShortcut, $parentBlock);
					$slideBlock->configureFromXml($xpath, $contextNode);
					return $slideBlock;
				case "trending":
					$trendingBlock = new TrendingBlock($parentShortcut, $parentBlock);
					$trendingBlock->configureFromXml($xpath, $contextNode);
					return $trendingBlock;
				default:
					return new UndefinedBlock($parentShortcut, $parentBlock);
			}
		}
	}