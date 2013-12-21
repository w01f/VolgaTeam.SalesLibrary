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

	return CMap::mergeArray(
		require($internalConfig), array(
			'name' => 'Sales Libraries',
			'params' => array(
				'appRoot' => dirname(__FILE__),
				'login' => array(
					'rememberMeField' => true,
					'forgotPasswordField' => true,
					'disclaimer' => true,
					'disclaimerText' => 'I understand  that this Website contains information that is privileged, confidential and exempt from disclosure under applicable law.  Only Authorized employees  and representatives of WPLG-TV, Miami, may access or download information from this site.',
					'disclaimerWarningText' => 'Please CONFIRM acceptance of the terms and conditions of the confidentiality statement before logging into the site.',
					'tempPasswordExpiredIn' => '7',
					'complex_password' => true
				),
				'home_tab' => array(
					'name' => 'HOME',
					'position' => 1,
				),
				'search_full_tab' => array(
					'visible' => true,
					'name' => 'SEARCH',
					'position' => 2,
					'show_money_button' => true,
				),
				'search_file_card_tab' => array(
					'visible' => true,
					'name' => 'Sales Success Models',
					'position' => 3,
				),
				'calendar_tab' => array(
					'visible' => true,
					'name' => 'WEBCAST',
					'position' => 4,
				),
				'favorites_tab' => array(
					'visible' => true,
					'name' => 'Favorites',
					'position' => 5,
				),
				'quiz_tab' => array(
					'visible' => true,
					'name' => 'Tests',
					'position' => 99,
				),
				'stations' => array(
					'tab_name' => 'Libraries',
					'column_name' => 'Libraries',
				),
				'tags' => array(
					'visible' => true,
					'tab_name' => 'Tag',
					'column_name' => 'Tag',
				),
				'search_options' => array(
					'hide_duplicate' => true,
					'hide_date_options' => false,
				),
				'tooltips' => array(
					'wallbin' => array(
						'ppt' => 'PowerPoint',
						'doc' => 'Word',
						'xls' => 'Excel',
						'png' => 'PNG',
						'jpeg' => 'JPEG',
						'pdf' => 'PDF',
						'video' => 'WMV Video',
						'wmv' => 'WMV Video',
						'mp4' => 'MP4 Video',
						'url' => 'Web URL',
						'key' => 'Apple Keynote',
					),
					'preview_dialog' => array(
						'ppt' => 'PPT Tooltip',
						'doc' => 'DOC Tooltip',
						'xls' => 'XLS Tooltip',
						'png' => 'PNG Tooltip',
						'jpeg' => 'JPEG Tooltip',
						'pdf' => 'PDF Tooltip',
						'video' => 'WMV Tooltip',
						'mp4' => 'MP4 Tooltip',
						'ogv' => 'OGV Tooltip',
						'tab' => 'TAB Tooltip',
						'url' => 'URL Tooltip',
						'key' => 'Open Apple Keynote',
						'email' => 'Email Tooltip',
						'outlook' => 'Email Tooltip',
						'download' => 'Download Tooltip',
						'favorites' => 'Favorites Tooltip',
					)
				),
				'email' => array(
					'from' => 'bcaudill@raycommedia.com',
					'copy_enabled' => true,
					'copy' => 'billy@adsalesapp.com',
					'new_user' => array(
						'subject' => 'RaycomResults.tv',
						'body' => 'You have a new account at RaycomResults.tv',
					),
					'send_link' => array(
						'subject' => 'Raycom Results File Link',
						'body' => 'View your file:',
					),
					'help_request_address' => 'billy@newlocaldirect.com',
				),
				'ticker' => array(
					'visible' => true,
					'show_label' => false,
					'show_logo' => true,
					'show_control' => true,
					'effect' => 'slide',
					'theme' => 6,
				),
				'ribbon_news' => array(
					'visible' => true,
					'title' => 'Industry News',
					'urls' => array(
						array('visible' => true, 'image' => '1.png', 'url' => 'http://www.tvb.org'),
						array('visible' => false, 'image' => '2.png', 'url' => 'http://www.spotsndots.com'),
						array('visible' => true, 'image' => '3.png', 'url' => 'http://www.nab.org'),
						array('visible' => true, 'image' => '4.png', 'url' => 'http://www.tvnewscheck.com'),)
				),
				'android_tablets' => array(
					'Mozilla/5.0 (Linux; U; Android 4.2.2; es-us; GT-P5210 Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30/1.05v.3406.d7',
				),
			),
			'components' => array(
				'db' => array(
					'connectionString' => 'mysql:host=localhost;dbname=sales_library',
					'username' => 'root',
					'password' => 'root',
				),
			),
		)
	);
?>