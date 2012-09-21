<?php
return array(
    'basePath' => dirname(__FILE__) . DIRECTORY_SEPARATOR . '..',
    'name' => 'Sales Libraries Cron Daemon',
    'preload' => array('log'),
    'import' => array(
        'application.commands.*',
        'application.models.*',
        'application.models.storage.*',
        'application.models.common.*',
        'application.models.wallbin.*',
        'application.components.*',
        'application.components.widgets.*',
        'application.components.core.*',
    ),
    'components' => array(
        'db' => array(
            'connectionString' => 'mysql:host=localhost;dbname=sales_library',
            'emulatePrepare' => true,
            'username' => 'root',
            'password' => 'root',
            'charset' => 'utf8',
            'autoConnect' => true,
            'tablePrefix' => 'tbl_',
            'schemaCachingDuration' => 3600,
        ),
        'log' => array(
            'class' => 'CLogRouter',
            'routes' => array(
                array(
                    'class' => 'CFileLogRoute',
                    'levels' => 'error, warning',
                ),
            ),
        ),
        'cacheDB' => array(
            'class' => 'system.caching.CDbCache',
            'connectionID' => 'db',
            'autoCreateCacheTable' => true,
            'cacheTableName' => 'tbl_cache',
        ),
        'email' => array(
            'class' => 'application.extensions.email.Email',
            'delivery' => 'php',
        ),
    ),
    'params' => require(dirname(__FILE__) . '/params.php'),
);
?>