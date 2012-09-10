<?php
$cs = Yii::app()->clientScript;
$cs->registerCoreScript('cookie');
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/video-js/video.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/overlay.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/linkViewing.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/scaling.js', CClientScript::POS_HEAD);

$this->widget('zii.widgets.grid.CGridView', array(
    'id' => 'search-grid',
    'dataProvider' => $dataProvider,
    'template' => '{items}',
    'cssFile' => Yii::app()->baseUrl . '/css/custom-grid.css',
    'columns' => array(
        array(
            'name' => 'library',
            'header' => 'Library',
            'type' => 'raw',
            'value' => '$data["library"]',
        ),
        array(
            'name' => 'name',
            'header' => 'Link',
            'type' => 'html',
            'value' => 'Yii::app()->controller->widget("application.components.widgets.LinkGridColumnWidget", array("linkName" => $data["name"], "fileName" => $data["file_name"]),true)',
        ),
        array(
            'name' => 'date',
            'header' => 'Date Modified',
            'type' => 'raw',
            'value' => null,
        ),
    ),
    'enableSorting' => true,
    'selectableRows' => 1,
    'selectionChanged' => 'function(){$.openViewDialogAjax($.fn.yiiGridView.getSelection("search-grid"));}',
    'beforeAjaxUpdate' => 'function(){$.showOverlayLight();}',
    'afterAjaxUpdate' => 'function(){
                            $.hideOverlayLight(); 
                            $.updateContentAreaWidth();
                            }',
    )
);
?>
