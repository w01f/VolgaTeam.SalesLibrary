<?php
class SearchControlPanel extends CWidget
{
    public function run()
    {
        $libraryManager = new LibraryManager();

        $libraries = $libraryManager->getLibraries();
        $libraryGroups = $libraryManager->getLibraryGroups();

        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
        {
            if (count($libraryGroups) > 1 || count($libraries) > 1)
                $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
            else
                unset(Yii::app()->request->cookies['selectedLibraryIds']);
        }

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
