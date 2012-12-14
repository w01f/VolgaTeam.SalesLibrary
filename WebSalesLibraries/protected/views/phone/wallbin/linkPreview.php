<ul data-role="listview" data-theme="c" data-divider-theme="c">
    <?php if (isset($link->originalFormat) && isset($link->availableFormats)): ?>
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
                    <?php if (isset($link->name) && $link->name != '' && isset($link->fileName) && $link->fileName != ''): ?>                            
                        <tr>
                            <td>                       
                                <span class ="file"><?php echo $link->fileName; ?></span>
                            </td>
                        </tr>
                    <?php endif; ?>                    
                </table>              
                <?php if (($link->originalFormat == 'ppt' || $link->originalFormat == 'doc' || $link->originalFormat == 'pdf') && !$this->isTabletMobileView): ?>                            
                    <hr align="center" width="100%" size="2" color="#b0b0b0" />                
                    <div data-role="navbar" class="res-selector">
                        <ul>
                            <li><a href="#" class="low-res-button ui-btn-active" data-corners="true" data-shadow="true">LOW Res</a></li>
                            <li><a href="#" class="hi-res-button" data-corners="true" data-shadow="true">HIGH Res</a></li>
                        </ul>
                    </div>        
                <?php endif; ?>                    
            </h4>
        </li>
        <?php
        foreach ($link->availableFormats as $format):
            $imageSource = '';
            $imageTitle = '';
            switch ($format)
            {
                case 'ppt':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/pptx.png';
                    $imageTitle = 'PowerPoint';
                    break;
                case 'doc':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/docx.png';
                    $imageTitle = 'Word';
                    break;
                case 'xls':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/xlsx.png';
                    $imageTitle = 'Excel';
                    break;
                case 'png':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/png.png';
                    $imageTitle = 'PNG';
                    break;
                case 'jpeg':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/jpeg.png';
                    $imageTitle = 'JPEG';
                    break;
                case 'pdf':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/pdf.png';
                    $imageTitle = 'PDF';
                    break;
                case 'tab':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/tab.png';
                    $imageTitle = 'QuickTime';
                    break;
                case 'url':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/url.png';
                    $imageTitle = 'Web Url';
                    break;
                case 'email':
                    $imageSource = Yii::app()->baseUrl . '/images/fileFormats_phone/email.png';
                    $imageTitle = 'Email Link';
                    break;
            }
            $fileSize = $link->getViewSize($format);
            $fileSizePhone = $link->getViewSize($format . '_phone');
            if ($imageSource != '' && $imageTitle != ''):
                ?>                
                <li>
                    <a class="preview-link" href="#">
                        <table class ="link-container">
                            <tr>
                                <td><img src="<?php echo $imageSource; ?>"/></td>
                                <td>
                                    <span><?php echo $imageTitle; ?></span>
                                    <?php if (isset($fileSize)): ?>
                                        <span class="file-size <?php echo isset($fileSizePhone) ? 'regular' : ''; ?>"> (<?php echo $fileSize; ?>)</span>
                                    <?php endif; ?>
                                    <?php if (isset($fileSizePhone)): ?>
                                        <span class="file-size phone"> (<?php echo $fileSizePhone; ?>)</span>
                                    <?php endif; ?>                                        
                                    <div class="item-content">
                                        <div class="file-type"><?php echo $link->originalFormat; ?></div>
                                        <div class="view-type"><?php echo $format; ?></div>
                                        <div class="file-size"><?php echo $link->originalFormat; ?></div>                                        
                                        <?php
                                        $viewLinks = $link->getViewSource($format);
                                        if (isset($viewLinks)):
                                            echo CHtml::openTag('div', array('class' => 'links'));
                                            if ($format == 'png' || $format == 'jpeg'):
                                                $thumbsLinks = $link->getViewSource('thumbs');
                                                $viewPhoneLinks = $format == 'png' ? $link->getViewSource('png_phone') : $link->getViewSource('jpeg_phone');
                                                if (isset($thumbsLinks)):
                                                    $i = 0;
                                                    foreach ($viewLinks as $viewLink):
                                                        ?>
                                                        <li class ="hi-res">
                                                            <a href="<?php echo $viewLink['href']; ?>" rel="external"><img src="<?php echo!$this->isTabletMobileView ? $thumbsLinks[$i]['href'] : $viewPhoneLinks[$i]['href']; ?>" alt="<?php echo $viewLink['title']; ?>" /></a>
                                                        </li>
                                                        <li class ="low-res">
                                                            <a href="<?php echo $viewPhoneLinks[$i]['href']; ?>" rel="external"><img src="<?php echo $thumbsLinks[$i]['href']; ?>" alt="<?php echo $viewLink['title']; ?>" /></a>
                                                        </li>                                                        
                                                        <?php
                                                        $i++;
                                                    endforeach;
                                                endif;
                                            else:
                                                echo json_encode($viewLinks);
                                            endif;
                                            echo CHtml::closeTag('div');
                                        endif;
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
