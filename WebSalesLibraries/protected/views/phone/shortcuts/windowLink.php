<div class="ui-block-a shortcuts-link window">
	<img src="<? echo $link->imagePath; ?>">
	<div class="service-data">
		<? $folder = $link->getWindow(); ?>
		<div class="folder-id"><?php echo $folder->id ?></div>
	</div>
</div>