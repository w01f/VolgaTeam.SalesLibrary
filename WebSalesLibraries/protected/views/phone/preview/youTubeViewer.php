<?
	/**
	 * @var $data YouTubePreviewData
	 */
?>
	<div data-role='page' id="link-viewer" class="link-viewer-page" data-cache="never" data-dom-cache="false" data-ajax="false">
		<div data-role='header' class="page-header" data-position="fixed">
			<? if ($data->config->userAuthorized): ?>
				<a href="#link-viewer-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
			<? endif; ?>
			<h1 class="header-title"></h1>
			<? if ($data->config->userAuthorized): ?>
				<a href="#link-viewer-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
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
					<div class="ui-grid-a">
						<div class="ui-block-a">
							<a href="<? echo $data->url; ?>" target="_blank" data-role="button" data-inline="true" data-theme="a">Open</a>
						</div>
						<div class="ui-block-b">
							<? if ($data->config->allowAddToQuickSite || $data->config->allowAddToFavorites): ?>
								<a href="#link-viewer-options-menu" data-role="button" data-rel="popup" data-inline="true" data-theme="a">Options</a>
							<? endif; ?>
						</div>
					</div>
				</div>
				<iframe width="100%" src="https://www.youtube.com/embed/<? echo $data->youTubeId;?>" frameborder="0" allowfullscreen></iframe>
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
			<div data-role="panel" data-display="overlay" id="link-viewer-popup-panel-left">
				<ul data-role="listview">
					<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
					<li data-icon="false">
						<a class="logout-button" href="#">Log Out</a>
					</li>
					<li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
					<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
				</ul>
			</div>
			<div data-role="panel" data-display="overlay" data-position="right" id="link-viewer-popup-panel-right">
				<ul data-role="listview">
					<? echo $this->renderPartial('../wallbin/libraryList'); ?>
				</ul>
			</div>
			<? if ($data->config->allowAddToQuickSite || $data->config->allowAddToFavorites): ?>
				<div data-role="popup" id="link-viewer-options-menu" data-theme="a">
					<ul data-role="listview" data-inset="true" style="min-width:250px;" data-corners="false">
						<li data-role="list-divider" data-theme="d">File Options...</li>
						<? if ($data->config->allowAddToQuickSite): ?>
							<li><a href="#email-page" data-transition="slidefade" data-ajax="false">Email this Link</a></li>
						<? endif; ?>
						<? if ($data->config->allowAddToFavorites): ?>
							<li><a href="#favorites-add-page" data-transition="slidefade" data-ajax="false">Save to Favorites</a></li>
						<? endif; ?>
					</ul>
				</div>
			<? endif; ?>
		<? endif; ?>
	</div>
<? if ($data->config->allowAddToQuickSite): ?>
	<? echo $this->renderPartial('emailPage', array('previewData' => $data)); ?>
<? endif; ?>
<? if ($data->config->allowAddToFavorites): ?>
	<? echo $this->renderPartial('../favorites/addPage', array('previewData' => $data)); ?>
<? endif; ?>