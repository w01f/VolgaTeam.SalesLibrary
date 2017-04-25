<?
	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\feeds\common\FeedItemSettings;
	use application\models\feeds\vertical\LinkFeedSettings;
	use application\models\feeds\vertical\LinkFeedStyle;

	/**
	 * @var string $feedId
	 * @var LinkFeedSettings $viewSettings
	 * @var LinkFeedItem[] $feedItems
	 */

	/** @var LinkFeedStyle $style */
	$style = $viewSettings->style;
?>
<ul class="feed-items-list">
	<? $linkNumber = 1; ?>
	<? foreach ($feedItems as $feedItem): ?>
        <li class="news-item">
            <a href="#" class="content library-link-block">
				<? if ($style->showLinkCounter): ?>
                    <div class="link-number"><? echo $linkNumber; ?>.</div>
				<? endif; ?>
                <div class="link-info">
					<?
						$itemSettingsName = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagName};
						$itemSettingsLibrary = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagLibrary};
						$itemSettingsViewsCount = $viewSettings->dataItemSettings->{FeedItemSettings::DataItemTagViewsCount};
					?>
					<? if ($itemSettingsName->enabled && $style->linkNamePosition === LinkFeedStyle::LinkNamePositionTop): ?>
                        <div class="text">
                                            <span class="feed-info link-name" style="
                                            <? if (!empty($itemSettingsName->color)): ?>
                                                    color: <? echo '#' . $itemSettingsName->color;?>;
                                    <? endif; ?>
                                            <? if (!empty($itemSettingsName->font)): ?>
                                                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsName->font->name); ?> !important;
                                                    font-size: <? echo $itemSettingsName->font->size; ?>pt !important;
                                                    font-weight: <? echo $itemSettingsName->font->isBold ? 'bold' : 'normal'; ?> !important;
                                                    font-style: <? echo $itemSettingsName->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                                    text-decoration: <? echo $itemSettingsName->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
                                            <? endif; ?>
                                                    ">
                                                <? echo $feedItem->linkName; ?>
                                            </span>
                        </div>
					<? endif; ?>
					<? if (!empty($feedItem->thumbnail)): ?>
                        <div class="image library-link-thumbnail">
                            <img src="<? echo $feedItem->thumbnail; ?>"/>
                        </div>
					<? endif; ?>
                    <div class="text">
						<? if ($itemSettingsName->enabled && $style->linkNamePosition === LinkFeedStyle::LinkNamePositionBottom): ?>
                            <span class="feed-info link-name" style="
							<? if (!empty($itemSettingsName->color)): ?>
                                    color: <? echo '#' . $itemSettingsName->color;?>;
                                    <? endif; ?>
							<? if (!empty($itemSettingsName->font)): ?>
                                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsName->font->name); ?> !important;
                                    font-size: <? echo $itemSettingsName->font->size; ?>pt !important;
                                    font-weight: <? echo $itemSettingsName->font->isBold ? 'bold' : 'normal'; ?> !important;
                                    font-style: <? echo $itemSettingsName->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                    text-decoration: <? echo $itemSettingsName->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
							<? endif; ?>
                                    ">
                                                <? echo $feedItem->linkName; ?>
                                            </span>
						<? endif; ?>
						<? if ($itemSettingsLibrary->enabled): ?>
                            <span class="feed-info library-name" style="
							<? if (!empty($itemSettingsLibrary->color)): ?>
                                    color: <? echo '#' . $itemSettingsLibrary->color;?>;
                                        <? endif; ?>
							<? if (!empty($itemSettingsLibrary->font)): ?>
                                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsLibrary->font->name); ?> !important;
                                    font-size: <? echo $itemSettingsLibrary->font->size; ?>pt !important;
                                    font-weight: <? echo $itemSettingsLibrary->font->isBold ? 'bold' : 'normal'; ?> !important;
                                    font-style: <? echo $itemSettingsLibrary->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                    text-decoration: <? echo $itemSettingsLibrary->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
							<? endif; ?>
                                    ">
                                                <? echo $feedItem->libraryName; ?>
                                            </span>
						<? endif; ?>
						<? if ($itemSettingsViewsCount->enabled && $feedItem->viewsCount > 0): ?>
                            <span class="feed-info views-count" style="
							<? if (!empty($itemSettingsViewsCount->color)): ?>
                                    color: <? echo '#' . $itemSettingsViewsCount->color;?>;
                                        <? endif; ?>
							<? if (!empty($itemSettingsViewsCount->font)): ?>
                                    font-family: <? echo FontReplacementHelper::replaceFont($itemSettingsViewsCount->font->name); ?> !important;
                                    font-size: <? echo $itemSettingsViewsCount->font->size; ?>pt !important;
                                    font-weight: <? echo $itemSettingsViewsCount->font->isBold ? 'bold' : 'normal'; ?> !important;
                                    font-style: <? echo $itemSettingsViewsCount->font->isItalic ? 'italic' : 'normal'; ?> !important;
                                    text-decoration: <? echo $itemSettingsViewsCount->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
							<? endif; ?>
                                    ">
                                                <? echo $feedItem->viewsCount; ?> views
                                            </span>
						<? endif; ?>
                    </div>
                </div>
                <div class="service-data">
                    <div class="link-id"><? echo $feedItem->linkId; ?></div>
                </div>
            </a>
        </li>
		<? $linkNumber++; ?>
	<? endforeach; ?>
</ul>
