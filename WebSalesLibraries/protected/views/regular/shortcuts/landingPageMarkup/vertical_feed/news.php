<?
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\NewsBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\NewsItem;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\NewsShortcutItem;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\NewsUrlItem;

	/** @var $contentBlock NewsBlock */

	/** @var NewsItem[] $newsItems */
	$newsItems = $contentBlock->items;
?>
<style>
    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a
    {
        <?if(!empty($contentBlock->viewSettings->style->buttonBackColor)):?>
            background-color: <? echo '#'.$contentBlock->viewSettings->style->buttonBackColor;?> !important;
        <?endif;?>
        <?if(!empty($contentBlock->viewSettings->style->buttonBorderColor)):?>
            border-color: <? echo '#'.$contentBlock->viewSettings->style->buttonBorderColor;?> !important;
        <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .panel-footer .pagination a .glyphicon
    {
        <?if(!empty($contentBlock->viewSettings->style->buttonIconColor)):?>
            color: <? echo '#'.$contentBlock->viewSettings->style->buttonIconColor;?> !important;
        <?endif;?>
    }

    #vertical-feed-<? echo $contentBlock->id; ?> .news-item
    {
        <?if(!empty($contentBlock->viewSettings->style->dividerColor)):?>
            border-bottom-color: <? echo '#'.$contentBlock->viewSettings->style->dividerColor;?> !important;
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
         style="<? if (!empty($contentBlock->viewSettings->style->outsideBorderColor)): ?>border-color: <? echo '#' . $contentBlock->viewSettings->style->outsideBorderColor; ?>;<? endif; ?>">
        <div class="panel-heading" style="<? if ($contentBlock->viewSettings->hideHeader): ?>display: none;<? endif; ?><? if (!empty($contentBlock->viewSettings->style->headerColor)): ?>background-color: <? echo '#' . $contentBlock->viewSettings->style->headerColor; ?>;<? endif; ?>">
	        <? if (!empty($contentBlock->viewSettings->icon)): ?>
                <i class="icomoon icomoon-lg <? echo $contentBlock->viewSettings->icon; ?>" style="<? if (!empty($contentBlock->viewSettings->style->headerIconColor)): ?>color: <? echo '#' . $contentBlock->viewSettings->style->headerIconColor; ?>;<? endif; ?>"></i>
	        <? else: ?>
                <span class="glyphicon glyphicon-list-alt"></span>
	        <? endif; ?>
            <strong style="<? if (!empty($contentBlock->viewSettings->style->headerTextColor)): ?>color: <? echo '#' . $contentBlock->viewSettings->style->headerTextColor; ?>;<? endif; ?>"><? echo $contentBlock->viewSettings->title; ?></strong>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12">
                    <ul class="feed-items-list">
						<? foreach ($newsItems as $newsItem): ?>
                            <li class="news-item">
								<? if ($newsItem->type === 'url'): ?>
								    <? /** @var NewsUrlItem $newsItem */ ?>
                                    <a href="<? echo $newsItem->url; ?>" target="_blank" class="content">
								<? elseif ($newsItem->type === 'shortcut'): ?>
									<? /** @var NewsShortcutItem $newsItem */ ?>
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
	                                        <? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $newsItem->items), true); ?>
                                        </div>
                                    </a>
                            </li>
						<? endforeach; ?>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel-footer"
             style="<? if ($contentBlock->viewSettings->hideFooter): ?>display: none;<? endif; ?><? if (!empty($contentBlock->viewSettings->style->footerColor)): ?>background-color: <? echo '#' . $contentBlock->viewSettings->style->footerColor; ?>;<? endif; ?>"></div>
    </div>
</div>
