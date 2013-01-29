<ul data-role="listview" data-theme="c" data-divider-theme="d">
    <li data-role="list-divider">
        <h4><?php echo $link->name; ?></h4>
    </li>
    <?php if (isset($link->folderContent)): ?>
    <?php foreach ($link->folderContent as $contentLink): ?>
            <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.phone.wallbin') . '/link.php', array('link' => $contentLink), true); ?>
        <?php endforeach; ?>
    <?php endif; ?>
</ul>