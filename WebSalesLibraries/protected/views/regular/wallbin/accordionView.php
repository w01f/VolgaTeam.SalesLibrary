<?
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/* @var $libraryPage LibraryPage
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
            <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column
            {
                width: 100%;
            }
        <?endif;?>

        <? if (isset($style->padding) && $style->padding->isConfigured): ?>
            <? if (isset($libraryPage->columns) && $libraryPage->enableColumns && !$style->columnStyleEnabled  && $screenSizeType == 'large'): ?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .header-container
                {
                    padding-top: <? echo $style->padding->top; ?>px !important;
                    padding-left: <? echo $style->padding->left; ?>px !important;
                    padding-right: <? echo $style->padding->right; ?>px !important;
                }
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .content-container
                {
                    padding-left: <? echo $style->padding->left; ?>px !important;
                    padding-bottom: <? echo $style->padding->bottom; ?>px !important;
                    padding-right: <? echo $style->padding->right; ?>px !important;
                }
            <?else:?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .content-container
                {
                    padding-top: <? echo $style->padding->top; ?>px !important;
                    padding-left: <? echo $style->padding->left; ?>px !important;
                    padding-bottom: <? echo $style->padding->bottom; ?>px !important;
                    padding-right: <? echo $style->padding->right; ?>px !important;
                }
            <?endif;?>
            <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0 .accordion-folder-container
            {
                margin-left: 0 !important;
            }
            <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column2 .accordion-folder-container
            {
                margin-right: 0 !important;
            }
        <?endif;?>
        <? if ($style->columnStyleEnabled): ?>
            <? if ($style->column1Style->enabled): ?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column0
                {
                    padding: 0 <? echo $style->column1Style->padding.'px';?> 0 <? echo $style->column1Style->padding.'px';?>;
                }
            <? endif; ?>

            <? if ($style->column2Style->enabled): ?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column1
                {
                    padding: 0 <? echo $style->column2Style->padding.'px';?> 0 <? echo $style->column2Style->padding.'px';?>;
                }
            <? endif; ?>

            <? if ($style->column3Style->enabled): ?>
                <? echo '#'.$containerId;?> #page-<? echo $libraryPage->id; ?> .page-column.column2
                {
                    padding: 0 <? echo $style->column3Style->padding.'px';?> 0 <? echo $style->column3Style->padding.'px';?>;
                }
            <? endif; ?>
        <?endif;?>
    </style>
    <div class="content-container">
        <div class="content-columns-container">
			<? for ($i = 0; $i < $totalColumnCount; $i++): ?>
				<? $folders = $foldersByColumns[$i]; ?>
				<? if (count($folders) > 0): ?>
                    <div class="page-column column<? echo $i; ?>">
						<? foreach ($folders as $folder): ?>
							<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/accordionFolder.php', array('folder' => $folder), true); ?>
						<? endforeach; ?>
                    </div>
				<? endif; ?>
			<? endfor; ?>
        </div>
    </div>
</div>
