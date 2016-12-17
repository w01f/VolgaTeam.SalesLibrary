<?
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/**
	 * @var $libraryPage LibraryPage
	 * @var $style \application\models\wallbin\models\web\style\WallbinPageStyle
	 */

	$foldersByColumns = array();
	for ($i = 0; $i < 3; $i++)
		$foldersByColumns[$i] = $libraryPage->getFoldersByColumn($i);

	$pageColumnInnerMargin = 1;
?>
<? if (!$style->columnStyleEnabled): ?>
	<style>
		.folder-body
		{
			min-height: 40px;
		}

		.folder-links-scroll-area
		{
			min-height: 80px;
		}
	</style>
<? else: ?>
	<style>
		#page-<? echo $libraryPage->id; ?> .content-columns-container {
			border-collapse: separate;
			border-spacing: 0 <? echo (max($style->column1Style->padding,$style->column2Style->padding,$style->column3Style->padding)*0.6).'px';?>;
		}

		#page-<? echo $libraryPage->id; ?> .page-column {
			border-collapse: collapse;
			border-spacing: 0;
		}

		#page-<? echo $libraryPage->id; ?> .folder-links-container {
			margin-bottom: 20px;
		}

		<?if(isset($style->verticalBorder1Color)):?>
		<?if($style->verticalBorderStretch):?>
		#page-<? echo $libraryPage->id; ?> .page-column.column0 {
			border-right: 1px solid <?echo '#'.$style->verticalBorder1Color;?>;
		}

		<? else: ?>
		<?if(!$style->column1Style->frozen):?>
		#page-<? echo $libraryPage->id; ?> .page-column.column0 .page-column-inner {
			border-right: 1px solid <?echo '#'.$style->verticalBorder1Color;?>;
		}

		<? endif; ?>

		#page-<? echo $libraryPage->id; ?> .page-column.column1 .page-column-inner {
			border-left: 1px solid <?echo '#'.$style->verticalBorder1Color;?>;
			margin-left: -<? echo $pageColumnInnerMargin; ?>px;
		}

		<?$pageColumnInnerMargin++;?>
		<? endif; ?>
		<? endif; ?>

		<?if(isset($style->verticalBorder2Color)):?>
		<?if($style->verticalBorderStretch):?>
		#page-<? echo $libraryPage->id; ?> .page-column.column1 {
			border-right: 1px solid <?echo '#'.$style->verticalBorder2Color;?>;
		}

		<? else: ?>
		#page-<? echo $libraryPage->id; ?> .page-column.column1 .page-column-inner {
			border-right: 1px solid <?echo '#'.$style->verticalBorder2Color;?>;
		}

		#page-<? echo $libraryPage->id; ?> .page-column.column2 .page-column-inner {
			border-left: 1px solid <?echo '#'.$style->verticalBorder2Color;?>;
			margin-left: -<? echo $pageColumnInnerMargin; ?>px;
		}

		<? endif; ?>
		<? endif; ?>

		<? if ($style->column1Style->enabled): ?>

		<?if($style->column1Style->frozen):?>
		#page-<? echo $libraryPage->id; ?> .page-column.column0 .page-column-inner {
			position: fixed;
			width: 30%;
		}

		<? endif; ?>

		<?if($style->verticalBorderStretch):?>
		#page-<? echo $libraryPage->id; ?> .page-column.column0 {
			padding: 0 <? echo $style->column1Style->padding.'px';?> 0 <? echo $style->column1Style->padding.'px';?>;
		}

		<? else: ?>
		#page-<? echo $libraryPage->id; ?> .page-column.column0 .page-column-inner {
			padding: 0 <? echo $style->column1Style->padding.'px';?> 0 <? echo $style->column1Style->padding.'px';?>;
		}

		<? endif; ?>

		#page-<? echo $libraryPage->id; ?> .page-column.column0 .folder-body {
			margin-bottom: <? echo ($style->column1Style->padding*0.6).'px';?>;
			border-bottom: 1px solid <?echo '#'.$style->column1Style->windowBorderColor;?> !important;
		}

		<? endif; ?>

		<? if ($style->column2Style->enabled): ?>
		<?if($style->verticalBorderStretch):?>
		#page-<? echo $libraryPage->id; ?> .page-column.column1 {
			padding: 0 <? echo $style->column2Style->padding.'px';?> 0 <? echo $style->column2Style->padding.'px';?>;
		}

		<? else: ?>
		#page-<? echo $libraryPage->id; ?> .page-column.column1 .page-column-inner {
			padding: 0 <? echo $style->column2Style->padding.'px';?> 0 <? echo $style->column2Style->padding.'px';?>;
		}

		<? endif; ?>

		#page-<? echo $libraryPage->id; ?> .page-column.column1 .folder-body {
			margin-bottom: <? echo ($style->column2Style->padding*0.6).'px';?>;
			border-bottom: 1px solid <?echo '#'.$style->column2Style->windowBorderColor;?> !important;
		}

		<? endif; ?>

		<? if ($style->column3Style->enabled): ?>
		<?if($style->verticalBorderStretch):?>
		#page-<? echo $libraryPage->id; ?> .page-column.column2 {
			padding: 0 <? echo $style->column3Style->padding.'px';?> 0 <? echo $style->column3Style->padding.'px';?>;
		}

		<? else: ?>
		#page-<? echo $libraryPage->id; ?> .page-column.column2 .page-column-inner {
			padding: 0 <? echo $style->column3Style->padding.'px';?> 0 <? echo $style->column3Style->padding.'px';?>;
		}

		<? endif; ?>

		#page-<? echo $libraryPage->id; ?> .page-column.column2 .folder-body {
			margin-bottom: <? echo ($style->column3Style->padding*0.6).'px';?>;
			border-bottom: 1px solid <?echo '#'.$style->column3Style->windowBorderColor;?> !important;
		}

		<? endif; ?>
	</style>
