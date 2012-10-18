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
            ),
            'home_tab' => array(
                'name' => 'Home',
                'position' => 1,
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
            'search_full_tab' => array(
                'visible' => true,
                'name' => 'Search',
                'position' => 2,
            ),
            'search_file_card_tab' => array(
                'visible' => true,
                'name' => 'Sales Success Models',
                'position' => 3,
            ),
            'email' => array(
                'from' => 'billy@adSALESapps.com',
                'new_user' => array(
                    'subject' => 'Accsess to isalesdepot.com',
                    'body' => 'You have been created accont at isalesdepot.com',
                ),
                'send_link' => array(
                    'subject' => 'iSalesDepot Link',
                    'body' => 'Check this link, please:',
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