<?
	$grid = $pageRecord->getGrid();
	$searchBar = $pageRecord->getSearchBar();
	$homeBar = $pageRecord->getHomeBar();
	echo $this->renderPartial('homeBar', array('homeBar' => $homeBar, 'enableSearchBar' => $searchBar->configured), true);
	echo $this->renderPartial('searchBar', array('searchBar' => $searchBar), true);
?>
<div class="shortcuts-page-content<? echo $grid->configured ? ' grid' : ' wrap'; ?>" id="shortcuts-page-content-<? echo $pageRecord->id_tab; ?>">
	<? if (isset($linkRecords) && count($linkRecords) > 0): ?>
		<? if ($grid->configured): ?>
			<?
			$columnNumber = 1;
			$columnsCount = Yii::app()->browser->isMobile() && $grid->columnsCount > 3 ? 3 : $grid->columnsCount;
			?>
			<div style="text-align: <? echo $grid->alignment; ?>;
				padding-top: <? echo $grid->paddingTop; ?>px;">
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
				<td style="padding-bottom: <? echo $grid->dividerWidth; ?>px;">
					<? echo $linkContent; ?>
				</td>
				<? if ($columnNumber == $grid->columnsCount): ?></tr><? endif; ?>
				<?
				$columnNumber++;
				if ($columnNumber > $columnsCount)
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
