<div class="view-dialog-body tool-dialog">
	<div class="title">
		<div class="link-name">
			<?php echo $object->name; ?>
		</div>
		<br>
		<?php if (isset($object->fileName) && $isLink && !(isset($object->isFolder) && $object->isFolder)): ?>
			<div class="description">
				<?php echo $object->fileName; ?>
			</div><br>
		<?php endif; ?>
	</div>
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
				<a id="context-add" class="item-text" href="#"><img class="item-image" src="<?php echo Yii::app()->baseUrl . '/images/special-dialog/add-link.png'; ?>"/><?if ($isLink): ?>Add <? if (isset($object->isFolder) && $object->isFolder): ?>Folder<? else: ?>Link<?endif; ?> to quickSITES cart<? else: ?>Add All Links in this window to quickSITES cart<?endif;?>
				</a>
				<div class="service-data">
					<div class="object-id"><?php echo $object->id; ?></div>
				</div>
			</li>
			<?if ($isLink): ?>
				<li class="single-column">
					<a id="context-email" class="item-text" href="#"><img class="item-image" src="<?php echo Yii::app()->baseUrl . '/images/special-dialog/email-link.png'; ?>"/>Send this <? if (isset($object->isFolder) && $object->isFolder): ?>Folder Link<? else: ?>Link<?endif; ?>
					</a>
					<div class="service-data">
						<div class="object-id"><?php echo $object->id; ?></div>
						<div class="object-name"><?php echo $object->name; ?></div>
					</div>
				</li>
			<? endif;?>
			<li class="single-column">
				<a id="context-manager" class="item-text" href="<?php echo Yii::app()->createUrl('qbuilder'); ?>" target="_blank"><img class="item-image" src="<?php echo Yii::app()->baseUrl . '/images/special-dialog/manage-sites.png'; ?>"/>Manage my quickSITES</a>
			</li>
		</ul>
	<?endif;?>
</div>