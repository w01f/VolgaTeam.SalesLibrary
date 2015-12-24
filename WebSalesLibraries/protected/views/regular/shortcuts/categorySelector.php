<?
	/**
	 * @var $categoryManager CategoryManager
	 */
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
?>
<div class="tag-condition-selector" data-log-group="Shortcut Tile" data-log-action="Search Bar">
	<div class="tool-dialog">
		<div class="group-panel">
			<button type="button" class="btn btn-default btn-block log-action tags-clear-all">Clear All <? echo $tagsName; ?></button>
			<br>
			<div class="group-title">What Are You Looking For?</div>
			<? if (isset($categoryManager->superFilters)): ?>
				<div class="btn-group btn-group-justified super-filter-list">
					<? foreach ($categoryManager->superFilters as $superFilter): ?>
						<div class="btn-group">
							<button type="button" class="btn btn-default log-action"><? echo $superFilter->value; ?></button>
						</div>
					<? endforeach; ?>
				</div>
			<? endif; ?>
		</div>
		<? if (isset($categoryManager->groups)): ?>
			<div class="tag-list-container" style="height: 400px; overflow: auto">
				<div class="accordion tag-list">
					<? foreach ($categoryManager->groups as $group): ?>
						<h3><span><? echo $group; ?></span></h3>
						<div class="checkbox  group-checkbox">
							<label class="group-selector-container"> <input class="group-selector log-action" type="checkbox">
								<span class="name"><? echo $group; ?></span> </label>
							<? foreach ($categoryManager->getTagsByGroup($group) as $tag): ?>
								<div class="checkbox">
									<label class="tag-selector-container"> <input type="checkbox" class="tag-selector log-action"> <span class="name"><? echo $tag['tag']; ?></span> </label>
								</div>
							<? endforeach; ?>
						</div>
					<? endforeach; ?>
				</div>
			</div>
		<? endif ?>
		<div class="buttons-area">
			<button class="btn btn-default log-action accept-button" type="button">OK</button>
			<button class="btn btn-default log-action cancel-button" type="button">Cancel</button>
		</div>
	</div>
</div>