<?

	use application\models\wallbin\models\web\LibraryPage;

	class LibraryPageBundleItem
	{
		public $name;
		public $shortcutId;

		/** @var  LibraryPageShortcut */
		public $shortcut;

		/** @var  LibraryPage */
		public $libraryPage;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return LibraryPageBundleItem
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$item = new self();

			$queryResult = $xpath->query('./Name', $contextNode);
			$item->name = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Id', $contextNode);
			$item->shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			if (isset($item->shortcutId))
			{
				/** @var  $shortcutRecord \ShortcutLinkRecord */
				$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($item->shortcutId);
				if (isset($shortcutRecord))
				{
					/** @var  $shortcut LibraryPageShortcut */
					$item->shortcut = $shortcutRecord->getModel(false, null);
					$item->shortcut->loadPageConfig();
					$item->libraryPage = $item->shortcut->getLibraryPage();
				}
			}

			return $item;
		}
	}