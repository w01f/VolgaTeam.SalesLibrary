<?
	/** @var $shortcut LibraryPageBundleShortcut */

	$this->renderPartial('../wallbin/pageBundleContent',
		array(
			'wallbinId' => $shortcut->id,
			'wallbinName' => $shortcut->title,
			'pageItems' => $shortcut->items,
			'defaultPageItem' => $shortcut->items[0],
			'openOnSamePage' => $shortcut->samePage)
	);
?>

