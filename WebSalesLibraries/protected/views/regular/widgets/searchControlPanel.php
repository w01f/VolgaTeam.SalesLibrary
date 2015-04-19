<?
	/**
	 * @var $categories CategoryManager
	 * @var $libraryGroups array
	 */
?>
<div id="search-control-panel">
<ul>
	<li><a href="#search-options-basic">Keyword</a></li>
	<li><a href="#search-options-tags"><? echo Yii::app()->params['tags']['tab_name']; ?></a></li>
	<li><a href="#search-options-files">File</a></li>
	<li><a href="#search-options-date">Date</a></li>
	<li><a href="#search-options-stations"><? echo Yii::app()->params['stations']['tab_name']; ?></a></li>
</ul>
<div id="search-options-basic">
	<div class="group-panel">
		<button type="button" class="btn btn-default btn-block" id="clear-all-content-value">Clear Keyword Settings</button>
	</div>
	<br>
	<div class="group-title">What Are You Looking For?</div>
	<div class="input-group search-input-container group-panel">
		<input type="text" class="form-control search-bar-text" id="condition-content-value" placeholder="Type Here...">
	  <span class="input-group-btn">
		<button id="clear-content-value" class="btn btn-default search-bar-run" type="button">
			<span class="glyphicon glyphicon-remove"></span>
		</button>
	  </span>
	</div>
	<br> <br>
	<div class="group-panel">
		<div class="group-title">Search Options:</div>
		<div class="row" id="content-compare-type">
			<div class="col-xs-5">
				<button type="button" class="btn btn-default btn-block" id="content-compare-exact">Exact Match</button>
			</div>
			<div class="col-xs-5 col-xs-offset-1">
				<button type="button" class="btn btn-default btn-block" id="content-compare-partial">Partial Match</button>
			</div>
		</div>
		<div class="row">
			<? if (Yii::app()->params['search_options']['hide_duplicate']): ?>
				<div class="col-xs-5">
					<button type="button" class="btn btn-default btn-block" id="hide-duplicated">Hide Duplicates</button>
				</div>
			<? endif; ?>
			<div class="col-xs-5 col-xs-offset-1">
				<button type="button" class="btn btn-default btn-block search-fields-option" id="content-only-file">File Names Only</button>
			</div>
		</div>
		<div class="row">
			<div class="col-xs-5">
				<button type="button" class="btn btn-default btn-block search-fields-option" id="content-only-text">File Content Only</button>
			</div>
			<div class="col-xs-5 col-xs-offset-1">
				<button type="button" class="btn btn-default btn-block search-fields-option" id="content-full">Full database</button>
			</div>
		</div>
	</div>
</div>
<div id="search-options-tags">
	<? if (isset($categories->groups) && Yii::app()->params['tags']['visible']): ?>
		<div class="group-panel">
			<?
				$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
				$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
			?>
			<button type="button" class="btn btn-default btn-block tags-clear-all">Clear All <? echo $tagsName; ?></button>
			<br>
			<div class="group-title">What Are You Looking For?</div>
			<? if (isset($categories->superFilters)): ?>
				<div class="btn-group btn-group-justified super-filter-list">
					<? foreach ($categories->superFilters as $superFilter): ?>
						<div class="btn-group">
							<button type="button" class="btn btn-default<? if ($superFilter->selected): ?> active<? endif; ?>"><? echo $superFilter->value; ?></button>
						</div>
					<? endforeach; ?>
				</div>
			<? endif; ?>
		</div>
		<div class="tag-list-container">
			<div class="accordion tag-list">
				<? foreach ($categories->groups as $group): ?>
					<h3><span><? echo $group; ?></span></h3>
					<div class="checkbox">
						<label class="group-selector-container">
							<input class="group-selector" type="checkbox" <? echo $categories->isGroupSelected($group) ? 'checked="checked"' : '' ?>>
							<? echo $group; ?>
						</label>
						<? foreach ($categories->getTagsByGroup($group) as $tag): ?>
							<div class="checkbox">
								<label>
									<input class="item-selector" type="checkbox" value="<? echo $group . '------' . $tag['tag']; ?>" <? echo $tag['selected'] ? 'checked="checked"' : '' ?>>
									<? echo $tag['tag']; ?>
								</label>
							</div>
						<? endforeach; ?>
					</div>
				<? endforeach; ?>
			</div>
		</div>
	<? endif; ?>
