<?
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SimpleFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SimpleFeedItem;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SimpleFeedShortcutItem;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SimpleFeedUrlItem;

	/**
     * @var $contentBlock SimpleFeedBlock
	 * @var $screenSettings array
     */

	/** @var SimpleFeedItem[] $newsItems */
	$newsItems = $contentBlock->items;
?>
<style>
    #vertical-feed-<? echo $contentBlock->id; ?> .panel
    {
        background: inherit;
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a
    {
        <?if(!empty($contentBlock->viewSettings->style->buttonBackColor)):?>
            background-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->buttonBackColor);?> !important;
        <?endif;?>
        <?if(!empty($contentBlock->viewSettings->style->buttonBorderColor)):?>
            border-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->buttonBorderColor);?> !important;
        <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a .glyphicon
    {
        <?if(!empty($contentBlock->viewSettings->style->buttonIconColor)):?>
            color: <? echo Utils::formatColor($contentBlock->viewSettings->style->buttonIconColor);?> !important;
        <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item
    {
        <?if(!empty($contentBlock->viewSettings->style->dividerColor)):?>
            border-bottom-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->dividerColor);?> !important;
        <?endif;?>
    }
</style>
<div id="vertical-feed-<? echo $contentBlock->id; ?>" class="vertical-feed news-block">
    <div class="service-data">
        <div class="encoded-object">
            <div class="view-settings"><? echo CJSON::encode($contentBlock->viewSettings); ?></div>
        </div>
    </div>
    <div class="panel panel-default"
         style="<? if (!empty($contentBlock->viewSettings->style->outsideBorderColor)): ?>border-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->outsideBorderColor); ?>; -webkit-box-shadow: 0 0 0 <? echo Utils::formatColor($contentBlock->viewSettings->style->outsideBorderColor); ?>; box-shadow: 0 0 0 <? echo Utils::formatColor($contentBlock->viewSettings->style->outsideBorderColor); ?>;<? endif; ?>">
        <div class="panel-heading" style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->viewSettings->style->headerPadding), true); ?><? if ($contentBlock->viewSettings->hideHeader): ?>display: none;<? endif; ?><? if (!empty($contentBlock->viewSettings->style->headerColor)): ?>background-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->headerColor); ?>;border-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->headerColor); ?>;<? endif; ?>">
	        <? if (!empty($contentBlock->viewSettings->icon)): ?>
                <i class="icomoon icomoon-lg <? echo $contentBlock->viewSettings->icon; ?>" style="<? if (!empty($contentBlock->viewSettings->style->headerIconColor)): ?>color: <? echo Utils::formatColor($contentBlock->viewSettings->style->headerIconColor); ?>;border-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->headerColor); ?>;<? endif; ?>"></i>
	        <? else: ?>
                <span class="glyphicon glyphicon-list-alt"></span>
	        <? endif; ?>
            <strong style="<? if (!empty($contentBlock->viewSettings->style->headerTextColor)): ?>color: <? echo Utils::formatColor($contentBlock->viewSettings->style->headerTextColor); ?>;<? endif; ?>"><? echo $contentBlock->viewSettings->title; ?></strong>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12">
                    <ul class="feed-items-list">
						<? foreach ($newsItems as $newsItem): ?>
                            <li class="news-item">
								<? if ($newsItem->type === 'url'): ?>
								    <? /** @var SimpleFeedUrlItem $newsItem */ ?>
                                    <a href="<? echo $newsItem->url; ?>" <? if ($newsItem->isMailTo!==false): ?>target="_self"<?else:?>target="_blank"<? endif; ?> class="content">
								<? elseif ($newsItem->type === 'shortcut'): ?>
									<? /** @var SimpleFeedShortcutItem $newsItem */ ?>
                                    <a href="<? echo isset($newsItem->shortcut) ? $newsItem->shortcut->getSourceLink() : '#'; ?>" class="content shortcuts-link<?if(!isset($newsItem->shortcut)):?> disabled<?endif;?>">
                                        <div class="service-data">
											<? echo isset($newsItem->shortcut) ? $newsItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
                                        </div>
								<? else: ?>
                                    <a href="#" class="content simple-block">
                                 <? endif; ?>
                                        <?if(!empty($newsItem->imageSettings->source)):?>
                                            <div class="image">
                                                <img src="<? echo $newsItem->imageSettings->source; ?>" width="60" class="img-circle" />
                                            </div>
                                        <? endif; ?>
                                        <div class="text">
	                                        <? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $newsItem->items, 'screenSettings' => $screenSettings), true); ?>
                                        </div>
                                    </a>
                            </li>
						<? endforeach; ?>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel-footer"
             style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->viewSettings->style->footerPadding), true); ?><? if ($contentBlock->viewSettings->hideFooter): ?>display: none;<? endif; ?><? if (!empty($contentBlock->viewSettings->style->footerColor)): ?>background-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->footerColor); ?>; border-top-color: <? echo Utils::formatColor($contentBlock->viewSettings->style->footerColor); ?>;<? endif; ?>"></div>
    </div>
</div>
