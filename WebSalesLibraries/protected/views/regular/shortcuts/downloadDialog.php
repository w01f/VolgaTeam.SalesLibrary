<?
	/** @var $link DownloadShortcut */
?>
<table class="tool-dialog">
	<tr>
		<td colspan="2">
			<h4>Download:</h4>
			<h2><? echo $link->fileName; ?></h2>
			<p><? echo $link->note; ?></p>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="buttons-area">
			<button class="btn btn-default" id="accept-button" type="button">Download</button>
		</td>
	</tr>
</table>
