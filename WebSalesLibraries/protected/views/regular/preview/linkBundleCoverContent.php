<?
	/**
	 * @var $itemData LinkBundlePreviewCoverItem
	 * @var $parentBundleData LinkBundlePreviewData
	 */

	$linkBundleInfo = $parentBundleData->getLinkBundleInfo();
	$imageUrlPrefix = Yii::app()->getBaseUrl(true);
?>
<a href="#">
    <img src="data:image/png;base64,<? echo $itemData->image; ?>">
    <div class="item-title"><? echo $itemData->title; ?></div>
    <div class="service-data">
		<? echo $itemData->getItemData(); ?>
        <div class="item-cover-content">
            <div class="link-viewer">
                <div class="row banner">
                    <div class="col-xs-12 text-center" style="padding: 30px">
                        <img src="data:image/png;base64,<? echo $itemData->logo; ?>" style="max-height: 365px">
                    </div>
                </div>
                <div class="row gallery-control-buttons">
                    <div class="col col-xs-8 text-left">
						<? if ($parentBundleData->config->enableRating): ?>
                            <div class="user-link-rate-container">
                                <img class="total-rate" src="" style="height:16px"/>
                                <input name="user-link-rate" class="user-link-rate rating">
                            </div>
						<? endif; ?>
                    </div>
					<?
						$footerGapSize = 0;
						if (!isset($parentBundleData->quickLinkUrl))
							$footerGapSize++;
						if (!$parentBundleData->config->allowDownload)
							$footerGapSize++;
						if (!$parentBundleData->config->allowAddToFavorites)
							$footerGapSize++;
						if (!$parentBundleData->config->allowAddToQuickSite)
							$footerGapSize++;
					?>
                    <div class="col col-xs-<? echo $footerGapSize; ?>"></div>
					<? if (isset($parentBundleData->quickLinkUrl)): ?>
                        <div class="col col-xs-1 text-center">
                            <div class="image-button log-action open-quick-link" data-log-action="Open Quick Link"
                                 title="<? echo $parentBundleData->quickLinkTitle; ?>">
							<span class="text-item">
								<img src="<? echo $parentBundleData->quickLinkLogo; ?>">
							</span>
                            </div>
                        </div>
					<? endif; ?>
					<? if ($parentBundleData->config->allowDownload): ?>
                        <div class="col col-xs-1 text-center">
                            <div class="image-button" title="download">
                                <div class="text-item dropup">
                                    <a href="#" data-toggle="dropdown" class="dropdown-toggle">
                                        <img src="<? echo sprintf('%s/images/preview/gallery/button-download.png', $imageUrlPrefix); ?>">
                                    </a>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <a href="#" class="log-action download-link-bundle"
                                               data-log-action="Download File">
												<? if (count($linkBundleInfo->downloadInfo) > 1): ?>
                                                    Download all <? echo count($linkBundleInfo->downloadInfo); ?> files (<? echo FileInfo::formatFileSize(FileDownloadInfo::getTotalSize($linkBundleInfo->downloadInfo)); ?>)
												<? else: ?>
                                                    Download this file (<? echo FileInfo::formatFileSize(FileDownloadInfo::getTotalSize($linkBundleInfo->downloadInfo)); ?>)
												<? endif; ?>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
					<? endif; ?>
					<? if ($parentBundleData->config->allowAddToFavorites): ?>
                        <div class="col col-xs-1 text-center">
                            <div class="image-button log-action add-favorites" data-log-action="Add to Favorites"
                                 title="save favorite">
							<span class="text-item">
								<img
                                        src="<? echo sprintf('%s/images/preview/gallery/button-favorites.png', $imageUrlPrefix); ?>">
							</span>
                            </div>
                        </div>
					<? endif; ?>
					<? if ($parentBundleData->config->allowAddToQuickSite): ?>
                        <div class="col col-xs-1 text-center">
                            <div class="image-button log-action add-quicksite" data-log-action="Add to QS"
                                 title="add to quickSITE">
							<span class="text-item">
								<img
                                        src="<? echo sprintf('%s/images/preview/gallery/button-quicksite.png', $imageUrlPrefix); ?>">
							</span>
                            </div>
                        </div>
					<? endif; ?>
                </div>
            </div>
        </div>
    </div>
</a>
