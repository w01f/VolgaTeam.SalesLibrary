<?php if (isset($library) && isset($selectedPage)): ?>
<ul>
	<?php foreach ($library->pages as $page): ?>
	<li><a href="#<?echo $page->id;?>"><?echo $page->name;?></a></li>
	<? endforeach; ?>
</ul>
<?php foreach ($library->pages as $page): ?>
	<div id="<?echo $page->id;?>" class="wallbin-container"><?if ($page->id == $selectedPage->id) {echo $wallbinView == 'columns' ? $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/columnsView.php', array('selectedPage' => $selectedPage), true): $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/accordionView.php', array('libraryPage' => $selectedPage), true);}?></div>
	<? endforeach; ?>
<? endif; ?>
