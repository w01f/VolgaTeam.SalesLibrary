<div>
	<table class="main-view tool-dialog">
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
							<input type="text" id="favorites-link-name" class="form-control" value="<?php echo $link->name; ?>">
						</div>
					</div>
					<div class="form-group">
						<label for="favorites-folder-name" class="col-xs-2 control-label">Folder:</label>
						<div class="col-xs-10">
							<div class="input-group">
								<input type="text" id="favorites-folder-name" class="form-control" value="<?php echo $link->name; ?>">
								<div class="input-group-btn">
									<button id="clear-folder" class="btn btn-default" type="button"><span class="glyphicon glyphicon-remove"></span></button>
									<button id="show-folder-selector" class="btn btn-default <? if (!(isset($folders) && count($folders) > 0)): ?>disabled<? endif; ?>" type="button"><span class="glyphicon glyphicon-folder-open"></span></button>
								</div>
							</div>
						</div>
					</div>
				</form>
			</td>
		</tr>
		<tr class="buttons-row">
			<td colspan="2" class="buttons-area">
				<button class="btn btn-default accept-button" type="button">OK</button>
				<button class="btn btn-default cancel-button" type="button">Cancel</button>
			</td>
		</tr>
	</table>
	<table class="folder-selector tool-dialog" style="display: none;">
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
									<a href="#"><? echo $folder; ?></a>
								</li>
							<? endforeach; ?>
						<? endif; ?>
					</ul>
				</div>
			</td>
		</tr>
		<tr class="buttons-row">
			<td colspan="2" class="buttons-area">
				<button class="btn btn-default accept-button" type="button">Select</button>
				<button class="btn btn-default cancel-button" type="button">Back</button>
			</td>
		</tr>
	</table>
</div>