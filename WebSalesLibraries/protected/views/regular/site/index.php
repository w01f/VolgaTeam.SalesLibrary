<?php
	$version = '73.0';
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->clientScript->getCoreScriptUrl() . '/jui/css/base/jquery-ui.css');
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/video-js/video-js.min.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/datepicker/css/daterangepicker.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/jixedbar/themes/default/jx.stylesheet.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fullcalendar/fullcalendar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/cubeportfolio/css/cubeportfolio.min.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/ribbon.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/minibar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/columns.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/accordion.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/wallbin-tabs.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/folder-links.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/banner.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/search.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/view-dialog.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/view-dialog-bar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/tool-dialog.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/file-card.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/links-grid.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/shortcuts.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/calendar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/favorites.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/link-rate.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/quizzes.css?' . $version);
	if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords))
	{
		$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/modern-ticker/css/modern-ticker.css?' . $version);
		$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/modern-ticker/themes/theme' . Yii::app()->params['ticker']['theme'] . '/theme.css?' . $version);
		$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/ticker.css?' . $version);
	}
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/logo-list.css?' . $version);
	$cs->registerCoreScript('jquery.ui');
	$cs->registerCoreScript('cookie');
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/date.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/daterangepicker.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/jixedbar/js/jquery.jixedbar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fullcalendar/fullcalendar.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fullcalendar/jquery.qtip-1.0.0-rc3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/gesture-handler/jquery.hammer.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/touch-punch/jquery.ui.touch-punch.min.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/cubeportfolio/js/jquery.cubeportfolio.min.js?' . $version, CClientScript::POS_HEAD);
	if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords))
	{
		$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/modern-ticker/js/jquery.modern-ticker.min.js', CClientScript::POS_HEAD);
		$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/ticker.js?' . $version, CClientScript::POS_HEAD);
	}
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/login.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/overlay.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/text-sizing.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/scaling.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-viewing.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/view-dialog-bar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/wallbin.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/links-grid.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/search-processor.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/search-view.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/shortcuts.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/calendar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/favorites.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/link-rate.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/minibar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/quizzes.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/ribbon.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/link-cart.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/page-list.js?' . $version, CClientScript::POS_HEAD);
	foreach (Yii::app()->params as $key => $row)
	{
		if (is_array($row))
			if (array_key_exists('position', $row))
				$tabParam[$key] = $row['position'];
	}

	$tabShortcuts = ShortcutsTabStorage::model()->findAll(array('order' => '`order`', 'condition' => 'enabled=:enabled', 'params' => array(':enabled' => true)));
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
?>
<div id="ribbon">
<div class="ribbon-window-title"></div>
<?php foreach ($tabParam as $tabName => $tabIndex): ?>
	<?php if ($tabName == 'home_tab'): ?>
		<div class="ribbon-tab" id="home-tab">
			<span class="ribbon-title"><?php echo Yii::app()->params['home_tab']['name'] ?></span>

			<div class="ribbon-section">
                    <span class="section-title" id="libraries-selector-title">
                        User:
						<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
							<?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
						<?php endif; ?>
                    </span>
				<table id="libraries-selector-container">
					<tr>
						<td><img src="" id="page-logo"/></td>
						<td>
							<table id="libraries-selector">
								<tr>
									<td><select id="select-library"></select></td>
								</tr>
								<tr>
									<td><select id="select-page"></select></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<div class="ribbon-section">
				<span class="section-title">quickSITES</span>
				<div class="ribbon-button ribbon-button-large qbuilder-button <? if (!$isMobile): ?>regular<? endif; ?>">
					<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/qbuilder.png' ?>"/>
					<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/qbuilder.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/qbuilder.png' ?>"/>
				</div>
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
								<a class="news-item" target="_blank" href="<? echo $url['url']; ?>"><img src="<? echo Yii::app()->getBaseUrl(true) . '/images/ribbon/urls/' . $url['image'] . '?' . $version; ?>"></a>
								<? if ($newsCount > 2 && (($counter % 2) || $counter == ($newsCount - 1))): ?>
									</div>
								<? endif; ?>
								<? $counter++; ?>
							<? endif; ?>
						<? endforeach; ?>
					</div>
				</div>
			<? endif; ?>
			<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
				<div class="ribbon-section">
					<span class="section-title">Logout</span>
					<div class="ribbon-button ribbon-button-large logout-button  <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					</div>
				</div>
			<?php endif; ?>
		</div>
	<?php elseif ($tabName == 'search_full_tab'): ?>
		<?php if (Yii::app()->params['search_full_tab']['visible']): ?>
			<div class="ribbon-tab" id="search-full-tab">
				<span class="ribbon-title"><?php echo Yii::app()->params['search_full_tab']['name'] ?></span>

				<div class="ribbon-section">
                        <span class="section-title">
                            <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
								<?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
							<?php endif; ?>
                        </span>
					<img class="ribbon-tab-logo" src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
				</div>
				<div class="ribbon-section">
					<span class="section-title">SideBar</span>
					<div class="ribbon-button ribbon-button-large side-bar-toggle<? echo $sideBarVisible ? ' sel' : ''; ?> <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/side-bar.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/side-bar.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/side-bar.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Search</span>
					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="run-search-full">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
					</div>
				</div>
				<?php if (Yii::app()->params['search_full_tab']['show_money_button']): ?>
					<div class="ribbon-section">
						<span class="section-title">Show Me the MONEY!</span>

						<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="search-file-card-button">
							<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>"/>
						</div>
					</div>
				<?php endif; ?>
				<div class="ribbon-section">
					<span class="section-title">Clear</span>

					<div class="ribbon-button ribbon-button-large clear-button <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
					</div>
				</div>
				<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<?php endif; ?>
			</div>
		<?php endif; ?>
	<?php
	elseif ($tabName == 'search_file_card_tab'): ?>
		<?php if (Yii::app()->params['search_file_card_tab']['visible']): ?>
			<div class="ribbon-tab" id="search-file-card-tab">
				<span class="ribbon-title"><?php echo Yii::app()->params['search_file_card_tab']['name'] ?></span>

				<div class="ribbon-section">
                        <span class="section-title">
                            <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
								<?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
							<?php endif; ?>
                        </span>
					<img class="ribbon-tab-logo" src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
				</div>
				<div class="ribbon-section">
					<span class="section-title">SideBar</span>
					<div class="ribbon-button ribbon-button-large side-bar-toggle<? echo $sideBarVisible ? ' sel' : ''; ?> <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/side-bar.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/side-bar.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/side-bar.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Search</span>

					<div class="ribbon-button ribbon-button-large <? if (!$isMobile): ?>regular<? endif; ?>" id="run-search-file-card">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Clear</span>

					<div class="ribbon-button ribbon-button-large clear-button <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
						<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
					</div>
				</div>
				<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<?php endif; ?>
			</div>
		<?php endif; ?>
	<?php
	elseif ($tabName == 'calendar_tab'): ?>
		<?php if (Yii::app()->params['calendar_tab']['visible']): ?>
			<div class="ribbon-tab" id="calendar-tab">
				<span class="ribbon-title"><?php echo Yii::app()->params['calendar_tab']['name'] ?></span>

				<div class="ribbon-section">
					<span class="section-title">Training</span>
					<img class="ribbon-tab-logo" src="<?php echo Yii::app()->baseUrl . '/images/calendar/ribbon_logo.png' ?>"/>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Courses for Sales Reps</span>

					<div id="calendar-pdf1" class="ribbon-button ribbon-button-large pdf <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/1.png"/>
						<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/1.png"/>
						<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/1.png"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Courses for Admins & Research</span>

					<div id="calendar-pdf2" class="ribbon-button ribbon-button-large pdf <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/2.png"/>
						<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/2.png"/>
						<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/2.png"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Courses for Managers</span>

					<div id="calendar-pdf3" class="ribbon-button ribbon-button-large pdf <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/3.png"/>
						<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/3.png"/>
						<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/3.png"/>
					</div>
				</div>
				<div class="ribbon-section">
					<span class="section-title">Help Using This Site</span>

					<div id="calendar-video" class="ribbon-button ribbon-button-large video <? if (!$isMobile): ?>regular<? endif; ?>">
						<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/4.png"/>
						<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/4.png"/>
						<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/4.png"/>
					</div>
				</div>
			</div>
		<?php endif; ?>
	<?
	elseif ($tabName == 'favorites_tab'): ?>
		<?php if (Yii::app()->params['favorites_tab']['visible']): ?>
			<div class="ribbon-tab" id="favorites-tab">
				<span class="ribbon-title"><?php echo Yii::app()->params['favorites_tab']['name'] ?></span>

				<div class="ribbon-section">
					<span class="section-title">Favorites</span>
					<img class="ribbon-tab-logo" src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
				</div>
				<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<?php endif; ?>
			</div>
		<?php endif; ?>
	<?
	elseif ($tabName == 'quiz_tab'): ?>
		<?php if (Yii::app()->params['quiz_tab']['visible']): ?>
			<div class="ribbon-tab" id="quiz-tab">
				<span class="ribbon-title"><?php echo Yii::app()->params['quiz_tab']['name'] ?></span>
				<div class="ribbon-section">
					<span class="section-title"><?php echo Yii::app()->params['quiz_tab']['name'] ?></span>
					<img class="ribbon-tab-logo" src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
				</div>
				<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<?php endif; ?>
			</div>
		<?php endif; ?>
	<?php
	elseif (strpos($tabName, 'shortcuts-tab-') !== false): ?>
		<?php $tabShortcutsRecord = ShortcutsTabStorage::model()->findByPk(str_replace('shortcuts-tab-', '', $tabName)); ?>
		<?php if (isset($tabShortcutsRecord)): ?>
			<div class="ribbon-tab shortcuts-tab" id="shortcuts-tab-<?php echo $tabShortcutsRecord->id; ?>">
				<span class="ribbon-title"><?php echo $tabShortcutsRecord->name; ?></span>

				<div class="ribbon-section">
					<span class="section-title">
						<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
							<?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
						<?php endif; ?>
					</span>
					<img class="ribbon-tab-logo" src="<?php echo Yii::app()->baseUrl . $tabShortcutsRecord->image_path . '?' . $tabShortcutsRecord->id; ?>"/>
					<img class="ribbon-link-logo" src="<?php echo Yii::app()->baseUrl . $tabShortcutsRecord->image_path . '?' . $tabShortcutsRecord->id; ?>" style="display: none"/>
				</div>
				<?php
					$pageShortcuts = ShortcutsPageStorage::model()->findAll(array('order' => '`order`', 'condition' => 'id_tab=:id_tab', 'params' => array(':id_tab' => $tabShortcutsRecord->id)));
					$selected = true;
				?>
				<?php foreach ($pageShortcuts as $pageShortcutsRecord): ?>
					<div class="ribbon-section <?php echo !$pageShortcutsRecord->isEnabled(Yii::app()->user->login) ? 'disabled' : ''; ?>">
						<span class="section-title"><?php echo $pageShortcutsRecord->name; ?></span>

						<div class="ribbon-button ribbon-button-large <?php echo $pageShortcutsRecord->isEnabled(Yii::app()->user->login) && $selected ? 'sel' : ''; ?> <?php echo !$pageShortcutsRecord->isEnabled(Yii::app()->user->login) ? 'disabled' : 'enabled'; ?> shortcuts-page <? if (!$isMobile): ?>regular<? endif; ?>" id="<?php echo $pageShortcutsRecord->id; ?>">
							<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . $pageShortcutsRecord->image_path . '?' . $pageShortcutsRecord->id; ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . $pageShortcutsRecord->image_path . '?' . $pageShortcutsRecord->id; ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . $pageShortcutsRecord->image_path . '?' . $pageShortcutsRecord->id; ?>"/>
						</div>
					</div>
					<?php $selected = $pageShortcutsRecord->isEnabled(Yii::app()->user->login) ? false : $selected; ?>
				<?php endforeach; ?>
				<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
					<div class="ribbon-section">
						<span class="section-title">Logout</span>

						<div class="ribbon-button ribbon-button-large logout-button <? if (!$isMobile): ?>regular<? endif; ?>">
							<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
							<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
						</div>
					</div>
				<?php endif; ?>
			</div>
		<?php endif; ?>
	<?php endif; ?>
