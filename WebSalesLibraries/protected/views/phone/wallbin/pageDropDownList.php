<?
	/**
	 * @var $selectedLibrary Library
	 * @var $selectedPage LibraryPage
	 */
	$libraryPagesList = "";
	foreach ($selectedLibrary->pages as $page)
	{
		$libraryPagesList .= '<option value=' . $page->logoLink;
		if ($selectedPage->name == $page->name)
			$libraryPagesList .= " selected='selected'";
		$libraryPagesList .= ' >' . $page->name . '</option>';
	}
	echo $libraryPagesList;
