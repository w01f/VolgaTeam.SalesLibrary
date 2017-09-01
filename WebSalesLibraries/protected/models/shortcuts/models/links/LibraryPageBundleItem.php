<?

	use application\models\wallbin\models\web\LibraryPage;

	class LibraryPageBundleItem
	{
		public $name;

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
			$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			if (isset($shortcutId))
			{
				/** @var  $shortcutRecord \ShortcutLinkRecord */
				$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($shortcutId);
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

		/**
		 * @param $libraryPage LibraryPage
		 * @return LibraryPageBundleItem
		 */
		public static function fromLibraryPage($libraryPage)
		{
			$item = new self();
			$item->name = $libraryPage->name;
			$item->libraryPage = $libraryPage;
			return $item;
		}
	}