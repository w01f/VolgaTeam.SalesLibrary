<?php
class PresentationPreviewContainer
{
    public $parent;
    public $links;
    public function __construct($libraryLink)
    {
        $this->parent = $libraryLink;
    }

    public function load($previewXMLNode)
    {
        $node = $previewXMLNode->getElementsByTagName("FolderName")->item(0);
        if (isset($node))
        {
            $previewFolderPath = realpath($this->parent->parent->parent->parent->presentationPreviewContainerPath . DIRECTORY_SEPARATOR . $node->nodeValue);
            if (file_exists($previewFolderPath))
            {
                foreach (new DirectoryIterator($previewFolderPath) as $file)
                {
                    if ($file->isFile())
                        $this->links[] = $this->parent->parent->parent->parent->presentationPreviewContainerLink . '/' . $node->nodeValue . '/' . $file->getBasename();
                }
                if (isset($this->links))
                    natsort($this->links);
            }
        }
    }

}

?>
