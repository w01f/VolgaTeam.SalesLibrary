<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\wallbin;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class LibraryPageBundleBlock
	 */
	class LibraryPageBundleBlock extends ContentBlock
	{
		/** @var  \LibraryPageBundleShortcut */
		public $shortcut;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'library-page-bundle';
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
					/** @var  $shortcut \LibraryPageBundleShortcut */
					$this->shortcut = $shortcutRecord->getRegularModel(false, null);
					$this->shortcut->loadPageConfig();
				}
			}
		}
	}