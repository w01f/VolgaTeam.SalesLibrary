<div>
	<table class="main-view tool-dialog">
		<tr class="title-row">
			<td colspan="2">
				<legend>Add to Favorites</legend>
			</td>
		</tr>
		<tr>
			<td class="title">
				<label class="control-label">Name:</label>
			</td>
			<td>
				<input type="text" id="favorites-link-name" value="<?php echo $link->name; ?>">
			</td>
		</tr>
		<tr>
			<td class="title">
				<label class="control-label">Folder:</label>
			</td>
			<td>
			<span class="button-edit input-append">
				<input type="text" id="favorites-folder-name" placeholder="Select or Type..." style="width: 220px;">
				<a class="btn" id="clear-folder" href="#"><i class="icon-remove-sign"/></a>
				<a class="btn <?if (!(isset($folders) && count($folders) > 0)): ?>disabled<? endif;?>"
				   id="show-folder-selector" href="#"><i class="icon-folder-open"/></a>
			</span>
			</td>
		</tr>
		<tr class="buttons-row">
			<td colspan="2" class="buttons-area">
				<button class="btn accept-button" type="button">OK</button>
				<button class="btn cancel-button" type="button">Cancel</button>
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
						<?if (isset($folders)): ?>
						<? foreach ($folders as $folder): ?>
							<li>
								<a href="#"><?echo $folder;?></a>
							</li>
							<? endforeach; ?>
						<? endif;?>
					</ul>
				</div>
			</td>
		</tr>
		<tr class="buttons-row">
			<td colspan="2" class="buttons-area">
				<button class="btn accept-button" type="button">Select</button>
				<button class="btn cancel-button" type="button">Back</button>
			</td>
		</tr>
	</table>
</div>