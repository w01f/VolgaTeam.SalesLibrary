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
					'tempPasswordExpiredIn' => '7',
					'complex_password' => false,
					'jqmlogintext' => 'Welcome to Raycom Results for Smartphones',
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
						'lan' => 'LAN Link',
						'quicksite' => 'QuickSite Link',
						'html5' => 'HTML5 Link',
						'internal library' => 'Library Link',
						'internal page' => 'Page Link',
						'internal window' => 'Window Link',
						'internal link' => 'Link',
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
					'from' => 'bcaudill@raycommedia.com',
					'copy_enabled' => true,
					'copy' => 'billy@adsalesapp.com',
					'new_user' => array(
						'subject' => 'RaycomResults.tv',
						'body' => 'You have a new account at RaycomResults.tv',
					),
					'help_request_address' => 'billy@newlocaldirect.com',
					'quiz' => array(
						'from' => 'help@adsalesapps.com',
						'copy_enabled' => true,
						'copy' => 'help@adsalesapps.com'
					),
				),
				'android_tablets' => array(
					'Mozilla/5.0 (Linux; U; Android 4.2.2; es-us; GT-P5210 Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30/1.05v.3406.d7',
				),
				'menu' => array(
					'BarColor' => '2C84EE',
					'IconSeparation' => 5,
					'SaveSuperGroup' => true
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