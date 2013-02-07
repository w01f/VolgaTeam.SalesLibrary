<table class ="tool-dialog">
    <tr>
        <td collspan="2">
            <h4>Video Files are usually VERY BIG!</h4>
            <div>Are you SURE you want to Download this file?</div>
        </td>
    </tr>    
    <?php if ($format == 'video'): ?>    
        <tr>
            <td colspan="2">
                <br>
                <div>Select the format you want to download to your PC</div>
            </td>
        </tr>                    
        <tr>
            <td colspan="2" class ="buttons-area download-type">
                <button class="btn active" type="button"><img src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/wmv-download.png'; ?>" alt="wmv"/></button>
                <button class="btn" type="button"><img src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/mp4-download.png'; ?>" alt="mp4"/></button>
            </td>
        </tr>                        
    <?php else: ?>
        <tr style="display: none;">
            <td colspan="2" class ="buttons-area download-type">
                <button class="btn active" type="button"><img src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/mp4-download.png'; ?>" alt="mp4"/></button>
            </td>
        </tr>                                
    <?php endif; ?>
    <tr>
        <td colspan="2">
            <br>
        </td>
    </tr>                    
    <tr>
        <td colspan="2" class ="buttons-area">
            <button class="btn" id="accept-button" type="button">Download</button>
            <button class="btn" id="cancel-button" type="button">Cancel</button>
        </td>
    </tr>                    
</table>
