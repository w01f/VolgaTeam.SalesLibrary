<? if (isset($pageShortcuts)): ?>
	<? foreach ($pageShortcuts as $pageShortcut): ?>
		<option value="<? echo $pageShortcut->id; ?>"><? echo $pageShortcut->name; ?></option>
	<? endforeach; ?>
<? endif; ?>
