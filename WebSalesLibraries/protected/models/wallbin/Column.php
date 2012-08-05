<?php
class Column
{
    public $parent;
    public $name;
    public $order;
    public $backColor;
    public $foreColor;
    public $font;
    public $showText;
    public $alignment;
    public $enableWidget;
    public $widget;
    public $banner;
    public function __construct($page)
    {
        $this->parent = $page;
        $this->name = '';
        $this->order = 0;
        $this->backColor = "000000";
        $this->foreColor = "FFFFFF";
        $this->font = new Font();
        $this->font->name = "Arial";
        $this->font->size = "14";
        $this->font->isBold = TRUE;
        $this->font->isItalic = FALSE;
        $this->showText = FALSE;
        $this->alignment = 'center';
        $this->enableWidget = FALSE;
    }

    public function load($columnXMLNode)
    {
        $this->name = $columnXMLNode->getElementsByTagName("Name")->item(0)->nodeValue;

        $node = $columnXMLNode->getElementsByTagName("ColumnOrder")->item(0);
        if (isset($node))
            $this->order = intval($node->nodeValue);

        $node = $columnXMLNode->getElementsByTagName("BackgroundColor")->item(0);
        if (isset($node))
            $this->backColor = str_pad(dechex(intval($node->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);

        $node = $columnXMLNode->getElementsByTagName("ForeColor")->item(0);
        if (isset($node))
            $this->foreColor = str_pad(dechex(intval($node->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);

        $node = $columnXMLNode->getElementsByTagName("HeaderFont")->item(0);
        if (isset($node))
            $this->font = Utils::parseFont($node->nodeValue);

        $node = $columnXMLNode->getElementsByTagName("EnableText")->item(0);
        if (isset($node))
            $this->showText = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);

        $node = $columnXMLNode->getElementsByTagName("HeaderAligment")->item(0);
        if (isset($node))
        {
            $alignmentValue = intval($node->nodeValue);
            switch ($alignmentValue)
            {
                case 0:
                    $this->alignment = "left";
                    break;
                case 1:
                    $this->alignment = "center";
                    break;
                case 2:
                    $this->alignment = "right";
                    break;
                default :
                    $this->alignment = "center";
                    break;
            }
        }

        $node = $columnXMLNode->getElementsByTagName("EnableWidget")->item(0);
        if (isset($node))
            $this->enableWidget = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);


        $node = $columnXMLNode->getElementsByTagName("Widget")->item(0);
        if (isset($node))
            $this->widget = $node->nodeValue;

        $node = $columnXMLNode->getElementsByTagName("BannerProperties")->item(0);
        if (isset($node))
        {
            $this->banner = new Banner($this);
            $this->banner->load($node);
        }
    }

    public function getWidget()
    {
        if (isset($this->enableWidget))
            if (isset($this->widget))
                if ($this->widget != '')
                    return $this->widget;
    }

    public static function columnComparer($x, $y)
    {
        if ($x->order == $y->order)
            return 0;
        else
            return ($x->order < $y->order) ? -1 : 1;
    }

}

?>
