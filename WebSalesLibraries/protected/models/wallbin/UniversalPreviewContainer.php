<?php
class UniversalPreviewContainer
{
    public $parent;
    public $pngLinks;
    public $jpegLinks;
    public $pdfLinks;
    public $videoLinks;
    public $oldOfficeFormatLinks;
    public $newOfficeFormatLinks;
    public function __construct($libraryLink)
    {
        $this->parent = $libraryLink;
    }

    public function load($previewXMLNode)
    {
        $node = $previewXMLNode->getElementsByTagName("FolderName")->item(0);
        if (isset($node))
        {
            $previewContainerFolderPath = realpath($this->parent->parent->parent->parent->universalPreviewContainerPath . DIRECTORY_SEPARATOR . $node->nodeValue);
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
                                        $this->pngLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'jpg':
                                        $this->jpegLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'pdf':
                                        $this->pdfLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'video':
                                        $this->videoLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'ppt':
                                    case 'doc':
                                        $this->oldOfficeFormatLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                    case 'docx':
                                    case 'pptx':
                                        $this->newOfficeFormatLinks[] = $this->parent->parent->parent->parent->universalPreviewContainerLink . '/' . $node->nodeValue . '/' . $previewFolderName . '/' . $file->getBasename();
                                        break;
                                }
                            }
                    }
                }
                if (isset($this->pngLinks))
                    natsort($this->pngLinks);
                if (isset($this->pdfLinks))
                    natsort($this->pdfLinks);
                if (isset($this->jpegLinks))
                    natsort($this->jpegLinks);
                if (isset($this->videoLinks))
                    natsort($this->videoLinks);
                if (isset($this->oldOfficeFormatLinks))
                    natsort($this->oldOfficeFormatLinks);
                if (isset($this->newOfficeFormatLinks))
                    natsort($this->newOfficeFormatLinks);
            }
        }
    }

}

?>