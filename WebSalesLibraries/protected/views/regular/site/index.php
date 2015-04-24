<?
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile($cs->getCoreScriptUrl() . '/jui/css/metro/jquery-ui.min.css');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/datepicker/css/daterangepicker.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/css/cubeportfolio.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/carousel/load/skin_modern_silver.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/carousel/load/html_content.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cleditor/jquery.cleditor.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/star-rating/css/star-rating.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/combobox/css/bootstrap-select.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/css/dataTables.bootstrap.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/ribbon.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/columns.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/accordion.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/wallbin-ribbon.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/wallbin-tabs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/folder-links.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/banner.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/search.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/view-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/view-dialog-bar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/tool-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/links-grid.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/data-table.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/shortcuts.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/shortcuts-search.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/shortcuts-search-bar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/calendar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/favorites.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/quizzes.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/logo-list.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/page-list.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/link-cart.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/links-grid.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/page-content.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/main-page.css?' . Yii::app()->params['version']);
	if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords))
	{
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/modern-ticker/css/modern-ticker.css?' . Yii::app()->params['version']);
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/modern-ticker/themes/theme' . Yii::app()->params['ticker']['theme'] . '/theme.css?' . Yii::app()->params['version']);
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/ticker.css?' . Yii::app()->params['version']);
	}
	$cs->registerCoreScript('jquery.ui');
	$cs->registerCoreScript('cookie');
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/datepicker/js/moment.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/datepicker/js/daterangepicker.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/gesture-handler/jquery.hammer.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/touch-punch/jquery.ui.touch-punch.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/js/jquery.cubeportfolio.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/carousel/java/FWDUltimate3DCarousel.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cleditor/jquery.cleditor.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/star-rating/js/star-rating.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/combobox/js/bootstrap-select.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/tabs-extension/jquery-ui-tabs-paging.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/js/jquery.dataTables.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/js/dataTables.bootstrap.min.js', CClientScript::POS_HEAD);
	if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords))
	{
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/modern-ticker/js/jquery.modern-ticker.min.js', CClientScript::POS_HEAD);
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/ticker.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	}
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/ribbon.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/login.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/overlay.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/link-viewing.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/view-dialog-bar.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/wallbin.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/links-grid.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/data-table.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/search-processor.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/search-view.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/shortcuts-search.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/shortcuts-search-bar.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/shortcuts.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/favorites.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/link-rate.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/quizzes.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/page-list.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/link-cart.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/page-content.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/main-page.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/controller.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	foreach (Yii::app()->params as $key => $row)
	{
		if (is_array($row))
			if (array_key_exists('position', $row))
				$tabParam[$key] = $row['position'];
	}

	$tabShortcuts = ShortcutsTabRecord::model()->findAll(array('order' => '`order`', 'condition' => 'enabled=:enabled', 'params' => array(':enabled' => true)));
	if (isset($tabShortcuts))
		foreach ($tabShortcuts as $tabShortcutsRecord)
			$tabParam['shortcuts-tab-' . $tabShortcutsRecord->id] = $tabShortcutsRecord->order;

	asort($tabParam);

	$sideBarVisible = true;
	if (isset(Yii::app()->request->cookies['sideBarVisible']->value))
		$sideBarVisible = Yii::app()->request->cookies['sideBarVisible']->value == "true";

	$isMobile = isset(Yii::app()->browser) && Yii::app()->browser->isMobile();

	$showPageList = isset(Yii::app()->request->cookies['showQPageList']->value) ? Yii::app()->request->cookies['showQPageList']->value == "true" : true;
	$showLinlCart = isset(Yii::app()->request->cookies['showLinkCart']->value) ? Yii::app()->request->cookies['showLinkCart']->value == "true" : true;
