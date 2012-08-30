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
    public function load($autoWidgetRecord)
    {
        $this->libraryId = $autoWidgetRecord->id_library;
        $this->extension = $autoWidgetRecord->extension;
        $this->widget = $autoWidgetRecord->widget;
    }

}

?>
