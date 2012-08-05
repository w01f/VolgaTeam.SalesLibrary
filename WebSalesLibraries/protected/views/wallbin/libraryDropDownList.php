<?php
$selectedLibrary = $libraryManager->getSelectedLibrary();
$librariesList = "";
foreach ($libraryManager->getLibraries() as $library)
{
    $librariesList .= '<option value=' . $library->logoPath;
    if ($selectedLibrary->name == $library->name)
        $librariesList .= " selected='selected'";
    $librariesList .= ' >' . $library->name . '</option>';
}
echo $librariesList;
?>
