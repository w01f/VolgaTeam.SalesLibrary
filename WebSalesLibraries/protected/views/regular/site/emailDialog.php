<table id ="email-dialog">
    <tr>
        <td colspan="2">
    <legend>Email link</legend>
</td>
</tr>
<tr>
    <td class="email-body-title">
        <div id ="email-to-select" class="dropdown">
            <button class="btn btn-block control-label dropdown-toggle <?php if (!isset($availableEmails)): ?>disabled<?php endif; ?>" type="button" data-toggle="dropdown">To:</button>
            <div class="dropdown-menu" role="menu">
                <ul>
                    <?php if (isset($availableEmails)): ?>
                        <?php foreach ($availableEmails as $email): ?>
                            <li>
                                <label class="checkbox">
                                    <input type="checkbox" value="<?php echo $email; ?>">
                                    <?php echo $email; ?>
                                </label>                        
                            </li>
                        <?php endforeach; ?>            
                    <?php endif; ?>
                </ul>
                <button class="btn btn-small btn-block apply-selection" type="button">Apply</button>
            </div>
        </div>
    </td>
    <td>
        <textarea id="email-to" rows="1"></textarea>
    </td>
</tr>    
<tr>
    <td class="email-body-title">
        <div id ="email-to-copy-select" class="dropdown">
            <button class="btn btn-block control-label  dropdown-toggle <?php if (!isset($availableEmails)): ?>disabled<?php endif; ?>" type="button"  data-toggle="dropdown">Cc:</button>
            <div class="dropdown-menu" role="menu">
                <ul>
                    <?php if (isset($availableEmails)): ?>
                        <?php foreach ($availableEmails as $email): ?>
                            <li>
                                <label class="checkbox">
                                    <input type="checkbox" value="<?php echo $email; ?>">
                                    <?php echo $email; ?>
                                </label>                        
                            </li>
                        <?php endforeach; ?>            
                    <?php endif; ?>
                </ul>
                <button class="btn btn-small btn-block apply-selection" type="button">Apply</button>
            </div>
        </div>
    </td>
    <td>
        <textarea id="email-to-copy" rows="1"></textarea>
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
    <td class="title">
        <label class="control-label" for="expires-in">Expires After:</label>
    </td>
    <td>
        <select id="expires-in">
            <option selected value="7">7 days</option>
            <option value="30">30 days</option>
            <option value="90">90 days</option>
            <option value="">Never</option>
        </select>
    </td>
</tr>                    
<tr>
    <td colspan="2" class ="buttons-area">
        <button class="btn" id="email-accept" type="button">Email</button>
        <button class="btn" id="email-cancel" type="button">Cancel</button>
    </td>
</tr>                    
</table>