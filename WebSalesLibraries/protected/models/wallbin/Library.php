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
    public $storagePath;
    public $storageLink;
    public $logoPath;
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
    public function load()
    {
        foreach (LibraryPageStorage::model()->findAll('id_library=?', array($this->id)) as $pageRecord)
        {
            $page = new LibraryPage($this);
            $page->load($pageRecord);
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

}

?>