<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <?php
        $version = '3.0';
        $cs = Yii::app()->clientScript;
        $cs->registerCoreScript('jquery');
        $cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.css?' . $version);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.js?' . $version, CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/login.js', CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/ribbon.js', CClientScript::POS_HEAD);
        ?>
    </head>
    <body>
        <div data-role='page'> 
            <div data-role='header' data-theme='b'>
                <div data-role="navbar" data-iconpos="left">
                    <ul>
                        <li>
                            <a href="#" data-icon="grid">
                                Sales Libraries
                            </a>
                        </li>
                    </ul>
                </div>
            </div> 
            <div data-role='content'>
                <?php echo $content; ?>
            </div> 
            <div data-role="tabbar" data-iconpos="top" data-theme="b" id="ribbon">
                <ul>
                    <li>
                        <a href="#libraries" data-icon="grid">
                            Libraries
                        </a>
                    </li>
                    <li>
                        <a href="#search" data-icon="search">
                            Search
                        </a>
                    </li>
                    <li>
                        <a href="#" data-icon="delete" id="logout">
                            Log Out
                        </a>
                    </li>
                </ul>
            </div>
        </div>        
    </body>
</html>