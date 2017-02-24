<?
	namespace application\models\shortcuts\models\landing_page\regular_markup;
	/**
	 * Class SliderBlock
	 */
	class SliderBlock extends BlockContainer
	{
		public $id;
		public $slideShow;
		public $slideShowInterval;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'slider';
			$this->id = $parentShortcut->id;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./SlideShow', $contextNode);
			$this->slideShow = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('./SlideShowInterval', $contextNode);
			$this->slideShowInterval = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 5000;

			$queryResult = $xpath->query('./ContentBlock', $contextNode);
			foreach ($queryResult as $node)
			{
				$typeAttribute = $node->attributes->getNamedItem("Type");
				$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
				$contentBlock = null;
				switch ($type)
				{
					case "url":
						$contentBlock = new SlideUrlBlock($this->parentShortcut, $this);
						$contentBlock->configureFromXml($xpath, $node);
						break;
					case "shortcut":
						$contentBlock = new SlideShortcutBlock($this->parentShortcut, $this);
						$contentBlock->configureFromXml($xpath, $node);
						break;
					default:
						$contentBlock = ContentBlock::fromXml($this->parentShortcut, $this, $xpath, $node);
						break;
				}
				if (isset($contentBlock))
					$this->items[] = $contentBlock;
			}
		}
	}