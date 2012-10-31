<ul data-role="listview" data-theme="c" data-divider-theme="c">
    <li data-role="list-divider" >
        <h4>                        
            <table class ="link-container">
                <tr>
                    <td>                        
                        <span class ="name">
                            <?php
                            if (isset($link->name) && $link->name != '')
                                echo $link->name;
                            else if (isset($link->fileName) && $link->fileName != '')
                                echo $link->fileName;
                            ?>
                        </span>
                    </td>
                </tr>
                <?php if (isset($link->name) && $link->name != ''): ?>                            
                    <tr>
                        <td>                       
                            <span class ="file"><?php echo $link->fileName; ?></span>
                        </td>
                    </tr>
                <?php endif; ?>                    
            </table>                           
        </h4>
    </li>
    <?php
    if (isset($link->originalFormat) && isset($link->availableFormats)):
        foreach ($link->availableFormats as $format):
            $imageSource = '';
            $imageTitle = '';
            switch ($format)
            {
                case 'ppt':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/pptx.png';
                    $imageTitle = 'PowerPoint';
                    break;
                case 'doc':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/docx.png';
                    $imageTitle = 'Word';
                    break;
                case 'xls':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/xlsx.png';
                    $imageTitle = 'Excel';
                    break;
                case 'png':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/png.png';
                    $imageTitle = 'PNG';
                    break;
                case 'jpeg':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/jpeg.png';
                    $imageTitle = 'JPEG';
                    break;
                case 'pdf':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/pdf.png';
                    $imageTitle = 'PDF';
                    break;
                case 'mp4':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/mp4.png';
                    $imageTitle = 'MP4';
                    break;
                case 'tab':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/tab.png';
                    $imageTitle = 'QuickTime';
                    break;
                case 'url':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats/url.png';
                    $imageTitle = 'Web Url';
                    break;
            }
            if ($imageSource != '' && $imageTitle != ''):
                ?>                
                <li>
                    <a class="preview-link" href="#">
                        <table class ="link-container">
                            <tr>
                                <td><img src="<?php echo $imageSource; ?>"/></td>
                                <td>
                                    <span><?php echo $imageTitle; ?></span>
                                    <div class="item-content">
                                        <div class="file-type"><?php echo $link->originalFormat; ?></div>
                                        <div class="view-type"><?php echo $format; ?></div>
                                        <?php
                                        $viewLinks = $link->getViewSource($format);
                                        if (isset($viewLinks))
                                        {
                                            echo CHtml::openTag('div', array('class' => 'links'));
                                            echo json_encode($viewLinks);
                                            echo CHtml::closeTag('div');
                                        }
                                        ?>
                                    </div>
                                </td>
                            </tr>
                        </table>                           
                    </a>                            
                </li>                    
            <?php endif; ?>                    
    <?php endforeach; ?>                    
<?php endif; ?>                    
</ul>
