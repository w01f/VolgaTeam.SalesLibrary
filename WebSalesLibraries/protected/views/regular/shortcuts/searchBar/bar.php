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
        <?if(!empty($style->labelBackColor)):?> background-color: <? echo Utils::formatColorToHex($style->labelBackColor);?> !important;
        <?endif;?><?if(!empty($style->labelTextColor)):?> color: <? echo Utils::formatColorToHex($style->labelTextColor);?> !important;
        <?endif;?><?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColorToHex($style->borderColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-button,
        <?echo '#'.$searchBarId; ?> .search-bar-button:hover,
        <?echo '#'.$searchBarId; ?> .search-bar-button:focus,
        <?echo '#'.$searchBarId; ?> .search-bar-button:focus:hover {
        <?if(!empty($style->buttonBackColor)):?> background-color: <? echo Utils::formatColorToHex($style->buttonBackColor);?> !important;
        <?endif;?><?if(!empty($style->buttonTextColor)):?> color: <? echo Utils::formatColorToHex($style->buttonTextColor);?> !important;
        <?endif;?><?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColorToHex($style->borderColor);?> !important;
        <?endif;?> outline: none !important;
            box-shadow: unset !important;
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-run,
        <?echo '#'.$searchBarId; ?> .search-bar-run:hover,
        <?echo '#'.$searchBarId; ?> .search-bar-run:focus,
        <?echo '#'.$searchBarId; ?> .search-bar-run:focus:hover {
        <?if(!empty($style->searchBackColor)):?> background-color: <? echo Utils::formatColorToHex($style->searchBackColor);?> !important;
        <?endif;?><?if(!empty($style->searchTextColor)):?> color: <? echo Utils::formatColorToHex($style->searchTextColor);?> !important;
        <?endif;?><?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColorToHex($style->borderColor);?> !important;
        <?endif;?> outline: none !important;
            box-shadow: unset !important;
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-run path {
        <?if(!empty($style->iconColor)):?> fill: <? echo Utils::formatColorToHex($style->iconColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        input::placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColorToHex($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        input::-moz-placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColorToHex($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        input:-ms-input-placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColorToHex($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        input::-webkit-input-placeholder {
        <?if(!empty($style->placeholderTextColor)):?> color: <? echo Utils::formatColorToHex($style->placeholderTextColor);?> !important;
        <?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-text {
            height: <? echo ($style->lineHeight+14);?>px !important;
            <?if($style->searchBackColor=='transparent'):?> background-color: transparent !important;<?endif;?>
            <?if(!empty($style->borderColor)):?> border-color: <? echo Utils::formatColorToHex($style->borderColor);?> !important;<?endif;?>
        }

        <?echo '#'.$searchBarId; ?>
        .btn {
            line-height: <? echo $style->lineHeight;?>px !important;
        }

        <?echo '#'.$searchBarId; ?>
        .search-bar-run {
            width: 48px;
            height: <? echo ($style->lineHeight+14);?>px !important;
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
                    <input class="form-control log-action search-bar-text tooltipster-target" type="text"
                           placeholder="<? echo $searchBar->placeholder; ?>" <? if (!empty($searchBar->hoverTips[SearchBar::HoverTipTagInput])): ?>title="<? echo $searchBar->hoverTips[SearchBar::HoverTipTagInput]; ?>"<? endif; ?>>
                    <span class="input-group-btn">
						<? if ($searchBar->showTagsSelector): ?>
                            <button class="btn btn-default log-action search-bar-button tags-filter-panel-switcher tooltipster-target"
                                    type="button" <? if (!empty($searchBar->hoverTips[SearchBar::HoverTipTagTagsButton])): ?>title="<? echo $searchBar->hoverTips[SearchBar::HoverTipTagTagsButton]; ?>"<? endif; ?>><? echo $tagsName; ?></button>
						<? endIf; ?>
                        <button class="btn btn-default log-action search-bar-button search-bar-options tooltipster-target"
                                type="button" <? if (!empty($searchBar->hoverTips[SearchBar::HoverTipTagOptionsButton])): ?>title="<? echo $searchBar->hoverTips[SearchBar::HoverTipTagOptionsButton]; ?>"<? endif; ?>>Search Options</button>
						<button class="btn btn-default search-bar-run tooltipster-target" type="button" <? if (!empty($searchBar->hoverTips[SearchBar::HoverTipTagActionButton])): ?>title="<? echo $searchBar->hoverTips[SearchBar::HoverTipTagActionButton]; ?>"<? endif; ?>>
							<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/search.svg'; ?>"
                                 style="height: 16px; width: 16px;">
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