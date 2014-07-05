<? /** @var $folders array */ ?>
<fieldset id="favorites-folders-list" data-role="controlgroup">
	<? $i = 0; ?>
	<? foreach ($folders as $folder): ?>
		<input type="radio" name="favorites-folder" id="favorites-folder<? echo $i; ?>" class="custom favorites-folder" value="<? echo $folder; ?>"/>
		<label for="favorites-folder<? echo $i; ?>"><? echo $folder; ?></label>
		<? $i++; ?>
	<? endforeach; ?>
</fieldset>
<br>
<a id="favorites-folders-apply-button" href="#favorites-add" data-role="button" data-corners="true" data-shadow="true" data-transition="pop" data-direction="reverse" data-theme="b" data-icon="check">Apply</a>