<?

	class PageHeaderSettings
	{
		public $title;
		public $icon;
		public $showMainSiteUrl;

		public $barBackColor;
		public $titleColor;
		public $iconColor;
		public $openMainMenuButtonColor;
		public $openActionMenuButtonColor;
		public $shortcutGroupsColor;

		/** @var  HideCondition */
		public $hideIconCondition;
		/** @var  HideCondition */
		public $hideTextCondition;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return PageHeaderSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = self::createEmpty();

			$queryResult = $xpath->query('./Icon', $contextNode);
			$instance->icon = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->icon;

			$queryResult = $xpath->query('./Fullsitelink', $contextNode);
			$instance->showMainSiteUrl = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->showMainSiteUrl;

			$queryResult = $xpath->query('./BarColor', $contextNode);
			$instance->barBackColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->barBackColor;

			$queryResult = $xpath->query('./TextColor', $contextNode);
			$instance->titleColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->titleColor;

			$queryResult = $xpath->query('./IconColor', $contextNode);
			$instance->iconColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->iconColor;

			$queryResult = $xpath->query('./MenuButtonColor', $contextNode);
			$instance->openMainMenuButtonColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->openMainMenuButtonColor;

			$queryResult = $xpath->query('./ActionMenuButtonColor', $contextNode);
			$instance->openActionMenuButtonColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->openActionMenuButtonColor;

			$queryResult = $xpath->query('./MenuItemsColor', $contextNode);
			$instance->shortcutGroupsColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->shortcutGroupsColor;

			$queryResult = $xpath->query('./HideIcon', $contextNode);
			if ($queryResult->length > 0)
				$instance->hideIconCondition = HideCondition::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./HideTitle', $contextNode);
			if ($queryResult->length > 0)
				$instance->hideTextCondition = HideCondition::fromXml($xpath, $queryResult->item(0));

			return $instance;
		}

		/**
		 * @return PageHeaderSettings
		 */
		public static function createEmpty()
		{
			$instance = new self();

			$instance->icon = '';
			$instance->showMainSiteUrl = false;

			$instance->barBackColor = Yii::app()->params['menu']['BarColor'];
			$instance->titleColor = Yii::app()->params['menu']['HeaderTextColor'];
			$instance->iconColor = Yii::app()->params['menu']['HeaderIconColor'];
			$instance->openMainMenuButtonColor = Yii::app()->params['menu']['MenuButtonColor'];
			$instance->openActionMenuButtonColor = Yii::app()->params['menu']['ActionMenuButtonColor'];
			$instance->shortcutGroupsColor = Yii::app()->params['menu']['MenuItemsColor'];

			$instance->hideIconCondition = new HideCondition();
			$instance->hideTextCondition = new HideCondition();

			return $instance;
		}
	}