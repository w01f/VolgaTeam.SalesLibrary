<?php
return CMap::mergeArray(
        require(dirname(__FILE__) . '/main.php'), array(
        'components' => array(
            'db' => array(
                'connectionString' => 'mysql:host=localhost;dbname=sales_library',
                'emulatePrepare' => true,
                'username' => 'root',
                'password' => 'root',
                'charset' => 'utf8',
                'autoConnect' => true,
                'tablePrefix' => 'tbl_',
                'schemaCachingDuration' => 3600,
            ),
        ),
        )
);