</div>
<div id="search-options-files">
	<div id="file-types-container">
		<div class="group-panel">
			<div class="group-title">What Are You Looking For?</div>
			<div id="file-types">
				<button class="search-file-type btn btn-default btn-block" id="search-file-type-powerpoint">
					<table class="caption">
						<tr>
							<td><img class="icon-search powerpoint"
									 src="images/search/search-powerpoint.png"/></td>
							<td><h4>PowerPoint</h4></td>
						</tr>
					</table>
				</button>
				<button class="search-file-type btn btn-default  btn-block" id="search-file-type-video">
					<table class="caption">
						<tr>
							<td><img class="icon-search video" src="images/search/search-video.png"/></td>
							<td><h4>Video</h4></td>
						</tr>
					</table>
				</button>
				<button class="search-file-type btn btn-default btn-block" id="search-file-type-pdf">
					<table class="caption">
						<tr>
							<td><img class="icon-search video" src="images/search/search-pdf.png"/></td>
							<td><h4>PDF</h4></td>
						</tr>
					</table>
				</button>
				<button class="search-file-type btn btn-default btn-block" id="search-file-type-word">
					<table class="caption">
						<tr>
							<td><img class="icon-search word" src="images/search/search-word.png"/></td>
							<td><h4>Word</h4></td>
						</tr>
					</table>
				</button>
				<button class="search-file-type btn btn-default btn-block" id="search-file-type-excel">
					<table class="caption">
						<tr>
							<td><img class="icon-search excel" src="images/search/search-excel.png"/></td>
							<td><h4>Excel</h4></td>
						</tr>
					</table>
				</button>
				<button class="search-file-type btn btn-default btn-block" id="search-file-type-url">
					<table class="caption">
						<tr>
							<td><img class="icon-search url" src="images/search/search-url.png"/></td>
							<td><h4>Web Links</h4></td>
						</tr>
					</table>
				</button>
				<button class="search-file-type btn btn-default btn-block" id="search-file-type-image">
					<table class="caption">
						<tr>
							<td><img class="icon-search image" src="images/search/search-image.png"/></td>
							<td><h4>Images</h4></td>
						</tr>
					</table>
				</button>
			</div>
		</div>
	</div>
</div>
<div id="search-options-date">
	<div class="group-panel">
		<button type="button" class="btn btn-default btn-block" id="clear-date-value">Clear Date Settings</button>
	</div>
	<br>
	<div class="group-panel" id="condition-date-panel">
		<div class="group-title">Date Range:</div>
		<div class="input-group" id="condition-date-range">
			<input class="form-control" type="text" readonly placeholder="Select Date Range...">
			<div class="input-group-btn">
				<button id="select-date-range" class="btn btn-default" type="button">
					<span class="glyphicon glyphicon-calendar"></span></button>
				<button id="clear-date-range" class="btn btn-default" type="button">
					<span class="glyphicon glyphicon-remove"></span></button>
			</div>
		</div>
		<? if (!Yii::app()->params['search_options']['hide_date_options']): ?>
			<br><br>
			<div class="group-panel">
				<div class="group-title">Search Options:</div>
				<div class="row">
					<div class="col-xs-5">
						<button type="button" class="btn btn-default btn-block" id="condition-date-link">Date uploaded</button>
					</div>
					<div class="col-xs-5 col-xs-offset-1">
						<button type="button" class="btn btn-default btn-block" id="condition-date-file">Date created</button>
					</div>
				</div>
			</div>
		<? endif; ?>
	</div>
</div>
<div id="search-options-stations">
	<div class="group-panel">
		<button type="button" class="btn btn-default btn-block" id="library-select-all">Select All</button>
		<button type="button" class="btn btn-default btn-block" id="library-clear-all">Clear All</button>
	</div>
	<div id="libraries-container">
		<div class="accordion" id="libraries">
			<? foreach ($libraryGroups as $group): ?>
				<h3><span><? echo $group->name; ?></span></h3>
				<div>
					<? foreach ($group->libraries as $library): ?>
						<div class="checkbox">
							<label>
								<input type="checkbox" value="<? echo $library->id; ?>" <? echo $library->selected ? 'checked="checked"' : '' ?>>
								<? echo $library->name; ?>
							</label>
						</div>
					<? endforeach; ?>
				</div>
			<? endforeach; ?>
		</div>
	</div>
</div>
</div>

