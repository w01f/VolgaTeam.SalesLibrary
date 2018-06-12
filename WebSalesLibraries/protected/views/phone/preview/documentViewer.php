<?
	/**
	 * @var $data DocumentPreviewData
	 */
?>
<div data-role='page' id="link-viewer" class="link-viewer-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed">
		<h1 class="header-title"></h1>
		<? if ($data->config->userAuthorized): ?>
			<a href="#link-viewer-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title gray"><? echo $data->fileName; ?></td>
				<td class="back">
					<a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
				</td>
			</tr>
		</table>
		<? if ($data->config->allowPreview): ?>
			<div class="actions">
				<div class="ui-grid-b">
					<div class="ui-block-a">
						<a href="#link-viewer-open-menu" data-role="button" data-rel="popup" data-inline="true" data-theme="a">Open</a>
					</div>
					<? if ($data->config->allowAddToQuickSite || $data->config->allowAddToFavorites || (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl))): ?>
						<div class="ui-block-b">
							<a id="link-viwer-open-full-screen" href="#" data-role="button" data-inline="true" data-theme="a" data-transition="slidefade">Full Screen</a>
						</div>
						<div class="ui-block-c">
							<a href="#link-viewer-options-menu" data-role="button" data-rel="popup" data-inline="true" data-theme="a">Options</a>
						</div>
					<? else: ?>
						<div class="ui-block-b"></div>
						<div class="ui-block-c">
							<a id="link-viwer-open-full-screen" href="#" data-role="button" data-inline="true" data-theme="a" data-transition="slidefade">Full Screen</a>
						</div>
					<? endif; ?>
				</div>
			</div>
			<div class="slider images">
				<? foreach ($data->pagesInPng as $pngPage): ?>
					<div><img src="<? echo $pngPage->href; ?>"></div>
				<? endforeach; ?>
			</div>
		<? else: ?>
			<p>Sorry...</p>
			<p>You are not authorized to view this link.</p>
		<? endif; ?>
	</div>
	<? if ($data->config->allowPreview): ?>
		<div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
			<div class="ui-grid-a">
				<div class="ui-block-a">
				</div>
				<div class="ui-block-b link-viewer-info">
					<span class="ui-mini"><strong><? echo $data->linkTitle; ?></strong></span>
				</div>
			</div>
		</div>
	<? endif; ?>
	<? if ($data->config->userAuthorized): ?>
		<div data-role="panel" data-display="overlay" data-position="right" id="link-viewer-popup-panel-right">
            <ul data-role="listview">
				<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
                <li data-icon="false">
                    <a class="logout-button" href="#">Log Out</a>
                </li>
                <li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
                <li data-role="list-divider"><p>Copyright 2018 adSALESapps.com</p></li>
            </ul>
		</div>
		<? if ($data->config->allowAddToQuickSite || $data->config->allowAddToFavorites || (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl))): ?>
			<div data-role="popup" id="link-viewer-options-menu" data-theme="a">
				<ul data-role="listview" data-inset="true" style="min-width:250px;" data-corners="false">
					<li data-role="list-divider" data-theme="d">File Options...</li>
					<? if ($data->config->allowAddToQuickSite): ?>
						<li><a href="#email-page" data-transition="slidefade" data-ajax="false">Email this Link</a></li>
					<? endif; ?>
					<? if (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl)): ?>
                        <li><a href="mailto:?body=<? echo $data->oneDriveUrl; ?>" data-rel="external">Email OneDrive Link</a></li>
					<? endif; ?>
					<? if ($data->config->allowAddToFavorites): ?>
						<li><a href="#favorites-add-page" data-transition="slidefade" data-ajax="false">Save to Favorites</a></li>
					<? endif; ?>
				</ul>
			</div>
		<? endif; ?>
	<? endif; ?>
	<div data-role="popup" id="link-viewer-open-menu" data-theme="a">
		<ul data-role="listview" data-inset="true" style="min-width:250px;" data-corners="false">
			<li data-role="list-divider" data-theme="d">Open this file...</li>
			<? if (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl)): ?>
                <li>
                    <a class="popup-open-action" href="<? echo $data->oneDriveUrl; ?>" target="_blank" data-rel="external">OneDrive Link</a>
                </li>
			<? endif; ?>
            <li>
				<a href="#link-viewer-gallery" data-transition="slidefade">Thumb Gallery</a>
			</li>
			<? if ($data->config->allowDownload): ?>
				<li>
					<a class="popup-open-action" href="<? echo $data->url; ?>" target="_blank" data-rel="external"><? echo $data->linkTitle; ?></a>
				</li>
			<? endif; ?>
			<? if ($data->config->allowPdf): ?>
				<li>
					<a class="popup-open-action" href="<? echo $data->documentInPdf->link; ?>" target="_blank" data-rel="external">Adobe PDF</a>
				</li>
			<? endif; ?>
		</ul>
	</div>
</div>
<div data-role='page' id="link-viewer-gallery" class="link-viewer-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed">
		<h1 class="header-title"></h1>
		<? if ($data->config->userAuthorized): ?>
			<a href="#link-viewer-gallery-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header">
			<tr>
				<td class="title gray"><? echo $data->fileName; ?></td>
				<td class="back">
					<a href="#link-viewer" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
				</td>
			</tr>
		</table>
		<ul class="gallery-items">
			<? for ($i = 0; $i < count($data->pagesInPng); $i++): ?>
				<li>
					<a href="<? echo $data->pagesInPng[$i]->href; ?>" rel="external"><img src="<? echo $data->thumbnails[$i]->link; ?>" alt="<? echo $data->pagesInPng[$i]->title; ?>"></a>
				</li>
			<? endfor; ?>
		</ul>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
		<div class="ui-grid-a">
			<div class="ui-block-a">
			</div>
			<div class="ui-block-b link-viewer-info">
				<span class="ui-mini"><strong><? echo count($data->pagesInPng) ?> Slides</strong></span>
			</div>
		</div>
	</div>
	<? if ($data->config->userAuthorized): ?>
		<div data-role="panel" data-display="overlay" data-position="right" id="link-viewer-gallery-popup-panel-right">
            <ul data-role="listview">
				<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
                <li data-icon="false">
                    <a class="logout-button" href="#">Log Out</a>
                </li>
                <li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
                <li data-role="list-divider"><p>Copyright 2018 adSALESapps.com</p></li>
            </ul>
		</div>
	<? endif; ?>
</div>
<? if ($data->config->allowAddToQuickSite): ?>
	<? echo $this->renderPartial('emailPage', array('previewData' => $data)); ?>
<? endif; ?>
<? if ($data->config->allowAddToFavorites): ?>
	<? echo $this->renderPartial('../favorites/addPage', array('previewData' => $data)); ?>
<? endif; ?>
