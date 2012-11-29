<?php
class Library
{
    /**
     * @var string id
     * @soap
     */
    public $id;
    /**
     * @var string name
     * @soap
     */
    public $name;
    public $groupId;
    public $order;
    public $storagePath;
    public $storageLink;
    public $logoPath;
    public $logoLink;
    /**
     * @var LibraryPage[]
     * @soap
     */
    public $pages;
    /**
     * @var AutoWidget[] 
     * @soap
     */
    public $autoWidgets;
    /**
     * @var UniversalPreviewContainer[]
     * @soap
     */
    public $previewContainers;
    /**
     * @var boolean selected
     * @soap
     */
    public $selected;
    public function load()
    {
        $userId = Yii::app()->user->getId();
        if (isset($userId))
            $availablePageIds = UserLibraryStorage::getPageIdsByUser(Yii::app()->user->getId());
        foreach (LibraryPageStorage::model()->findAll('id_library=?', array($this->id)) as $pageRecord)
        {
            $page = new LibraryPage($this);
            $page->load($pageRecord);
            if (in_array($page->id, $availablePageIds))
                $this->pages[] = $page;
        }
        if (isset($this->pages))
            usort($this->pages, "LibraryPage::libraryPageComparer");

        foreach (AutoWidgetStorage::model()->findAll('id_library=?', array($this->id)) as $autoWidgetRecord)
        {
            $autoWidget = new AutoWidget();
            $autoWidget->load($autoWidgetRecord);
            $this->autoWidgets[] = $autoWidget;
        }
    }

    public function buildCache($controller)
    {
        foreach ($this->pages as $page)
            $page->buildCache($controller);
    }

    public function getAutoWidget($extension)
    {
        foreach ($this->autoWidgets as $autoWidget)
            if (strpos($autoWidget->extension, $extension) !== false)
                return $autoWidget->widget;
        return null;
    }

    public static function libraryComparerByName($x, $y)
    {
        if ($x->name == $y->name)
            return 0;
        else
            return ($x->name < $y->name) ? -1 : 1;
    }

    public static function libraryComparerByGroup($x, $y)
    {
        if ($x->order == $y->order)
            return 0;
        else
            return ($x->order < $y->order) ? -1 : 1;
    }

}

?>