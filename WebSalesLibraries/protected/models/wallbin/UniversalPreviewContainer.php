<?php
class UniversalPreviewContainer
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
     * @var string[]
     * @soap
     */
    public $pngLinks;
    /**
     * @var string[]
     * @soap
     */
    public $jpegLinks;
    /**
     * @var string[]
     * @soap
     */
    public $pdfLinks;
    /**
     * @var string[]
     * @soap
     */
    public $mp4Links;
    /**
     * @var string[]
     * @soap
     */
    public $ogvLinks;
    /**
     * @var string[]
     * @soap
     */
    public $oldOfficeFormatLinks;
    /**
     * @var string[]
     * @soap
     */
    public $newOfficeFormatLinks;
    /**
     * @var string[]
     * @soap
     */
    public $thumbsLinks;
    /**
     * @var int
     * @soap
     */
    public $thumbsWidth;
    /**
     * @var int
     * @soap
     */
    public $thumbsHeight;
    public function __construct($library)
    {
        $this->parent = $library;
    }

    public function load($previewRecords)
    {
        foreach ($previewRecords as $record)
        {
            $this->id = $record->id_container;
            $this->libraryId = $record->id_library;
            $previewLink = str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->storageLink . '/' . $record->relative_path)));
            switch ($record->type)
            {
                case 'png':
                    $this->pngLinks[] = $previewLink;
                    break;
                case 'jpeg':
                    $this->jpegLinks[] = $previewLink;
                    break;
                case 'pdf':
                    $this->pdfLinks[] = $previewLink;
                    break;
                case 'thumbs':
                    $this->thumbsLinks[] = $previewLink;
                    break;
                case 'mp4':
                    $this->mp4Links[] = $previewLink;
                    break;
                case 'ogv':
                    $this->ogvLinks[] = $previewLink;
                    break;
            }
            if ($record->thumb_width > 0)
                $thumbsWidth = $record->thumb_width * 0.75;
            if ($record->thumb_height > 0)
                $thumbsHeight = $record->thumb_height * 0.75;
        }
        if (isset($thumbsHeight))
            $this->thumbsHeight = $thumbsHeight;
        if (isset($thumbsWidth))
            $this->thumbsWidth = $thumbsWidth;
        if (isset($this->pngLinks))
            natsort($this->pngLinks);
        if (isset($this->pdfLinks))
            natsort($this->pdfLinks);
        if (isset($this->jpegLinks))
            natsort($this->jpegLinks);
        if (isset($this->oldOfficeFormatLinks))
            natsort($this->oldOfficeFormatLinks);
        if (isset($this->newOfficeFormatLinks))
            natsort($this->newOfficeFormatLinks);
        if (isset($this->thumbsLinks))
            natsort($this->thumbsLinks);
        if (isset($this->mp4Links))
            natsort($this->mp4Links);
        if (isset($this->ogvLinks))
            natsort($this->ogvLinks);
    }

}

?>