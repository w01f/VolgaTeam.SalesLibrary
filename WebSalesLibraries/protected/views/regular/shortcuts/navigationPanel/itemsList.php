<?
	/** @var NavigationPanel $navigationPanel */
?>
<style>
    #content .navigation-panel.expanded ul {
        overflow-y: <?if($navigationPanel->showScroll):?>auto<?else:?>hidden <?endif;?>;
    }

    #content .navigation-panel li .item-title {
        color: <?echo Utils::formatColorToHex($navigationPanel->textColor);?>;
        font-size: <?echo $navigationPanel->textSize;?>px;
    }

    #content .navigation-panel li .item-icon {
        margin-bottom: <?echo $navigationPanel->imagePadding;?>px;
    }

    #content .navigation-panel.expanded {
        background-color: <?echo Utils::formatColorToHex($navigationPanel->backColorExpanded);?>;
        border-right: <?echo Utils::formatColorToHex($navigationPanel->dividerColorExpanded);?> solid <?echo $navigationPanel->dividerWidthExpanded;?>px;
    }

    #content .navigation-panel.expanded li a {
        padding-top: <?echo intval($navigationPanel->itemsGapExpanded/2);?>px;
        padding-bottom: <?echo intval($navigationPanel->itemsGapExpanded/2);?>px;
    }

    #content .navigation-panel .control-bar .button-collapse svg g,
    #content .navigation-panel .control-bar .button-collapse svg path {
        fill: <?echo Utils::formatColorToHex($navigationPanel->buttonColorCollapse);?> !important;
    }

    #content .navigation-panel .control-bar .button-expand svg g,
    #content .navigation-panel .control-bar .button-expand svg path {
        fill: <?echo Utils::formatColorToHex($navigationPanel->buttonColorExpand);?> !important;
    }

    #content .navigation-panel.expanded .control-bar a:hover,
    #content .navigation-panel.expanded .control-bar a:focus:hover,
    #content .navigation-panel.expanded li.enabled a:hover,
    #content .navigation-panel.expanded li.enabled a:focus:hover {
        background-color: <?echo Utils::formatColorToHex($navigationPanel->hoverColorExpanded);?>;
    }

    #content .navigation-panel.collapsed {
        background-color: <?echo Utils::formatColorToHex($navigationPanel->backColorCollapsed);?>;
        border-right: <?echo Utils::formatColorToHex($navigationPanel->dividerColorCollapsed);?> solid <?echo $navigationPanel->dividerWidthCollapsed;?>px;
    }

    #content .navigation-panel.collapsed li a {
        padding-top: <?echo intval($navigationPanel->itemsGapCollapsed/2);?>px;
        padding-bottom: <?echo intval($navigationPanel->itemsGapCollapsed/2);?>px;
    }

    #content .navigation-panel.collapsed .control-bar a:hover,
    #content .navigation-panel.collapsed .control-bar a:focus:hover,
    #content .navigation-panel.collapsed li.enabled a:hover,
    #content .navigation-panel.collapsed li.enabled a:focus:hover {
        background-color: <?echo Utils::formatColorToHex($navigationPanel->hoverColorCollapsed);?>;
    }
</style>
<div class="control-bar">
    <a class="button button-collapse" href="#" title="Collapse">
        <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/left-panel/collapse_panel.svg'; ?>"
             style="display: none">
    </a>
    <a class="button button-expand" href="#" title="Expand">
        <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/left-panel/expand_panel.svg'; ?>"
             style="display: none">
    </a>
</div>
<div class="navigation-item-list-container">
    <div class="navigation-item-list">
        <ul class="nav nav-pills">
            <? foreach ($navigationPanel->items as $navigationItem): ?>
                <?
                /** @var RegularNavigationItemSettings $settings */
                $settings = $navigationItem->settings;
                ?>
                <li class="navigation-item<? if ($settings->enabled): ?> enabled<? else: ?> disabled<? endif; ?>" <? if (!Yii::app()->browser->isMobile() && !empty($settings->tooltip)): ?> title="<? echo $settings->tooltip; ?>"<? endif; ?>>
                    <?
                        $viewPath = \Yii::getPathOfAlias('application.views.regular.shortcuts.navigationPanel') . '/' . $navigationItem->contentView . '.php';
                        echo $this->renderFile($viewPath, array('itemData' => $navigationItem), true);
                    ?>
                </li>
            <? endforeach; ?>
        </ul>
    </div>
</div>
