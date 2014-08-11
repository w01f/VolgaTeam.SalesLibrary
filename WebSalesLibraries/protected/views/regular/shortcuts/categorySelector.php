<?
	/**
	 * @var $categoryManager CategoryManager
	 */
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
?>
?>
<div class="tag-condition-selector">
	<div class="tool-dialog">
		<div class="group-panel">
			<button type="button" class="btn btn-default btn-block tags-clear-all">Clear All <? echo $tagsName; ?></button>
			<br>
			<div class="group-title">What Are You Looking For?</div>
			<? if (isset($categoryManager->superFilters)): ?>
				<div class="btn-group btn-group-justified super-filter-list">
					<? foreach ($categoryManager->superFilters as $superFilter): ?>
						<div class="btn-group">
							<button type="button" class="btn btn-default"><? echo $superFilter->value; ?></button>
						</div>
					<? endforeach; ?>
				</div>
			<? endif; ?>
		</div>
		<? if (isset($categoryManager->groups)): ?>
			<div class="tag-list-container" style="height: 400px">
				<div class="accordion tag-list">
					<? foreach ($categoryManager->groups as $group): ?>
						<h3><span><? echo $group; ?></span></h3>
						<div class="checkbox">
							<label class="group-selector-title"> <input class="group-selector" type="checkbox">
								<? echo $group; ?>
							</label>
							<? foreach ($categoryManager->getTagsByGroup($group) as $tag): ?>
								<div class="checkbox">
									<label>
										<input class="item-selector" type="checkbox" value="<? echo $group . '------' . $tag['tag']; ?>">
										<? echo $tag['tag']; ?>
									</label>
								</div>
							<? endforeach; ?>
						</div>
					<? endforeach; ?>
				</div>
			</div>
		<? endif ?>
		<div class="buttons-area">
			<button class="btn btn-default accept-button" type="button">OK</button>
			<button class="btn btn-default cancel-button" type="button">Cancel</button>
		</div>
	</div>
</div>