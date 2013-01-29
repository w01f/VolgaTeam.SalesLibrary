<ul data-role="listview" data-theme="c" data-divider-theme="d">
    <li data-role="list-divider">
        <h4><?php echo $folder->parent->name . ' - ' . $folder->name; ?></h4>
    </li>
    <?php if (isset($folder->files)): ?>
    <?php foreach ($folder->files as $link): ?>
        <?php if ($link->type != 6 && ((isset($link->name) && $link->name != '') || (isset($link->fileName) && $link->fileName != ''))): ?>
            <?php echo $this->renderFile(Yii::getPathOfAlias('application.views.phone.wallbin') . '/link.php', array('link' => $link), true); ?>
            <?php endif; ?>
        <?php endforeach; ?>
    <?php endif; ?>
</ul>