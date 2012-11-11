<ul data-role="listview" data-theme="c" data-divider-theme="d">
    <li data-role="list-divider" >
        <h4>
            <?php
            if (isset($links))
                echo 'Results: ' . count($links);
            else
                echo 'Files was not found';
            ?>
        </h4>
    </li>
    <?php if (isset($links)): ?>
        <?php foreach ($links as $link): ?>
            <li>
                <a class ="file-link" href="#link<?php echo $link['id']; ?>">
                    <table class ="link-container">
                        <tr>
                            <?php if ($link['hasDetails']): ?>
                                <td rowspan="2" class ="link-details-container">
                                    <a class ="file-link-detail" href="#link<?php echo $link['id']; ?>">
                                        <img src="<?php echo Yii::app()->baseUrl . '/images/search/expand.png'; ?>"/>
                                    </a>
                                </td>
                            <?php endif; ?>                                                    
                            <td>                        
                                <span class ="name">
                                    <?php
                                    if (isset($link['name']) && $link['name'] != '')
                                        echo $link['name'];
                                    else if (isset($link['file_name']) && $link['file_name'] != '')
                                        echo $link['file_name'];
                                    ?>
                                </span>
                            </td>
                        </tr>
                        <?php if (isset($link['name']) && $link['name'] != ''): ?>                            
                            <tr>
                                <td>                        
                                    <span class ="file"><?php echo $link['file_name']; ?></span>
                                </td>
                            </tr>
                        <?php endif; ?>                    
                    </table>                           
                </a>
            </li>                    
        <?php endforeach; ?>                    
    <?php endif; ?>                    
</ul>