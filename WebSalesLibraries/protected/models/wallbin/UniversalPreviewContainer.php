<?php
class UniversalPreviewContainer
{
    public $parent;
    public $pngLinks;
    public $jpegLinks;
    public $pdfLinks;
    public $mp4Links;
    public $ogvLinks;
    public $oldOfficeFormatLinks;
    public $newOfficeFormatLinks;
    public $thumbsLinks;
    public $thumbsWidth;
    public $thumbsHeight;
    public function __construct($libraryLink)
    {
        $this->parent = $libraryLink;
    }

    public function load($previewXMLNode)
    {
        $node = $previewXMLNode->getElementsByTagName("FolderName")->item(0);
        if (isset($node))
        {
            //load graphics preview
            $previewContainerFolderPath = realpath($this->parent->parent->parent->parent->universalPreviewContainerPath . DIRECTORY_SEPARATOR . 'files' . DIRECTORY_SEPARATOR . $node->nodeValue);
            if (file_exists($previewContainerFolderPath))
            {
                $previewContainerFolder = new DirectoryIterator($previewContainerFolderPath);
                foreach ($previewContainerFolder as $subFolder)
                {
                    if ($subFolder->isDir() && !$subFolder->isDot())
                    {
                        $previewFolder = new DirectoryIterator($subFolder->getPathname());
                        foreach ($previewFolder as $file)
                            if ($file->isFile())
                            {
                                $previewFolderName = strtolower($subFolder->getBasename());
                                switch ($previewFolderName)
                                {
                                    case 'png':
                                        $this->pngLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/files/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'jpg':
                                        $this->jpegLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/files/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'pdf':
                                        $this->pdfLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/files/' . $node->nodeValue . '/' . $previewFolderName . '/' . str_replace('&', '%26', $file->getBasename());
                                        break;
                                    case 'ppt':
                                    case 'doc':
                                        $this->oldOfficeFormatLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/files/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'docx':
                                    case 'pptx':
                                        $this->newOfficeFormatLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/files/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'thumbs':
                                        $this->thumbsLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/files/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        if (!isset($this->thumbsWidth) && !isset($this->thumbsHeight))
                                        {
                                            $imageData = getimagesize($file->getPathname());
                                            if (isset($imageData))
                                            {
                                                $this->thumbsWidth = $imageData[0];
                                                $this->thumbsHeight = $imageData[1];
                                            }
                                        }
                                        break;
                                }
                            }
                    }
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
                }
            }

            //load video preview
            $previewContainerFolderPath = realpath($this->parent->parent->parent->parent->universalPreviewContainerPath . DIRECTORY_SEPARATOR . 'video' . DIRECTORY_SEPARATOR . $node->nodeValue);
            if (file_exists($previewContainerFolderPath))
            {
                $previewContainerFolder = new DirectoryIterator($previewContainerFolderPath);
                foreach ($previewContainerFolder as $subFolder)
                {
                    if ($subFolder->isDir() && !$subFolder->isDot())
                    {
                        $previewFolder = new DirectoryIterator($subFolder->getPathname());
                        foreach ($previewFolder as $file)
                            if ($file->isFile())
                            {
                                $previewFolderName = strtolower($subFolder->getBasename());
                                switch ($previewFolderName)
                                {
                                    case 'mp4':
                                        $this->mp4Links[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/video/' . $node->nodeValue . '/' . $previewFolderName . '/' . str_replace('&', '%26', $file->getBasename());
                                        break;
                                    case 'ogv':
                                        $this->ogvLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/video/' . $node->nodeValue . '/' . $previewFolderName . '/' . str_replace('&', '%26', $file->getBasename());
                                        break;
                                }
                            }
                    }
                    if (isset($this->mp4Links))
                        natsort($this->mp4Links);
                    if (isset($this->ogvLinks))
                        natsort($this->ogvLinks);
                }
            }
        }
    }

}

?>