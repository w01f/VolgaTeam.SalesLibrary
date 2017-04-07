<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\NewsBlock */

	$newsItems = $contentBlock->items;
?>
<div id="news-block-<? echo $contentBlock->id; ?>" class="news-block">
    <div class="service-data">
        <div class="encoded-object">
			<? echo CJSON::encode($contentBlock->settings); ?>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading"><span
                    class="glyphicon glyphicon-list-alt"></span><b><? echo $contentBlock->settings->title; ?></b></div>
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
        <div class="panel-footer"></div>
    </div>
</div>