?>
<div id="ribbon">
<div class="ribbon-window-title"></div>
<? foreach ($tabParam as $tabName => $tabIndex): ?>
	<? if ($tabName == 'home_tab'): ?>
		<div class="ribbon-tab" id="home-tab">
			<span class="ribbon-title"><? echo Yii::app()->params['home_tab']['name'] ?></span>

			<div class="ribbon-section">
                    <span class="section-title" id="libraries-selector-title">
                        User:
						<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
							<? echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
						<? endif; ?>
                    </span>
				<table id="libraries-selector-container">
					<tr>
						<td><img src="" id="page-logo"/></td>
						<td>
							<table id="libraries-selector">
								<tr>
									<td>
										<label for="select-library"></label><select id="select-library" class="selectpicker"></select>
									</td>
								</tr>
								<tr>
									<td>
										<label for="select-page"></label><select id="select-page" class="selectpicker"></select>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Columns</span>
				<div id="columns-view" class="ribbon-button ribbon-button-large wallbin-style-options  <? if (!$isMobile): ?>regular<? endif; ?>">
					<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/columns.png' ?>"/>
					<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/columns.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/columns.png' ?>"/>
				</div>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Boxes</span>
				<div id="accordion-view" class="ribbon-button ribbon-button-large wallbin-style-options  <? if (!$isMobile): ?>regular<? endif; ?>">
					<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/accordion.png' ?>"/>
					<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/accordion.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/accordion.png' ?>"/>
				</div>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Tabs</span>
				<div id="tabs-view" class="ribbon-button ribbon-button-large wallbin-style-options  <? if (!$isMobile): ?>regular<? endif; ?>">
					<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/tabs.png' ?>"/>
					<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/tabs.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/wallbin/tabs.png' ?>"/>
				</div>
			</div>
			<div class="ribbon-section wallbin-view-options">
				<span class="section-title">View Options</span>
				<div class="row">
					<div class="col-xs-6">
						<button id="increase-text-size" type="button" class="btn btn-default btn-sm">Larger Text</button>
					</div>
					<div class="col-xs-6">
						<button id="increase-text-space" type="button" class="btn btn-default btn-sm">More Spacing</button>
					</div>
				</div>
				<div class="row">
					<div class="col-xs-6">
						<button id="decrease-text-size" type="button" class="btn btn-default btn-sm">Smaller Text</button>
					</div>
					<div class="col-xs-6">
						<button id="decrease-text-space" type="button" class="btn btn-default btn-sm">Less Spacing</button>
					</div>
				</div>
			</div>
			<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
				<div class="ribbon-section">
					<span class="section-title">Logout</span>
					<div class="ribbon-button ribbon-button-large logout-button  <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
					</div>
				</div>
			<? endif; ?>
		</div>
	<? elseif ($tabName == 'search_full_tab'): ?>
		<? if (Yii::app()->params['search_full_tab']['visible']): ?>
			<div class="ribbon-tab ribbon-tab-overflow" id="search-full-tab">
				<span class="ribbon-title"><? echo Yii::app()->params['search_full_tab']['name'] ?></span>
				<div class="ribbon-section">
					<span class="section-title">Advanced Search Engine</span>
					<div class="input-group search-ribbon-condition-content-container">
						<input type="text" class="form-control search-bar-text" id="search-ribbon-condition-content-value" placeholder="What are you looking for?">
						  <span class="input-group-btn">
							<button id="search-ribbon-clear-content-value" class="btn btn-default search-bar-run" type="button">
								<span class="glyphicon glyphicon-remove"></span>
							</button>
						  </span>
					</div>
					<div class="input-group" id="search-ribbon-condition-date-container">
						<input class="form-control" type="text" readonly placeholder="Select Date Range...">
						<div class="input-group-btn">
							<button class="btn btn-default select-date-toggle" type="button">
								<span class="glyphicon glyphicon-calendar"></span></button>
							<button id="search-ribbon-condition-date-clear" class="btn btn-default" type="button">
								<span class="glyphicon glyphicon-remove"></span></button>
						</div>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Filters</span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-ribbon-filter">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/filter.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/filter.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/filter.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">File Types</span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-ribbon-file-types">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/file-types.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/file-types.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/file-types.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title"><? echo Yii::app()->params['tags']['tab_name']; ?></span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-ribbon-tags">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/tags.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/tags.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/tags.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Super Tags</span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-ribbon-super-filters">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/super-filters.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/super-filters.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/super-filters.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title"><? echo Yii::app()->params['stations']['tab_name']; ?></span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-ribbon-libraries">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/libraries.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/libraries.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/libraries.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Search</span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-ribbon-run">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/search.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/search.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/search.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Clear Page</span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-ribbon-clear">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/ribbon/clear.png' ?>"/>
					</div>
				</div>
			</div>
		<? endif; ?>
	<?
	elseif ($tabName == 'favorites_tab'): ?>
		<? if (Yii::app()->params['favorites_tab']['visible']): ?>
			<div class="ribbon-tab ribbon-tab-overflow" id="favorites-tab">
				<span class="ribbon-title"><? echo Yii::app()->params['favorites_tab']['name'] ?></span>

				<div class="ribbon-section">
					<span class="section-title">Favorites</span>
					<img class="ribbon-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . '/images/rbntab2logo.png' ?>"/>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<?
	elseif ($tabName == 'quiz_tab'): ?>
		<? if (Yii::app()->params['quiz_tab']['visible']): ?>
			<div class="ribbon-tab ribbon-tab-overflow" id="quiz-tab">
				<span class="ribbon-title"><? echo Yii::app()->params['quiz_tab']['name'] ?></span>
				<div class="ribbon-section">
					<span class="section-title"><? echo Yii::app()->params['quiz_tab']['name'] ?></span>
					<img class="ribbon-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . '/images/rbntab2logo.png' ?>"/>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<?
	elseif ($tabName == 'qbuilder_tab'): ?>
		<? if (Yii::app()->params['qbuilder_tab']['visible']): ?>
			<div class="ribbon-tab ribbon-tab-overflow" id="qbuilder-tab">
				<span class="ribbon-title"><? echo Yii::app()->params['qbuilder_tab']['name'] ?></span>
				<div class="ribbon-section">
					<span class="section-title"><? echo Yii::app()->params['qbuilder_tab']['name'] ?></span>
					<img class="ribbon-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . '/images/rbntab2logo.png' ?>"/>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Site List</span>
					<div class="ribbon-button ribbon-button-large <? if ($showPageList): ?>sel<? endif; ?>" id="page-list-button" rel="tooltip" title="quickSITES Panel">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-list.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-list.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-list.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Link Cart</span>
					<div class="ribbon-button ribbon-button-large <? if ($showLinlCart): ?>sel<? endif; ?>" id="link-cart-button" rel="tooltip" title="Link Cart">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/link-cart.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/link-cart.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/link-cart.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">quickSITE</span>
					<div class="ribbon-button ribbon-button-large" id="page-add-button" rel="tooltip" title="Add New quickSITE">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-add.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-add.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-add.png' ?>"/>
						<span>Add</span>
					</div>
					<div class="ribbon-button ribbon-button-large" id="page-delete-button" rel="tooltip" title="DELETE Selected quickSITE">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-delete.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-delete.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-delete.png' ?>"/>
						<span>Delete</span>
					</div>
					<div class="ribbon-button ribbon-button-large" id="page-save-button" rel="tooltip" title="SAVE this quickSITE">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-save.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-save.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-save.png' ?>"/>
						<span>Save</span>
					</div>
					<a class="ribbon-button ribbon-button-large" id="page-preview-button" rel="tooltip" title="PREVIEW this quickSITE" href="#" target="_blank">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-preview.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-preview.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-preview.png' ?>"/>
						<span>Preview</span> </a>
					<div class="ribbon-button ribbon-button-large" id="page-email-outlook-button" rel="tooltip" title="Send URL with Outlook">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-email.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-email.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-email.png' ?>"/>
						<span>Email</span>
					</div>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<?
	elseif (strpos($tabName, 'shortcuts-tab-') !== false): ?>
		<? /** @var $tabShortcutsRecord ShortcutsTabRecord */
		$tabShortcutsRecord = ShortcutsTabRecord::model()->findByPk(str_replace('shortcuts-tab-', '', $tabName)); ?>
		<? if (isset($tabShortcutsRecord)): ?>
			<div class="ribbon-tab ribbon-tab-overflow shortcuts-tab" id="shortcuts-tab-<? echo $tabShortcutsRecord->id; ?>">
				<span class="ribbon-title"><? echo $tabShortcutsRecord->name; ?></span>

				<div class="ribbon-section">
					<span class="section-title">
						<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
							<? echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
						<? endif; ?>
					</span>
					<img class="ribbon-tab-logo ribbon-shortcut-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . $tabShortcutsRecord->image_path . '?' . $tabShortcutsRecord->id; ?>"/>
					<img class="ribbon-tab-logo ribbon-shortcut-custom-logo" src="<? echo Yii::app()->getBaseUrl(true) . $tabShortcutsRecord->image_path . '?' . $tabShortcutsRecord->id; ?>" style="display: none"/>
				</div>
				<?
					/** @var $pageShortcuts ShortcutsPageRecord[] */
					$pageShortcuts = ShortcutsPageRecord::model()->findAll(array('order' => '`order`', 'condition' => 'id_tab=:id_tab', 'params' => array(':id_tab' => $tabShortcutsRecord->id)));
					$selected = true;
				?>
				<? foreach ($pageShortcuts as $pageShortcutsRecord): ?>
					<div class="ribbon-section <? echo !$pageShortcutsRecord->isEnabled(Yii::app()->user->login) ? 'disabled' : ''; ?>">
						<span class="section-title"><? echo $pageShortcutsRecord->name; ?></span>

						<div class="ribbon-button ribbon-button-large <? echo $pageShortcutsRecord->isEnabled(Yii::app()->user->login) && $selected ? 'sel' : ''; ?> <? echo !$pageShortcutsRecord->isEnabled(Yii::app()->user->login) ? 'disabled' : 'enabled'; ?> shortcuts-page <? if (!$isMobile): ?>regular<? endif; ?>" id="<? echo $pageShortcutsRecord->id; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . $pageShortcutsRecord->image_path . '?' . $pageShortcutsRecord->id; ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . $pageShortcutsRecord->image_path . '?' . $pageShortcutsRecord->id; ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . $pageShortcutsRecord->image_path . '?' . $pageShortcutsRecord->id; ?>"/>
						</div>
					</div>
					<? $selected = $pageShortcutsRecord->isEnabled(Yii::app()->user->login) ? false : $selected; ?>
				<? endforeach; ?>
				<div class="ribbon-section search-bar-toggle-section disabled">
					<span class="section-title">Show Search</span>
					<div class="ribbon-button ribbon-button-large<? if (!$isMobile): ?> regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/search-bar-toggle.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/search-bar-toggle.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/search-bar-toggle.png' ?>"/>
					</div>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/logout.png' ?>"/>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<? endif; ?>