<?php endforeach; ?>
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
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/columns.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="accordion-view" title="Accordion View">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/accordion.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="tabs-view" title="Tabs View">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/tabs.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="increase-text-size" title="Increase Text Size">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/increaseTextSize.png' ?>" alt=""/></a>
		</li>
	</ul>
	<ul>
		<li id="decrease-text-size" title="Decrease Text Size">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/decreaseTextSize.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
	<ul>
		<li id="increase-text-space" title="Increase Line Spacing">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/increaseTextSpace.png' ?>" alt=""/></a>
		</li>
	</ul>
	<ul>
		<li id="decrease-text-space" title="Decrease Line Spacing">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/decreaseTextSpace.png' ?>" alt=""/></a>
		</li>
	</ul>
	<span class="jx-separator-left"></span>
</div><!-------------------------><!---------Ticker--------->
<?php if (Yii::app()->params['ticker']['visible'] && isset($tickerRecords)): ?>
	<div class="modern-ticker mt-round <?php echo Yii::app()->params['ticker']['effect']; ?>">
		<?php if ((Yii::app()->params['ticker']['show_label'] || Yii::app()->params['ticker']['show_logo']) && isset($tickerRecords)): ?>
			<div class="mt-label">
				<?php if (Yii::app()->params['ticker']['show_logo']): ?>
					<img src="<?php echo Yii::app()->baseUrl . '/images/tickerlogo.png?' . $version; ?>">
				<?php endif; ?>
				<?php if (Yii::app()->params['ticker']['show_label']): ?>
					<span>NEWS:</span>
				<?php endif; ?>
			</div>
		<?php endif; ?>
		<div class="mt-news">
			<ul>
				<? foreach ($tickerRecords as $tickerRecord): ?>
					<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.ticker') . '/tickerLink.php', array('tickerLink' => $tickerRecord), true); ?>
				<? endforeach; ?>
			</ul>
		</div>
		<?php if (Yii::app()->params['ticker']['show_control']): ?>
			<div class="mt-controls">
				<div class="mt-prev"></div>
				<div class="mt-play"></div>
				<div class="mt-next"></div>
			</div>
		<?php endif; ?>
	</div>
<?php endif; ?>
<!------------------------->