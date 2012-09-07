<?php
echo CHtml::openTag('div', array('id' => 'search-container'));
{
    echo CHtml::openTag('div', array('id' => 'right-navbar'));
    {
        $this->widget('application.components.widgets.SearchControlPanel', array());
    }
    echo CHtml::closeTag('div');
    
    echo CHtml::openTag('div', array('id' => 'search-result'));
    {
    }
    echo CHtml::closeTag('div');
}
echo CHtml::closeTag('div');
?>
