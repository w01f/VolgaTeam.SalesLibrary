<?php

if (isset($_SERVER['HTTP_USER_AGENT']))
{
    $http_user_agent = $_SERVER['HTTP_USER_AGENT'];
    if (preg_match('/Word|Excel|PowerPoint|ms-office/i', $http_user_agent))
    {
        // Prevent MS office products detecting the upcoming re-direct .. forces them to launch the browser to this link
        die();
    }
}

$webRoot = dirname(__FILE__);

ini_set("soap.wsdl_cache_dir",$webRoot."/protected/runtime");

if ($_SERVER['HTTP_HOST'] == 'localhost')
{
    define('YII_DEBUG', true);
    require_once($webRoot . '/yii/framework/yii.php');
}
else
{
    define('YII_DEBUG', false);
    require_once($webRoot . '/yii/framework/yii.php');
}
$app = Yii::createWebApplication($webRoot . '/configuration.php')->run();
