<?php
return CMap::mergeArray(
        require(dirname(__FILE__) . '/main.php'), array(
        'components' => array(
            'db' => array(
                'connectionString' => 'mysql:host=127.0.0.1;dbname=isalesde_db',
                'emulatePrepare' => true,
                'charset' => 'utf8',
                'tablePrefix' => 'tbl_',
                'schemaCachingDuration' => 3600,
            ),
        ),
        )
);
