<? /** @var $clone boolean */ ?>
<table class="tool-dialog logger-form" data-log-group="QBuilder" data-log-action="QBuilder Activity">
	<tr class="title-row">
		<td colspan="2">
			<legend><? echo $clone ? 'Clone quickSITE' : 'Add quickSITE' ?></legend>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<form class="form-horizontal">
				<div class="form-group">
					<label for="add-page-name" class="col-xs-2 control-label">Name:</label>
					<div class="col-xs-10">
						<input type="text" id="add-page-name" class="form-control log-action" value="">
					</div>
				</div>
			</form>
		</td>
	</tr>
	<tr class="buttons-row">
		<td colspan="2" class="buttons-area">
			<br>
			<button class="btn btn-default log-action accept-button" type="button">OK</button>
			<button class="btn btn-default log-action cancel-button" type="button">Cancel</button>
		</td>
	</tr>
</table>