<?php
class Attachment
{
    public $parent;
    public $id;
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
    public $browser;
    public $universalPreview;
    public function __construct($link)
    {
        $this->parent = $link;
    }

    public function load($attachmentRecord)
    {
        $this->id = $attachmentRecord->id;
        $this->linkId = $attachmentRecord->id_link;
        $this->libraryId = $attachmentRecord->id_library;
        $this->name = $attachmentRecord->name;
        $this->path = $attachmentRecord->path;
        $this->originalFormat = $attachmentRecord->format;
        if ($this->originalFormat != 'url')
            $this->link = str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->parent->parent->parent->storageLink . '/' . $this->path)));
        else
            $this->link = $attachmentRecord->path;

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
                        case 'phone':
                            $this->availableFormats[] = 'tab';
                            break;
                        case 'mobile':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'tab';
                            $this->availableFormats[] = 'download';
                            break;
                        case 'ie':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'video';
                            $this->availableFormats[] = 'download';
                            break;
                        case 'webkit':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'tab';
                            $this->availableFormats[] = 'download';
                            break;
                        case 'firefox':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'ogv';
                            $this->availableFormats[] = 'download';
                            break;
                        case 'opera':
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'tab';
                            $this->availableFormats[] = 'ogv';
                            $this->availableFormats[] = 'download';
                            break;
                        default:
                            $this->availableFormats[] = 'video';
                            $this->availableFormats[] = 'mp4';
                            $this->availableFormats[] = 'ogv';
                            $this->availableFormats[] = 'tab';
                            $this->availableFormats[] = 'download';
                            break;
                    }
                    break;
                case 'mp4':
                    $this->availableFormats[] = 'mp4';
                    $this->availableFormats[] = 'tab';
                    $this->availableFormats[] = 'download';
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
                    case 'email':
                        $viewSources[] = array('title' => $this->name, 'href' => $this->path);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Slide ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'png_phone':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngPhoneLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngPhoneLinks);
                                foreach ($this->universalPreview->pngPhoneLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Slide ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
                                    $viewSources[] = array('title' => ($this->name . ' - Slide ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'jpeg_phone':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->jpegPhoneLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->jpegPhoneLinks);
                                foreach ($this->universalPreview->jpegPhoneLinks as $link)
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
                        {
                            if ($this->browser == 'phone')
                            {
                                if (isset($this->universalPreview->thumbsPhoneLinks))
                                    foreach ($this->universalPreview->thumbsPhoneLinks as $link)
                                        $viewSources[] = array('href' => $link);
                            }
                            else
                            {
                                if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
                                    foreach ($this->universalPreview->thumbsLinks as $link)
                                        $viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
                            }
                        }
                        break;
                }
                break;
            case 'doc':
                switch ($format)
                {
                    case 'doc':
                        $viewSources[] = array('href' => $this->link);
                        break;
                    case 'email':
                        $viewSources[] = array('title' => $this->name, 'href' => $this->path);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'png_phone':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngPhoneLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngPhoneLinks);
                                foreach ($this->universalPreview->pngPhoneLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
                    case 'jpeg_phone':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->jpegPhoneLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->jpegPhoneLinks);
                                foreach ($this->universalPreview->jpegPhoneLinks as $link)
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
                        {
                            if ($this->browser == 'phone')
                            {
                                if (isset($this->universalPreview->thumbsPhoneLinks))
                                    foreach ($this->universalPreview->thumbsPhoneLinks as $link)
                                        $viewSources[] = array('href' => $link);
                            }
                            else
                            {
                                if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
                                    foreach ($this->universalPreview->thumbsLinks as $link)
                                        $viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
                            }
                        }
                        break;
                }
                break;
            case 'xls':
                switch ($format)
                {
                    case 'xls':
                        $viewSources[] = array('href' => $this->link);
                        break;
                    case 'email':
                        $viewSources[] = array('title' => $this->name, 'href' => $this->path);
                        break;
                }
                break;
            case 'pdf':
                switch ($format)
                {
                    case 'pdf':
                        $viewSources[] = array('href' => $this->link);
                        break;
                    case 'email':
                        $viewSources[] = array('title' => $this->name, 'href' => $this->path);
                        break;
                    case 'png':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngLinks);
                                foreach ($this->universalPreview->pngLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'png_phone':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->pngPhoneLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->pngPhoneLinks);
                                foreach ($this->universalPreview->pngPhoneLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
                    case 'jpeg_phone':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->jpegPhoneLinks))
                            {
                                $i = 1;
                                $count = count($this->universalPreview->jpegPhoneLinks);
                                foreach ($this->universalPreview->jpegPhoneLinks as $link)
                                {
                                    $viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
                                    $i++;
                                }
                            }
                        break;
                    case 'thumbs':
                        if (isset($this->universalPreview))
                        {
                            if ($this->browser == 'phone')
                            {
                                if (isset($this->universalPreview->thumbsPhoneLinks))
                                    foreach ($this->universalPreview->thumbsPhoneLinks as $link)
                                        $viewSources[] = array('href' => $link);
                            }
                            else
                            {
                                if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
                                    foreach ($this->universalPreview->thumbsLinks as $link)
                                        $viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
                            }
                        }
                        break;
                }
                break;
            case 'jpeg':
            case 'png':
                switch ($format)
                {
                    case 'jpeg':
                    case 'png':
                        $viewSources[] = array('href' => $this->link);
                        break;
                    case 'email':
                        $viewSources[] = array('title' => $this->name, 'href' => $this->path);
                        break;
                }
                break;
            case 'url':
            case 'other':
                $viewSources[] = array('href' => $this->link);
                break;
            case 'video':
                switch ($format)
                {
                    case 'video':
                        $viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name);
                        break;
                    case 'email':
                    case 'download':                        
                        $viewSources[] = array('title' => $this->name, 'href' => $this->path);
                        break;
                    case 'mp4':
                        if (isset($this->universalPreview))
                        {
                            if (isset($this->universalPreview->mp4Links))
                                foreach ($this->universalPreview->mp4Links as $link)
                                    $viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                            if (isset($this->universalPreview->ogvLinks))
                                foreach ($this->universalPreview->ogvLinks as $link)
                                    $viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                        }
                    case 'tab':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->mp4Links))
                                foreach ($this->universalPreview->mp4Links as $link)
                                    $viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                        break;
                    case 'ogv':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->ogvLinks))
                                foreach ($this->universalPreview->ogvLinks as $link)
                                    $viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                        break;
                }
                break;
            case 'mp4':
                switch ($format)
                {
                    case 'email':
                    case 'download':                        
                        $viewSources[] = array('title' => $this->name, 'href' => $this->path);
                        break;
                    default:
                        $viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                        break;
                }
                break;
        }
        if (isset($viewSources))
            return $viewSources;
    }

    public static function attachmentComparer($x, $y)
    {
        if ($x->originalFormat == $y->originalFormat)
            return 0;
        else
            return ($x->originalFormat < $y->originalFormat) ? -1 : 1;
    }

}

?>