<?
	/**
	 * @var $data FilePreviewData
	 */
	$authorized = UserIdentity::isUserAuthorized();
?>
<div data-role='page' id="link-viewer" class="link-viewer-page" data-cache="never" data-dom-cache="false" data-ajax="false">
    <div class="service-data">
        <div class="activity-data">
	        <? echo CJSON::encode(array('type' => 'Link Preview', 'subType' => 'Open Link Preview', 'data' => array('file' => $data->fileName))); ?>
        </div>
    </div>
    <div data-role='header' class="page-header" data-position="fixed">
		<h1 class="header-title"></h1>
		<? if ($authorized): ?>
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
				<ul data-role="listview">
					<? foreach ($data->dialogActions as $action): ?>
						<li class="action">
							<span class="action-text"><? echo $action->text; ?></span>
							<div class="service-data">
								<div class="tag"><? echo $action->tag; ?></div>
							</div>
						</li>
					<? endforeach; ?>
					<? if (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl)): ?>
                        <li class="action">
                            <a href="<? echo $data->oneDriveUrl; ?>" target="_blank" data-role="button" data-rel="external">OneDrive Link</a>
                        </li>
                        <li class="action">
                            <a href="mailto:?body=<? echo $data->oneDriveUrl; ?>" target="_blank" data-role="button" data-rel="external">Email OneDrive Link</a>
                        </li>
					<? endif; ?>
				</ul>
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
	<? if ($authorized): ?>
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
	<? endif; ?>
</div>
<? if ($data->config->allowAddToQuickSite): ?>
	<? echo $this->renderPartial('emailPage', array('previewData' => $data)); ?>
<? endif; ?>
<? if ($data->config->allowAddToFavorites): ?>
	<? echo $this->renderPartial('../favorites/addPage', array('previewData' => $data)); ?>
<? endif; ?>