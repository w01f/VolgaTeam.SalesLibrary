<?php
return CMap::mergeArray(
        require(dirname(__FILE__) . '/main.php'), array(
        'behaviors' => array(
            'onBeginRequest' => array(
                'class' => 'application.components.core.RequireLogin'
            )
        ),
        'components' => array(
            'db' => array(
                'connectionString' => 'mysql:host=127.0.0.1;dbname=isalesde_db',
                'emulatePrepare' => true,
                'username' => 'isalesde_admin',
                'password' => 'a284vlink',
                'charset' => 'utf8',
                'tablePrefix' => 'tbl_',
                'schemaCachingDuration' => 3600,
            ),
        ),
        )
);
?>