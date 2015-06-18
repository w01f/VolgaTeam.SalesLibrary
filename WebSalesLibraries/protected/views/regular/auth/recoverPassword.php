<table class="tool-dialog">
	<tr>
		<td>
			<legend>Site Help</legend>
		</td>
	</tr>
	<tr>
		<td>
			<p>Reset your account password:</p>
		</td>
	</tr>
	<tr>
		<td>
			<div class="form-group">
				<label class="control-label" for="login">Enter your username:</label>
				<input type="text" class="form-control" id="login" value="">
				<p class="text-danger">
					<small>*User Name is <strong style="text-decoration: underline;">NOT</strong> an email address</small>
				</p>
			</div>
		</td>
	</tr>
	<tr>
		<td>
			<div class="form-group">
				<label class="control-label" for="email">Enter your email address:</label>
				<input type="email" class="form-control" id="email" value="">
			</div>
		</td>
	</tr>
	<tr>
		<td class="error-message"></td>
	</tr>
	<tr>
		<td class="buttons-area">
			<button class="btn btn-default" id="accept-button" type="button">Accept</button>
			<button class="btn btn-default" id="cancel-button" type="button">Cancel</button>
		</td>
	</tr>
	<tr>
		<td style="text-align: center;">
			<a href="mailto:<?php echo Yii::app()->params['email']['help_request_address']; ?>?subject=Site Help Request - <? echo Yii::app()->request->serverName; ?>" style="text-decoration: underline;"><strong>Click Here</b></strong></a> if you are
			<strong>NOT</strong> receiving<br>your site login email or to contact support.
		</td>
	</tr>
</table>