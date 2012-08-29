<?php
class Banner
{
    public $parent;
    /**
     * @var string
     * @soap
     */
    public $id;
    /**
     * @var string
     * @soap
     */
    public $libraryId;
    /**
     * @var boolean
     * @soap
     */
    public $isEnabled;
    /**
     * @var string
     * @soap
     */
    public $image;
    /**
     * @var boolean
     * @soap
     */
    public $showText;
    /**
     * @var string
     * @soap
     */
    public $imageAlignment;
    /**
     * @var string
     * @soap
     */
    public $text;
    /**
     * @var Font
     * @soap
     */
    public $font;
    /**
     * @var string
     * @soap
     */
    public $foreColor;
    public function __construct($link)
    {
        $this->parent = $link;
        $this->imageAlignment = "left";
        $this->isEnabled = FALSE;
        $this->showText = FALSE;
    }

    public function load($bannerXMLNode)
    {
        $node = $bannerXMLNode->getElementsByTagName("Enable")->item(0);
        if (isset($node))
            $this->isEnabled = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);
        else
            $this->isEnabled = FALSE;

        $node = $bannerXMLNode->getElementsByTagName("Image")->item(0);
        if (isset($node))
            $this->image = $node->nodeValue;

        $node = $bannerXMLNode->getElementsByTagName("ShowText")->item(0);
        if (isset($node))
            $this->showText = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);
        else
            $this->showText = FALSE;

        $node = $bannerXMLNode->getElementsByTagName("ImageAligement")->item(0);
        if (isset($node))
        {
            $alignmentValue = intval($node->nodeValue);
            switch ($alignmentValue)
            {
                case 0:
                    $this->imageAlignment = "left";
                    break;
                case 1:
                    $this->imageAlignment = "center";
                    break;
                case 2:
                    $this->imageAlignment = "right";
                    break;
                default :
                    $this->imageAlignment = "left";
                    break;
            }
        }
        else
            $this->imageAlignment = "left";

        $node = $bannerXMLNode->getElementsByTagName("Text")->item(0);
        if (isset($node))
            $this->text = nl2br($node->nodeValue);
        else
            $this->text = '';

        $node = $bannerXMLNode->getElementsByTagName("Font")->item(0);
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

        $node = $bannerXMLNode->getElementsByTagName("ForeColor")->item(0);
        if (isset($node))
            $this->foreColor = str_pad(dechex(intval($node->nodeValue) + 16777216), 6, "0", STR_PAD_LEFT);
        else
            $this->foreColor = "000000";
    }

}

?>
