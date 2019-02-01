<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;

	/** @var $shortcut SearchAppShortcut */

	$categoryManager = new CategoryManager();
	$categoryManager->loadCategories();

	$libraryManager = new LibraryManager();
	$libraries = $libraryManager->getLibraries();
	$libraryGroups = $libraryManager->getLibraryGroups();
?>
    <div data-role='page' id="search" class="shortcut-link-page" data-cache="never" data-dom-cache="false"
         data-ajax="false">
        <div data-role='header' class="page-header" data-position="fixed" data-theme="a">
	        <? if ($shortcut->showNavigationPanel): ?>
                <a href="#search-popup-panel-left" class="navigation-panel-toggle" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	        <? endif; ?>
            <h1 class="header-title">Search</h1>
            <a href="#search-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round"
               data-iconpos="notext"></a>
        </div>
        <div data-role='content' class="main-content">
			<?
				$navBarTabsCount = 2;
				if (Yii::app()->params['search_options']['hide_tag'] != true)
					$navBarTabsCount++;
				if (Yii::app()->params['search_options']['hide_supertag'] != true || Yii::app()->params['search_options']['hide_libraries'] != true)
					$navBarTabsCount++;

				switch ($navBarTabsCount)
				{
					case 3:
						$navBarGridColumnIndex = 'b';
						break;
					case 4:
						$navBarGridColumnIndex = 'c';
						break;
					default:
						$navBarGridColumnIndex = 'a';
						break;
				}
			?>
            <div id="search-tabs" data-role="tabs">
                <div class="navbar" data-role="navbar" data-grid="<? echo $navBarGridColumnIndex; ?>">
                    <ul>
                        <li><a href="#search-tab-filters" class="ui-btn-active">Filters</a></li>
                        <li><a href="#search-tab-file-types">Files</a></li>
						<? if (Yii::app()->params['search_options']['hide_tag'] != true): ?>
                            <li><a href="#search-tab-categories"><? echo Yii::app()->params['tags']['tab_name']; ?></a>
                            </li>
						<? endif; ?>
						<? if (Yii::app()->params['search_options']['hide_supertag'] != true || Yii::app()->params['search_options']['hide_libraries'] != true): ?>
                            <li><a href="#search-tab-libraries">Advanced</a></li>
						<? endif; ?>
                    </ul>
                </div>
                <div id="search-tab-filters" class="ui-content search-tab-content">
                    <legend>What are you looking for?</legend>
                    <ul class="edit-fields" data-role="listview">
                        <li class="edit-field" data-icon="false">
                            <label for="search-tab-filters-text" class="ui-hidden-accessible"></label>
                            <input type="text" id="search-tab-filters-text" value="" placeholder="Type keyword here…">
                        </li>
                        <li class="edit-field" data-icon="false">
                            <label for="search-tab-filters-date-start" class="ui-hidden-accessible"></label>
                            <input type="text" id="search-tab-filters-date-start" placeholder="Start Date"
                                   data-role="date" readonly>
                        </li>
                        <li class="edit-field" data-icon="false">
                            <label for="search-tab-filters-date-end" class="ui-hidden-accessible"></label>
                            <input type="text" id="search-tab-filters-date-end" placeholder="End Date" data-role="date"
                                   readonly>
                        </li>
                    </ul>
                    <legend style="margin-top: 40px;">Filter Search by:</legend>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <label class="ios-checkbox ui-icon-ion-checkmark" for="search-tab-filters-file-name-only">File
                                Names Only</label>
                            <input type="checkbox" id="search-tab-filters-file-name-only">
                        </div>
                        <div class="ui-block-b">
                            <label class="ios-checkbox ui-icon-ion-checkmark" for="search-tab-filters-exact-search">Exact
                                Search</label>
                            <input type="checkbox" id="search-tab-filters-exact-search">
                        </div>
                    </div>
                </div>
                <div id="search-tab-file-types" class="ui-content search-tab-content">
                    <legend>Specific File Types:</legend>
                    <fieldset data-role="controlgroup">
                        <label for="search-tab-file-types-power-point" class="ios-checkbox ui-icon-ion-checkmark">PowerPoint</label><input
                                type="checkbox" id="search-tab-file-types-power-point"
                                class="search-tab-file-type-toggle">
                        <label for="search-tab-file-types-video"
                               class="ios-checkbox ui-icon-ion-checkmark">Video</label><input type="checkbox"
                                                                                              id="search-tab-file-types-video"
                                                                                              class="search-tab-file-type-toggle">
                        <label for="search-tab-file-types-pdf" class="ios-checkbox ui-icon-ion-checkmark">Adobe
                            PDF</label><input type="checkbox" id="search-tab-file-types-pdf"
                                              class="search-tab-file-type-toggle">
                        <label for="search-tab-file-types-word" class="ios-checkbox ui-icon-ion-checkmark">Word
                            Document</label><input type="checkbox" id="search-tab-file-types-word"
                                                   class="search-tab-file-type-toggle">
                        <label for="search-tab-file-types-excel" class="ios-checkbox ui-icon-ion-checkmark">Excel
                            Files</label><input type="checkbox" id="search-tab-file-types-excel"
                                                class="search-tab-file-type-toggle">
                        <label for="search-tab-file-types-image" class="ios-checkbox ui-icon-ion-checkmark">JPEG-PNG
                            Images</label><input type="checkbox" id="search-tab-file-types-image"
                                                 class="search-tab-file-type-toggle">
                        <label for="search-tab-file-types-url" class="ios-checkbox ui-icon-ion-checkmark">Web URL
                            Links</label><input type="checkbox" id="search-tab-file-types-url"
                                                class="search-tab-file-type-toggle">
                    </fieldset>
                </div>
				<? if (Yii::app()->params['search_options']['hide_tag'] != true): ?>
                    <div id="search-tab-categories" class="ui-content search-tab-content">
                        <legend>Specific Categories:</legend>
                        <div class="category-group-buttons">
							<? foreach ($categoryManager->categories as $category): ?>
								<? $categoryCode = hash('md5', $category); ?>
                                <a href="#search-category-group-panel-<? echo $categoryCode; ?>"
                                   id="search-category-group-toggle-<? echo $categoryCode; ?>"
                                   class="category-group-button" data-role="button" data-mini="true" data-inline="true"
                                   data-ajax="false"><? echo $category; ?></a>
							<? endforeach; ?>
                        </div>
                    </div>
				<? endif; ?>
				<? if (Yii::app()->params['search_options']['hide_supertag'] != true || Yii::app()->params['search_options']['hide_libraries'] != true): ?>
                    <div id="search-tab-libraries" class="ui-content search-tab-content">
                        <legend>Advanced Options:</legend>
                        <div class="advanced-filter-buttons">
							<? if (Yii::app()->params['search_options']['hide_libraries'] != true): ?>
                                <a href="#search-library-panel" id="search-filter-library-button"
                                   class="advanced-filter-button" data-role="button" data-theme="a">Search specific
                                    libraries?</a>
							<? endif; ?>
							<? if (Yii::app()->params['search_options']['hide_supertag'] != true && isset($categoryManager->superFilters)): ?>
                                <a href="#search-super-filter-panel" id="search-super-filter-button"
                                   class="advanced-filter-button" data-role="button" data-theme="a">Search for special
                                    tags?</a>
							<? endif; ?>
                        </div>
                    </div>
				<? endif; ?>
            </div>
        </div>
        <div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
            <ul data-role="listview">
                <li class="search-actions">
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a id="search-button-clear" class="search-action" data-role="button" data-icon="delete"
                               data-inline="true" data-theme="a" data-ajax="false">Clear</a>
                        </div>
                        <div class="ui-block-b">
                            <a id="search-button-run" class="search-action" data-role="button" data-icon="search"
                               data-inline="true" data-theme="a" data-ajax="false">Search</a>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
	    <? if ($shortcut->showNavigationPanel): ?>
            <div data-role="panel" data-display="overlay" id="search-popup-panel-left">
                <ul class="navigation-items-container navigation-items-container-main" data-role="listview">
                </ul>
            </div>
	    <? endif; ?>
        <div data-role="panel" data-display="overlay" data-position="right" id="search-popup-panel-right">
            <ul data-role="listview">
				<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
                <li data-icon="false">
                    <a class="logout-button" href="#">Log Out</a>
                </li>
                <li data-role="list-divider"><p class="user-info">
                        User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
                <li data-role="list-divider"><p>Copyright 2018 adSALESapps.com</p></li>
            </ul>
        </div>
		<? if (Yii::app()->params['search_options']['hide_tag'] != true): ?>
			<? foreach ($categoryManager->categories as $category): ?>
				<? $categoryCode = hash('md5', $category); ?>
                <div id="search-category-group-panel-<? echo $categoryCode; ?>" class="search-category-group-panel"
                     data-role="panel" data-display="overlay" data-position="right">
                    <div data-role='header'>
                        <h3><span class="group-name"><? echo $category; ?></span>:</h3>
                    </div>
                    <div data-role='content'>
                        <div class="ui-grid-a buttons">
                            <div class="ui-block-a">
                                <a class="select-all" href="#" data-role="button" data-inline="true" data-mini="true"
                                   data-theme="d">Select All</a>
                            </div>
                            <div class="ui-block-b">
                                <a class="clear-all" href="#" data-role="button" data-inline="true" data-mini="true"
                                   data-theme="d">Clear All</a>
                            </div>
                        </div>
                        <fieldset data-role="controlgroup">
                            <legend>What categories do you want to look for?</legend>
							<? foreach ($categoryManager->getTagsByCategory($category) as $tag): ?>
								<? $tagCode = hash('md5', $tag['tag']); ?>
                                <label for="search-category-tag-<? echo $tagCode; ?>"
                                       class="ios-checkbox ui-icon-ion-checkmark"><? echo $tag['tag']; ?></label>
                                <input type="checkbox" id="search-category-tag-<? echo $tagCode; ?>"
                                       class="search-category-tag-toggle">
							<? endforeach; ?>
                        </fieldset>
                        <div class="ui-grid-solo buttons">
                            <div class="ui-block-a">
                                <a class="accept" href="#search" data-role="button" data-inline="true" data-rel="close"
                                   data-mini="true" data-theme="d">Apply</a>
                            </div>
                        </div>
                    </div>
                    <div class="service-data">
                        <div class="category-group-code"><? echo $categoryCode; ?></div>
                    </div>
                </div>
			<? endforeach; ?>
		<? endif; ?>
		<? if (Yii::app()->params['search_options']['hide_libraries'] != true): ?>
            <div id="search-library-panel" data-role="panel" data-display="overlay" data-position="right">
                <div data-role='content'>
					<? foreach ($libraryGroups as $category): ?>
                        <fieldset data-role="controlgroup">
                            <legend><? echo $category->name; ?>:</legend>
							<? foreach ($category->libraries as $library): ?>
                                <label for="search-library-item-<? echo $library->id; ?>"
                                       class="ios-checkbox ui-icon-ion-checkmark"><? echo $library->name; ?></label>
                                <input type="checkbox" id="search-library-item-<? echo $library->id; ?>"
                                       class="search-filter-library-toggle" value="<? echo $library->id; ?>">
							<? endforeach; ?>
                        </fieldset>
					<? endforeach; ?>
                    <div class="ui-grid-solo buttons">
                        <div class="ui-block-a">
                            <a class="accept" href="#search" data-role="button" data-inline="true" data-rel="close"
                               data-mini="true" data-theme="d">Apply</a>
                        </div>
                    </div>
                </div>
            </div>
		<? endif; ?>
		<? if (Yii::app()->params['search_options']['hide_supertag'] != true && isset($categoryManager->superFilters)): ?>
            <div id="search-super-filter-panel" data-role="panel" data-display="overlay" data-position="right">
                <div data-role='header'>
                    <h3>Super Tags:</h3>
                </div>
                <div data-role='content'>
                    <fieldset data-role="controlgroup">
                        <legend>Search for files with these special tags...</legend>
						<? $count = count($categoryManager->superFilters); ?>
						<? for ($i = 0; $i < $count; $i++): ?>
                            <label for="search-super-filter-item-<? echo $i; ?>"
                                   class="ios-checkbox ui-icon-ion-checkmark"><? echo $categoryManager->superFilters[$i]->value; ?></label>
                            <input type="checkbox" id="search-super-filter-item-<? echo $i; ?>"
                                   class="search-super-filter-toggle">
						<? endfor; ?>
                    </fieldset>
                    <div class="ui-grid-solo buttons">
                        <div class="ui-block-a">
                            <a class="accept" href="#search" data-role="button" data-inline="true" data-rel="close"
                               data-mini="true" data-theme="d">Apply</a>
                        </div>
                    </div>
                </div>
            </div>
		<? endif; ?>
    </div>
<? $this->renderPartial('../search/searchResultPage', array('parentShortcutId' => 'search', 'backPageId' => 'search')); ?>