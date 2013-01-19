<table id ="tool-dialog">
    <tr>
        <td colspan="2">
            <legend>Add Favorite</legend>
        </td>
    </tr>
    <tr>
        <td class="title">
            <label class="control-label">Name:</label>
        </td>
        <td>
            <input type="text" id="favorite-name" value="<?php echo Yii::app()->user->email; ?>">
        </td>
    </tr>        
    <tr>
        <td class="title">
            <label class="control-label">Folder:</label>
        </td>
        <td>
            <input type="text" id="favorite-folder" value="<?php echo Yii::app()->params['email']['send_link']['subject']; ?>">
            <br>
        </td>
    </tr>            
    <tr>
        <td colspan="2" class ="buttons-area">
            <button class="btn" id="accept-button" type="button">Email</button>
            <button class="btn" id="cancel-button" type="button">Cancel</button>
        </td>
    </tr>                    
</table>