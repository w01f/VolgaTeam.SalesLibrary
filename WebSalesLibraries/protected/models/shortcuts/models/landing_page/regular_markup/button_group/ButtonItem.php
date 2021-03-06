<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\button_group;

	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	class ButtonItem
	{
		/** @var ButtonGroupBlock */
		public $parentGroup;

		public $id;
		public $type;

		public $icon;
		public $image;
		public $text;
		public $hoverTip;

		/** @var  TextAppearance */
		public $textAppearance;

		public $backgroundColor;
		public $backgroundHoverColor;

		public $isAccessGranted;

		/** @var  \BaseShortcut */
		public $shortcut;
		public $url;

		/**
		 * @param $parentGroup ButtonGroupBlock
		 */
		public function __construct($parentGroup)
		{
			$this->id = uniqid();
			$this->type = 'undefined';
			$this->parentGroup = $parentGroup;
			$this->backgroundColor = "ffffff";
			$this->backgroundHoverColor = "ffffff";
			$this->isAccessGranted = true;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Text', $contextNode);
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->text;

			$queryResult = $xpath->query('./HoverTip', $contextNode);
			$this->hoverTip = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->hoverTip;

			$queryResult = $xpath->query('./Icon', $contextNode);
			$this->icon = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->icon;

			$queryResult = $xpath->query('./Image', $contextNode);
			$imageFileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (!empty($imageFileName))
				$this->image = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $this->parentGroup->parentShortcut->relativeLink . '/images/' . $imageFileName);
			else
				$this->image = null;

			$queryResult = $xpath->query('./TextStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->textAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));
			else
				$this->textAppearance = $this->parentGroup->textAppearance;

			$queryResult = $xpath->query('./BackgroundColor', $contextNode);
			$this->backgroundColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->backgroundColor;

			$queryResult = $xpath->query('./BackgroundHoverColor', $contextNode);
			$this->backgroundHoverColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->backgroundHoverColor;

			$queryResult = $xpath->query('./Url', $contextNode);
			if ($queryResult->length > 0)
			{
				$this->type = 'url';
				$this->url = trim($queryResult->item(0)->nodeValue);
			}
			else
			{
				$queryResult = $xpath->query('./ShortcutID', $contextNode);
				$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
				if (isset($shortcutId))
				{
					/** @var  $shortcutRecord \ShortcutLinkRecord */
					$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($shortcutId);
					if (isset($shortcutRecord))
					{
						/** @var  $shortcut \CustomHandledShortcut */
						$shortcut = $shortcutRecord->getRegularModel(false, null);
						if ($shortcut->samePage)
						{
							$this->type = 'shortcut';
							$this->shortcut = $shortcut;
						}
						else
						{
							$this->type = 'url';
							$this->url = $shortcut->getSourceLink();
						}
					}
				}
			}
		}
	}