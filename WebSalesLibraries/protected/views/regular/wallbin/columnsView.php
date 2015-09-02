<? /** @var $libraryPage LibraryPage */ ?>
<div class="page-container" id="page-<? echo $libraryPage->id; ?>" oncontextmenu="return false;">
	<? if (isset($libraryPage->columns) && $libraryPage->enableColumns): ?>
		<div class="header-container">
			<? for ($i = 0; $i < 3; $i++): ?>
				<? if (isset($libraryPage->columns[$i])): ?>
					<? $column = $libraryPage->columns[$i]; ?>
					<div class="column-header-container column-header-container-<? echo $i; ?>"
						 style="font-family: <? echo $column->font->name; ?>,serif;
							 font-size: <? echo $column->font->size; ?>pt;
							 font-weight: <? echo $column->font->isBold ? ' bold' : ' normal'; ?>;
							 font-style: <? echo $column->font->isItalic ? ' italic' : ' normal'; ?>;
							 text-align: <? echo $column->alignment; ?>;
							 background-color: <? echo $column->backColor; ?>;
							 color: <? echo $column->foreColor; ?>;">
						<? if (isset($column->banner) && $column->banner->isEnabled): ?>
							<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $column->banner, 'isLinkBanner' => false), true); ?>
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