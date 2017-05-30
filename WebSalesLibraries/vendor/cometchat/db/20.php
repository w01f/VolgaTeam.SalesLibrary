<?php

include_once(dirname(dirname(__FILE__)).DIRECTORY_SEPARATOR.'config.php');

cometchatDBConnect();
global $dbh;
$DB_NAME = DB_NAME;
$addColumn = "";
if(!isset($errors)){
   $errors='';
}
$sql = "SELECT IF(count(*) = 1, 'Exist','Not Exist') AS result FROM information_schema.columns WHERE table_schema = '{$DB_NAME}'
    AND table_name = 'cometchat_users' AND column_name = 'uid'";

if($query = mysqli_query($dbh, $sql)){
   $errors .= mysqli_error($dbh);
}
$column = mysqli_fetch_assoc($query);

if ($column['result'] == 'Not Exist') {
   $addColumn = "ALTER TABLE `cometchat_users` add column(`uid` varchar(255) NOT NULL);";
}

$content = <<<EOD
{$addColumn}
REPLACE INTO `cometchat_settings` (setting_key,value,key_type) values ('dbversion','20','1');
EOD;

$q = preg_split('/;[\r\n]+/',$content);
foreach ($q as $query) {
  if (strlen($query) > 4) {
    if (!mysqli_query($dbh,$query)) {
      $errors .= mysqli_error($dbh)."<br/>\n";
    }
  }
}
removeCachedSettings($client.'settings');
