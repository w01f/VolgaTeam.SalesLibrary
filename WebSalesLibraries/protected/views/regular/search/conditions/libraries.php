<?
	$libraryManager = new LibraryManager();
	$libraries = $libraryManager->getLibraries();
	$libraryGroups = $libraryManager->getLibraryGroups();
?>
<div class="row">
	<div class="col-xs-12">
		<button type="button" class="btn btn-default btn-block" id="search-filter-edit-select-all">Select All</button>
		<button type="button" class="btn btn-default btn-block" id="search-filter-edit-clear-all">Clear All</button>
	</div>
</div>
<br>
<div class="row">
	<div class="col-xs-12">
		<div class="accordion" id="search-filter-edit-libraries">
			<? foreach ($libraryGroups as $group): ?>
				<h3><span><? echo $group->name; ?></span></h3>
				<div>
					<? foreach ($group->libraries as $library): ?>
						<div class="checkbox">
							<label> <input type="checkbox" value="<? echo $library->id; ?>">
								<span class="name"><? echo $library->name; ?></span>
							</label>
						</div>
					<? endforeach; ?>
				</div>
			<? endforeach; ?>
		</div>
	</div>
</div>


