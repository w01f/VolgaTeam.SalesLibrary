<div class="view-dialog-body tool-dialog">
	<div class="title">
		<div class="link-name">
			<?php echo $isLineBreak ? 'Line Break' : $object->name; ?>
		</div>
		<br>
		<?php if (isset($object->fileName) && $isLink && !(isset($object->isFolder) && $object->isFolder)): ?>
			<div class="description">
				<?php echo $object->fileName; ?>
			</div><br>
		<?php endif; ?>
	</div>
	<? $logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'special-dialog'; ?>
	<?if (isset($object->noShare) && $object->noShare): ?>
		<div class="warning">
			<br>This Link is RESTRICTED, and you are Unable to share this link on the web.<br><br>
		</div>
		<div class="buttons-area">
			<button class="btn accept-button" type="button" style="width: 120px;">OK</button>
		</div>
	<? else: ?>
		<ul class="nav nav-pills nav-stacked format-list">
			<li class="single-column">
				<a id="context-add" class="item-text" href="#"><img class="item-image" src="<?php echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'add-link.png')); ?>"/>
					<?if ($isLink): ?>Add
						<? if (isset($object->isFolder) && $object->isFolder): ?>
							Folder
						<?php elseif ($isLineBreak): ?>
							this LineBreak
						<?else:?>
							Link
						<?endif;?>
						to quickSITES cart
					<? else: ?>
						Add All Links in this window to quickSITES cart
					<?endif;?>
				</a>
				<div class="service-data">
					<div class="object-id"><?php echo $object->id; ?></div>
				</div>
			</li>
			<?if ($isLink && !$isLineBreak): ?>
				<li class="single-column">
					<a id="context-email" class="item-text" href="#"><img class="item-image" src="<?php echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'email-link.png')); ?>"/>Send this <? if (isset($object->isFolder) && $object->isFolder): ?>Folder Link<? else: ?>Link<?endif; ?>
					</a>
					<div class="service-data">
						<div class="object-id"><?php echo $object->id; ?></div>
						<div class="object-name"><?php echo $object->name; ?></div>
						<div class="object-file-name"><?php echo $object->fileName; ?></div>
						<div class="object-file-type"><?php echo $object->originalFormat; ?></div>
					</div>
				</li>
			<? endif;?>
			<li class="single-column">
				<a id="context-manager" class="item-text" href="<?php echo Yii::app()->createUrl('qbuilder'); ?>" target="_blank"><img class="item-image" src="<?php echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'manage-sites.png')); ?>"/>Manage my quickSITES</a>
			</li>
		</ul>
	<?endif;?>
</div>