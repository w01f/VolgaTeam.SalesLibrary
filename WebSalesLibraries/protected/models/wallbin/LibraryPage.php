<?php
class LibraryPage
{
    public $parent;
    public $name;
    public $order;
    public $logoPath;
    public $folders;
    public $enableColumns;
    public $columns;
    public function __construct($library)
    {
        $this->parent = $library;
    }

    public function load($pageXMLNode)
    {
        $this->name = trim($pageXMLNode->getElementsByTagName("Name")->item(0)->nodeValue);
        $this->order = intval($pageXMLNode->getElementsByTagName("Order")->item(0)->nodeValue);

        $logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $this->parent->name . "/page" . strval($this->order + 1) . ".png";
        if (file_exists($logoPath))
            $this->logoPath = $logoPath;
        else
            $this->logoPath = $this->parent->logoPath;

        $folderNodes = $pageXMLNode->getElementsByTagName("Folder");
        foreach ($folderNodes as $folderNode)
        {
            $folder = new LibraryFolder($this);
            $folder->load($folderNode);
            $this->folders[] = $folder;
        }
        if (isset($this->folders))
            usort($this->folders, "LibraryFolder::libraryFolderComparer");


        $node = $pageXMLNode->getElementsByTagName("EnableColumnTitles")->item(0);
        if (isset($node))
            $this->enableColumns = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);
        else
            $this->enableColumns = FALSE;

        $columnNodes = $pageXMLNode->getElementsByTagName("ColumnTitle");
        foreach ($columnNodes as $columnNode)
        {
            $column = new Column($this);
            $column->load($columnNode);
            $this->columns[] = $column;
        }
        if (isset($this->columns))
            usort($this->columns, "Column::columnComparer");
    }

    public function getFoldersByColumn($columnOrder)
    {
        foreach ($this->folders as $folder)
        {
            if ($folder->columnOrder == $columnOrder)
                $columnFolders[] = $folder;
        }
        if (isset($columnFolders))
            return $columnFolders;
        else
            return NULL;
    }

    public static function libraryPageComparer($x, $y)
    {
        if ($x->order == $y->order)
            return 0;
        else
            return ($x->order < $y->order) ? -1 : 1;
    }

}

?>