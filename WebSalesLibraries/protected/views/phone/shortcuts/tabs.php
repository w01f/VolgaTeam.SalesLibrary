<? /** @var $tabShortcuts ShortcutsTabRecord[] */ ?>
<? foreach ($tabShortcuts as $tabShortcut): ?>
	<option value="<? echo $tabShortcut->id; ?>"><? echo $tabShortcut->name; ?></option>
<? endforeach; ?>
