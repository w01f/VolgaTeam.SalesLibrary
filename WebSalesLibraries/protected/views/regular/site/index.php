<?
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile($cs->getCoreScriptUrl() . '/jui/css/base/jquery-ui.min.css');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/datepicker/css/daterangepicker.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/jixedbar/themes/default/jx.stylesheet.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/css/cubeportfolio.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cleditor/jquery.cleditor.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/ribbon.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/minibar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/columns.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/accordion.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/wallbin-tabs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/folder-links.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/banner.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/search.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/view-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/view-dialog-bar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/tool-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/file-card.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/links-grid.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/shortcuts.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/calendar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/favorites.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/quizzes.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/page-list.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/link-cart.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/links-grid.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/logo-list.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/page-content.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/main-page.css?' . Yii::app()->params['version']);
	if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords))
	{
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/modern-ticker/css/modern-ticker.css?' . Yii::app()->params['version']);
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/modern-ticker/themes/theme' . Yii::app()->params['ticker']['theme'] . '/theme.css?' . Yii::app()->params['version']);
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/ticker.css?' . Yii::app()->params['version']);
	}
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/logo-list.css?' . Yii::app()->params['version']);
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
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/jixedbar/js/jquery.jixedbar.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/gesture-handler/jquery.hammer.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/touch-punch/jquery.ui.touch-punch.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/js/jquery.cubeportfolio.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cleditor/jquery.cleditor.min.js', CClientScript::POS_HEAD);
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
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/search-processor.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/search-view.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/shortcuts.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/favorites.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/link-rate.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/minibar.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
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

	$newsCount = 0;
	if (Yii::app()->params['ribbon_news']['visible'])
		foreach (Yii::app()->params['ribbon_news']['urls'] as $url)
			if ($url['visible'])
				$newsCount++;

	$sideBarVisible = true;
	if (isset(Yii::app()->request->cookies['sideBarVisible']->value))
		$sideBarVisible = Yii::app()->request->cookies['sideBarVisible']->value == "true";

	$isMobile = isset(Yii::app()->browser) && Yii::app()->browser->isMobile();

	$showPageList = isset(Yii::app()->request->cookies['showQPageList']->value) ? Yii::app()->request->cookies['showQPageList']->value == "true" : true;
	$showLinlCart = isset(Yii::app()->request->cookies['showLinkCart']->value) ? Yii::app()->request->cookies['showLinkCart']->value == "true" : true;
	$popupLink = '';
	if ($isMobile)
		$popupLink = 'ipad_popups.pdf';
	else
	{
		$browser = Yii::app()->browser->getBrowser();
		switch ($browser)
		{
			case 'Internet Explorer':
				$popupLink = 'ie_popups.pdf';
				break;
			case 'Chrome':
				$popupLink = 'chrome_popups.pdf';
				break;
			case 'Safari':
				$popupLink = 'ipad_popups.pdf';
				break;
			case 'Firefox':
				$popupLink = 'firefox_popups.pdf';
				break;
		}
	}
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
									<td><label for="select-library"></label><select id="select-library"></select></td>
								</tr>
								<tr>
									<td><label for="select-page"></label><select id="select-page"></select></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<? if ($newsCount > 0): ?>
				<div class="ribbon-section">
					<span class="section-title"><? echo Yii::app()->params['ribbon_news']['title']; ?></span>
					<div class="ribbon-news-container">
						<? $counter = 0; ?>
						<? foreach (Yii::app()->params['ribbon_news']['urls'] as $url): ?>
							<? if ($url['visible']): ?>
								<? if ($newsCount > 2 && !($counter % 2)): ?>
									<div class="news-block">
								<? endif; ?>
								<a class="news-item" target="_blank" href="<? echo $url['url']; ?>"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/urls/' . $url['image'] . '?' . Yii::app()->params['version']; ?>"></a>
								<? if ($newsCount > 2 && (($counter % 2) || $counter == ($newsCount - 1))): ?>
									</div>
								<? endif; ?>
								<? $counter++; ?>
							<? endif; ?>
						<? endforeach; ?>
					</div>
				</div>
			<? endif; ?>
			<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
				<div class="ribbon-section">
					<span class="section-title">Logout</span>
					<div class="ribbon-button ribbon-button-large logout-button  <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
					</div>
				</div>
			<? endif; ?>
		</div>
	<? elseif ($tabName == 'search_full_tab'): ?>
		<? if (Yii::app()->params['search_full_tab']['visible']): ?>
			<div class="ribbon-tab" id="search-full-tab">
				<span class="ribbon-title"><? echo Yii::app()->params['search_full_tab']['name'] ?></span>

				<div class="ribbon-section">
                        <span class="section-title">
                            <? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
								<? echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
							<? endif; ?>
                        </span>
					<img class="ribbon-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . '/images/rbntab2logo.png' ?>"/>
				</div>
				<div class="ribbon-section">
					<span class="section-title">SideBar</span>
					<div class="ribbon-button ribbon-button-large side-bar-toggle<? echo $sideBarVisible ? ' sel' : ''; ?> <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/side-bar.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/side-bar.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/side-bar.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Search</span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="run-search-full">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/search.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/search.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/search.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Clear</span>

					<div class="ribbon-button ribbon-button-large clear-button <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/clear.png' ?>"/>
					</div>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<?
	elseif ($tabName == 'favorites_tab'): ?>
		<? if (Yii::app()->params['favorites_tab']['visible']): ?>
			<div class="ribbon-tab" id="favorites-tab">
				<span class="ribbon-title"><? echo Yii::app()->params['favorites_tab']['name'] ?></span>

				<div class="ribbon-section">
					<span class="section-title">Favorites</span>
					<img class="ribbon-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . '/images/rbntab2logo.png' ?>"/>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<?
	elseif ($tabName == 'quiz_tab'): ?>
		<? if (Yii::app()->params['quiz_tab']['visible']): ?>
			<div class="ribbon-tab" id="quiz-tab">
				<span class="ribbon-title"><? echo Yii::app()->params['quiz_tab']['name'] ?></span>
				<div class="ribbon-section">
					<span class="section-title"><? echo Yii::app()->params['quiz_tab']['name'] ?></span>
					<img class="ribbon-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . '/images/rbntab2logo.png' ?>"/>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<?
	elseif ($tabName == 'qbuilder_tab'): ?>
		<? if (Yii::app()->params['qbuilder_tab']['visible']): ?>
			<div class="ribbon-tab" id="qbuilder-tab">
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
					<div class="ribbon-button ribbon-button-large" id="page-preview-button" rel="tooltip" title="PREVIEW this quickSITE">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-preview.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-preview.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-preview.png' ?>"/>
						<span>Preview</span>
					</div>
					<div class="ribbon-button ribbon-button-large" id="page-email-outlook-button" rel="tooltip" title="Send URL with Outlook">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-email.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-email.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/page-email.png' ?>"/>
						<span>Email</span>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Enable Popups</span>
					<a class="ribbon-button ribbon-button-large" href="<?php echo Yii::app()->getBaseUrl(true) . '/sd_cache/popups/' . $popupLink; ?>" target="_blanck" rel="tooltip" title="Popups MUST be Enabled for this site">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/popup-help.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/popup-help.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/qpages/ribbon/popup-help.png' ?>"/>
					</a>
				</div>
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
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
			<div class="ribbon-tab shortcuts-tab" id="shortcuts-tab-<? echo $tabShortcutsRecord->id; ?>">
				<span class="ribbon-title"><? echo $tabShortcutsRecord->name; ?></span>

				<div class="ribbon-section">
					<span class="section-title">
						<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
							<? echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
						<? endif; ?>
					</span>
					<img class="ribbon-tab-logo" src="<? echo Yii::app()->getBaseUrl(true) . $tabShortcutsRecord->image_path . '?' . $tabShortcutsRecord->id; ?>"/>
					<img class="ribbon-link-logo" src="<? echo Yii::app()->getBaseUrl(true) . $tabShortcutsRecord->image_path . '?' . $tabShortcutsRecord->id; ?>" style="display: none"/>
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
				<? if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/normal/logout.png' ?>"/>
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
<!---------Minibar--------->
<div id="minibar">
	<ul>
		<li id="columns-view" title="Columns View">
			<a href="#"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/minibar/columns.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="accordion-view" title="Accordion View">
			<a href="#"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/minibar/accordion.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="tabs-view" title="Tabs View">
			<a href="#"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/minibar/tabs.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="increase-text-size" title="Increase Text Size">
			<a href="#"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/minibar/increaseTextSize.png' ?>" alt=""/></a>
		</li>
	</ul>
	<ul>
		<li id="decrease-text-size" title="Decrease Text Size">
			<a href="#"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/minibar/decreaseTextSize.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="increase-text-space" title="Increase Line Spacing">
			<a href="#"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/minibar/increaseTextSpace.png' ?>" alt=""/></a>
		</li>
	</ul>
	<ul>
		<li id="decrease-text-space" title="Decrease Line Spacing">
			<a href="#"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/minibar/decreaseTextSpace.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
</div><!-------------------------><!---------Ticker--------->
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