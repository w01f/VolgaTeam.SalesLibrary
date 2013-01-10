<div class="folder-links-scroll-area" 
     style="background-color: <?php echo $folder->windowBackColor; ?>;
     color: <?php echo $folder->borderColor; ?>;">
     <?php if (isset($folder->files)): ?>
        <div class="folder-links-container">
            <?php foreach ($folder->files as $link): ?>
                <?php $linkContainerClass = (isset($link->originalFormat) && isset($link->availableFormats) ? 'link-container clickable' : 'link-container'); ?>
                <div class="<?php echo $linkContainerClass; ?>">
                    <?php if (isset($link->banner) && $link->banner->isEnabled): ?>
                        <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $link->banner, 'isLinkBanner' => true), true); ?>
                    <?php else: ?>        
                        <?php
                        $widget = $link->getWidget();
                        if ($link->getIsLineBreak())
                        {
                            $displayWidget = isset($widget) && $widget != '';
                            $linkClass = 'link-line-break' . ($displayWidget ? ' widget' : '');
                            $linkFontProperties = 'font-family: ' . $link->lineBreakProperties->font->name . '; '
                                . 'font-size: ' . $link->lineBreakProperties->font->size . 'pt; '
                                . 'font-weight: ' . ($link->lineBreakProperties->font->isBold ? ' bold' : ' normal') . '; '
                                . 'font-style: ' . ($link->lineBreakProperties->font->isItalic ? ' italic' : ' normal') . '; '
                                . 'color: ' . $link->lineBreakProperties->foreColor . '; ';
                        }
                        else
                        {
                            $displayWidget = $folder->displayLinkWidgets;
                            $linkClass = 'link-text' . ($displayWidget ? ' widget' : '');
                            $linkFontProperties = 'font-family: ' . $folder->windowFont->name . '; '
                                . 'font-size: ' . $folder->windowFont->size . 'pt; '
                                . 'font-weight: ' . ($link->isBold ? 'bold' : ($folder->windowFont->isBold ? ' bold' : ' normal')) . '; '
                                . 'font-style: ' . ($folder->windowFont->isItalic ? ' italic' : ' normal') . '; '
                                . 'color: ' . $folder->windowForeColor . '; ';
                        }
                        ?>
                        <div class="<?php echo $linkClass; ?> "
                             style="background-image: <?php echo isset($widget) ? "url('data:image/png;base64," . $widget . "')" : ""; ?>;
                             <?php echo $linkFontProperties; ?>">
                                 <?php echo $link->name; ?>
                                 <?php if (isset($link->note) && $link->note != ""): ?>
                                <span class="link-note"><?php echo $link->note; ?></span>
                            <?php endif; ?> 
                        </div>
                    <?php endif; ?> 
                    <div class="view-dialog-content">
                        <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/viewDialog.php', array('link' => $link), true); ?>                            
                    </div>
                </div>
            <?php endforeach; ?>
        </div>
    <?php endif; ?>
</div>