<? endforeach; ?>
</div>
<div id="content" oncontextmenu="return false;"></div>
<div id="content-overlay"></div>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container">View Options</a>

	<div id="view-dialog-wrapper">
		<div id="view-dialog-container"></div>
	</div>
</div>
<!------------------------->
<!---------Ticker--------->
<? if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords)): ?>
	<div class="modern-ticker mt-round <? echo Yii::app()->params['ticker']['effect']; ?>">
		<? if ((Yii::app()->params['ticker']['show_label'] || Yii::app()->params['ticker']['show_logo']) && isset($tickerRecords)): ?>
			<div class="mt-label">
				<? if (Yii::app()->params['ticker']['show_logo']): ?>
					<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/tickerlogo.png?' . Yii::app()->params['version']; ?>">
				<? endif; ?>
				<? if (Yii::app()->params['ticker']['show_label']): ?>
					<span>NEWS:</span>
				<? endif; ?>
			</div>
		<? endif; ?>
		<div class="mt-news">
			<ul>
				<? foreach ($tickerRecords as $tickerRecord): ?>
					<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.ticker') . '/tickerLink.php', array('tickerLink' => $tickerRecord), true); ?>
				<? endforeach; ?>
			</ul>
		</div>
		<? if (Yii::app()->params['ticker']['show_control']): ?>
			<div class="mt-controls">
				<div class="mt-prev"></div>
				<div class="mt-play"></div>
				<div class="mt-next"></div>
			</div>
		<? endif; ?>
	</div>
<? endif; ?>
<!------------------------->