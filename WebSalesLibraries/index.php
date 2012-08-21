<?php
$webRoot = dirname(__FILE__);
if ($_SERVER['HTTP_HOST'] == 'localhost')
{
    define('YII_DEBUG', true);
    require_once($webRoot . '/yii/framework/yii.php');
    $configFile = $webRoot . '/protected/config/development.php';
}
else
{
    define('YII_DEBUG', false);
    require_once($webRoot . '/yii/framework/yii.php');
    $configFile = $webRoot . '/protected/config/production.php';
}
$app = Yii::createWebApplication($configFile)->run();
?>
