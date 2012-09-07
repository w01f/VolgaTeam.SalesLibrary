<?php
$this->widget('zii.widgets.grid.CGridView', array(
    'id' => 'search-grid',
    'dataProvider' => $dataProvider,
    'template' => '{items}',
    'cssFile' => Yii::app()->baseUrl . '/css/custom-grid.css',
    'columns' => array(
        array(
            'name' => 'Library',
            'type' => 'raw',
            'value' => '$data["library"]',
        ),                
        array(
            'name' => 'Name',
            'type' => 'raw',
            'value' => '$data["name"]',
        ),
        array(
            'name' => 'File',
            'type' => 'raw',
            'value' => '$data["file_name"]',
        ),
        array(
            'name' => 'Date Modified',
            'type' => 'raw',
            'value' => null,
        ),        
    ),
    )
);
?>
