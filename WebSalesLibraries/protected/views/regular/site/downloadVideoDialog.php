<table id ="email-dialog">
    <tr>
        <td collspan="2">
            <h4>Video Files are usually VERY BIG!</h4>
            <div>Are you SURE you want to Download this file?</div>
        </td>
    </tr>    
    <tr>
        <td colspan="2">
            <br>
            <div>Select and click to format you want to download</div>
        </td>
    </tr>                    
    <tr>
        <td colspan="2" class ="buttons-area download-type">
            <button class="btn active" type="button"><img src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/wmv-download.png'; ?>" alt="wmv"/></button>
            <button class="btn" type="button"><img src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/mp4-download.png'; ?>" alt="mp4"/></button>
        </td>
    </tr>                        
    <tr>
        <td colspan="2">
            <br>
        </td>
    </tr>                    
    <tr>
        <td colspan="2" class ="buttons-area">
            <button class="btn" id="download-accept" type="button">Download</button>
            <button class="btn" id="download-cancel" type="button">Cancel</button>
        </td>
    </tr>                    
</table>
