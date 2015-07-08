<?
	/**
	 * @var $previewData FilePreviewData|UrlPreviewData
	 */

	$authorized = false;
	$userId = Yii::app()->user->getId();
	$userFolders = array();
	if (isset($userId))
	{
		$authorized = true;
		$userFolders = FavoritesFolderRecord::getAllFolderNames($userId);
	}
?>
<? if ($authorized && $previewData->allowAddToFavorites): ?>
	<div id="favorites-add-page" data-role="page" data-cache="never" data-dom-cache="false" data-ajax="false">
		<div class="page-header" data-role="header" data-position="fixed" data-theme="a">
			<h3>Add to Favorites</h3>
		</div>
		<div data-role='main' class="main-content">
			<legend>Link Name:</legend>
			<ul class="edit-fields" data-role="listview">
				<li class="edit-field" data-icon="false">
					<label for="favorites-add-page-link-name" class="ui-hidden-accessible"></label>
					<textarea id="favorites-add-page-link-name" rows="3"><? echo isset($previewData->fileName) ? $previewData->fileName : $previewData->name; ?></textarea>
				</li>
			</ul>
			<legend>Folder Name:</legend>
			<ul class="edit-fields" data-role="listview">
				<li class="edit-field" data-icon="false">
					<label for="favorites-add-page-folder-name" class="ui-hidden-accessible"></label>
					<textarea id="favorites-add-page-folder-name" rows="3"></textarea>
				</li>
			</ul>
			<? if (count($userFolders) > 0): ?>
				<a id="favorites-add-page-select-folder" href="#favorites-add-select-folder-popup" data-role="button" data-rel="popup" data-inline="true" data-transition="pop">Select Existed Folder</a>
			<? endif; ?>
		</div>
		<div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
			<div class="ui-grid-a buttons">
				<div class="ui-block-a">
					<a class="button accept" href="#" data-role="button" data-inline="true" data-mini="true" data-theme="d">Save</a>
				</div>
				<div class="ui-block-b">
					<a class="button cancel" href="#link-viewer" data-role="button" data-inline="true" data-transition="slidefade" data-direction="reverse" data-mini="true" data-theme="b">Cancel</a>
				</div>
			</div>
		</div>
		<? if (count($userFolders) > 0): ?>
			<div id="favorites-add-select-folder-popup" data-role="popup" data-theme="a">
				<ul data-role="listview">
					<? foreach ($userFolders as $folder): ?>
						<li class="favorites-folder-item" data-icon="false"><a href="#"><? echo $folder; ?></a></li>
					<? endforeach; ?>
				</ul>
			</div>
		<? endif; ?>
		<div class="service-data">
			<div class="link-id"><? echo $previewData->linkId; ?></div>
		</div>
	</div>
<? endif; ?>