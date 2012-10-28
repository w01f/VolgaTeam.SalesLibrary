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
    /**
     * @var string
     * @soap
     */
    public $fileExtension;
    /**
     * @var string
     * @soap
     */
    public $fileDate;
    /**
     * @var string
     * @soap
     */
    public $originalFormat;
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
     * @var boolean
     * @soap
     */
    public $enableFileCard;
    /**
     * @var FileCard
     * @soap
     */
    public $fileCard;
    /**
     * @var string
     * @soap
     */
    public $previewId;
    /**
     * @var LinkCategory[]
     * @soap
     */
    public $categories;
    /**
     * @var string
     * @soap
     */
    public $tags;
    /**
     * @var string
     * @soap
     */
    public $dateAdd;
    /**
     * @var string
     * @soap
     */
    public $dateModify;
    /**
     * @var string
     * @soap
     */
    public $contentPath;
    /**
     * @var boolean
     * @soap
     */
    public $enableAttachments;
    /**
     * @var Attachment[]
     * @soap
     */
    public $attachments;
    public $fileLink;
    public $universalPreview;
    public $browser;
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
        $this->originalFormat = $linkRecord->format;

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

        $this->enableFileCard = $linkRecord->enable_file_card;
        $fileCardRecord = FileCardStorage::model()->findByPk($linkRecord->id_file_card);
        if ($fileCardRecord !== null)
        {
            $this->fileCard = new FileCard();
            $this->fileCard->load($fileCardRecord);
        }

        $this->enableAttachments = $linkRecord->enable_attachments;
        $attachmentRecords = AttachmentStorage::model()->findAll('id_link=?', array($linkRecord->id));
        if ($attachmentRecords !== null)
        {
            foreach ($attachmentRecords as $attachmentRecord)
            {
                $attachment = new Attachment($this);
                $attachment->browser = $this->browser;
                $attachment->load($attachmentRecord);
                $this->attachments[] = $attachment;
            }
        }
        if (isset($this->attachments))
            usort($this->attachments, "Attachment::attachmentComparer");

        $linkCategoryRecords = LinkCategoryStorage::model()->findAll('id_link=?', array($linkRecord->id));
        if ($linkCategoryRecords !== null)
        {
            foreach ($linkCategoryRecords as $linkCategoryRecord)
            {
                $linkCategory = new LinkCategory();
                $linkCategory->load($linkCategoryRecord);
                $this->categories[] = $linkCategory;
            }
        }

        if ($linkRecord->id_preview != null)
        {
            $previewRecords = PreviewStorage::model()->findAll('id_container=?', array($linkRecord->id_preview));

            if ($previewRecords !== null)
            {
                $this->universalPreview = new UniversalPreviewContainer($this->parent->parent->parent);
                $this->universalPreview->load($previewRecords);
            }
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
        }
        else
        {
            $this->fileRelativePath = str_replace('\\', '/', $this->fileRelativePath);
            $this->filePath = $this->parent->parent->parent->storagePath . $this->fileRelativePath;
            $this->fileLink = str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->parent->parent->storageLink . $this->fileRelativePath)));
        }
        $this->getFormats();
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
        if (isset($this->originalFormat))
        {
            switch ($this->originalFormat)
            {
                case 'ppt':
                    $this->availableFormats[] = 'ppt';
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    $this->availableFormats[] = 'email';
                    break;
                case 'doc':
                    $this->availableFormats[] = 'doc';
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    $this->availableFormats[] = 'email';
                    break;
                case 'xls':
                    $this->availableFormats[] = 'xls';
                    $this->availableFormats[] = 'email';
                    break;
                case 'pdf':
                    $this->availableFormats[] = 'pdf';
                    $this->availableFormats[] = 'png';
                    $this->availableFormats[] = 'jpeg';
                    $this->availableFormats[] = 'email';
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
                    $this->availableFormats[] = 'email';
                    break;
                case 'jpeg':
                    $this->availableFormats[] = 'jpeg';
                    $this->availableFormats[] = 'email';
                    break;
                case 'url':
                    $this->availableFormats[] = 'url';
                    break;
                default:
                    $this->originalFormat = 'other';
                    if (isset($this->fileLink))
                        if ($this->fileLink != '')
                            $this->availableFormats[] = 'url';
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
                    case 'email':
                        $viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
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
                    case 'email':
                        $viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
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
                    case 'email':
                        $viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
                        break;
                }
                break;
            case 'pdf':
                switch ($format)
                {
                    case 'pdf':
                        $viewSources[] = array('href' => $this->fileLink);
                        break;
                    case 'email':
                        $viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
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
                switch ($format)
                {
                    case 'jpeg':
                    case 'png':
                        $viewSources[] = array('href' => $this->fileLink);
                        break;
                    case 'email':
                        $viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
                        break;
                }
                break;
            case 'url':
            case 'other':
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
                                    $viewSources[] = array('src' => $link, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                            if (isset($this->universalPreview->ogvLinks))
                                foreach ($this->universalPreview->ogvLinks as $link)
                                    $viewSources[] = array('src' => $link, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                        }
                    case 'tab':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->mp4Links))
                                foreach ($this->universalPreview->mp4Links as $link)
                                    $viewSources[] = array('src' => $link, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                        break;
                    case 'ogv':
                        if (isset($this->universalPreview))
                            if (isset($this->universalPreview->ogvLinks))
                                foreach ($this->universalPreview->ogvLinks as $link)
                                    $viewSources[] = array('src' => $link, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                        break;
                }
                break;
            case 'mp4':
                $viewSources[] = array('src' => $this->fileLink, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
                break;
        }
        if (isset($viewSources))
            return $viewSources;
    }

    public static function libraryLinkComparer($x, $y)
    {
        if ($x->order == $y->order)
            return 0;
        else
            return ($x->order < $y->order) ? -1 : 1;
    }

}

?>