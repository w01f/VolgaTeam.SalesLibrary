<?php
class SearchControlPanel extends CWidget
{
    public function run()
    {
        $libraryManager = new LibraryManager();
        $libraryObjects = $libraryManager->getLibraries();

        if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
            $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
        foreach ($libraryObjects as $libraryObject)
        {
            $library['id'] = $libraryObject->id;
            $library['name'] = $libraryObject->name;
            if (isset($checkedLibraryIds))
                $library['selected'] = in_array($libraryObject->id, $checkedLibraryIds);
            else
                $library['selected'] = true;
            $libraries[] = $library;
        }
        if (!isset($libraries))
            $libraries[] = 'All';
        
        $categories = new CategoryManager();
        $categories->loadCategories();

        $this->render('application.views.regular.widgets.searchControlPanel', array('libraries' => $libraries,'categories' => $categories));
    }

}

?>
