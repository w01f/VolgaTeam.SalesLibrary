<?php
class LibraryLink
{
    private $_enableWidget;
    private $_widget;
    public $parent;
    public $name;
    public $identifier;
    public $fileRelativePath;
    public $fileName;
    public $fileLink;
    public $fileExtension;
    public $note;
    public $isBold;
    public $order;
    public $type;
    public $originalFormat;
    public $availableFormats;
    public $lineBreakProperties;
    public $banner;
    public $presentationPreview;
    public $universalPreview;
    public function __construct($folder)
    {
        $this->parent = $folder;
        $this->identifier = uniqid();
    }

    public function load($fileXMLNode)
    {
        $node = $fileXMLNode->getElementsByTagName("DisplayName")->item(0);
        if (isset($node))
            $this->name = $node->nodeValue;

        $node = $fileXMLNode->getElementsByTagName("Type")->item(0);
        if (isset($node))
            $this->type = intval($node->nodeValue);

        $node = $fileXMLNode->getElementsByTagName("RelativePath")->item(0);
        if (isset($node))
        {
            $this->fileRelativePath = $node->nodeValue;
            if (isset($this->type) && ($this->type == 5))
            {
                $this->fileName = $this->fileRelativePath;
            }
            else if (isset($this->type) && ($this->type == 6))
            {
                
            }
            else if (isset($this->type) && ($this->type == 8))
            {
                $this->fileRelativePath = str_replace('\\', '', $this->fileRelativePath);
                $this->fileName = $this->fileRelativePath;
                $this->fileLink = $this->fileRelativePath;
                $this->originalFormat = 'url';
                $this->availableFormats[] = 'url';
            }
            else
            {
                $this->fileRelativePath = str_replace('\\', '/', $node->nodeValue);
                $fileFullPath = $this->parent->parent->parent->storagePath . $this->fileRelativePath;
                $this->fileName = basename($fileFullPath);
                $this->fileExtension = strtolower(pathinfo($fileFullPath, PATHINFO_EXTENSION));
                $this->fileLink = str_replace('&', '%26', $this->parent->parent->parent->storageLink . $this->fileRelativePath);
                $this->getFormats();
            }
        }

        $node = $fileXMLNode->getElementsByTagName("Note")->item(0);
        if (isset($node))
            $this->note = $node->nodeValue;

        $node = $fileXMLNode->getElementsByTagName("IsBold")->item(0);
        if (isset($node))
            $this->isBold = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);
        else
            $this->isBold = FALSE;

        $node = $fileXMLNode->getElementsByTagName("Order")->item(0);
        if (isset($node))
            $this->order = intval($node->nodeValue);

        $node = $fileXMLNode->getElementsByTagName("EnableWidget")->item(0);
        if (isset($node))
            $this->_enableWidget = filter_var($node->nodeValue, FILTER_VALIDATE_BOOLEAN);


        $node = $fileXMLNode->getElementsByTagName("Widget")->item(0);
        if (isset($node))
            $this->_widget = $node->nodeValue;

        $node = $fileXMLNode->getElementsByTagName("LineBreakProperties")->item(0);
        if (isset($node))
        {
            $this->lineBreakProperties = new LineBreak($this);
            $this->lineBreakProperties->load($node);
        }

        $node = $fileXMLNode->getElementsByTagName("BannerProperties")->item(0);
        if (isset($node))
        {
            $this->banner = new Banner($this);
            $this->banner->load($node);
        }
        else
        //Compatibility with old versios of Cache
        {
            $enableBannerNode = $fileXMLNode->getElementsByTagName("EnableBanner")->item(0);
            $bannerNode = $fileXMLNode->getElementsByTagName("Banner")->item(0);
            if (isset($enableBannerNode) && isset($bannerNode))
            {
                $this->banner = new Banner($this);
                $this->banner->isEnabled = filter_var($enableBannerNode->nodeValue, FILTER_VALIDATE_BOOLEAN);
                if ($this->banner->isEnabled)
                    $this->banner->image = $bannerNode->nodeValue;
            }
        }

        $node = $fileXMLNode->getElementsByTagName("UniversalPreviewContainer")->item(0);
        if (isset($node))
        {
            $this->universalPreview = new UniversalPreviewContainer($this);
            $this->universalPreview->load($node);
        }
        else
        {
            $node = $fileXMLNode->getElementsByTagName("PreviewContainer")->item(0);
            if (isset($node))
            {
                $this->presentationPreview = new PresentationPreviewContainer($this);
                $this->presentationPreview->load($node);
            }
        }
    }

    public function getWidget()
    {
        if (isset($this->_enableWidget))
            if (isset($this->_widget))
                return $this->_widget;
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
                    if (Yii::app()->browser->isMobile())
                    {
                        $this->availableFormats[] = 'mp4';
                        $this->availableFormats[] = 'tab';
                    }
                    else
                    {
                        $browser = Yii::app()->browser->getBrowser();
                        switch ($browser)
                        {
                            case 'Internet Explorer':
                                $this->availableFormats[] = 'mp4';
                                $this->availableFormats[] = 'video';
                                break;
                            case 'Chrome':
                            case 'Safari':
                                $this->availableFormats[] = 'mp4';
                                $this->availableFormats[] = 'tab';
                                break;
                            case 'Firefox':
                                $this->availableFormats[] = 'mp4';
                                $this->availableFormats[] = 'ogv';
                                break;
                            case 'Opera':
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
                        else if (isset($this->presentationPreview))
                            if (isset($this->presentationPreview->links))
                            {
                                $i = 1;
                                $count = count($this->presentationPreview->links);
                                foreach ($this->presentationPreview->links as $link)
                                {
                                    $viewSources[] = array('title' => ($this->fileName . ' - Slide ' . $i . ' of ' . $count), 'href' => $link);
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