<fieldset id="favorites-folders-list" data-role="controlgroup">
	<?php $i = 0; ?>
	<?php foreach ($folders as $folder): ?>
	<input type="radio" name="favorites-folder" id="favorites-folder<?php echo $i; ?>" class="favorites-folder" class="custom" value="<?php echo $folder; ?>"/>
	<label for="favorites-folder<?php echo $i; ?>"><?php echo $folder; ?></label>
	<?php $i++; ?>
	<?php endforeach; ?>
</fieldset>
<br>
<a id="favorites-folders-apply-button" href="#favorites-add" data-role="button" data-corners="true" data-shadow="true" data-transition="pop" data-direction="reverse" data-theme="b" data-icon="check">Apply</a>