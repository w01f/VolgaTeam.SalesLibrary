<?
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/**
	 * @var $libraryPage LibraryPage
	 * @var $containerId string
	 * @var $style \application\models\wallbin\models\web\style\WallbinPageStyle
	 * @var $screenSettings array
	 */

	$totalColumnCount = 3;
	$screenSizeType = 'large';
	if ($style->showResponsiveColumns)
	{
		switch ($screenSettings['screenSizeType'])
		{
			case 'large':
			case 'medium':
				$screenSizeType = 'large';
				$totalColumnCount = 3;
				break;
			case 'small':
				$screenSizeType = 'small';
				$totalColumnCount = 2;
				break;
			case 'extrasmall':
				$screenSizeType = 'extrasmall';
				$totalColumnCount = 1;
				break;
		}
	}

	$foldersByColumns = array();
	for ($i = 0; $i < $totalColumnCount; $i++)
		$foldersByColumns[$i] = $libraryPage->getFoldersByColumn($i, $totalColumnCount);

	$pageColumnInnerMargin = 1;
?>
<div class="page-container" id="page-<? echo $libraryPage->id; ?>">
    <style>
        <?if($totalColumnCount === 3):?>
            <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column
            {
                width: 33%;
            }
        <?elseif ($totalColumnCount === 2):?>
            <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column
            {
                width: 50%;
            }
        <?else:?>
            <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?>  .page-column
            {
                width: 100%;
            }
        <?endif;?>

        <? if (isset($style->padding) && $style->padding->isConfigured): ?>
            <? if (isset($libraryPage->columns) && $libraryPage->enableColumns && !$style->columnStyleEnabled && $screenSizeType == 'large'): ?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .header-container {
                    padding-top: <? echo $style->padding->top; ?>px !important;
                    padding-left: <? echo $style->padding->left; ?>px !important;
                    padding-right: <? echo $style->padding->right; ?>px !important;
                }
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .content-container {
                    padding-left: <? echo $style->padding->left; ?>px !important;
                    padding-bottom: <? echo $style->padding->bottom; ?>px !important;
                    padding-right: <? echo $style->padding->right; ?>px !important;
                }
            <?else:?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .content-container {

                    padding-top: <? echo $style->padding->top; ?>px !important;
                    padding-left: <? echo $style->padding->left; ?>px !important;
                    padding-bottom: <? echo $style->padding->bottom; ?>px !important;
                    padding-right: <? echo $style->padding->right; ?>px !important;
                }
            <?endif;?>
        <?endif;?>
        <? if (!$style->columnStyleEnabled): ?>

                .folder-body
                {
                    min-height: 40px;
                }

                .folder-links-scroll-area
                {
                    min-height: 80px;
                }
        <? else: ?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .content-columns-container
                {
                    border-collapse: separate;
                    border-spacing: 0 <? echo (max($style->column1Style->padding,$style->column2Style->padding,$style->column3Style->padding)* 1.2).'px';?>;
                }

                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column
                {
                    border-collapse: collapse;
                    border-spacing: 0;
                }

                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .folder-links-container
                {
                    margin-bottom: 20px;
                }

                <?if($totalColumnCount > 1 && isset($style->verticalBorder1Color)):?>
                    <?if($style->verticalBorderStretch):?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0
                        {
                            border-right: 1px solid <?echo Utils::formatColor($style->verticalBorder1Color);?>;
                        }
                    <? else: ?>
                        <?if(!$style->column1Style->frozen):?>
                            <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0 .page-column-inner
                            {
                                border-right: 1px solid <?echo Utils::formatColor($style->verticalBorder1Color);?>;
                            }
                        <? endif; ?>

                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column1 .page-column-inner
                        {
                            border-left: 1px solid <?echo Utils::formatColor($style->verticalBorder1Color);?>;
                            margin-left: -<? echo $pageColumnInnerMargin; ?>px;
                        }
                        <?$pageColumnInnerMargin++;?>
                    <? endif; ?>
                <? endif; ?>

                <?if($totalColumnCount > 2 && isset($style->verticalBorder2Color)):?>
                    <?if($style->verticalBorderStretch):?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column1
                        {
                            border-right: 1px solid <?echo Utils::formatColor($style->verticalBorder2Color);?>;
                        }
                    <? else: ?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column1 .page-column-inner
                        {
                            border-right: 1px solid <?echo Utils::formatColor($style->verticalBorder2Color);?>;
                        }
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column2 .page-column-inner
                        {
                            border-left: 1px solid <?echo Utils::formatColor($style->verticalBorder2Color);?>;
                            margin-left: -<? echo $pageColumnInnerMargin; ?>px;
                        }
                    <? endif; ?>
                <? endif; ?>

                <? if ($style->column1Style->enabled): ?>
                    <?if($style->column1Style->frozen):?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0 .page-column-inner
                        {
                            position: fixed;
                            width: 30%;
                        }
                    <? endif; ?>

                    <?if($style->verticalBorderStretch):?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0
                        {
                            padding: 0 <? echo $style->column1Style->padding.'px';?> 0 <? echo $style->column1Style->padding.'px';?>;
                        }
                    <? else: ?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0 .page-column-inner
                        {
                            padding: 0 <? echo $style->column1Style->padding.'px';?> 0 <? echo $style->column1Style->padding.'px';?>;
                        }
                    <? endif; ?>

                    <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0 .folder-body {
                        margin-bottom: <? echo ($style->column1Style->padding*0.6).'px';?>;
                        border-bottom: 1px solid <?echo Utils::formatColor($style->column1Style->windowBorderColor);?> !important;
                    }
                <? endif; ?>

                <? if ($style->column2Style->enabled): ?>
                    <?if($style->verticalBorderStretch):?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column1
                        {
                            padding: 0 <? echo $style->column2Style->padding.'px';?> 0 <? echo $style->column2Style->padding.'px';?>;
                        }
                    <? else: ?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column1 .page-column-inner
                        {
                            padding: 0 <? echo $style->column2Style->padding.'px';?> 0 <? echo $style->column2Style->padding.'px';?>;
                        }
                    <? endif; ?>

                    <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column1 .folder-body {
                        margin-bottom: <? echo ($style->column2Style->padding*0.6).'px';?>;
                        border-bottom: 1px solid <?echo Utils::formatColor($style->column2Style->windowBorderColor);?> !important;
                    }
                <? endif; ?>

                <? if ($style->column3Style->enabled): ?>
                    <?if($style->verticalBorderStretch):?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column2
                        {
                            padding: 0 <? echo $style->column3Style->padding.'px';?> 0 <? echo $style->column3Style->padding.'px';?>;
                        }
                    <? else: ?>
                        <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column2 .page-column-inner
                        {
                            padding: 0 <? echo $style->column3Style->padding.'px';?> 0 <? echo $style->column3Style->padding.'px';?>;
                        }
                    <? endif; ?>

                    <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column2 .folder-body
                    {
                        margin-bottom: <? echo ($style->column3Style->padding*0.6).'px';?>;
                        border-bottom: 1px solid <?echo Utils::formatColor($style->column3Style->windowBorderColor);?> !important;
                    }
                <? endif; ?>
        <? endif; ?>
    </style>
	<? if (isset($libraryPage->columns) && $libraryPage->enableColumns && !$style->columnStyleEnabled && $screenSizeType == 'large'): ?>
		<div class="header-container">
            <div>
                <? for ($i = 0; $i < $totalColumnCount; $i++): ?>
                    <? if (isset($libraryPage->columns[$i])): ?>
                        <? $column = $libraryPage->columns[$i]; ?>
                        <div class="column-header-container column-header-container-<? echo $i; ?>"
                             style="font-family: <? echo FontReplacementHelper::replaceFont(isset($column->banner) && $column->banner->isEnabled ? $column->banner->font->name : $column->font->name); ?>;
                                 font-size: <? echo isset($column->banner) && $column->banner->isEnabled ? $column->banner->font->size->single : $column->font->size->single; ?>pt;
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
		</div>
	<? endif; ?>
	<div class="content-container">
		<div class="content-columns-container">
			<div class="page-column-outer">
				<? for ($i = 0; $i < $totalColumnCount; $i++): ?>
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