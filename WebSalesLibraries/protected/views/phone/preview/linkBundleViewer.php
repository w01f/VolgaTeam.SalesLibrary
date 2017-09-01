<?
	/**
	 * @var $data LinkBundlePreviewData
	 */
	$authorized = UserIdentity::isUserAuthorized();
?>
<div data-role='page' id="link-bundle-viewer" data-cache="never" data-dom-cache="false"
     data-ajax="false">
    <div data-role='header' class="page-header" data-position="fixed">
        <h1 class="header-title"></h1>
		<? if ($authorized): ?>
            <a href="#link-bundle-viewer-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round"
               data-iconpos="notext"></a>
		<? endif; ?>
    </div>
    <div data-role='content' class="main-content link-bundle-content">
        <table class="content-header">
            <tr>
                <td class="title gray"><? echo $data->linkTitle; ?></td>
                <td class="back">
                    <a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true"
                       data-transition="slidefade" data-direction="reverse">BACK</a>
                </td>
            </tr>
        </table>
		<? if ($data->config->allowPreview): ?>
			<?
			/** @var LinkBundlePreviewInfoItem[]|LinkBundlePreviewStrategyItem[] $infoItems */
			$infoItems = array();
			/** @var LinkBundlePreviewBaseItem[] $fileItems */
			$fileItems = array();
			/** @var LinkBundlePreviewLinkItem[] $videoItems */
			$videoItems = array();
			foreach ($data->bundleItems as $bundleItem)
			{
				if (!$bundleItem->visible) continue;
				if ($bundleItem instanceof LinkBundlePreviewLinkItem)
				{
					/** @var LinkBundlePreviewLinkItem $linkBundleItem */
					$linkBundleItem = $bundleItem;
					switch ($linkBundleItem->libraryLinkFormat)
					{
						case 'video':
						case 'youtube':
						case 'vimeo':
							$videoItems[] = $linkBundleItem;
							break;
						default:
							$fileItems[] = $linkBundleItem;
							break;
					}
				}
				else if ($bundleItem instanceof LinkBundlePreviewUrlItem)
				{
					$fileItems[] = $bundleItem;
				}
				else if ($bundleItem instanceof LinkBundlePreviewInfoItem)
				{
					$infoItems[] = $bundleItem;
				}
				else if ($bundleItem instanceof LinkBundlePreviewStrategyItem)
				{
					$infoItems[] = $bundleItem;
				}
			}

			$tabsCount = 0;
			if (count($infoItems) > 0)
				$tabsCount++;
			if (count($fileItems) > 0)
				$tabsCount++;
			if (count($videoItems) > 0)
				$tabsCount++;
			?>
            <div data-role="tabs" id="tabs">
                <div data-role="navbar">
					<? if ($tabsCount > 1): ?>
                        <ul>
							<? $setActive = true; ?>
							<? if (count($infoItems) > 0): ?>
                                <li><a href="#info"
                                       data-ajax="false"<? if ($setActive): ?> class="ui-btn-active"<? endif; ?>>Info</a>
                                </li>
								<? $setActive = false; ?>
							<? endif; ?>
							<? if (count($fileItems) > 0): ?>
                                <li><a href="#files"
                                       data-ajax="false"<? if ($setActive): ?> class="ui-btn-active"<? endif; ?>>Files</a>
                                </li>
								<? $setActive = false; ?>
							<? endif; ?>
							<? if (count($videoItems) > 0): ?>
                                <li><a href="#video"
                                       data-ajax="false"<? if ($setActive): ?> class="ui-btn-active"<? endif; ?>>Video</a>
                                </li>
								<? $setActive = false; ?>
							<? endif; ?>
                        </ul>
					<? endif; ?>
                </div>
				<? if (count($infoItems) > 0): ?>
                    <div id="info" class="link-bundle-section">
                        <h2><? echo $infoItems[0]->header; ?></h2>
                        <div style="color: <? echo $infoItems[0]->foreColor; ?>;
						<? if ($infoItems[0]->backColor !== '#FFFFFF'): ?>background-color: <? echo $infoItems[0]->backColor; ?>;<? endif; ?>
                                font-family: <? echo FontReplacementHelper::replaceFont($infoItems[0]->font->name); ?>;
                                font-size: <? echo $infoItems[0]->font->size; ?>pt;
                                font-weight: <? echo $infoItems[0]->font->isBold ? 'bold' : 'normal'; ?>;
                                font-style: <? echo $infoItems[0]->font->isItalic ? 'italic' : 'normal'; ?>;
                                text-decoration: <? echo $infoItems[0]->font->isUnderlined ? 'underline' : 'none'; ?>;">
							<? echo nl2br($infoItems[0]->body); ?>
                        </div>
                    </div>
				<? endif; ?>
				<? if (count($fileItems) > 0): ?>
                    <div id="files" class="link-bundle-section">
						<? foreach ($fileItems as $bundleItem): ?>
							<? if ($bundleItem instanceof LinkBundlePreviewLinkItem): ?>
								<? /** @var LinkBundlePreviewLinkItem $bundleItem */ ?>
                                <div class="link-bundle-item ">
                                    <a class="preview-link"
                                       href="#"><span><? echo nl2br($bundleItem->title); ?></span>
                                        <div class="service-data">
											<? echo $bundleItem->getItemData(); ?>
                                        </div>
                                    </a>
                                </div>
							<? endif; ?>
							<? if ($bundleItem instanceof LinkBundlePreviewUrlItem): ?>
								<? /** @var LinkBundlePreviewUrlItem $bundleItem */ ?>
                                <div class="link-bundle-item">
                                    <a href="<? echo $bundleItem->url; ?>"
                                       target="_blank"><span><? echo !empty($bundleItem->title) ? nl2br($bundleItem->title) : $bundleItem->url; ?></span></a>
                                </div>
							<? endif; ?>
						<? endforeach; ?>
                    </div>
				<? endif; ?>
				<? if (count($videoItems) > 0): ?>
                    <div id="video" class="link-bundle-section">
						<? foreach ($videoItems as $bundleItem): ?>
                            <div class="link-bundle-item ">
                                <a class="preview-link"
                                   href="#"><span><? echo nl2br($bundleItem->title); ?></span>
                                    <div class="service-data">
										<? echo $bundleItem->getItemData(); ?>
                                    </div>
                                </a>
                            </div>
						<? endforeach; ?>
                    </div>
				<? endif; ?>
            </div>
		<? else: ?>
            <p>Sorry...</p>
            <p>You are not authorized to view this link.</p>
		<? endif; ?>
    </div>
	<? if ($authorized): ?>
        <div data-role="panel" data-display="overlay" data-position="right" id="link-bundle-viewer-popup-panel-right">
            <ul data-role="listview">
				<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
                <li data-icon="false">
                    <a class="logout-button" href="#">Log Out</a>
                </li>
                <li data-role="list-divider"><p class="user-info">
                        User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
                <li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
            </ul>
        </div>
	<? endif; ?>
</div>