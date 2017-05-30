<?php
include_once(dirname(dirname(dirname(__FILE__))).DIRECTORY_SEPARATOR.'config.php');

if(CROSS_DOMAIN != '1'){
  $http = 'http://';
  if(!empty($_SERVER['HTTPS'])){
    $http = 'https://';
  }
  $baseUrl = $http.$_SERVER['SERVER_NAME'].BASE_URL;
}else{
  $baseUrl = BASE_URL;
  if(strpos($baseUrl,'http') === false){
     $baseUrl = 'http:'.BASE_URL;
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/* SETTINGS START */
if(empty($client)) {
  $facebookKey = setConfigValue('facebookKey','');
  $facebookSecret = setConfigValue('facebookSecret','');
  $twitterKey = setConfigValue('twitterKey','');
  $twitterSecret = setConfigValue('twitterSecret','');
  $googleKey = setConfigValue('googleKey','');
  $googleSecret = setConfigValue('googleSecret','');
}

/* SETTINGS END */

return array (
  'base_url' => $baseUrl.'functions/login/',
  'networks' =>
  array (
    'facebook' =>
    array (
      'name' => 'Facebook',
      'enabled' => true,
      'keys' =>
      array (
        'key' => $facebookKey,
        'secret' => $facebookSecret,
      ),
    ),
    'twitter' =>
    array (
      'name' => 'Twitter',
      'enabled' => true,
      'keys' =>
      array (
        'key' => $twitterKey,
        'secret' => $twitterSecret,
      ),
    ),
    'google' =>
    array (
      'name' => 'Google',
      'enabled' => true,
      'keys' =>
      array (
        'key' => $googleKey,
        'secret' => $googleSecret,
      ),
    ),
  ),
  'debug_enabled' => false,
  'log_file' => '',
);
