<?php
	$webRoot = dirname(__FILE__);
	if (array_key_exists('HTTP_HOST', $_SERVER))
	{
		if ($_SERVER['HTTP_HOST'] == 'localhost')
			$internalConfig = $webRoot . '/protected/config/development.php';
		else
			$internalConfig = $webRoot . '/protected/config/production.php';
	}
	else
		$internalConfig = $webRoot . '/protected/config/console.php';

	$configArray = CMap::mergeArray(
		require($internalConfig), array(
			'name' => 'Sales Libraries',
			'params' => array(
				'appRoot' => dirname(__FILE__),
				'login' => array(
					'rememberMeField' => true,
					'forgotPasswordField' => true,
					'disclaimer' => true,
					'disclaimerText' => 'I understand  that this Website contains information that is privileged, confidential and exempt from disclosure under applicable law.  Only Authorized employees  and representatives of WPLG-TV, Miami, may access or download information from this site.',
					'tempPasswordExpiredIn' => '7',
					'complex_password' => false,
					'inactivity_refresh_timeout' => -1,
					'inactivity_logout_timeout' => -1,
					'theme_color' => '2196f3',
					'use_token_connection' => true,
					'secret_key' => 'graysalestv-bfc041fe-859a-8123-a308-38e51ef9ab28'
				),
				'stations' => array(
					'tab_name' => 'Libraries',
					'column_name' => 'Libraries',
					'locations' => array(
						'Libraries',
					),
				),
				'tags' => array(
					'tab_name' => 'Tag',
					'column_name' => 'Tag',
				),
				'search_options' => array(
					'hide_tag' => false,
					'hide_supertag' => false,
					'hide_libraries' => false,
				),
				'tooltips' => array(
					'wallbin' => array(
						//Base file link formats
						'ppt' => 'PowerPoint',
						'pps' => 'PowerPoint Show',
						'doc' => 'Word',
						'xls' => 'Excel',
						'png' => 'PNG',
						'jpeg' => 'JPEG',
						'pdf' => 'PDF',
						'video' => 'Video',
						'mp3' => 'MP3 Track',
						'key' => 'Apple Keynote',
						//Non-file link formats
						'url' => 'Web URL',
						'youtube' => 'YouTube',
						'vimeo' => 'Vimeo',
						'lan' => 'LAN Link',
						'quicksite' => 'QuickSite Link',
						'html5' => 'HTML5 Link',
						'internal library' => 'Library Link',
						'internal page' => 'Page Link',
						'internal window' => 'Window Link',
						'internal link' => 'Link',
						'internal shortcut' => 'Shortcut',
						'app' => 'App Link',
						'link bundle' => 'Link Bundle',
						//Additional file formats
						'xml' => 'XML File',
						'eps' => 'Postscript Vector',
						'svg' => 'Scalable Vector',
						'rar' => 'WinRar Archive',
						'7z' => '7Zip Archive',
						'zip' => 'Zip File',
						'ai' => 'Adobe Illustrator',
						'ait' => 'Adobe Illustrator',
						'psd' => 'Adobe Photoshop',
						'pdd' => 'Adobe Photoshop',
						'aep' => 'Adobe After Effects',
						'aet' => 'Adobe After Effects',

						'other' => 'Undefined file',
					),
				),
				'email' => array(
					'from' => 'user1@domem.com',
					'copy_enabled' => true,
					'copy' => 'user2@domem.com',
					'new_user' => array(
						'subject' => 'Company',
						'body' => 'You have a new account at site',
					),
					'help_request_address' => 'user1@domem.com',
					'quiz' => array(
						'from' => 'user2@domem.com',
						'copy_enabled' => true,
						'copy' => 'user3@domem.com'
					),
					'sales_requests' => array(
						'from' => 'research@wfaa.com',
						'copy_enabled' => true,
						'copy' => 'billy@adsalesapps.com',
						'copy_user' => true
					),
					'sales_contest' => array(
						'from' => 'research@wfaa.com',
						'copy_enabled' => true,
						'copy' => 'billy@adsalesapps.com',
						'copy_user' => true
					)
				),
				'android_tablets' => array(
					'Mozilla/5.0 (Linux; U; Android 4.2.2; es-us; GT-P5210 Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30/1.05v.3406.d7',
				),
				'menu' => array(
					'BarColor' => '2C84EE',
					'MenuButtonColor' => 'ffffff',
					'HeaderIconColor' => 'ffffff',
					'HeaderTextColor' => 'ffffff',
					'MenuItemsColor' => 'ffffff',
					'ActionMenuButtonColor' => 'ffffff  ',
					'IconSeparation' => 5,
					'SaveSuperGroup' => true
				),
				'icomoon' => array(
					'version' => 1
				),
				'custom_fonts' => array(
					//array(
					//	'version' => 1,
					//	'css_path' => '/proxima-nova-web-fonts-master/fonts/fonts.min.css'
					//),
					//array(
					//	'version' => 1,
					//	'css_path' => '/proxima-nova-web-fonts-master/fonts/fonts.min.css'
					//)
				),
				'froala_editor' => array(
					'key' => ''
				),
				'comet_chat' => array(
					'enabled' => false,
				),
				'ifly_chat' => array(
					'enabled' => false,
					'app_id' => '',
					'app_key' => ''
				),
				'one_drive_links' => array(
					'enabled' => true,
					'app_id' => '',
					'app_key' => '',
				),
				'jqm_theme' => array(
					'jqm_enabled' => true,
					'enabled' => true,
					'major_color' => "EACE23",
					'minor_color' => "F6ECB6",
				),
				'google_analytics' => array(
					'id' => 'UA-82056057-1',
				),
			),
			'components' => array(
				'db' => array(
					'connectionString' => 'mysql:host=localhost;dbname=sales_library',
					'username' => 'root',
					'password' => null,
				),
			),
		)
	);
	if(!array_key_exists('HTTP_HOST', $_SERVER))
	{
		$configArray =CMap::mergeArray($configArray,array(
			'components' => array(
				'request' => array(
					'hostInfo' => 'http://localhost',
					'baseUrl' => '',
					'scriptUrl' => '',
				),
			),
		));
	}
	return $configArray;
?>