<? if (isset($pageShortcuts)): ?>
	<? foreach ($pageShortcuts as $pageShortcut): ?>
		<? if ($pageShortcut->isEnabled(Yii::app()->user->login)): ?>
			<option value="<? echo $pageShortcut->id; ?>"><? echo $pageShortcut->name; ?></option>
		<? endif; ?>
	<? endforeach; ?>
<? endif; ?>
