<?php
class LibraryFolder
{
    public $parent;
    public $name;
    public $rowOrder;
    public $columnOrder;
    public $borderColor;
    public $windowBackColor;
    public $windowForeColor;
    public $headerBackColor;
    public $headerForeColor;
    public $windowFont;
    public $headerFont;
    public $headerAlignment;
    public $enableWidget;
    public $widget;
    public $banner;
    public $files;
    public $displayLinkWidgets;
    public function __construct($page)
    {
        $this->parent = $page;
    }

    public function load($folderXMLNode)
    {
        $this->name = $folderXMLNode->getElementsByTagName("Name")->item(0)->nodeValue;
        $this->rowOrder = intval($folderXMLNode->getElementsByTagName("RowOrder")->item(0)->nodeValue);
        $this->columnOrder = intval($folderXMLNode->getElementsByTagName("ColumnOrder")->item(0)->nodeValue);

        $node = $folderXMLNode->getElementsByTagName("BorderColor")->item(0);
        if (isset($node))
            $this->borderColor = str_pad(dechex(intval($node->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);
        else
            $this->borderColor = "000000";
        $this->windowBackColor = str_pad(dechex(intval($folderXMLNode->getElementsByTagName("BackgroundWindowColor")->item(0)->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);
        $this->windowForeColor = str_pad(dechex(intval($folderXMLNode->getElementsByTagName("ForeWindowColor")->item(0)->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);
        $this->headerBackColor = str_pad(dechex(intval($folderXMLNode->getElementsByTagName("BackgroundHeaderColor")->item(0)->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);
        $this->headerForeColor = str_pad(dechex(intval($folderXMLNode->getElementsByTagName("ForeHeaderColor")->item(0)->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);

        $node = $folderXMLNode->getElementsByTagName("HeaderFont")->item(0);
        if (isset($node))
        {
            $this->headerFont = Utils::parseFont($node->nodeValue);
        }
        else
        {
            $this->headerFont = new Font();
            $this->headerFont->name = "Arial";
            $this->headerFont->size = "14";
            $this->headerFont->isBold = TRUE;
            $this->headerFont->isItalic = FALSE;
        }

        $node = $folderXMLNode->getElementsByTagName("WindowFont")->item(0);
        if (isset($node))
        {
            $this->windowFont = Utils::parseFont($node->nodeValue);
        }
        else
        {
            $this->windowFont = new Font();
            $this->windowFont->name = "Arial";
            $this->windowFont->size = "12";
            $this->windowFont->isBold = FALSE;
            $this->windowFont->isItalic = FALSE;
        }

        $node = $folderXMLNode->getElementsByTagName("HeaderAligment")->item(0);
        if (isset($node))
        {
            $alignmentValue = intval($node->nodeValue);
            switch ($alignmentValue)
            {
                case 0:
                    $this->headerAlignment = "left";
                    break;
                case 1:
                    $this->headerAlignment = "center";
                    break;
                case 2:
                    $this->headerAlignment = "right";
                    break;
                default :
                    $this->headerAlignment = "center";
                    break;
            }
        }
        else
            $this->headerAlignment = "center";

        $node = $folderXMLNode->getElementsByTagName("EnableWidget")->item(0);
        if (isset($node))
            $this->enableWidget = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);


        $node = $folderXMLNode->getElementsByTagName("Widget")->item(0);
        if (isset($node))
            $this->widget = $node->nodeValue;

        $node = $folderXMLNode->getElementsByTagName("BannerProperties")->item(0);
        if (isset($node))
        {
            $this->banner = new Banner($this);
            $this->banner->load($node);
        }

        $fileNodes = $folderXMLNode->getElementsByTagName("File");
        foreach ($fileNodes as $fileNode)
        {
            $file = new LibraryLink($this);
            $file->load($fileNode);
            if (isset($file->name))
                $this->files[] = $file;
        }

        $this->displayLinkWidgets = FALSE;
        if (isset($this->files))
            foreach ($this->files as $file)
            {
                $widget = $file->getWidget();
                if ($widget != null)
                {
                    $this->displayLinkWidgets = TRUE;
                    break;
                }
            }
    }

    public function getWidget()
    {
        if (isset($this->enableWidget))
            if (isset($this->widget))
                if ($this->widget != '')
                    return $this->widget;
    }

    public static function libraryFolderComparer($x, $y)
    {
        if ($x->rowOrder == $y->rowOrder)
            return 0;
        else
            return ($x->rowOrder < $y->rowOrder) ? -1 : 1;
    }

}

?>