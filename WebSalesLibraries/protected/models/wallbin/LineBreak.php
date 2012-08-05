<?php
class LineBreak
{
    public $parent;
    public $font;
    public $foreColor;
    public $note;
    
    public function __construct($link)
    {
        $this->parent = $link;
    }

    public function load($lineBreakXMLNode)
    {

        $node = $lineBreakXMLNode->getElementsByTagName("Font")->item(0);
        if (isset($node))
        {
            $this->font = Utils::parseFont($node->nodeValue);
        }
        else
        {
            $this->font = new Font();
            $this->font->name = "Arial";
            $this->font->size = "12";
            $this->font->isBold = FALSE;
            $this->font->isItalic = FALSE;
        }

        $node = $lineBreakXMLNode->getElementsByTagName("ForeColor")->item(0);
        if (isset($node))
            $this->foreColor = str_pad(dechex(intval($node->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);
        else
            $this->foreColor = "000000";

        $node = $lineBreakXMLNode->getElementsByTagName("Note")->item(0);
        if (isset($node))
            $this->note = $node->nodeValue;
        else
            $this->note = '';
    }
}

?>