<? endif; ?>
<div class="page-container" id="page-<? echo $libraryPage->id; ?>">
	<? if (isset($libraryPage->columns) && $libraryPage->enableColumns && !$style->columnStyleEnabled): ?>
		<div class="header-container">
			<? for ($i = 0; $i < 3; $i++): ?>
				<? if (isset($libraryPage->columns[$i])): ?>
					<? $column = $libraryPage->columns[$i]; ?>
					<div class="column-header-container column-header-container-<? echo $i; ?>"
					     style="font-family: <? echo isset($column->banner) && $column->banner->isEnabled ? $column->banner->font->name : $column->font->name; ?>,serif;
						     font-size: <? echo isset($column->banner) && $column->banner->isEnabled ? $column->banner->font->size : $column->font->size; ?>pt;
						     font-weight: <? echo (isset($column->banner) && $column->banner->isEnabled ? $column->banner->font->isBold : $column->font->isBold) ? ' bold' : ' normal'; ?>;
						     font-style: <? echo (isset($column->banner) && $column->banner->isEnabled ? $column->banner->font->isItalic : $column->font->isItalic) ? ' italic' : ' normal'; ?>;
						     text-decoration: <? echo (isset($column->banner) && $column->banner->isEnabled ? $column->banner->font->isUnderlined : $column->font->isUnderlined) ? ' underline' : ' inherit'; ?>;
						     text-align: <? echo isset($column->banner) && $column->banner->isEnabled ? $column->banner->imageAlignment : $column->alignment; ?>;
						     background-color: <? echo $column->backColor; ?>;
						     color: <? echo isset($column->banner) && $column->banner->isEnabled ? $column->banner->foreColor : $column->foreColor; ?>;">
						<? if (isset($column->banner) && $column->banner->isEnabled): ?>
							<table style="height: 100%; display: inline;">
								<tr>
									<td><img src="data:image/png;base64,<? echo $column->banner->image; ?>"></td>
									<? if ($column->banner->showText): ?>
										<td>
											<span style="text-align: <? echo $column->banner->imageAlignment; ?>;">
												<? echo nl2br($column->banner->text); ?>
											</span>
										</td>
									<? endif; ?>
								</tr>
							</table>
						<? else: ?>
							<? $widget = $column->getWidget(); ?>
							<? if (isset($widget)): ?>
								<img class="column-widget" src="data:image/png;base64,<? echo $widget; ?>">
							<? endif; ?>
							<? if ($column->showText): ?>
								<span class="column-header"><? echo $column->name; ?></span>
							<? endif; ?>
						<? endif; ?>
					</div>
				<? else: ?>
					<div class="column-header-container"></div>
				<? endif; ?>
			<? endfor; ?>
		</div>
	<? endif; ?>
	<div class="content-container">
		<div class="content-columns-container">
			<div class="page-column-outer">
				<? for ($i = 0; $i < 3; $i++): ?>
					<? $folders = $foldersByColumns[$i]; ?>
					<div class="page-column column<? echo $i; ?>">
						<? if (!$style->verticalBorderStretch): ?>
						<div class="page-column-inner"><? endif; ?>
							<? foreach ($folders as $folder)
								{
									$folderStyle = \application\models\wallbin\models\web\style\FolderStyle::createDefault();
									if ($style->columnStyleEnabled)
										$folder->borderColor = '#ffffff';
									switch ($i)
									{
										case 0:
											$folderStyle = $style->column1Style->windowStyle;
											break;
										case 1:
											$folderStyle = $style->column2Style->windowStyle;
											break;
										case 2:
											$folderStyle = $style->column3Style->windowStyle;
											break;
									}
									echo $this->renderFile(
										Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderContainer.php',
										array(
											'folder' => $folder,
											'style' => $folderStyle
										), true);
								}
							?>
							<? if (!$style->verticalBorderStretch): ?></div><? endif; ?>
					</div>
				<? endfor; ?>
			</div>
		</div>
	</div>
</div>