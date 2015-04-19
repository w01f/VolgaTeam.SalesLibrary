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
        <td colspan="2" class ="buttons-area">
            <button class="btn btn-default" id="accept-button" type="button">OK</button>
        </td>
    </tr>                    
</table>