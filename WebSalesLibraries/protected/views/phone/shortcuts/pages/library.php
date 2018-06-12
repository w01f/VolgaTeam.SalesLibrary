<?
	/** @var $shortcut WallbinShortcut */

	/** @var LibraryPageBundleItem[] $pageItems */
	$pageItems = array();
	foreach ($shortcut->library->pages as $page)
		$pageItems[] = LibraryPageBundleItem::fromLibraryPage($page);

	$pageItems[0]->libraryPage->loadData();

	$this->renderPartial('../wallbin/pageBundleContent',
		array(
			'wallbinId' => $shortcut->id,
			'wallbinName' => $shortcut->library->alias,
			'pageItems' => $pageItems,
			'defaultPageItem' => $pageItems[0],
			'openOnSamePage' => $shortcut->samePage)
	);
?>

