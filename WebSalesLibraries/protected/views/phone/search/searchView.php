<input type="search" name="search" id="search-keyword" value=""/>
<fieldset id="search-match-selector" data-role="controlgroup" data-mini="true">
	<input type="radio" name="radio-choice" id="search-match-exact" value="choice-1"/>
	<label for="search-match-exact">Exact Match</label>
	<input type="radio" name="radio-choice" id="search-match-partial" value="choice-1"/>
	<label for="search-match-partial">Partial Match</label>
</fieldset>
<fieldset id="search-fields-options-container" data-role="controlgroup" data-mini="true">
	<input type="radio" name="radio-choice" id="content-full" value="choice-1"/>
	<label for="content-full">Total Keyword Database Search</label>
	<input type="radio" name="radio-choice" id="content-only-file" value="choice-2"/>
	<label for="content-only-file">Search File Names Only</label>
	<input type="radio" name="radio-choice" id="content-only-text" value="choice-2"/>
	<label for="content-only-text">Search Only the TEXT in each file</label>
</fieldset>
<?if (Yii::app()->params['search_options']['hide_duplicate']): ?>
	<input type="checkbox" name="hide-duplicated" id="hide-duplicated" class="custom" data-mini="true"/>
	<label for="hide-duplicated">Hide Duplicate Files</label>
<? endif;?>
<? if (Yii::app()->params['search_full_tab']['show_money_button']): ?>
	<input type="checkbox" name="search-only-filecards" id="search-only-filecards" class="custom" data-mini="true"/>
	<label for="search-only-filecards">Show Me the Money!</label>
<? endif;?>