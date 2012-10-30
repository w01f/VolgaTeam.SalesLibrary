<?php
$version = '3.0';
$cs = Yii::app()->clientScript;
$cs->registerCoreScript('jquery');
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/libraries.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/login.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/libraries.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/search.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/ribbon.js?' . $version, CClientScript::POS_HEAD);
?>
<div data-role='page' id="libraries"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a id ="button-cllapse-all" class="ui-btn-right" href="#" data-role="button" data-mini="true" data-icon="plus" data-corners="true" data-shadow="true" data-iconshadow="true"  data-transition="slidefade" data-theme="b">Collapse</a>
        <span class="ui-title">Sales Libraries</span>
    </div>                 
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search" href="#search" data-icon="search" data-transition="slidefade">
                        Search
                    </a>
                </li>
                <li>
                    <a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade">
                        Log Out
                    </a>
                </li>
            </ul>
        </div>             
    </div>             
</div>        
<div data-role='page' id="search"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <h3>Search</h3>        
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search ui-btn ui-btn-active ui-state-persist" href="#search" data-icon="search" data-transition="slidefade">
                        Search
                    </a>
                </li>
                <li>
                    <a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade">
                        Log Out
                    </a>
                </li>
            </ul>
        </div>             
    </div>             
</div>        
<div data-role='page' id="links"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="link back ui-btn-right" href="#libraries" data-role="button" data-mini="true" data-icon="back" data-corners="true" data-shadow="true" data-iconshadow="true"  data-transition="slidefade" data-theme="b">Back</a>
        <span class="ui-title library-title"></span>
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search" href="#search" data-icon="search" data-transition="slidefade">
                        Search
                    </a>
                </li>
                <li>
                    <a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade">
                        Log Out
                    </a>
                </li>
            </ul>
        </div>             
    </div>             
</div>        

