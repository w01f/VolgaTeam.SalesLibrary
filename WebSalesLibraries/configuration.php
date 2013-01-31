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
				'stations' => array(
					'tab_name' => 'Libraries',
					'column_name' => 'Libraries',
				),
				'email' => array(
					'from' => 'bcaudill@raycommedia.com',
					'new_user' => array(
						'subject' => 'RaycomResults.tv',
						'body' => 'You have a new account at RaycomResults.tv',
					),
					'send_link' => array(
						'subject' => 'Raycom Results File Link',
						'body' => 'View your file:',
					),
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