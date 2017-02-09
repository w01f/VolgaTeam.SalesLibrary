<?
	/** @var NavigationPanel $navigationPanel */
?>
<style>
    #content .navigation-panel.expanded ul {
        overflow-y: <?if($navigationPanel->showScroll):?>auto<?else:?>hidden <?endif;?>;
    }

    #content .navigation-panel li .item-title {
        color: <?echo '#'.$navigationPanel->textColor;?>;
        font-size: <?echo $navigationPanel->textSize;?>px;
    }

    #content .navigation-panel li .item-icon {
        margin-bottom: <?echo $navigationPanel->imagePadding;?>px;
    }

    #content .navigation-panel.expanded {
        background-color: <?echo '#'.$navigationPanel->backColorExpanded;?>;
        border-right: <?echo '#'.$navigationPanel->dividerColorExpanded;?> solid <?echo $navigationPanel->dividerWidthExpanded;?>px;
    }

    #content .navigation-panel.expanded li a {
        padding-bottom: <?echo $navigationPanel->itemsGapExpanded;?>px;
    }

    #content .navigation-panel.expanded .control-bar a:hover,
    #content .navigation-panel.expanded .control-bar a:focus:hover,
    #content .navigation-panel.expanded li a:hover,
    #content .navigation-panel.expanded li a:focus:hover {
        background-color: <?echo '#'.$navigationPanel->hoverColorExpanded;?>;
    }

    #content .navigation-panel.collapsed {
        background-color: <?echo '#'.$navigationPanel->backColorCollapsed;?>;
        border-right: <?echo '#'.$navigationPanel->dividerColorCollapsed;?> solid <?echo $navigationPanel->dividerWidthCollapsed;?>px;
    }

    #content .navigation-panel.collapsed li a {
        padding-bottom: <?echo $navigationPanel->itemsGapCollapsed;?>px;
    }

    #content .navigation-panel.collapsed .control-bar a:hover,
    #content .navigation-panel.collapsed .control-bar a:focus:hover,
    #content .navigation-panel.collapsed li a:hover,
    #content .navigation-panel.collapsed li a:focus:hover {
        background-color: <?echo '#'.$navigationPanel->hoverColorCollapsed;?>;
    }
</style>
<div class="control-bar">
    <a class="button button-collapse" href="#" title="Collapse">
        <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/left-panel/collapse_panel.svg'; ?>">
    </a>
    <a class="button button-expand" href="#" title="Expand">
        <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/left-panel/expand_panel.svg'; ?>">
    </a>
</div>
<ul class="nav nav-pills">
	<? foreach ($navigationPanel->items as $navigationItem): ?>
        <li class="navigation-item" <? if (!Yii::app()->browser->isMobile() && !empty($navigationItem->tooltip)): ?> title="<? echo $navigationItem->tooltip; ?>"<? endif; ?>>
			<?
				$viewPath = \Yii::getPathOfAlias('application.views.regular.shortcuts.navigationPanel') . '/' . $navigationItem->contentView . '.php';
				echo $this->renderFile($viewPath, array('itemData' => $navigationItem), true);
			?>
        </li>
	<? endforeach; ?>
</ul>
