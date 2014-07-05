<? /** @var $link LibraryLink */ ?>
<ul data-role="listview" data-theme="c" data-divider-theme="d">
	<li data-role="list-divider">
		<h4><? echo $link->name; ?></h4>
	</li>
	<? if (isset($link->folderContent)): ?>
		<? foreach ($link->folderContent as $contentLink): ?>
			<? echo $this->renderFile(Yii::getPathOfAlias('application.views.phone.wallbin') . '/link.php', array('link' => $contentLink), true); ?>
		<? endforeach; ?>
	<? endif; ?>
</ul>