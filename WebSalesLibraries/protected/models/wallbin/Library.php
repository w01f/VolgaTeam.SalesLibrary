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
    public $storageFile;
    public $storageLink;
    public $presentationPreviewContainerLink;
    public $universalPreviewContainerLink;
    public $presentationPreviewContainerPath;
    public $universalPreviewContainerPath;
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
        if (file_exists($this->storageFile))
        {
            $doc = new DOMDocument();
            $doc->load($this->storageFile);

            $pageNodes = $doc->getElementsByTagName("Page");
            foreach ($pageNodes as $pageNode)
            {
                $page = new LibraryPage($this);
                $page->load($pageNode);
                $this->pages[] = $page;
            }
            usort($this->pages, "LibraryPage::libraryPageComparer");

            $autoWidgetNodes = $doc->getElementsByTagName("AutoWidget");
            foreach ($autoWidgetNodes as $autoWidgetNode)
            {
                $autoWidget = new AutoWidget();
                $autoWidget->load($autoWidgetNode);
                $this->autoWidgets[] = $autoWidget;
            }
        }
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