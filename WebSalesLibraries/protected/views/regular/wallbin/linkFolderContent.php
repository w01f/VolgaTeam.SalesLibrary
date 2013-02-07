<?php foreach ($link->folderContent as $contentLink): ?>
<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $contentLink), true); ?>
<?php endforeach; ?>