<div class="folder-links-scroll-area" 
     style="background-color: <?php echo $folder->windowBackColor; ?>;
     color: <?php echo $folder->borderColor; ?>;">
     <?php if (isset($folder->files)): ?>
        <div class="folder-links-container">
            <?php foreach ($folder->files as $link): ?>
                <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $link), true); ?>                            
            <?php endforeach; ?>
        </div>
    <?php endif; ?>
</div>