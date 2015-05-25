<?
	/**
	 * @var $object FolderRecord
	 */
?>
<div class="link-viewer-special tool-dialog">
	<div class="title">
		<div class="link-name">
			<?php echo $object->name; ?>
		</div>
		<br>
	</div>
	<? $logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images'. DIRECTORY_SEPARATOR . 'preview' . DIRECTORY_SEPARATOR . 'special-dialog'; ?>
	<ul class="nav nav-pills nav-stacked format-list">
		<li class="single-column">
			<a id="context-add" class="item-text" href="#"><img class="item-image" src="<?php echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'add-link.png')); ?>"/> Add All Links in this window to quickSITES cart
			</a>
			<div class="service-data">
				<div class="object-id"><?php echo $object->id; ?></div>
			</div>
		</li>
	</ul>
</div>