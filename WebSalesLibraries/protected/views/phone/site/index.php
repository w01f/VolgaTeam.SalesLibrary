<?php
$version = '3.0';
$cs = Yii::app()->clientScript;
$cs->registerCoreScript('jquery');
$cs->registerCoreScript('cookie');
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/photoswipe/photoswipe.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobiscroll/css/mobiscroll-2.1.custom.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/libraries.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/search.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/file-card.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/photoswipe/lib/klass.min.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/photoswipe/code.photoswipe.jquery-3.0.5.min.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobiscroll/js/mobiscroll-2.1.custom.min.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/login.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/linkViewing.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/libraries.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/search.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/ribbon.js?' . $version, CClientScript::POS_HEAD);
?>
<div data-role='page' id="libraries" data-cache="never" data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <span class="ui-title">Sales Libraries</span>
    </div>                 
    <div data-role='content' class ="page-content">
        <table id ="selectors-container">
            <tr>
                <td>
                    <label for="libraries-selector" class="select">Select Sales Library:</label>
                    <select id="libraries-selector" name="libraries-selector" data-native-menu="true">
                    </select>                
                </td>
            </tr>        
            <tr>
                <td>
                    <br>
                    <label for="page-selector" class="select">Select Page:</label>
                    <select id="page-selector" name="page-selector" data-native-menu="true">
                    </select>                
                </td>
            </tr>
            <tr>        
                <td>
                    <br>
                    <br>
                    <a id ="load-page-button" href="#" data-role="button" data-theme="b">Load Library</a>
                </td>        
            </tr>                
        </table>        
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="folders" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <span class="ui-title library-title"></span>
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="links" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="link back ui-btn-right" href="#folders" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction ="reverse" data-theme="b">Back</a>
        <span class="ui-title library-title"></span>
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="link-details" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="link back ui-btn-right" href="#links" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction ="reverse" data-theme="b">Back</a>
        <span class="ui-title library-title"></span>
    </div>             
    <div data-role='content' class ="page-content">
        Here will be the list of attachments with the file card at top
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="preview" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="link back ui-btn-right" href="#links" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction ="reverse" data-theme="b">Back</a>
        <span class="ui-title library-title"></span>
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="gallery-page" data-cache="never" data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction ="reverse" data-theme="b">Back</a>
        <span class="ui-title library-title"></span>
    </div>             
    <div data-role='content' class ="page-content">
        <ul data-role="listview" data-theme="c" data-divider-theme="c">
            <li data-role="list-divider" >
                <h4 id="gallery-title">                        
                </h4>                        
            </li>            
        </ul>
        <br>
        <ul id ="gallery">
        </ul>
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
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
<div data-role='page' id="search-basic" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
        <span class="ui-title">Search</span>
        <div data-role="navbar">
            <ul>
                <li>
                    <a class ="ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-transition="none">Search</a>
                </li>
                <li>
                    <a class ="tab-search-tags" href="#search-tags" data-transition="none">Tags</a>
                </li>
                <li>
                    <a class ="tab-search-libraries" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations_tab']['name']; ?></a>
                </li>
            </ul>
        </div>
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="search-tags" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
        <span class="ui-title">Search</span>
        <div data-role="navbar">
            <ul>
                <li>
                    <a href="#search-basic" data-transition="none">Search</a>
                </li>
                <li>
                    <a class ="tab-search-tags ui-btn ui-btn-active ui-state-persist" href="#search-tags" data-transition="none">Tags</a>
                </li>
                <li>
                    <a class ="tab-search-libraries" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations_tab']['name']; ?></a>
                </li>
            </ul>
        </div>
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="search-libraries" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
        <span class="ui-title">Search</span>
        <div data-role="navbar">
            <ul>
                <li>
                    <a href="#search-basic" data-transition="none">Search</a>
                </li>
                <li>
                    <a class ="tab-search-tags" href="#search-tags" data-transition="none">Tags</a>
                </li>
                <li>
                    <a class ="tab-search-libraries ui-btn ui-btn-active ui-state-persist" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations_tab']['name']; ?></a>
                </li>
            </ul>
        </div>
    </div>             
    <div data-role='content' class ="page-content">
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade">
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
<div data-role='page' id="search-result" data-cache="never"  data-dom-cache ="false" data-ajax="false"> 
    <div data-role='header' class ="page-header" data-position="fixed" data-theme="b">
        <a class="link back ui-btn-right" href="#search-basic" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction ="reverse" data-theme="b">Back</a>
        <span class="ui-title library-title">Search</span>
    </div>             
    <div data-role='content' class ="page-content">
        <ul data-role="listview" data-theme="c" data-divider-theme="c">
            <li data-role="list-divider" >
                <h4>
                    <table class="layout-group">
                        <tr>
                            <td id ="search-result-links-number" class="on-left">
                                Files was not found
                            </td>
                            <td id ="search-result-sort-column-container" class="on-center">
                                <select name="search-result-sort-column" id="search-result-sort-column" data-mini="true">
                                    <option value="link-name" selected>By Name</option>
                                    <option value="link-type">By Type</option>
                                    <option value="link-date">By Date</option>
                                    <option value="library">By Library</option>
                                </select>
                            </td>
                            <td id ="search-result-sort-order-container" class="on-right">
                                <select name="search-result-sort-order" id="search-result-sort-order" data-role="slider" data-mini="true" data-track-theme="b">
                                    <option value="asc">Asc</option>
                                    <option value="desc">Desc</option>
                                </select>
                            </td>                            
                        </tr>
                    </table>
                </h4>
            </li>
        </ul>
        <br>
        <ul id ="search-result-body" data-role="listview" data-theme="c" data-divider-theme="d">
        </ul>
    </div> 
    <div class ="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
        <div data-role="navbar" data-iconpos="top">
            <ul>
                <li>
                    <a class ="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction ="reverse">
                        Libraries
                    </a>
                </li>
                <li>
                    <a class ="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade">
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