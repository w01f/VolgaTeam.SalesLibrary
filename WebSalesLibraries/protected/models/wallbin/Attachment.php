<?php
class Attachment
{
    public $parent;
    /**
     * @var string
     * @soap
     */
    public $linkId;
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
    public $path;
    /**
     * @var string
     * @soap
     */
    public $originalFormat;
    /**
     * @var string
     * @soap
     */
    public $previewId;
    public $link;
    public $availableFormats;
    public function __construct($link)
    {
        $this->parent = $link;
    }

    public function load($attachmentRecord)
    {
        $this->linkId = $attachmentRecord->id_link;
        $this->libraryId = $attachmentRecord->id_library;
        $this->name = $attachmentRecord->name;
        $this->path = $attachmentRecord->path;
        $this->link = str_replace('&', '%26', str_replace('\\', '/', $this->parent->parent->parent->parent->storageLink . '/' . $this->path));
        $this->originalFormat = $attachmentRecord->format;

        if ($attachmentRecord->id_preview != null)
        {
            $previewRecords = PreviewStorage::model()->findAll('id_container=?', array($attachmentRecord->id_preview));
            if ($previewRecords !== null)
            {
                $this->universalPreview = new UniversalPreviewContainer($this->parent->parent->parent->parent);
                $this->universalPreview->load($previewRecords);
            }
        }
        $this->getFormats();
    }

    public function getFormats()
    {
        if (isset($this->originalFormat))
        {
            switch ($this->originalFormat)
            {
                case 'ppt':
                    $this->availableFormats[] = 'ppt';
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'doc':
                    $this->availableFormats[] = 'doc';
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'xls':
                    $this->availableFormats[] = 'xls';
                    break;
                case 'pdf':
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'video':
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
                    $this->availableFormats[] = 'mp4';
                    $this->availableFormats[] = 'tab';
                    break;
                case 'png':
                    $this->availableFormats[] = 'png';
                    break;
                case 'jpeg':
                    $this->availableFormats[] = 'jpeg';
                    break;
                case 'url':
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
                        $viewSources[] = array('href' => $this->link);
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
                                    $viewSources[] = array('title' => ($this->name . ' - Slide ' . $i . ' of ' . $count), 'href' => $link);
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
                                    $viewSources[] = array('title' => ($this->name . ' - Slide ' . $i . ' of ' . $count), 'href' => $link);
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
                        $viewSources[] = array('href' => $this->link);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
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
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
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
                        $viewSources[] = array('href' => $this->link);
                        break;
                }
                break;
            case 'pdf':
                switch ($format)
                {
                    case 'pdf':
                        $viewSources[] = array('href' => $this->link);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
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
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
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
                $viewSources[] = array('href' => $this->link);
                break;
            case 'video':
                switch ($format)
                {
                    case 'video':
                        $viewSources[] = array('src' => $this->link);
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
                $viewSources[] = array('src' => $this->link, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/js/video-js/video-js.swf');
                break;
        }
        if (isset($viewSources))
            return $viewSources;
    }

}

?>