<div class ="view-dialog-body">
    <?php if (isset($link->originalFormat) && isset($link->availableFormats)): ?>
        <div class="title">
            <div class ="link-name">
                <?php echo $link->name; ?>
            </div>            
            <br>
            <div class ="description">
                <?php echo $link->fileName; ?>
            </div>
            <br>
        </div>
        <table class="format-list">
            <tr>
                <?php foreach ($link->availableFormats as $format): ?>
                    <?php
                    $imageSource = '';
                    switch ($format)
                    {
                        case 'ppt':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/pptx.png';
                            break;
                        case 'doc':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/docx.png';
                            break;
                        case 'xls':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/xlsx.png';
                            break;
                        case 'png':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/png.png';
                            break;
                        case 'jpeg':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/jpeg.png';
                            break;
                        case 'pdf':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/pdf.png';
                            break;
                        case 'video':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/wmv.png';
                            break;
                        case 'mp4':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/mp4.png';
                            break;
                        case 'ogv':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/ogv.png';
                            break;
                        case 'tab':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/tab.png';
                            break;
                        case 'url':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/url.png';
                            break;
                        case 'email':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/email.png';
                            break;
                    }
                    ?>
                    <?php if ($imageSource != ''): ?>
                        <td>
                            <img src="<?php echo $imageSource; ?>" />
                            <div class ="service-data">
                                <div class ="file-type"><?php echo $link->originalFormat; ?></div>
                                <div class ="view-type"><?php echo $format; ?></div>
                                <?php $viewLinks = $link->getViewSource($format); ?>
                                <?php if (isset($viewLinks)): ?>
                                    <div class ="links"><?php echo json_encode($viewLinks); ?></div>
                                <?php endif; ?>
                                <?php if ($format == 'png' || $format == 'jpeg'): ?>
                                    <?php $thumbsLinks = $link->getViewSource('thumbs'); ?>
                                    <?php if (isset($thumbsLinks)): ?>
                                        <div class ="thumbs"><?php echo json_encode($thumbsLinks); ?></div>
                                    <?php endif; ?>
                                <?php endif; ?>
                            </div>
                        </td>                        
                    <?php endif; ?>
                <?php endforeach; ?>
            </tr>
        </table>
        <?php if ($link->originalFormat == 'ppt' || $link->originalFormat == 'doc' || $link->originalFormat == 'pdf' || $link->originalFormat == 'jpeg' || $link->originalFormat == 'png'): ?>
            <br>
            <label class="checkbox">
                <input class="use-fullscreen" type="checkbox" value="">
                View PNG & JPEG in a full screen new tab
            </label>
        <?php endif; ?>
    <?php else: ?>
        <div class="title">
            <div class ="description">
                This link is not available for preview
            </div>
        </div>
    <?php endif; ?>
</div>