<?
	/**
	 * @var $categoryManager CategoryManager
	 */
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
	$iconUrlPrefix = Yii::app()->getBaseUrl(true) . '/images/category-group-icons';
?>
<div class="tag-condition-selector" data-log-group="Shortcut Tile" data-log-action="Search Bar">
    <div class="category-filter-list">
        <ul class="nav nav-pills">
			<? foreach ($categoryManager->groups as $filter): ?>
                <li class="category-filter">
                    <a href="#" onfocus="this.blur();">
						<?
							$iconName = $categoryManager->groupIcons[$filter];
							$useIcon = true;
							if (strpos($iconName, '.png') !== false || strpos($iconName, '.svg') !== false)
								$useIcon = false;
						?>
						<? if ($useIcon): ?>
                            <i class="title-part icon <? echo $iconName; ?>"></i>
						<? else: ?>
                            <img class="title-part icon" src="<? echo $iconUrlPrefix . '/' . $iconName; ?>">
						<? endif; ?>
                        <span class="title-part text"><? echo $filter; ?></span>
                    </a>
                </li>
			<? endforeach; ?>
        </ul>
    </div>
    <div class="category-content">
        <table>
            <tr class="content-item header">
                <td colspan="2">
                    <div class="selected-category-label">
                        <span></span>
                    </div>
                </td>
            </tr>
            <tr class="content-item body">
                <td class="category-list">
                    <ul class="nav nav-pills">
						<? foreach ($categoryManager->groups as $group): ?>
							<? foreach ($categoryManager->getCategoriesByGroup($group) as $filter): ?>
                                <li class="category" data-category-filter="<? echo $group; ?>">
                                    <a href="#" onfocus="this.blur();">
                                        <span><? echo $filter; ?></span>
                                    </a>
                                </li>
							<? endforeach; ?>
						<? endforeach; ?>
                    </ul>
                </td>
                <td class="tag-list">
                    <div class="tag-scroll-wrapper">
						<? foreach ($categoryManager->categories as $filter): ?>
                            <div class="tag-group" data-category="<? echo $filter; ?>">
                                <div class="checkbox select-all-selector">
                                    <label>
                                        <input type="checkbox">
                                        <span class="name">Select All</span>
                                    </label>
                                </div>
								<? foreach ($categoryManager->getTagsByCategory($filter) as $tag): ?>
                                    <div class="checkbox tag-selector">
                                        <label>
                                            <input type="checkbox" class="tag log-action">
                                            <span class="name"><? echo $tag['tag']; ?></span>
                                        </label>
                                    </div>
								<? endforeach; ?>
                            </div>
						<? endforeach; ?>
                    </div>
                </td>
            </tr>
            <tr class="content-item footer">
                <td colspan="2">
                    <div class="row">
                        <div class="col-xs-8 text-left">
                            <button class="btn btn-default tags-clear-all">Clear All <? echo $tagsName; ?></button>
                        </div>
                        <div class="col-xs-2 text-center">
                            <button class="btn btn-default accept-button">OK</button>
                        </div>
                        <div class="col-xs-2 text-center accept-button">
                            <button class="btn btn-default cancel-button">Cancel</button>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>