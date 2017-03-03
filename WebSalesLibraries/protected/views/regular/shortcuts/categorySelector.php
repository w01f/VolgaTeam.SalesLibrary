<?
	/**
	 * @var $categoryManager CategoryManager
	 */
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
?>
<div class="tag-condition-selector search-bar-modal" data-log-group="Shortcut Tile" data-log-action="Search Bar">
    <div class="row">
        <div class="col-xs-12">
            <h3>
                <img src="data:image/png;base64,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"
                     style="height: 48px;">
                What Are You Looking For?
            </h3>
        </div>
    </div>
	<? if (count($categoryManager->superFilters) > 0): ?>
        <div class="row">
            <div class="col-xs-12">
                <div class="btn-group btn-group-justified super-filter-list">
					<? foreach ($categoryManager->superFilters as $superFilter): ?>
                        <div class="btn-group">
                            <button type="button"
                                    class="btn btn-default log-action"><? echo $superFilter->value; ?></button>
                        </div>
					<? endforeach; ?>
                </div>
            </div>
        </div>
	<? endif; ?>
    <div class="row">
        <div class="col-xs-12">
            <h4>Content Groups:</h4>
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
            <h4>Category Tags:</h4>
            <div class="category-list-container" style="height: 300px; overflow: auto;">
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
    </div>
    <div class="row buttons-area">
        <div class="col-xs-4">
            <button type="button" class="btn btn-default btn-block log-action tags-clear-all">Clear
                All <? echo $tagsName; ?></button>
        </div>
        <div class="col-xs-1">
        </div>
        <div class="col-xs-3">
            <button class="btn btn-default btn-block log-action accept-button" type="button">OK</button>
        </div>
        <div class="col-xs-1">
        </div>
        <div class="col-xs-3">
            <button class="btn btn-default btn-block log-action cancel-button" type="button">Cancel</button>
        </div>
    </div>
</div>