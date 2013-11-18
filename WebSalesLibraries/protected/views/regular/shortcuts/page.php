<?
	$homeBar = $pageRecord->getHomeBar();
	$this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/homeBar.php', array('homeBar' => $homeBar));
	$searchBar = $pageRecord->getSearchBar();
	$this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/searchBar.php', array('searchBar' => $searchBar));
?>
<? $grid = $pageRecord->getGrid(); ?>
<div class="shortcuts-page-content<? echo $grid->configured ? ' grid' : ' wrap'; ?>" id="shortcuts-page-content-<? echo $pageRecord->id_tab; ?>">
	<? if (isset($linkRecords) && count($linkRecords) > 0): ?>
		<? if ($grid->configured): ?>
			<? $columnNumber = 1; ?>
			<div style="text-align: <? echo $grid->alignment; ?>; padding-top: <? echo $grid->paddingTop; ?>px;">
			<table class="shortcuts-links grid">
		<? else: ?>
			<ul class="shortcuts-links">
		<?endif; ?>
		<? foreach ($linkRecords as $linkRecord): ?>
			<?
			$linkContent = '';
			switch ($linkRecord->type)
			{
				case 'mp4':
					$linkContent = $this->renderPartial('videoLink', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'url':
				case 'file':
				case 'window':
				case 'quicklist':
				case 'search':
					$linkContent = $this->renderPartial('directLink', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'page':
					$linkContent = $this->renderPartial('pageLink', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'libraryfile':
					$linkContent = $this->renderPartial('libraryFileLink', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'none':
					$linkContent = $this->renderPartial('emptyLink', array('link' => $linkRecord->GetModel()), true);
					break;
			}
			?>
			<? if ($grid->configured): ?>
				<? if ($columnNumber === 1): ?><tr><? endif; ?>
				<td>
					<? echo $linkContent; ?>
				</td>
				<? if ($columnNumber == $grid->columnsCount): ?></tr><? endif; ?>
				<?
				$columnNumber++;
				if ($columnNumber > $grid->columnsCount)
					$columnNumber = 1;
				?>
			<? else: ?>
				<li>
					<? echo $linkContent; ?>
				</li>
			<?endif; ?>
		<? endforeach; ?>
		<? if ($grid->configured): ?>
			</table>
			</div>
		<? else: ?>
			</ul>
		<?endif; ?>
	<? endif; ?>
</div>
