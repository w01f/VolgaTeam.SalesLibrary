<fieldset data-role="controlgroup" data-mini="true">
	<input type="radio" name="radio-choice" id="search-date-file" value="choice-1"/>
	<label for="search-date-file">Search by Date File was Created</label>
	<input type="radio" name="radio-choice" id="search-date-link" value="choice-2"/>
	<label for="search-date-link">Search by Date File was Uploaded</label>
</fieldset>
<table class="layout-group">
	<tr>
		<td class="on-left">Start Date:</td>
	</tr>
	<tr>
		<td class="on-left">
			<input id="search-date-start" name="search-date-start" type="text" value="" data-mini="true"/>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="on-left">End Date:</td>
	</tr>
	<tr>
		<td colspan="2" class="on-left">
			<input id="search-date-end" name="search-date-end" type="text" value="" data-mini="true"/>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="on-center">
			<a id="search-clear-date-button" href="#" data-role="button" data-mini="true" data-icon="delete"
			   data-inline="true" data-theme="b">Clear Dates</a>
		</td>
	</tr>
</table>
