<div id ="page-header-container">
    <?php
    for ($i = 0; $i < 3; $i++):
        ?>
        <?php if (isset($libraryPage->columns) && $libraryPage->enableColumns): ?>
            <?php if (isset($libraryPage->columns[$i])): ?>
                <?php $column = $libraryPage->columns[$i]; ?>
                <div id="column-header-container-<?php echo $i; ?>" class="column-header-container"
                     style="font-family: <?php echo $column->font->name; ?>;
                     font-size: <?php echo $column->font->size; ?>pt;
                     font-weight: <?php echo $column->font->isBold ? ' bold' : ' normal'; ?>;
                     font-style: <?php echo $column->font->isItalic ? ' italic' : ' normal'; ?>;
                     text-align: <?php echo $column->alignment; ?>;
                     background-color: <?php echo $column->backColor; ?>;
                     color: <?php echo $column->foreColor; ?>;">
                     <?php if (isset($column->banner) && $column->banner->isEnabled): ?>
                         <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $column->banner, 'isLinkBanner' => false), true); ?>
                     <?php else: ?>        
                         <?php $widget = $column->getWidget(); ?>
                         <?php if (isset($widget)): ?>
                            <img class="column-widget" src="data:image/png;base64,<?php echo $widget; ?>">
                        <?php endif; ?>                            
                        <?php if ($column->showText): ?>
                            <span class="column-header"><?php echo $column->name; ?></span>
                        <?php endif; ?>                                                
                    <?php endif; ?>                            
                </div>
            <?php else: ?>        
                <div class="column-header-container"></div>
            <?php endif; ?>        
        <?php endif; ?>            
    <?php endfor; ?>
</div>
<div id="page-content-container">
    <?php
    for ($i = 0; $i < 3; $i++):
        ?>  
        <?php $folders = $libraryPage->getFoldersByColumn($i); ?>
        <?php if (isset($folders)): ?>
            <div class="page-column" id="column<?php echo $i; ?>">
                <?php foreach ($folders as $folder): ?>
                    <div class="folder-body">
                        <div class="folder-header-container"
                             style="font-family: <?php echo $folder->headerFont->name; ?>;
                             font-size: <?php echo $folder->headerFont->size; ?>pt;
                             font-weight: <?php echo $folder->headerFont->isBold ? ' bold' : ' normal'; ?>;
                             font-style: <?php echo $folder->headerFont->isItalic ? ' italic' : ' normal'; ?>;
                             text-align: <?php echo $folder->headerAlignment; ?>;
                             background-color: <?php echo $folder->headerBackColor; ?>;
                             color: <?php echo $folder->headerForeColor; ?>;
                             border-bottom-color: <?php echo $folder->borderColor; ?>;">
                             <?php if (isset($folder->banner) && $folder->banner->isEnabled): ?>
                                 <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $folder->banner, 'isLinkBanner' => false), true); ?>
                             <?php else: ?>        
                                 <?php $widget = $folder->getWidget(); ?>
                                 <?php if (isset($widget)): ?>
                                    <img class="folder-widget" src="data:image/png;base64,<?php echo $widget; ?>">
                                <?php endif; ?>                            
                                <span class="folder-header"><?php echo $folder->name; ?></span>
                            <?php endif; ?>                            
                        </div>
                        <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderLinks.php', array('folder' => $folder), true); ?>                            
                    </div>
                <?php endforeach; ?>
            </div>
        <?php endif; ?>                            
    <?php endfor; ?>    
</div>