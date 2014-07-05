<? /** @var $categories CategoryManager */ ?>
<? if (isset($categories) && Yii::app()->params['tags']['visible']): ?>
	<?
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
	?>
	<a id="search-tags-clear-button" href="#" data-role="button" data-mini="true" data-icon="delete" data-theme="b">Clear <? echo $tagsName; ?></a>
	<? if (isset($categories->superFilters)): ?>
		<fieldset id="super-filter-list" data-role="controlgroup">
			<? $i = 0; ?>
			<? foreach ($categories->superFilters as $superFilter): ?>
				<input type="checkbox" name="super-filter-item<? echo $i; ?>" id="super-filter-item<? echo $i; ?>" class="custom super-filter-item" <? if ($superFilter->selected): ?>checked<? endif; ?> value="<? echo $superFilter->value; ?>"/>
				<label for="super-filter-item<? echo $i; ?>"><? echo $superFilter->value; ?></label>
				<? $i++; ?>
			<? endforeach; ?>
		</fieldset>
	<? endif; ?>
	<? foreach ($categories->groups as $group): ?>
		<div class="search-tags-group" data-role="collapsible" data-collapsed="true" data-inset="false">
			<h3>
				<? echo $group; ?>
			</h3>
			<input class="custom search-tags-item group-selector" type="checkbox" name="<? echo $group . '-select-all'; ?>"
				   id="<? echo $group . '-select-all'; ?>"/>
			<label for="<? echo $group . '-select-all'; ?>"><? echo $group; ?></label>
			<fieldset data-role="controlgroup">
				<? foreach ($categories->getTagsByGroup($group) as $tag): ?>
					<input class="custom search-tags-item item-selector" type="checkbox" name="<? echo $group . '------' . $tag['tag']; ?>"
						   id="<? echo $group . '------' . $tag['tag']; ?>" data-mini="true"/>
					<label for="<? echo $group . '------' . $tag['tag']; ?>"><? echo $tag['tag']; ?></label>
				<? endforeach; ?>
			</fieldset>
		</div>
	<? endforeach; ?>
<? endif; ?>
