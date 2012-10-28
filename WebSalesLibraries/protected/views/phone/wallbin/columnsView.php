<?php
$selectedPage->loadData('mobile');
?>
<div class="item-content" id="page<?php echo $selectedPage->id; ?>">
    <div class="back-button">
        <li data-corners="false" data-shadow="false" data-iconshadow="true" data-theme="e" class="ui-btn ui-btn-up-d ui-li">
            <div data-role="header">
                <a class="link back ui-btn-right" href="#libraries" data-role="button" data-mini="true" data-icon="back" data-corners="true" data-shadow="true" data-iconshadow="true" data-theme="b">Back</a>
                <span class="ui-title"><?php echo $selectedPage->name; ?></span>
            </div>
        </li>
    </div>                
    <?php foreach ($selectedPage->folders as $folder): ?>
        <div class="item-content" id="folder<?php echo $folder->id; ?>">
            <div class="title">
                <li data-corners="false" data-shadow="false" data-iconshadow="true" data-icon="arrow-r" data-iconpos="right" class="ui-btn ui-btn-up-d ui-btn-icon-right ui-li-has-arrow ui-li">
                    <a href="#folder<?php echo $folder->id; ?>" class="link">
                        <?php echo $folder->name; ?>
                    </a>
                </li>
            </div>
            <div class="back-button">
                <li data-corners="false" data-shadow="false" data-iconshadow="true" data-theme="e" class="ui-btn ui-btn-up-d ui-li">
                    <div data-role="header">
                        <a class="link back ui-btn-right" href="#page<?php echo $selectedPage->id; ?>" data-role="button" data-mini="true" data-icon="back" data-corners="true" data-shadow="true" data-iconshadow="true" data-theme="b">Back</a>
                        <span class="ui-title"><?php echo $folder->name; ?></span>
                    </div>                
                </li>
            </div>            
            <?php foreach ($folder->files as $link): ?>
                <?php if (isset($link->name) && isset($link->fileName) && $link->name != '' && $link->fileName != ''): ?>
                    <div class="item-content" id="link<?php echo $link->id; ?>">
                        <div class="title">
                            <li data-corners="false" data-shadow="false" data-iconshadow="true" data-icon="arrow-r" data-iconpos="right" class="ui-btn ui-btn-up-d ui-btn-icon-right ui-li-has-arrow ui-li">
                                <a href="#link<?php echo $link->id; ?>" class="link">
                                    <?php
                                    if ($link->name != '')
                                        echo $link->name;
                                    else
                                        echo $link->fileName;
                                    ?>
                                </a>
                            </li>
                        </div>
                        <div class="back-button">
                            <li data-corners="false" data-shadow="false" data-iconshadow="true" data-theme="e" class="ui-btn ui-btn-up-d ui-li">
                                <div data-role="header">
                                    <a class="link back ui-btn-right" href="#folder<?php echo $folder->id; ?>" data-role="button" data-mini="true" data-icon="back" data-corners="true" data-shadow="true" data-iconshadow="true" data-theme="b">Back</a>
                                    <span class="ui-title">
                                        <?php
                                        if ($link->name != '')
                                            echo $link->name;
                                        else
                                            echo $link->fileName;
                                        ?>
                                    </span>
                                </div>                
                            </li>
                        </div>                        
                        <div class="item-content">
                        </div>                        
                    </div>
                <?php endif; ?>                    
            <?php endforeach; ?>                    
        </div>
    <?php endforeach; ?>                    
</div>
