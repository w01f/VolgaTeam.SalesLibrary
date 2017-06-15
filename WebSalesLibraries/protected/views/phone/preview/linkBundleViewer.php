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
            <div class="link-bundle-section">
                <h4>Files:</h4>
				<? foreach ($data->bundleItems as $bundleItem): ?>
					<? if ($bundleItem->visible): ?>
						<? if ($bundleItem instanceof LinkBundlePreviewLinkItem): ?>
							<? /** @var LinkBundlePreviewLinkItem $bundleItem */ ?>
							<? if ($bundleItem->libraryLinkFormat != 'video'): ?>
                                <div class="link-bundle-item ">
                                    <a class="preview-link"
                                       href="#"><span><? echo nl2br($bundleItem->title); ?></span>
                                        <div class="service-data">
											<? echo $bundleItem->getItemData(); ?>
                                        </div>
                                    </a>
                                </div>
							<? endif; ?>
						<? endif; ?>
						<? if ($bundleItem instanceof LinkBundlePreviewUrlItem): ?>
							<? /** @var LinkBundlePreviewUrlItem $bundleItem */ ?>
                            <div class="link-bundle-item">
                                <a href="<? echo $bundleItem->url; ?>"
                                   target="_blank"><span><? echo !empty($bundleItem->title) ? nl2br($bundleItem->title) : $bundleItem->url; ?></span></a>
                            </div>
						<? endif; ?>
					<? endif; ?>
				<? endforeach; ?>
            </div>
            <div class="link-bundle-section">
                <h4>Video:</h4>
				<? foreach ($data->bundleItems as $bundleItem): ?>
					<? if ($bundleItem->visible): ?>
						<? if ($bundleItem instanceof LinkBundlePreviewLinkItem): ?>
							<? /** @var LinkBundlePreviewLinkItem $bundleItem */ ?>
							<? if ($bundleItem->libraryLinkFormat == 'video'): ?>
                                <div class="link-bundle-item ">
                                    <a class="preview-link" href="#"><span><? echo nl2br($bundleItem->title); ?></span>
                                        <div class="service-data">
											<? echo $bundleItem->getItemData(); ?>
                                        </div>
                                    </a>
                                </div>
							<? endif; ?>
						<? endif; ?>
					<? endif; ?>
				<? endforeach; ?>
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