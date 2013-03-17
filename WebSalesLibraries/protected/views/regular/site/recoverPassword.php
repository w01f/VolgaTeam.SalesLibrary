<table class ="tool-dialog">
    <tr>
        <td>
            <legend>Password Recovery</legend>
        </td>
    </tr>
    <tr>
        <td>
			<label class="control-label" for="login">Enter your username:</label>
            <input type="text" id="login" value="">
        </td>
    </tr>    
    <tr>
        <td>
			<label class="control-label" for="email">Enter your email address:</label>
            <input type="text" id="email" value="">
        </td>
    </tr>        
    <tr>
        <td class ="error-message">
        </td>
    </tr>            
    <tr>
        <td class ="buttons-area">
            <button class="btn" id="accept-button" type="button">Accept</button>
            <button class="btn" id="cancel-button" type="button">Cancel</button>
        </td>
    </tr>
	<tr>
		<td style="text-align: center;"><br><a href="mailto:<?php echo Yii::app()->params['email']['help_request_address']; ?>?subject=Site Help Request"><u><b>Click Here</b></u></a> if you are <b>NOT</b> receiving<br>your site login email or to contact support.</td>
	</tr>
</table>