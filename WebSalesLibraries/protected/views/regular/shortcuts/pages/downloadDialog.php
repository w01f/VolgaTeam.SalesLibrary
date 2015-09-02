<?
	/** @var $shortcut DownloadShortcut */
?>
<table class="tool-dialog">
	<tr>
		<td colspan="2">
			<h4>Download:</h4>
			<h2><? echo $shortcut->fileName; ?></h2>
			<p><? echo $shortcut->description; ?></p>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="buttons-area">
			<button class="btn btn-default" id="accept-button" type="button">Download</button>
		</td>
	</tr>
</table>
