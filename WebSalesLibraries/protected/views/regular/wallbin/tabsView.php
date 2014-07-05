<?
	/** @var $wallbinView string */
	if (isset($library) && isset($selectedPage)): ?>
		<ul>
			<? foreach ($library->pages as $page): ?>
				<li><a href="#<? echo $page->id; ?>"><? echo $page->name; ?></a></li>
			<? endforeach; ?>
		</ul>
		<? foreach ($library->pages as $page): ?>
			<div id="<? echo $page->id; ?>" class="wallbin-container">
				<?
					if ($page->id == $selectedPage->id)
					{
						echo $wallbinView == 'accordion' ?
							$this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/accordionView.php', array('libraryPage' => $selectedPage), true) :
							$this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/columnsView.php', array('selectedPage' => $selectedPage), true);
					}
				?>
			</div>
		<? endforeach; ?>
	<? endif; ?>