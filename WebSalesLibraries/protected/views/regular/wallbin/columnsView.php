<?
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/** @var $libraryPage LibraryPage */
?>
<div class="page-container" id="page-<? echo $libraryPage->id; ?>">
	<? if (isset($libraryPage->columns) && $libraryPage->enableColumns): ?>
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
												<? echo $column->banner->text; ?>
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
			<? for ($i = 0; $i < 3; $i++): ?>
				<? $folders = $libraryPage->getFoldersByColumn($i); ?>
				<? if (isset($folders)): ?>
					<div class="page-column column<? echo $i; ?>">
						<?
							foreach ($folders as $folder)
								echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderContainer.php', array('folder' => $folder), true);
						?>
					</div>
				<? endif; ?>
			<? endfor; ?>
		</div>
	</div>
</div>