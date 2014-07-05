<?
	/**
	 * @var $header string
	 * @var $content string
	 */
?>
<table class ="tool-dialog">
    <tr>
        <td colspan="2">
			<legend><? echo $header; ?></legend>
            <div><? echo $content; ?></div>
        </td>
    </tr>
    <tr>
		<td colspan="2" class="buttons-area">
			<button class="btn btn-default accept-button" type="button">OK</button>
			<button class="btn btn-default favorites-button" type="button">Favorites</button>
		</td>
    </tr>                    
</table>