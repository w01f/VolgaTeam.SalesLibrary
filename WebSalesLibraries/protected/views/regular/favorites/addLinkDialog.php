<?
	/**
	 * @var $link LinkRecord
	 */

	$selectedFolderTagName = 'favorites-selected-folder-name';
	$selectedFolderName = isset(Yii::app()->request->cookies[$selectedFolderTagName]) ?
		Yii::app()->request->cookies[$selectedFolderTagName]->value :
		null;
?>
<div>
	<table class="main-view tool-dialog logger-form" data-log-group="Link" data-log-action="Favorites Activity">
		<tr class="title-row">
			<td colspan="2">
				<legend>Add to Favorites</legend>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<form class="form-horizontal">
					<div class="form-group">
						<label for="favorites-link-name" class="col-xs-2 control-label">Name:</label>
						<div class="col-xs-10">
							<input type="text" id="favorites-link-name" class="form-control log-action"
							       value="<? echo $link->name; ?>">
						</div>
					</div>
					<div class="form-group">
						<label for="favorites-folder-name" class="col-xs-2 control-label">Folder:</label>
						<div class="col-xs-10">
							<div class="input-group">
								<input type="text" id="favorites-folder-name" class="form-control log-action"
								       value="<? echo $selectedFolderName; ?>">
								<div class="input-group-btn">
									<button id="clear-folder" class="btn btn-default log-action" type="button">
										<span class="glyphicon glyphicon-remove"></span></button>
									<button id="show-folder-selector"
									        class="btn btn-default log-action <? if (!(isset($folders) && count($folders) > 0)): ?>disabled<? endif; ?>"
									        type="button">
										<span class="glyphicon glyphicon-folder-open"></span></button>
								</div>
							</div>
						</div>
					</div>
				</form>
			</td>
		</tr>
		<tr class="buttons-row">
			<td colspan="2" class="buttons-area">
				<button class="btn btn-default accept-button log-action" type="button"
				        data-log-action="Add to Favorites">OK
				</button>
				<button class="btn btn-default cancel-button log-action" type="button">Cancel</button>
			</td>
		</tr>
	</table>
	<table class="folder-selector tool-dialog logger-form" data-log-group="Link" data-log-action="Favorites Activity"
	       style="display: none;">
		<tr class="title-row">
			<td colspan="2">
				<legend>Select Folder from the list</legend>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<div class="list-container">
					<ul class="nav nav-pills">
						<? if (isset($folders)): ?>
							<? foreach ($folders as $folder): ?>
								<li>
									<a href="#" class="log-action"><? echo $folder; ?></a>
								</li>
							<? endforeach; ?>
						<? endif; ?>
					</ul>
				</div>
			</td>
		</tr>
		<tr class="buttons-row">
			<td colspan="2" class="buttons-area">
				<button class="btn btn-default accept-button log-action" type="button">Select</button>
				<button class="btn btn-default cancel-button log-action" type="button">Back</button>
			</td>
		</tr>
	</table>
</div>