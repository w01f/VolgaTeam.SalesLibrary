<?php

include_once(dirname(dirname(__FILE__)).DIRECTORY_SEPARATOR.'config.php');

cometchatDBConnect();
global $dbh;

$content = <<<EOD
ALTER TABLE `cometchat_chatrooms`
add column(
`guid` varchar(255) default NULL
);

REPLACE INTO `cometchat_settings` (setting_key,value,key_type) values ('dbversion','19','1');
EOD;
$q = preg_split('/;[\r\n]+/',$content);
if(!isset($errors)){
   $errors='';
}
foreach ($q as $query) {
  if (strlen($query) > 4) {
    if (!mysqli_query($dbh,$query)) {
      $errors .= mysqli_error($dbh)."<br/>\n";
    }
  }
}
removeCachedSettings($client.'settings');
