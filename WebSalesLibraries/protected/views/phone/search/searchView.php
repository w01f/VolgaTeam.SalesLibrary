<input type="search" name="search" id="search-keyword" value=""/>
<table class="layout-group">
	<tr>
		<td class="on-center">
			<div id="search-match-selector" data-role="navbar">
				<ul>
					<li><a href="#" id="search-match-exact" data-corners="true" data-shadow="true">Exact</a></li>
					<li><a href="#" id="search-match-partial" data-corners="true" data-shadow="true">Partial</a></li>
				</ul>
			</div>
		</td>
	</tr>
	<? if (Yii::app()->params['search_full_tab']['show_money_button']): ?>
	<tr>
		<td class="on-center">
			<input type="checkbox" name="search-only-filecards" id="search-only-filecards" class="custom"
				   data-mini="true"/>
			<label for="search-only-filecards">Show Me the Money!</label>
		</td>
	</tr>
	<? endif;?>
	<?if (Yii::app()->params['search_options']['hide_duplicate']): ?>
	<tr>
		<td class="on-center">
			<input type="checkbox" name="hide-duplicated" id="hide-duplicated" class="custom" data-mini="true"/>
			<label for="hide-duplicated">Hide Duplicate Files</label>
		</td>
	</tr>
	<? endif;?>
</table>