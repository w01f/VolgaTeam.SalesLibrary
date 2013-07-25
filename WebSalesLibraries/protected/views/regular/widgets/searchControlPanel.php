<div id="search-control-panel">
	<ul>
		<li><a href="#search-options-basic">Keyword</a></li>
		<li><a href="#search-options-tags">Tag</a></li>
		<li><a href="#search-options-files">File</a></li>
		<li><a href="#search-options-date">Date</a></li>
		<li><a href="#search-options-stations"><?php echo Yii::app()->params['stations']['tab_name']; ?></a></li>
	</ul>
	<div id="search-options-basic">
		<div class="group-panel">
			<button type="button" class="btn btn-block" id="clear-all-content-value">Clear Keyword Settings</button>
		</div>
		<br>
		<div class="group-title">What Are You Looking For?</div>
		<table class="button-edit input-append">
			<tr>
				<td class="editor"><input type="text" id="condition-content-value" placeholder="Type Here..."></td>
				<td class="buttons">
					<a class="btn" id="clear-content-value" href="#"><i class="icon-remove-sign"/></a>
				</td>
			</tr>
		</table>
		<br> <br>
		<div class="group-panel">
			<div class="group-title">Search Options:</div>
			<div class="btn-group centered" id="content-compare-type">
				<button type="button" class="btn" id="content-compare-exact" style="width: 140px;">Exact Match</button>
				<button type="button" class="btn" id="content-compare-partial" style="width: 140px;">Partial Match</button>
			</div>
			<div class="btn-group centered">
				<?if (Yii::app()->params['search_options']['hide_duplicate']): ?>
					<button type="button" class="btn" id="hide-duplicated" style="width: 140px;">Hide Duplicates</button>
				<? endif;?>
				<button type="button" class="btn search-fields-option" id="content-only-file" style="width: 140px;">File Names Only</button>
			</div>
			<div class="btn-group centered">
				<button type="button" class="btn search-fields-option" id="content-only-text" style="width: 140px;">File Content Only</button>
				<button type="button" class="btn search-fields-option" id="content-full" style="width: 140px;">Full database</button>
			</div>
		</div>
	</div>
	<div id="search-options-tags">
		<?php if (isset($categories->groups) && Yii::app()->params['tags']['visible']): ?>
			<div class="group-panel">
				<button type="button" class="btn btn-block" id="tags-clear-all">Clear All Tags</button>
				<br>
				<div class="group-title">What Are You Looking For?</div>
				<?php if (isset($categories->superFilters)): ?>
					<div class="btn-group centered" id="super-filter-list">
						<?php foreach ($categories->superFilters as $superFilter): ?>
							<button type="button" class="btn<? if ($superFilter->selected): ?> active<? endif; ?>"><?echo $superFilter->value;?></button>
						<?php endforeach; ?>
					</div>
				<?php endif; ?>
			</div>
			<div id="categories-container">
				<div class="accordion" id="categories">
					<?php foreach ($categories->groups as $group): ?>
						<h3><span><?php echo $group; ?></span></h3>
						<div>
							<?php foreach ($categories->getTagsByGroup($group) as $tag): ?>
								<label class="checkbox">
									<input type="checkbox" value="<?php echo $group . '------' . $tag['tag']; ?>" <?php echo $tag['selected'] ? 'checked="checked"' : '' ?>>
									<?php echo $tag['tag']; ?>
								</label>
							<?php endforeach; ?>
						</div>
					<?php endforeach; ?>
				</div>
			</div>
		<?php endif; ?>
	</div>
	<div id="search-options-files">
		<div id="file-types-container">
			<div class="group-panel">
				<div class="group-title">What Are You Looking For?</div>
				<table id="file-types">
					<tr>
						<td>
							<button class="search-file-type btn btn-block" id="search-file-type-powerpoint">
								<table class="caption">
									<tr>
										<td><img class="icon-search powerpoint"
												 src="images/search/search-powerpoint.png"/></td>
										<td><h4>PowerPoint</h4></td>
									</tr>
								</table>
							</button>
						</td>
					</tr>
					<tr>
						<td>
							<button class="search-file-type btn btn-block" id="search-file-type-video">
								<table class="caption">
									<tr>
										<td><img class="icon-search video" src="images/search/search-video.png"/></td>
										<td><h4>Video</h4></td>
									</tr>
								</table>
							</button>
						</td>
					</tr>
					<tr>
						<td>
							<button class="search-file-type btn btn-block" id="search-file-type-pdf">
								<table class="caption">
									<tr>
										<td><img class="icon-search video" src="images/search/search-pdf.png"/></td>
										<td><h4>PDF</h4></td>
									</tr>
								</table>
							</button>
						</td>
					</tr>
					<tr>
						<td>
							<button class="search-file-type btn btn-block" id="search-file-type-word">
								<table class="caption">
									<tr>
										<td><img class="icon-search word" src="images/search/search-word.png"/></td>
										<td><h4>Word</h4></td>
									</tr>
								</table>
							</button>
						</td>
					</tr>
					<tr>
						<td>
							<button class="search-file-type btn btn-block" id="search-file-type-excel">
								<table class="caption">
									<tr>
										<td><img class="icon-search excel" src="images/search/search-excel.png"/></td>
										<td><h4>Excel</h4></td>
									</tr>
								</table>
							</button>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</div>
	<div id="search-options-date">
		<div class="group-panel">
			<button type="button" class="btn btn-block" id="clear-date-value">Clear Date Settings</button>
		</div>
		<br>
		<div class="group-panel" id="condition-date-panel">
			<div class="group-title">Date Range:</div>
			<table class="button-edit input-append" id="condition-date-range">
				<tr>
					<td class="editor"><input type="text" readonly placeholder="Select Date Range..."></td>
					<td class="buttons">
						<a class="btn" id="select-date-range" href="#"><i class="icon-calendar"/></a> <a class="btn"
																										 id="clear-date-range"
																										 href="#"><i
								class="icon-remove-sign"/></a>
					</td>
				</tr>
			</table>
			<br><br>
			<div class="group-panel">
				<div class="group-title">Search Options:</div>
				<div class="btn-group centered">
					<button type="button" class="btn" id="condition-date-link">Date uploaded</button>
					<button type="button" class="btn" id="condition-date-file">Date created</button>
				</div>
			</div>
		</div>
	</div>
	<div id="search-options-stations">
		<div class="group-panel">
			<button type="button" class="btn btn-block" id="library-select-all">Select All</button>
			<button type="button" class="btn btn-block" id="library-clear-all">Clear All</button>
		</div>
		<div id="libraries-container">
			<div class="accordion" id="libraries">
				<?php foreach ($libraryGroups as $group): ?>
					<h3><span><?php echo $group->name; ?></span></h3>
					<div>
						<?php foreach ($group->libraries as $library): ?>
							<label class="checkbox"> <input type="checkbox"
															value="<?php echo $library->id; ?>" <?php echo $library->selected ? 'checked="checked"' : '' ?>>
								<?php echo $library->name; ?>
							</label>
						<?php endforeach; ?>
					</div>
				<?php endforeach; ?>
			</div>
		</div>
	</div>
</div>

