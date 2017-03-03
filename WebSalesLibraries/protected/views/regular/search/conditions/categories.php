<?
	$categoryManager = new CategoryManager();
	$categoryManager->loadCategories();

	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
?>
<div class="row">
	<div class="col-xs-12">
		<button type="button" class="btn btn-default btn-block log-action" id="search-filter-edit-clear-all">Clear All <? echo $tagsName; ?></button>
	</div>
</div>
<div class="row">
    <div class="col-xs-12">
        <h4>Filter <? echo $tagsName; ?>:</h4>
        <ul class="category-filter-list nav nav-pills">
			<? foreach ($categoryManager->groups as $group): ?>
                <li>
                    <a href="#" onfocus="this.blur();"><? echo $group; ?></a>
                </li>
			<? endforeach; ?>
        </ul>
    </div>
</div>
<div class="row">
	<div class="col-xs-12">
		<div class="accordion category-list">
			<? foreach ($categoryManager->groups as $group): ?>
				<? foreach ($categoryManager->getCategoriesByGroup($group) as $category): ?>
                    <h3 class="category-item-header" data-category="<? echo $group; ?>">
                        <span><? echo $category; ?></span></h3>
                    <div class="checkbox  group-checkbox">
                        <label class="group-selector-container">
                            <input class="group-selector log-action" type="checkbox">
                            <span class="name"><? echo $category; ?></span>
                        </label>
						<? foreach ($categoryManager->getTagsByCategory($category) as $tag): ?>
                            <div class="checkbox">
                                <label class="tag-selector-container">
                                    <input type="checkbox" class="tag-selector log-action">
                                    <span class="name"><? echo $tag['tag']; ?></span>
                                </label>
                            </div>
						<? endforeach; ?>
                    </div>
				<? endforeach; ?>
			<? endforeach; ?>
		</div>
	</div>
</div>