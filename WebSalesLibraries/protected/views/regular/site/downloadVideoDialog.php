<table class="tool-dialog">
	<tr>
		<td collspan="2">
			<h4>Video Files are usually VERY BIG!</h4>

			<div>Are you SURE you want to Download this file?</div>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<br>

			<div>Select the format you want to download to your PC</div>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="buttons-area download-type">
			<button class="btn btn-default active" type="button"><img
					src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/wmv-download.png'; ?>" alt="wmv"/>
			</button>
			<button class="btn btn-default" type="button"><img
					src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/mp4-download.png'; ?>" alt="mp4"/>
			</button>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<br>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="buttons-area">
			<button class="btn btn-default" id="accept-button" type="button">Download</button>
			<button class="btn btn-default" id="cancel-button" type="button">Cancel</button>
		</td>
	</tr>
</table>
