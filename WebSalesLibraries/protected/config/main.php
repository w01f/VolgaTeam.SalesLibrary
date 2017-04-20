<?php
	return array(
		'basePath' => dirname(__FILE__) . DIRECTORY_SEPARATOR . '..',
		'name' => 'Sales Libraries',
		'defaultController' => 'site',
		'preload' => array('log', 'browser'),
		'import' => array(
			'application.components.*',
			'application.components.core.*',
			'application.components.controllers.*',
			'application.components.behaviors.*',
			'application.models.*',
			'application.models.common.*',
			'application.models.rate.*',
			'application.models.auth.models.*',
			'application.models.auth.records.*',
			'application.models.menu.models.*',
			'application.models.menu.records.*',
			'application.models.favorites.models.*',
			'application.models.favorites.records.*',
			'application.models.link_config_profile.models.*',
			'application.models.link_config_profile.records.*',
			'application.models.link_config_profile.*',
			'application.models.link_user_profile.models.*',
			'application.models.link_user_profile.records.*',
			'application.models.link_user_profile.*',
			'application.models.local_app_meta_data.*',
			'application.models.qpages.models.*',
			'application.models.qpages.records.*',
			'application.models.quizzes.models.*',
			'application.models.quizzes.records.*',
			'application.models.shortcuts.models.*',
			'application.models.shortcuts.models.links.*',
			'application.models.shortcuts.models.groups.*',
			'application.models.shortcuts.models.navigation_panel.*',
			'application.models.shortcuts.models.landing_page.regular_markup.common.*',
			'application.models.shortcuts.models.landing_page.regular_markup.list_block.*',
			'application.models.shortcuts.models.landing_page.regular_markup.masonry.*',
			'application.models.shortcuts.models.landing_page.regular_markup.scroll_stripe.*',
			'application.models.shortcuts.models.landing_page.regular_markup.slider.*',
			'application.models.shortcuts.models.landing_page.regular_markup.horizontal_feed.*',
			'application.models.shortcuts.models.landing_page.regular_markup.vertical_feed.*',
			'application.models.shortcuts.models.landing_page.mobile_items.*',
			'application.models.shortcuts.models.common.*',
			'application.models.shortcuts.records.*',
			'application.models.statistic.models.common.*',
			'application.models.statistic.models.links.*',
			'application.models.statistic.models.reports.*',
			'application.models.statistic.models.*',
			'application.models.statistic.records.*',
			'application.models.wallbin.models.web.*',
			'application.models.wallbin.models.web.link_settings.*',
			'application.models.wallbin.models.web.bundle_settings.*',
			'application.models.wallbin.models.web.style.*',
			'application.models.wallbin.models.soap.*',
			'application.models.wallbin.models.cadmin.*',
			'application.models.wallbin.models.cadmin.entities.*',
			'application.models.wallbin.models.cadmin.settings.*',
			'application.models.wallbin.records.*',
			'application.models.search.*',
			'application.models.data_query.common.*',
			'application.models.data_query.data_table.*',
			'application.models.data_query.link_feed.*',
			'application.models.preview.common.*',
			'application.models.preview.config.*',
			'application.models.preview.internal_links.*',
			'application.models.preview.link_bundles.*',
			'application.models.services_data.common.dictionaries.*',
			'application.models.services_data.common.meta_data.*',
			'application.models.services_data.common.rest.*',
			'application.models.services_data.cadmin.records.*',
			'application.models.services_data.cadmin.models.connection.*',
			'application.models.services_data.cadmin.models.library_data.*',
			'application.models.services_data.cadmin.models.versions_management.*',
			'application.models.feeds.common.*',
			'application.models.feeds.horizontal.*',
			'application.models.feeds.vertical.*',
		),
		'behaviors' => array(
			'onBeginRequest' => array(
				'class' => 'application.components.behaviors.BehaviorManager'
			)
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
				'caseSensitive' => true,
				'rules' => array(
					// REST patterns
					array('fileManagerData/get', 'pattern' => 'FileManagerData/<model:\w+>', 'verb' => 'POST'),
					array('cloudAdmin/get', 'pattern' => 'CloudAdmin/<model:\w+>', 'verb' => 'POST'),
					array('cloudAdmin/set', 'pattern' => 'CloudAdmin/<model:\w+>', 'verb' => 'PUT'),
					// Other patterns
					'<controller:\w+>/<action:\w+>' => '<controller>/<action>',
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