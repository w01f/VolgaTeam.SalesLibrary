<?
	$categories = new CategoryManager();
	$categories->loadCategories();
?>
<div class="row">
	<div class="col-xs-12">
		<button type="button" class="btn btn-default btn-block" id="search-filter-edit-clear-all">Clear All <? echo Yii::app()->params['tags']['tab_name']; ?></button>
	</div>
</div>
<br>
<div class="row">
	<div class="col-xs-12">
		<div class="accordion tag-list" id="search-filter-edit-categories">
			<? foreach ($categories->groups as $group): ?>
				<h3><span><? echo $group; ?></span></h3>
				<div class="checkbox group-checkbox">
					<label class="group-selector-container"> <input class="group-selector" type="checkbox">
						<span class="name"><? echo $group; ?></span> </label>
					<? foreach ($categories->getTagsByGroup($group) as $tag): ?>
						<div class="checkbox">
							<label class="tag-selector-container"> <input type="checkbox" class="tag-selector"> <span class="name"><? echo $tag['tag']; ?></span> </label>
						</div>
					<? endforeach; ?>
				</div>
			<? endforeach; ?>
		</div>
	</div>
</div>