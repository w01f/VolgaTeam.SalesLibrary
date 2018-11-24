<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\button_group;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	class ButtonGroupBlock extends ContentBlock
	{
		public $height;

		/** @var  ButtonItem[] */
		public $buttons;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'button-group';
			$this->height = 0;
			$this->buttons = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			if (!$this->parentShortcut->usePermissions || $this->isAccessGranted)
			{
				$queryResult = $xpath->query('./Height', $contextNode);
				$this->height = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->height;

				$queryResult = $xpath->query('./Button', $contextNode);
				foreach ($queryResult as $node)
				{
					$typeAttribute = $node->attributes->getNamedItem("Type");
					$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
					$button = null;
					switch ($type)
					{
						default:
							$button = new ButtonItem($this);
							$button->configureFromXml($xpath, $node);
							break;
					}
					if (isset($button) && $button->isAccessGranted)
						$this->buttons[] = $button;
				}
			}
		}
	}