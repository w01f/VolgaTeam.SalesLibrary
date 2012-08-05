<?php

class AutoWidget
{
    public $extension;
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
