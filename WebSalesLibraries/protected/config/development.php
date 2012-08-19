<?php
return array(
    'basePath' => dirname(__FILE__) . DIRECTORY_SEPARATOR . '..',
    'name' => 'Sales Libraries',
    'defaultController' => 'wallbin',
    'preload' => array('log'),
    'import' => array(
        'application.models.*',
        'application.models.common.*',
        'application.models.wallbin.*',
        'application.components.*',
    ),
//    'behaviors' => array(
//        'onBeginRequest' => array(
//            'class' => 'application.components.RequireLogin'
//        )
//    ),
    'components' => array(
        'session' => array(
            'autoStart' => true,
        ),
        'log' => array(
            'class' => 'CLogRouter',
            'routes' => array(
//                array(
//                    'class' => 'CFileLogRoute',
//                    'levels' => 'error, warning',
//                ),
                array(
                    'class' => 'CWebLogRoute',
                    'levels' => 'error, warning',
                ),
            ),
        ),
        'user' => array(
            'loginUrl' => array('site/login'),
            'allowAutoLogin' => true,
        ),
        'db' => array(
            'connectionString' => 'mysql:host=localhost;dbname=sales_library',
            'emulatePrepare' => true,
            'username' => 'root',
            'password' => 'root',
            'charset' => 'utf8',
            'tablePrefix' => 'tbl_',
            'schemaCachingDuration' => 3600,
        ),
        'errorHandler' => array(
            // use 'site/error' action to display errors
            'errorAction' => 'site/error',
        ),
        'urlManager' => array(
            'urlFormat' => 'path',
            'showScriptName' => false,
            'rules' => array(
                '<controller:\w+>/<id:\d+>' => '<controller>/view',
                '<controller:\w+>/<action:\w+>/<id:\d+>' => '<controller>/<action>',
                '<controller:\w+>/<action:\w+>' => '<controller>/<action>',
            )
        ),
        'browser' => array(
            'class' => 'application.extensions.browser.CBrowserComponent',
        ),
        'cacheFile' => array(
            'class' => 'system.caching.CFileCache',
        ),
        'email' => array(
            'class' => 'application.extensions.email.Email',
            'delivery' => 'php',
        ),
    ),
    'params' => require(dirname(__FILE__) . '/params.php'),
);