<br>
<br>
<div id="calendar"></div>
<?php
$schedule = file_exists(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..' . DIRECTORY_SEPARATOR . 'schedule.php') ? require(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..' . DIRECTORY_SEPARATOR . 'schedule.php') : null;
if (isset($schedule)):
    ?>
    <div id="schedule"><?php echo CHtml::encode(CJSON::encode($schedule)); ?></div>
<?php endif; ?>
