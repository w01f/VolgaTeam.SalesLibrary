<?php
class LibraryLink
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
    public $folderId;
    /**
     * @var string
     * @soap
     */
    public $libraryId;
    /**
     * @var string
     * @soap
     */
    public $name;
    /**
     * @var string
     * @soap
     */
    public $fileRelativePath;
    /**
     * @var string
     * @soap
     */
    public $fileName;
    public $fileLink;
    /**
     * @var string
     * @soap
     */
    public $fileExtension;
    /**
     * @var string
     * @soap
     */
    public $note;
    /**
     * @var boolean
     * @soap
     */
    public $isBold;
    /**
     * @var int
     * @soap
     */
    public $order;
    /**
     * @var int
     * @soap
     */
    public $type;
    /**
     * @var LineBreak
     * @soap
     */
    public $lineBreakProperties;
    /**
     * @var boolean
     * @soap
     */
    public $enableWidget;
    /**
     * @var string
     * @soap
     */
    public $widget;
    /**
     * @var Banner
     * @soap
     */
    public $banner;
    /**
     * @var UniversalPreviewContainer
     * @soap
     */
    public $universalPreview;
    public $browser;
    public $originalFormat;
    public $availableFormats;
    public function __construct($folder)
    {
        $this->parent = $folder;
        $this->id = uniqid();
    }

    public function load($linkRecord)
    {
        $this->id = $linkRecord->id;
        $this->folderId = $linkRecord->id_folder;
        $this->libraryId = $linkRecord->id_library;
        $this->name = $linkRecord->name;
        $this->fileRelativePath = $linkRecord->file_relative_path;
        $this->fileName = $linkRecord->file_name;
        $this->fileExtension = $linkRecord->file_extension;
        $this->note = $linkRecord->note;
        $this->isBold = $linkRecord->is_bold;
        $this->order = $linkRecord->order;
        $this->type = $linkRecord->type;
        $this->enableWidget = $linkRecord->enable_widget;
        $this->widget = $linkRecord->widget;

        $lineBreakRecord = LineBreakStorage::model()->findByPk($linkRecord->id_line_break);
        if ($lineBreakRecord !== null)
        {
            $this->lineBreakProperties = new LineBreak();
            $this->lineBreakProperties->load($lineBreakRecord);
        }

        $bannerRecord = BannerStorage::model()->findByPk($linkRecord->id_banner);
        if ($bannerRecord !== null)
        {
            $this->banner = new Banner();
            $this->banner->load($bannerRecord);
        }

        $previewRecords = PreviewStorage::model()->findAll('id_link=?', array($this->id));
        if ($previewRecords !== null)
        {
            $this->universalPreview = new UniversalPreviewContainer($this);
            $this->universalPreview->load($previewRecords);
        }

        if ($this->type == 5)
        {
            $this->fileName = $this->fileRelativePath;
        }
        else if ($this->type == 6)
        {
            
        }
        else if ($this->type == 8)
        {
            $this->fileRelativePath = str_replace('\\', '', $this->fileRelativePath);
            $this->fileName = $this->fileRelativePath;
            $this->fileLink = $this->fileRelativePath;
            $this->originalFormat = 'url';
            $this->availableFormats[] = 'url';
        }
        else
        {
            $this->fileRelativePath = str_replace('\\', '/', $this->fileRelativePath);
            $this->fileLink = str_replace('&', '%26', str_replace('\\', '/', $this->parent->parent->parent->storageLink . $this->fileRelativePath));
            $this->getFormats();
        }
    }

    public function getWidget()
    {
        if (isset($this->enableWidget))
            if (isset($this->widget))
                return $this->widget;
        return $this->parent->parent->parent->getAutoWidget($this->fileExtension);
    }

    public function getIsLineBreak()
    {
        return $this->type === 6 && isset($this->lineBreakProperties);
    }

    public function getFormats()
    {
        if (isset($this->fileExtension))
        {
            switch ($this->fileExtension)
            {
                case 'ppt':
                case 'pptx':
                    $this->originalFormat = 'ppt';
                    $this->availableFormats[] = 'ppt';
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'doc':
                case 'docx':
                    $this->originalFormat = 'doc';
                    $this->availableFormats[] = 'doc';
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'xls':
                case 'xlsx':
                    $this->originalFormat = 'xls';
                    $this->availableFormats[] = 'xls';
                    break;
                case 'pdf':
                    $this->originalFormat = 'pdf';
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'mpeg':
                case 'wmv':
                case 'avi':
                case 'wmz':
                case 'mpg':
                case 'asf':
                case 'mov':
                case 'm4v':
                case 'flv':
                case 'ogv':
                case 'ogm':
                case 'ogx':
                    $this->originalFormat = 'video';
                    switch ($this->browser)
                    {
                        case 'mobile':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'tab';
                            break;
                        case 'ie':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'video';
                            break;
                        case 'webkit':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'tab';
                            break;
                        case 'firefox':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'ogv';
                            break;
                        case 'opera':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'tab';
                            $this->availableFormats[] = 'ogv';
                            break;
                        default:
                            $this->availableFormats[] = 'video';
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'ogv';
                            $this->availableFormats[] = 'tab';
                            break;
                    }
                    break;
                case 'mp4':
                    $this->originalFormat = 'mp4';
                    $this->availableFormats[] = 'mp4';
                    $this->availableFormats[] = 'tab';
                    break;
                case 'png':
                    $this->originalFormat = 'png';
                    $this->availableFormats[] = 'png';
                    break;
                case 'jpg':
                case 'jpeg':
                    $this->originalFormat = 'jpeg';
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'url':
                    $this->originalFormat = 'url';
                    $this->availableFormats[] = 'url';
                    break;
                default:
                    $this->originalFormat = 'other';
                    break;
            }
        }
    }

    public function getViewSource($format)
    {
        switch ($this->originalFormat)
        {
            case 'ppt':
                switch ($format)
                {
                    case 'ppt':
                        $viewSources[] = array('href' => $this->fileLink);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                        {
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->fileName . ' - Slide ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        }
                        break;
                    case 'jpeg':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->jpegLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->jpegLinks);
                                foreach ($this->universalPreview->jpegLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->fileName . ' - Slide ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'pdf':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pdfLinks))
                            {
                                $i = 1;
                                foreach ($this->universalPreview->pdfLinks as $link)
                                {
                                    $viewSources[] = array('href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'thumbs':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
                                foreach ($this->universalPreview->thumbsLinks as $link)
                                    $viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
                        break;
                }
                break;
            case 'doc':
                switch ($format)
                {
                    case 'doc':
                        $viewSources[] = array('href' => $this->fileLink);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->fileName . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'jpeg':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->jpegLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->jpegLinks);
                                foreach ($this->universalPreview->jpegLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->fileName . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'pdf':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pdfLinks))
                            {
                                $i = 1;
                                foreach ($this->universalPreview->pdfLinks as $link)
                                {
                                    $viewSources[] = array('href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'thumbs':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
                                foreach ($this->universalPreview->thumbsLinks as $link)
                                    $viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
                        break;
                }
                break;
            case 'xls':
                switch ($format)
                {
                    case 'xls':
                        $viewSources[] = array('href' => $this->fileLink);
                        break;
                }
                break;
            case 'pdf':
                switch ($format)
                {
                    case 'pdf':
                        $viewSources[] = array('href' => $this->fileLink);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->fileName . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'jpeg':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->jpegLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->jpegLinks);
                                foreach ($this->universalPreview->jpegLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->fileName . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'thumbs':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
                                foreach ($this->universalPreview->thumbsLinks as $link)
                                    $viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
                        break;
                }
                break;
            case 'jpeg':
            case 'png':
            case 'url':
                $viewSources[] = array('href' => $this->fileLink);
                break;
            case 'video':
                switch ($format)
                {
                    case 'video':
                        $viewSources[] = array('src' => $this->fileLink);
                        break;
                    case 'mp4':
                        if (isset($this->universalPreview))
                        {
                            if (isset($this->universalPreview->mp4Links))
                                foreach ($this->universalPreview->mp4Links as $link)
                                    $viewSources[] = array('src' => $link, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/js/video-js/video-js.swf');
                            if (isset($this->universalPreview->ogvLinks))
                                foreach ($this->universalPreview->ogvLinks as $link)
                                    $viewSources[] = array('src' => $link, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/js/video-js/video-js.swf');
                        }
                    case 'tab':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->mp4Links))
                                foreach ($this->universalPreview->mp4Links as $link)
                                    $viewSources[] = array('src' => $link, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/js/video-js/video-js.swf');
                        break;
                    case 'ogv':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->ogvLinks))
                                foreach ($this->universalPreview->ogvLinks as $link)
                                    $viewSources[] = array('src' => $link, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/js/video-js/video-js.swf');
                        break;
                }
                break;
            case 'mp4':
                $viewSources[] = array('src' => $this->fileLink, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/js/video-js/video-js.swf');
                break;
        }
        if (isset($viewSources))
            return $viewSources;
    }

}

?>