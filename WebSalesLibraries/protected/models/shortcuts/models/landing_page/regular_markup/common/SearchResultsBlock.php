<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\common;


	class SearchResultsBlock extends ContentBlock
	{
		/** @var  \SearchLinkShortcut */
		public $shortcut;
		public $fixedHeight;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'search-results';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ShortcutID', $contextNode);
			$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($shortcutId))
			{
				/** @var  $shortcutRecord \ShortcutLinkRecord */
				$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($shortcutId);
				if (isset($shortcutRecord))
				{
					/** @var  $shortcut \SearchLinkShortcut */
					$this->shortcut = $shortcutRecord->getRegularModel(false, null);
					$this->shortcut->loadPageConfig();
				}
			}

			$queryResult = $xpath->query('./FixedTableHeight', $contextNode);
			$this->fixedHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : null;
		}
	}