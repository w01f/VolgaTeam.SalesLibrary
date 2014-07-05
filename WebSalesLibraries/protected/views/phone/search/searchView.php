<label for="search-keyword" class="ui-hide-label"></label>
<input type="search" name="search" id="search-keyword" value=""/>
<fieldset id="search-match-selector" data-role="controlgroup" data-mini="true">
	<input type="radio" name="radio-choice" id="search-match-exact" value="choice-1"/>
	<label for="search-match-exact">Exact Match</label>
	<input type="radio" name="radio-choice" id="search-match-partial" value="choice-1"/>
	<label for="search-match-partial">Partial Match</label>
</fieldset>
<? if (Yii::app()->params['search_options']['hide_duplicate']): ?>
	<input type="checkbox" name="hide-duplicated" id="hide-duplicated" class="custom" data-mini="true"/>
	<label for="hide-duplicated">Hide Duplicates</label>
<? endif; ?>
<fieldset id="search-fields-options-container" data-role="controlgroup" data-mini="true">
	<input type="radio" name="radio-choice" id="content-only-file" value="choice-1"/>
	<label for="content-only-file">File Names Only</label>
	<input type="radio" name="radio-choice" id="content-only-text" value="choice-2"/>
	<label for="content-only-text">File Content Only</label>
	<input type="radio" name="radio-choice" id="content-full" value="choice-3"/>
	<label for="content-full">Full Database</label>
</fieldset>