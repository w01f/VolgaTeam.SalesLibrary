<? /** @var $libraryGroups LibraryGroup[] */ ?>
<? if (isset($libraryGroups)): ?>
	<table class="layout-group">
		<tr>
			<td class="on-left">
				<a id="search-libraries-select-button" href="#" data-role="button" data-mini="true" data-icon="check" data-inline="true" data-theme="b">Select All</a>
			</td>
			<td class="on-right">
				<a id="search-libraries-clear-button" href="#" data-role="button" data-mini="true" data-icon="delete" data-inline="true" data-theme="b">Clear All</a>
			</td>
		</tr>
	</table>
	<? if (count($libraryGroups) > 1): ?>
		<? foreach ($libraryGroups as $group): ?>
			<div class="search-libraries-group" data-role="collapsible" data-collapsed="true" data-inset="false">
				<h3>
					<? echo $group->name; ?>
				</h3>
				<fieldset data-role="controlgroup">
					<? foreach ($group->libraries as $library): ?>
						<input class="search-libraries-item" type="checkbox" name="<? echo $library->name; ?>" id="<? echo $library->id; ?>" class="custom" <? if ($library->selected) echo 'checked'; ?>/>
						<label for="<? echo $library->id; ?>"><? echo $library->name; ?></label>
					<? endforeach; ?>
				</fieldset>
			</div>
		<? endforeach; ?>
	<? else: ?>
		<fieldset data-role="controlgroup">
			<? foreach ($libraryGroups[0]->libraries as $library): ?>
				<input class="search-libraries-item" type="checkbox" name="<? echo $library->name; ?>" id="<? echo $library->id; ?>" class="custom" <? if ($library->selected) echo 'checked'; ?>/>
				<label for="<? echo $library->id; ?>"><? echo $library->name; ?></label>
			<? endforeach; ?>
		</fieldset>
	<? endif; ?>
<? endif; ?>