<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\NewsBlock */

	/** @var \application\models\shortcuts\models\landing_page\regular_markup\NewsItem[] $newsItems */
	$newsItems = $contentBlock->items;
?>
<style>
    #news-block-<? echo $contentBlock->id; ?> .panel-footer .pagination a
    {
        <?if(!empty($contentBlock->settings->style->buttonBackColor)):?>
            background-color: <? echo '#'.$contentBlock->settings->style->buttonBackColor;?> !important;
        <?endif;?>
        <?if(!empty($contentBlock->settings->style->buttonBorderColor)):?>
            border-color: <? echo '#'.$contentBlock->settings->style->buttonBorderColor;?> !important;
        <?endif;?>
    }

    #news-block-<? echo $contentBlock->id; ?> .panel-footer .pagination a .glyphicon
    {
        <?if(!empty($contentBlock->settings->style->buttonIconColor)):?>
            color: <? echo '#'.$contentBlock->settings->style->buttonIconColor;?> !important;
        <?endif;?>
    }

    #news-block-<? echo $contentBlock->id; ?> .news-item
    {
        <?if(!empty($contentBlock->settings->style->dividerColor)):?>
            border-bottom-color: <? echo '#'.$contentBlock->settings->style->dividerColor;?> !important;
        <?endif;?>
    }
</style>
<div id="news-block-<? echo $contentBlock->id; ?>" class="news-block">
    <div class="service-data">
        <div class="encoded-object">
			<? echo CJSON::encode($contentBlock->settings); ?>
        </div>
    </div>
    <div class="panel panel-default"
         style="<? if (!empty($contentBlock->settings->style->outsideBorderColor)): ?>border-color: <? echo '#' . $contentBlock->settings->style->outsideBorderColor; ?>;<? endif; ?>">
        <div class="panel-heading" style="<? if ($contentBlock->settings->hideHeader): ?>display: none;<? endif; ?><? if (!empty($contentBlock->settings->style->headerColor)): ?>background-color: <? echo '#' . $contentBlock->settings->style->headerColor; ?>;<? endif; ?>">
	        <? if (!empty($contentBlock->settings->icon)): ?>
                <i class="icomoon icomoon-lg <? echo $contentBlock->settings->icon; ?>" style="<? if (!empty($contentBlock->settings->style->headerIconColor)): ?>color: <? echo '#' . $contentBlock->settings->style->headerIconColor; ?>;<? endif; ?>"></i>
	        <? else: ?>
                <span class="glyphicon glyphicon-list-alt"></span>
	        <? endif; ?>
            <strong style="<? if (!empty($contentBlock->settings->style->headerTextColor)): ?>color: <? echo '#' . $contentBlock->settings->style->headerTextColor; ?>;<? endif; ?>"><? echo $contentBlock->settings->title; ?></strong>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12">
                    <ul class="news-block-list">
						<? foreach ($newsItems as $newsItem): ?>
                            <li class="news-item">
								<? if ($newsItem->type === 'url'): ?>
								    <? /** @var \application\models\shortcuts\models\landing_page\regular_markup\NewsUrlItem $newsItem */ ?>
                                    <a href="<? echo $newsItem->url; ?>" target="_blank" class="content">
								<? elseif ($newsItem->type === 'shortcut'): ?>
									<? /** @var \application\models\shortcuts\models\landing_page\regular_markup\NewsShortcutItem $newsItem */ ?>
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
	                                        <? echo $this->renderPartial('landingPageMarkup/blockContainer', array('contentBlocks' => $newsItem->items), true); ?>
                                        </div>
                                    </a>
                            </li>
						<? endforeach; ?>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel-footer"
             style="<? if ($contentBlock->settings->hideFooter): ?>display: none;<? endif; ?><? if (!empty($contentBlock->settings->style->footerColor)): ?>background-color: <? echo '#' . $contentBlock->settings->style->footerColor; ?>;<? endif; ?>"></div>
    </div>
</div>
