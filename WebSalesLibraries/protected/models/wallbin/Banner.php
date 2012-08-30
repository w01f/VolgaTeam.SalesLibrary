<?php
class Banner
{
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

    public function load($bannerRecord)
    {
        $this->id = $bannerRecord->id;
        $this->libraryId = $bannerRecord->id_library;        
        $this->isEnabled = $bannerRecord->enabled;
        $this->image = $bannerRecord->image;
        $this->showText = $bannerRecord->show_text;
        $this->imageAlignment = $bannerRecord->image_alignment;
        $this->text = $bannerRecord->text;
        $this->foreColor = $bannerRecord->fore_color;
        $this->font = new Font();
        $this->font->name = $bannerRecord->font_name;
        $this->font->size = $bannerRecord->font_size;
        $this->font->isBold = $bannerRecord->font_bold;
        $this->font->isItalic = $bannerRecord->font_italic;
    }
}

?>
