<?
	/**
	 * @var $searchBar SearchBar
	 */

?>
<? if ($searchBar->configured): ?>
	<?
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
	$searchBar->categoryManager->loadCategories();
	$searchBarId = 'search-bar-' . $searchBar->id;
	$style = $searchBar->style;
	?>
    <style>
        <?echo '#'.$searchBarId; ?>
        .search-bar-label {
        <?if(!empty($style->labelBackColor)):?> background-color: <? echo Utils::formatColor($style->labelBackColor);?> !important;
        <?endif;?><?if(!empty($style->labelTextColor)):?> color: <? echo Utils::formatColor($style->labelTextColor);?> !important;
        <?endif;?><?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColor($style->borderColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-button,
        <?echo '#'.$searchBarId; ?> .search-bar-button:hover,
        <?echo '#'.$searchBarId; ?> .search-bar-button:focus,
        <?echo '#'.$searchBarId; ?> .search-bar-button:focus:hover {
        <?if(!empty($style->buttonBackColor)):?> background-color: <? echo Utils::formatColor($style->buttonBackColor);?> !important;
        <?endif;?><?if(!empty($style->buttonTextColor)):?> color: <? echo Utils::formatColor($style->buttonTextColor);?> !important;
        <?endif;?><?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColor($style->borderColor);?> !important;
        <?endif;?>
            outline: none !important;
            box-shadow: unset !important;
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-run,
        <?echo '#'.$searchBarId; ?> .search-bar-run:hover,
        <?echo '#'.$searchBarId; ?> .search-bar-run:focus,
        <?echo '#'.$searchBarId; ?> .search-bar-run:focus:hover {
        <?if(!empty($style->searchBackColor)):?> background-color: <? echo Utils::formatColor($style->searchBackColor);?> !important;
        <?endif;?><?if(!empty($style->searchTextColor)):?> color: <? echo Utils::formatColor($style->searchTextColor);?> !important;
        <?endif;?><?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColor($style->borderColor);?> !important;
        <?endif;?>
            outline: none !important;
            box-shadow: unset !important;
        }

        <?echo '#'.$searchBarId; ?>
        input::placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColor($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        input::-moz-placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColor($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        input:-ms-input-placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColor($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        input::-webkit-input-placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColor($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-text {
            height: <? echo ($style->lineHeight+14);?>px !important;
        <?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColor($style->borderColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        .btn {
            line-height: <? echo $style->lineHeight;?>px !important;
        }
    </style>
    <table id="<? echo $searchBarId; ?>" class="shortcuts-search-bar logger-form open" data-log-group="Shortcut Tile"
           data-log-action="Search Bar"
           style="text-align: <? echo $searchBar->alignment; ?>;">
        <tr>
            <td>
				<? $this->renderPartial('../shortcuts/searchConditions', array('searchContainer' => $searchBar)); ?>
                <div class="tag-condition-selector-wrapper">
					<? $this->renderPartial('../shortcuts/categorySelector', array('categoryManager' => $searchBar->categoryManager), false, true); ?>
                </div>
                <div class="search-bar-actions">
					<? $this->renderPartial('../menu/actionItems', array('actionContainer' => $searchBar), false, true); ?>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group search-input-container">
					<? if (!empty($searchBar->label)): ?>
                        <span class="input-group-addon search-bar-label"><? echo $searchBar->label; ?>:</span>
					<? endif; ?>
                    <input class="form-control log-action search-bar-text" type="text"
                           placeholder="<? echo $searchBar->placeholder; ?>">
                    <span class="input-group-btn">
						<? if ($searchBar->showTagsSelector): ?>
                            <button class="btn btn-default log-action search-bar-button tags-filter-panel-switcher"
                                    type="button"><? echo $tagsName; ?></button>
						<? endIf; ?>
                        <button class="btn btn-default log-action search-bar-button search-bar-options"
                                type="button">Search Options</button>
						<button class="btn btn-default search-bar-run" type="button">
							<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/search.png'; ?>">
						</button>
				  	</span>
                </div>
                <p class="tag-condition-selected text-muted">
                    <small>Fusce dapibus, tellus ac cursus commodo, tortor mauris nibh.</small>
                </p>
            </td>
        </tr>
    </table>
<? endif; ?>