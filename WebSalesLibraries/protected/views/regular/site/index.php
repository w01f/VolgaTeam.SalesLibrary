<?php
$version = '43.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->clientScript->getCoreScriptUrl() . '/jui/css/base/jquery-ui.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/video-js/video-js.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/datepicker/css/daterangepicker.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/ribbon.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/columns.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/search.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/view-dialog.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/email-dialog.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/file-card.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/search-grid.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/help.css?' . $version);
$cs->registerCoreScript('jquery.ui');
$cs->registerCoreScript('cookie');
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/date.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/daterangepicker.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/login.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/overlay.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/textSizing.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/scaling.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/linkViewing.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/columns.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/search-grid.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/search.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/help.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/ribbon.js?' . $version, CClientScript::POS_HEAD);
$this->pageTitle = Yii::app()->name;

foreach (Yii::app()->params as $key => $row)
{
    if (is_array($row))
        if (array_key_exists('position', $row))
            $tabParam[$key] = $row['position'];
}

$tabHelpRecords = HelpTabStorage::model()->findAll(array('order' => '`order`', 'condition' => 'enabled=:enabled', 'params' => array(':enabled' => true)));
if (isset($tabHelpRecords))
    foreach ($tabHelpRecords as $tabHelpRecord)
        $tabParam['help-tab-' . $tabHelpRecord->id] = $tabHelpRecord->order;

asort($tabParam);
?>
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
                        <div class="ribbon-button ribbon-button-large logout-button">
                            <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                            <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                            <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                        </div>
                    </div> 
                <?php endif; ?>                        
            </div>
        <?php elseif ($tabName == 'search_full_tab'): ?>                                    
            <?php if (Yii::app()->params['search_full_tab']['visible']): ?>
                <div class="ribbon-tab" id="search-full-tab">
                    <span class="ribbon-title"><?php echo Yii::app()->params['search_full_tab']['name'] ?></span>
                    <div class="ribbon-section" >
                        <span class="section-title">
                            <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
                                <?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
                            <?php endif; ?>
                        </span>
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
                    <?php if (Yii::app()->params['search_full_tab']['show_money_button']): ?>                   
                        <div class="ribbon-section">
                            <span class="section-title">Show Me the MONEY!</span>
                            <div class="ribbon-button ribbon-button-large" id="search-file-card-button">
                                <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>" />
                                <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>" />
                                <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>" />
                            </div>
                        </div>                                        
                    <?php endif; ?>
                    <div class="ribbon-section">
                        <span class="section-title">Clear</span>
                        <div class="ribbon-button ribbon-button-large clear-button">
                            <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>" />
                            <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>" />
                            <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>" />
                        </div>
                    </div>                             
                    <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>                   
                        <div class="ribbon-section">
                            <span class="section-title">Logout</span>
                            <div class="ribbon-button ribbon-button-large logout-button">
                                <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                            </div>
                        </div> 
                    <?php endif; ?>                                                    
                </div>            
            <?php endif; ?>
        <?php elseif ($tabName == 'search_file_card_tab'): ?>                                    
            <?php if (Yii::app()->params['search_file_card_tab']['visible']): ?>
                <div class="ribbon-tab" id="search-file-card-tab">
                    <span class="ribbon-title"><?php echo Yii::app()->params['search_file_card_tab']['name'] ?></span>
                    <div class="ribbon-section" >
                        <span class="section-title">
                            <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
                                <?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
                            <?php endif; ?>
                        </span>
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
                    <div class="ribbon-section">
                        <span class="section-title">Clear</span>
                        <div class="ribbon-button ribbon-button-large clear-button">
                            <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>" />
                            <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>" />
                            <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>" />
                        </div>
                    </div>                            
                    <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>                   
                        <div class="ribbon-section">
                            <span class="section-title">Logout</span>
                            <div class="ribbon-button ribbon-button-large logout-button">
                                <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                            </div>
                        </div> 
                    <?php endif; ?>                        
                </div>            
            <?php endif; ?>   
        <?php elseif (strpos($tabName, 'help-tab-') !== false): ?>                                        
            <?php $tabHelpRecord = HelpTabStorage::model()->findByPk(str_replace('help-tab-', '', $tabName)); ?>
            <?php if (isset($tabHelpRecord)): ?>
                <div class="ribbon-tab help-tab" id="help-tab-<?php echo $tabHelpRecord->id; ?>">
                    <span class="ribbon-title"><?php echo $tabHelpRecord->name; ?></span>
                    <div class="ribbon-section" >
                        <span class="section-title">
                            <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
                                <?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
                            <?php endif; ?>
                        </span>
                        <img src="<?php echo Yii::app()->baseUrl . $tabHelpRecord->image_path; ?>"/>
                    </div>
                    <?php
                    $pageHelpRecords = HelpPageStorage::model()->findAll(array('order' => '`order`', 'condition' => 'id_tab=:id_tab', 'params' => array(':id_tab' => $tabHelpRecord->id)));
                    $selected = true;
                    ?>
                    <?php if (isset($pageHelpRecords)): ?>
                        <?php foreach ($pageHelpRecords as $pageHelpRecord): ?>
                            <div class="ribbon-section <?php echo!$pageHelpRecord->enabled ? 'disabled' : ''; ?>">
                                <span class="section-title"><?php echo $pageHelpRecord->name; ?></span>
                                <div class="ribbon-button ribbon-button-large <?php echo $pageHelpRecord->enabled && $selected ? 'sel' : ''; ?> <?php echo!$pageHelpRecord->enabled ? 'disabled' : 'enabled'; ?> help-page" id="<?php echo $pageHelpRecord->id; ?>">
                                    <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . $pageHelpRecord->image_path; ?>" />
                                    <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . $pageHelpRecord->image_path; ?>" />
                                    <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . $pageHelpRecord->image_path; ?>" />
                                </div>
                            </div> 
                            <?php $selected = $pageHelpRecord->enabled ? false : $selected; ?>            
                        <?php endforeach; ?>            
                    <?php endif; ?>
                    <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>                   
                        <div class="ribbon-section">
                            <span class="section-title">Logout</span>
                            <div class="ribbon-button ribbon-button-large logout-button">
                                <img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                <img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                                <img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>" />
                            </div>
                        </div> 
                    <?php endif; ?>                        
                </div>            
            <?php endif; ?>
        <?php endif; ?>       
    <?php endforeach; ?>                
</div>
<div id="content">
</div>
<div id="content-overlay">
</div>        
<!--  View dialog hidden part  -->
<div>
    <a id="view-dialog-link" href="#view-dialog-container">View Options</a>
    <div id="view-dialog-wrapper">
        <div id="view-dialog-container">
        </div>
    </div>
</div>
<!------------------------->

