<?php
class SearchControlPanel extends CWidget
{
    public function run()
    {
        $libraryManager = new LibraryManager();
        $libraryGroups = $libraryManager->getLibraryGroups();

        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
            $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);

        foreach ($libraryGroups as $libraryGroup)
            foreach ($libraryGroup->libraries as $library)
                if (isset($checkedLibraryIds))
                    $library->selected = in_array($library->id, $checkedLibraryIds);
                else
                    $library->selected = true;

        $categories = new CategoryManager();
        $categories->loadCategories();

        $this->render('application.views.regular.widgets.searchControlPanel', array('libraryGroups' => $libraryGroups, 'categories' => $categories));
    }

}

?>
