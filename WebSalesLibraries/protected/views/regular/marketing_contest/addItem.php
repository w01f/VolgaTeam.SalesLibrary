<? /** @var $isCloning boolean */ ?>
<table class="tool-dialog logger-form" data-log-group="Marketing Contest" data-log-action="Marketing Contest Activity" style="width: 400px">
	<tr class="title-row">
		<td colspan="2">
			<legend><? echo $isCloning ? 'Clone Nomination!' : 'Add a NEW Nomination!' ?></legend>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<form class="form-horizontal">
				<div class="form-group">
					<label for="add-item-name" class="col-xs-3 control-label text-left">Save As:</label>
					<div class="col-xs-9">
						<input type="text" id="add-item-name" class="form-control log-action" value="">
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