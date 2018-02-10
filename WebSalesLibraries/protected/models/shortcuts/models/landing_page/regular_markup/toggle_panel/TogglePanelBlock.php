<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\toggle_panel;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;

	/**
	 * Class TogglePanelBlock
	 */
	class TogglePanelBlock extends BlockContainer
	{
		/** @var  TogglePanelButton[] */
		public $toggleButtons;

		/** @var  TogglePanelButton */
		public $defaultToggleButton;

		/** @var  ToggleButtonStyle */
		public $buttonStyle;

		/** @var  \Padding */
		public $buttonPadding;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'toggle-panel';
			$this->toggleButtons = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Toggles/Item', $contextNode);
			foreach ($queryResult as $node)
			{
				$togglePanelButton = new TogglePanelButton();
				$togglePanelButton->configureFromXml($xpath, $node);
				if ($togglePanelButton->isAccessGranted)
				{
					$this->toggleButtons[] = $togglePanelButton;
					if ($togglePanelButton->isDefault)
						$this->defaultToggleButton = $togglePanelButton;
				}
			}

			if (isset($this->defaultToggleButton))
				foreach ($this->items as $item)
				{
					/** @var $item TogglePanelItem */
					if ($item->tag === $this->defaultToggleButton->tag)
					{
						$item->isDefault = true;
						break;
					}
				}

			$queryResult = $xpath->query('./ButtonStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->buttonStyle = ToggleButtonStyle::fromXml($xpath, $queryResult->item(0));
			else
				$this->buttonStyle = ToggleButtonStyle::createDefault();

			$queryResult = $xpath->query('./Toggles/Padding', $contextNode);
			if ($queryResult->length > 0)
				$this->buttonPadding = \Padding::fromXml($xpath, $queryResult->item(0));
		}
	}