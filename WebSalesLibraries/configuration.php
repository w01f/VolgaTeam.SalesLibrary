<?php
$webRoot = dirname(__FILE__);
if ($_SERVER['HTTP_HOST'] == 'localhost')
    $internalConfig = $webRoot . '/protected/config/development.php';
else
    $internalConfig = $webRoot . '/protected/config/production.php';

return CMap::mergeArray(
        require($internalConfig), array(
        'name' => 'Sales Libraries',
        'params' => array(
            'home_tab' => array(
                'name' => 'Home',
                'list_button' => array(
                    'visible' => true,
                ),
                'buttons_button' => array(
                    'visible' => true,
                ),
                'search_button' => array(
                    'visible' => true,
                ),
            ),
            'search_tab' => array(
                'visible' => false,
                'name' => 'Search',
            ),
            'email' => array(
                'from' => 'billy@adSALESapps.com',
                'new_user' => array(
                    'subject' => 'Accsess to isalesdepot.com',
                    'body' => 'You have been created accont at isalesdepot.com',
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