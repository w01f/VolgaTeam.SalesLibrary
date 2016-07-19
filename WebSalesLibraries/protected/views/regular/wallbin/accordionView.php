<?
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/** @var $libraryPage LibraryPage */
?>
<div class="page-container" id="page-<? echo $libraryPage->id; ?>">
	<div class="content-container">
		<div class="content-columns-container">
			<? for ($i = 0; $i < 3; $i++): ?>
				<? $folders = $libraryPage->getFoldersByColumn($i); ?>
				<? if (isset($folders)): ?>
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
