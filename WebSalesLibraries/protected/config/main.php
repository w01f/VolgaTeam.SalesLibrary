<?php
	return array(
		'basePath' => dirname(__FILE__) . DIRECTORY_SEPARATOR . '..',
		'name' => 'Sales Libraries',
		'defaultController' => 'site',
		'preload' => array('log', 'browser'),
		'import' => array(
			'application.components.*',
			'application.components.widgets.*',
			'application.components.core.*',
			'application.models.*',
			'application.models.common.*',
			'application.models.rate.*',
			'application.models.auth.models.*',
			'application.models.auth.records.*',
			'application.models.menu.models.*',
			'application.models.menu.records.*',
			'application.models.favorites.models.*',
			'application.models.favorites.records.*',
			'application.models.qpages.models.*',
			'application.models.qpages.records.*',
			'application.models.quizzes.models.*',
			'application.models.quizzes.records.*',
			'application.models.shortcuts.models.*',
			'application.models.shortcuts.records.*',
			'application.models.statistic.models.*',
			'application.models.statistic.records.*',
			'application.models.ticker.models.*',
			'application.models.ticker.records.*',
			'application.models.wallbin.models.*',
			'application.models.wallbin.records.*',
		),
		'components' => array(
			'session' => array(
				'autoStart' => true,
			),
			'user' => array(
				'loginUrl' => array('auth/login'),
				'allowAutoLogin' => true,
			),
			'errorHandler' => array(
				'errorAction' => 'site/error',
			),
			'urlManager' => array(
				'urlFormat' => 'path',
				'showScriptName' => false,
				'rules' => array(
					'<controller:\w+>/<id:\d+>' => '<controller>/view',
					'<controller:\w+>/<action:\w+>' => '<controller>/<action>',
					'qpage/<controller:\w+>/<action:\w+>' => '<controller>/<action>',
				)
			),
			'log' => array(
				'class' => 'CLogRouter',
				'routes' => array(
					array(
						'class' => 'CFileLogRoute',
						'levels' => 'error, warning',
					),
					array(
						'class' => 'CWebLogRoute',
						'enabled' => YII_DEBUG,
						'levels' => 'error, warning, trace, notice',
						'categories' => 'application',
						'showInFireBug' => true,
					),
				),
			),
			'browser' => array(
				'class' => 'application.extensions.browser.CBrowserComponent',
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