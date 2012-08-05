<!DOCTYPE html>
<html>
    <head>
        <?php
        $cs = Yii::app()->clientScript;
        $cs->registerCssFile(Yii::app()->baseUrl . '/protected/extensions/fancybox/source/jquery.fancybox.css');
        $cs->registerCssFile(Yii::app()->baseUrl . '/css/ribbon.css');
        $cs->registerCssFile(Yii::app()->baseUrl . '/css/ajaxLoader.css');
        $cs->registerCoreScript('jquery');
        $cs->registerCoreScript('cookie');
        $cs->registerScriptFile(Yii::app()->baseUrl . '/protected/extensions/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/protected/extensions/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/js/ribbon.js', CClientScript::POS_HEAD);
        ?>
    </head>
    <body >
        <div id="ribbon">
            <div class="ribbon-window-title" ></div>
            <div class="ribbon-tab" id="format-tab">
                <span class="ribbon-title">Home</span>
                <div class="ribbon-section" >
                    <span class="section-title" id="librariesSelectorTitle">Sales Library</span>
                    <div id ="librariesSelectorContainer">
                        <img src="" id="pagelogo"/>
                        <div id="librariesSelector">
                            <select  id="selectLibrary">
                            </select>
                            <select  id="selectPage">
                            </select>
                        </div>  
                    </div>  
                </div>
                <div class="ribbon-section">
                    <span class="section-title">Page Style</span>
                    <div class="ribbon-button sel ribbon-button-large" id="openColumns">
                        <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/columns.png' ?>"/>
                        <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/columns.png' ?>" />
                        <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/columns.png' ?>" />
                        <span class="button-title">Columns</span>
                        <span class="button-help">This button will show you the columns view.</span>
                    </div>
                    <div class="ribbon-button ribbon-button-large disabled" ontouchstart="void(0);" onclick="void(0);" id="openList">
                        <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/list.png' ?>"/>
                        <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/list.png' ?>" />
                        <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/list.png' ?>" />
                        <span class="button-title">List</span>
                        <span class="button-help">This button will show you the list view.</span>
                    </div>
                    <div class="ribbon-button ribbon-button-large disabled" ontouchstart="void(0);" onclick="void(0);" id="openButtons">
                        <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/buttons.png' ?>"/>
                        <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/buttons.png' ?>" />
                        <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/buttons.png' ?>" />
                        <span class="button-title">Buttons</span>
                        <span class="button-help">This button will show you the buttons view.</span>
                    </div>                    
                    <div class="ribbon-button ribbon-button-large disabled" ontouchstart="void(0);" onclick="void(0);" id="openSearch">
                        <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/search.png' ?>"/>
                        <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/search.png' ?>" />
                        <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/search.png' ?>" />
                        <span class="button-title">Search</span>
                        <span class="button-help">This button will show you the search view.</span>
                    </div>                                        
                </div>
                <div class="ribbon-section">
                    <span class="section-title">Text Size</span>
                    <div class="ribbon-button ribbon-button-large" id="increaseTextSize">
                        <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSize.png' ?>" />
                        <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSize.png' ?>" />
                        <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSize.png' ?>" />
                    </div>
                    <div class="ribbon-button ribbon-button-large" id="decreaseTextSize">
                        <img  class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSize.png' ?>" />
                        <img  class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSize.png' ?>" />
                        <img  class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSize.png' ?>" />
                    </div>
                </div>
                <div class="ribbon-section">
                    <span class="section-title">Line Spacing</span>
                    <div class="ribbon-button ribbon-button-large" id="increaseTextSpace">
                        <img  class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSpace.png' ?>" />
                    </div>
                    <div class="ribbon-button ribbon-button-large" id="decreaseTextSpace">
                        <img  class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSpace.png' ?>" />
                    </div>
                </div>
            </div>
        </div>
        <div id="content">
            <?php echo $content; ?>
        </div>
        <div id="contentOverlay">
        </div>        
        <!--  View dialog hidden part  -->
        <div>
            <a id="viewDialogLink" href="#viewDialogContainer">View Options</a>
            <div id="viewDialogWrapper">
                <div id="viewDialogContainer">
                </div>
            </div>
        </div>
        <!------------------------->
    </body>
</html>
