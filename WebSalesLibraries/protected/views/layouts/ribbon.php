<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    </head>
    <body >
        <div id="ribbon">
            <div class="ribbon-window-title" ></div>
            <div class="ribbon-tab" id="home-tab">
                <span class="ribbon-title"><?php echo Yii::app()->params['home_tab']['name'] ?></span>
                <div class="ribbon-section" >
                    <span class="section-title" id="libraries-selector-title">Sales Library</span>
                    <div id ="libraries-selector-container">
                        <img src="" id="page-logo"/>
                        <div id="libraries-selector">
                            <select  id="select-library">
                            </select>
                            <select  id="select-page">
                            </select>
                        </div>  
                    </div>  
                </div>
                <?php if (Yii::app()->params['home_tab']['list_button']['visible'] || Yii::app()->params['home_tab']['buttons_button']['visible'] || Yii::app()->params['home_tab']['search_button']['visible']): ?>                                    
                    <div class="ribbon-section">
                        <span class="section-title">Page Style</span>
                        <div class="ribbon-button sel ribbon-button-large" id="open-columns">
                            <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/columns.png' ?>"/>
                            <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/columns.png' ?>" />
                            <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/columns.png' ?>" />
                            <span class="button-title">Columns</span>
                            <span class="button-help">This button will show you the columns view.</span>
                        </div>
                        <?php if (Yii::app()->params['home_tab']['list_button']['visible']): ?>                    
                            <div class="ribbon-button ribbon-button-large disabled" ontouchstart="void(0);" onclick="void(0);" id="open-list">
                                <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/list.png' ?>"/>
                                <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/list.png' ?>" />
                                <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/list.png' ?>" />
                                <span class="button-title">List</span>
                                <span class="button-help">This button will show you the list view.</span>
                            </div>
                        <?php endif; ?>                    
                        <?php if (Yii::app()->params['home_tab']['buttons_button']['visible']): ?>                                        
                            <div class="ribbon-button ribbon-button-large disabled" ontouchstart="void(0);" onclick="void(0);" id="open-buttons">
                                <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/buttons.png' ?>"/>
                                <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/buttons.png' ?>" />
                                <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/buttons.png' ?>" />
                                <span class="button-title">Buttons</span>
                                <span class="button-help">This button will show you the buttons view.</span>
                            </div>                    
                        <?php endif; ?>                                        
                        <?php if (Yii::app()->params['home_tab']['search_button']['visible']): ?>                                                            
                            <div class="ribbon-button ribbon-button-large disabled" ontouchstart="void(0);" onclick="void(0);" id="open-search">
                                <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/search.png' ?>"/>
                                <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/hot/search.png' ?>" />
                                <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/disabled/search.png' ?>" />
                                <span class="button-title">Search</span>
                                <span class="button-help">This button will show you the search view.</span>
                            </div>                                        
                        <?php endif; ?>                                                            
                    </div>
                <?php endif; ?>                                                        
                <div class="ribbon-section">
                    <span class="section-title">Text Size</span>
                    <div class="ribbon-button ribbon-button-large" id="increase-text-size">
                        <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSize.png' ?>" />
                        <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSize.png' ?>" />
                        <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSize.png' ?>" />
                    </div>
                    <div class="ribbon-button ribbon-button-large" id="decrease-text-size">
                        <img  class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSize.png' ?>" />
                        <img  class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSize.png' ?>" />
                        <img  class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSize.png' ?>" />
                    </div>
                </div>
                <div class="ribbon-section">
                    <span class="section-title">Line Spacing</span>
                    <div class="ribbon-button ribbon-button-large" id="increase-text-space">
                        <img  class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/increaseTextSpace.png' ?>" />
                    </div>
                    <div class="ribbon-button ribbon-button-large" id="decrease-text-space">
                        <img  class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSpace.png' ?>" />
                        <img  class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/decreaseTextSpace.png' ?>" />
                    </div>
                </div>
            </div>
            <?php if (Yii::app()->params['search_tab']['visible']): ?>
                <div class="ribbon-tab" id="search-tab">
                    <span class="ribbon-title"><?php echo Yii::app()->params['search_tab']['name'] ?></span>
                    <div class="ribbon-section" >
                        <span class="section-title"><?php echo Yii::app()->params['search_tab']['name'] ?></span>
                        <img src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
                    </div>
                    <div class="ribbon-section">
                        <span class="section-title">Search</span>
                        <div class="ribbon-button ribbon-button-large" id="run-search">
                            <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                            <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                            <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                        </div>
                    </div>                    
                </div>            
            <?php endif; ?>
        </div>
        <div id="content">
            <?php echo $content; ?>
        </div>
        <div id="content-overlay">
        </div>        
        <!--  View dialog hidden part  -->
        <div>
            <a id="view-dialog-link" href="#viewDialogContainer">View Options</a>
            <div id="view-dialog-wrapper">
                <div id="view-dialog-container">
                </div>
            </div>
        </div>
        <!------------------------->
    </body>
</html>
