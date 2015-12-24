<?
	$categories = new CategoryManager();
	$categories->loadCategories();
?>
	<p class="text-muted">Search for files with these special tags:</p>
<? if (isset($categories->superFilters)): ?>
	<? $count = count($categories->superFilters); ?>
	<? for ($i = 0; $i < $count; $i += 2): ?>
		<div class="row">
			<div class="col-xs-5 col-xs-offset-1">
				<div class="checkbox">
					<label><input class="log-action" type="checkbox"><span class="name"><? echo $categories->superFilters[$i]->value; ?></span>
					</label>
				</div>
			</div>
			<? if (($i + 1) < $count): ?>
				<div class="col-xs-5 col-xs-offset-1">
					<div class="checkbox">
						<label><input class="log-action" type="checkbox"><span class="name"><? echo $categories->superFilters[$i + 1]->value; ?></span>
						</label>
					</div>
				</div>
			<? endif; ?>
		</div>
	<? endfor; ?>
<? endif; ?>