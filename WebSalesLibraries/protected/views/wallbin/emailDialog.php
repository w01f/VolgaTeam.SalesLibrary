<table id ="email-dialog">
    <tr>
        <td colspan="2">
            <legend>Email link</legend>
        </td>
    </tr>
    <tr>
        <td class="title">
            <label class="control-label" for="email-to">To:</label>
        </td>
        <td>
            <input type="text" id="email-to" value="">
        </td>
    </tr>    
    <tr>
        <td class="title">
            <label class="control-label" for="email-from">From:</label>
        </td>
        <td>
            <input type="text" id="email-from" value="<?php echo Yii::app()->user->email; ?>">
        </td>
    </tr>        
    <tr>
        <td class="title">
            <label class="control-label" for="email-subject">Subject:</label>
        </td>
        <td>
            <input type="text" id="email-subject" value="<?php echo Yii::app()->params['email']['send_link']['subject']; ?>">
        </td>
    </tr>            
    <tr>
        <td class="email-body-title">
            <label class="control-label" for="email-body">Body:</label>
        </td>
        <td>
            <textarea id="email-body" rows="3"><?php echo Yii::app()->params['email']['send_link']['body']; ?></textarea>
        </td>
    </tr>                
    <tr>
        <td colspan="2" class ="buttons-area">
            <button class="btn" id="email-accept" type="button">Email</button>
            <button class="btn" id="email-cancel" type="button">Cancel</button>
        </td>
    </tr>                    
</table>