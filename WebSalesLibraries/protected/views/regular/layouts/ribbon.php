<?php
foreach (Yii::app()->params as $key => $row)
{
    if (is_array($row))
        if (array_key_exists('position', $row))
            $tabParam[$key] = $row['position'];
}
asort($tabParam);
?>
<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    </head>
    <body >
        <div id="ribbon">
            <div class="ribbon-window-title" ></div>
            <?php foreach ($tabParam as $tabName => $tabIndex): ?>            
                <?php if ($tabName == 'home_tab'): ?>                        
                    <div class="ribbon-tab" id="home-tab">
                        <span class="ribbon-title"><?php echo Yii::app()->params['home_tab']['name'] ?></span>
                        <div class="ribbon-section" >
                            <span class="section-title" id="libraries-selector-title">
                                User: 
                                <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
                                    <?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
                                <?php endif; ?>
                            </span>
                            <table id ="libraries-selector-container">
                                <tr>
                                    <td><img src="" id="page-logo"/></td>
                                    <td>
                                        <table id="libraries-selector">
                                            <tr><td><select  id="select-library"></select></td></tr>
                                            <tr><td><select  id="select-page"></select></td></tr>
                                        </table>  
                                    </td>
                                </tr>
                            </table>  
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
                        <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>                   
                            <div class="ribbon-section">
                                <span class="section-title">Logout</span>
                                <div class="ribbon-button ribbon-button-large" id="logout">
                                    <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                    <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                    <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                </div>
                            </div> 
                        <?php endif; ?>                        
                    </div>
                <?php endif; ?>
                <?php if ($tabName == 'search_full_tab'): ?>                                    
                    <?php if (Yii::app()->params['search_full_tab']['visible']): ?>
                        <div class="ribbon-tab" id="search-full-tab">
                            <span class="ribbon-title"><?php echo Yii::app()->params['search_full_tab']['name'] ?></span>
                            <div class="ribbon-section" >
                                <span class="section-title"><?php echo Yii::app()->params['search_full_tab']['name'] ?></span>
                                <img src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
                            </div>
                            <div class="ribbon-section">
                                <span class="section-title">Search</span>
                                <div class="ribbon-button ribbon-button-large" id="run-search-full">
                                    <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                                    <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                                    <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                                </div>
                            </div>
                            <div class="ribbon-section">
                                <span class="section-title">Show Me the MONEY!</span>
                                <div class="ribbon-button ribbon-button-large" id="search-file-card-button">
                                    <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>" />
                                    <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>" />
                                    <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>" />
                                </div>
                            </div>                                        
                        </div>            
                    <?php endif; ?>
                <?php endif; ?>            
                <?php if ($tabName == 'search_file_card_tab'): ?>                                    
                    <?php if (Yii::app()->params['search_file_card_tab']['visible']): ?>
                        <div class="ribbon-tab" id="search-file-card-tab">
                            <span class="ribbon-title"><?php echo Yii::app()->params['search_file_card_tab']['name'] ?></span>
                            <div class="ribbon-section" >
                                <span class="section-title"><?php echo Yii::app()->params['search_file_card_tab']['name'] ?></span>
                                <img src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
                            </div>
                            <div class="ribbon-section">
                                <span class="section-title">Search</span>
                                <div class="ribbon-button ribbon-button-large" id="run-search-file-card">
                                    <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                                    <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                                    <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>" />
                                </div>
                            </div>                    
                        </div>            
                    <?php endif; ?>   
                <?php endif; ?>                        
            <?php endforeach; ?>            
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
