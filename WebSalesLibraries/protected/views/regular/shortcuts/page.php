<? $homeBar = $pageRecord->getHomeBar(); ?>
<? if ($homeBar->configured): ?>
	<?php
	$useText = false;
	$homeBarImageMarginLeft = '0px';
	$homeBarImageMarginRight = '0px';
	if ($homeBar->imageAlignment === 'left')
	{
		$useText = true;
		$homeBarImageMarginLeft = '0px';
		$homeBarImageMarginRight = 'auto';
	}
	else if ($homeBar->imageAlignment === 'center')
	{
		$homeBarImageMarginLeft = 'auto';
		$homeBarImageMarginRight = 'auto';
	}
	else if ($homeBar->imageAlignment === 'right')
	{
		$homeBarImageMarginLeft = 'auto';
		$homeBarImageMarginRight = '0px';
	}
	?>
	<table class="shortcuts-home-bar"
		   style="background-color: <?php echo $homeBar->backColor; ?>;
			   color: <?php echo $homeBar->foreColor; ?>;">
		<tr>
			<td class="logo-container">
				<img src="<? echo $homeBar->imagePath ?>" style="display: block; margin-left: <?php echo $homeBarImageMarginLeft; ?>; margin-right: <?php echo $homeBarImageMarginRight; ?>;">
			</td>
			<? if ($useText): ?>
				<td class="title-container">
					<span class="title"
						  style="padding-right: 10px;
							  font-family: <? echo $homeBar->font->name; ?>,serif;
							  font-size: <?php echo $homeBar->font->size; ?>pt;
							  font-weight: <?php echo $homeBar->font->isBold ? ' bold' : ' normal'; ?>;
							  font-style: <?php echo $homeBar->font->isItalic ? ' italic' : ' normal'; ?>;">
					</span>
				</td>
			<? endif; ?>
		</tr>
	</table>
<?php endif; ?>
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
					$linkContent = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/videoLink.php', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'url':
				case 'file':
				case 'window':
				case 'quicklist':
				case 'search':
					$linkContent = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/directLink.php', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'page':
					$linkContent = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/pageLink.php', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'libraryfile':
					$linkContent = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/libraryFileLink.php', array('link' => $linkRecord->GetModel()), true);
					break;
				case 'none':
					$linkContent = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/emptyLink.php', array('link' => $linkRecord->GetModel()), true);
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
