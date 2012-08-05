<?php
$libraryPagesList = "";
foreach ($selectedLibrary->pages as $page)
{
    $libraryPagesList .= '<option value=' . $page->logoPath;
    if ($selectedPage->name == $page->name)
        $libraryPagesList .= " selected='selected'";
    $libraryPagesList .= ' >' . $page->name . '</option>';
}
echo $libraryPagesList;
?>
