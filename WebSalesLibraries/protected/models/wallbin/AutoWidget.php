<?php

class AutoWidget
{
    /**
     * @var string name
     * @soap
     */
    public $libraryId;    
    /**
     * @var string name
     * @soap
     */    
    public $extension;
    /**
     * @var string name
     * @soap
     */    
    public $widget;

    public function load($widgetNode)
    {
        $node = $widgetNode->getElementsByTagName("Extension")->item(0);
        if (isset($node))
            $this->extension = $node->nodeValue;

        $node = $widgetNode->getElementsByTagName("Widget")->item(0);
        if (isset($node))
            $this->widget = $node->nodeValue;
    }
}
?>
