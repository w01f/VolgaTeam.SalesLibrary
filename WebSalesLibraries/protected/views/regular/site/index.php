<?php
	$version = '118.0';
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->clientScript->getCoreScriptUrl() . '/jui/css/base/jquery-ui.css');
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/video-js/video-js.min.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/datepicker/css/daterangepicker.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/jixedbar/themes/default/jx.stylesheet.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fullcalendar/fullcalendar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/ribbon.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/minibar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/columns.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/accordion.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/folder-links.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/banner.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/search.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/view-dialog.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/tool-dialog.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/file-card.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/links-grid.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/help.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/calendar.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/favorites.css?' . $version);
	$cs->registerCoreScript('jquery.ui');
	$cs->registerCoreScript('cookie');
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/date.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/daterangepicker.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/jixedbar/js/jquery.jixedbar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fullcalendar/fullcalendar.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fullcalendar/jquery.qtip-1.0.0-rc3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/login.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/overlay.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/textSizing.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/scaling.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/linkViewing.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/wallbin.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/links-grid.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/search.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/help.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/calendar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/favorites.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/minibar.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/ribbon.js?' . $version, CClientScript::POS_HEAD);
	$this->pageTitle = Yii::app()->name;

	foreach (Yii::app()->params as $key => $row)
	{
		if (is_array($row))
			if (array_key_exists('position', $row))
				$tabParam[$key] = $row['position'];
	}

	$tabHelpRecords = HelpTabStorage::model()->findAll(array('order' => '`order`', 'condition' => 'enabled=:enabled', 'params' => array(':enabled' => true)));
	if (isset($tabHelpRecords))
		foreach ($tabHelpRecords as $tabHelpRecord)
			$tabParam['help-tab-' . $tabHelpRecord->id] = $tabHelpRecord->order;

	asort($tabParam);
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
		<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
		<div class="ribbon-section">
			<span class="section-title">Logout</span>

			<div class="ribbon-button ribbon-button-large logout-button">
				<img class="ribbon-icon ribbon-normal"
					 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
				<img class="ribbon-icon ribbon-hot"
					 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled"
					 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
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
				<img src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Search</span>

				<div class="ribbon-button ribbon-button-large" id="run-search-full">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
				</div>
			</div>
			<?php if (Yii::app()->params['search_full_tab']['show_money_button']): ?>
			<div class="ribbon-section">
				<span class="section-title">Show Me the MONEY!</span>

				<div class="ribbon-button ribbon-button-large" id="search-file-card-button">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search-money.png' ?>"/>
				</div>
			</div>
			<?php endif; ?>
			<div class="ribbon-section">
				<span class="section-title">Clear</span>

				<div class="ribbon-button ribbon-button-large clear-button">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
				</div>
			</div>
			<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
			<div class="ribbon-section">
				<span class="section-title">Logout</span>

				<div class="ribbon-button ribbon-button-large logout-button">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
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
				<img src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Search</span>

				<div class="ribbon-button ribbon-button-large" id="run-search-file-card">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/search/search.png' ?>"/>
				</div>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Clear</span>

				<div class="ribbon-button ribbon-button-large clear-button">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/clear.png' ?>"/>
				</div>
			</div>
			<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
			<div class="ribbon-section">
				<span class="section-title">Logout</span>

				<div class="ribbon-button ribbon-button-large logout-button">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
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
				<img src="<?php echo Yii::app()->baseUrl . '/images/calendar/ribbon_logo.png' ?>"/>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Courses for Sales Reps</span>

				<div id="calendar-pdf1" class="ribbon-button ribbon-button-large pdf">
					<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/1.png"/>
					<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/1.png"/>
					<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/1.png"/>
				</div>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Courses for Admins & Research</span>

				<div id="calendar-pdf2" class="ribbon-button ribbon-button-large pdf">
					<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/2.png"/>
					<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/2.png"/>
					<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/2.png"/>
				</div>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Courses for Managers</span>

				<div id="calendar-pdf3" class="ribbon-button ribbon-button-large pdf">
					<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/3.png"/>
					<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/3.png"/>
					<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/3.png"/>
				</div>
			</div>
			<div class="ribbon-section">
				<span class="section-title">Help Using This Site</span>

				<div id="calendar-video" class="ribbon-button ribbon-button-large video">
					<img class="ribbon-icon ribbon-normal calendar-button-icon" src="images/calendar/4.png"/>
					<img class="ribbon-icon ribbon-hot calendar-button-icon" src="images/calendar/4.png"/>
					<img class="ribbon-icon ribbon-disabled calendar-button-icon" src="images/calendar/4.png"/>
				</div>
			</div>
		</div>
			<?php endif; ?>
		<?php
	elseif ($tabName == 'favorites_tab'): ?>
		<?php if (Yii::app()->params['favorites_tab']['visible']): ?>
		<div class="ribbon-tab" id="favorites-tab">
			<span class="ribbon-title"><?php echo Yii::app()->params['favorites_tab']['name'] ?></span>

			<div class="ribbon-section">
				<span class="section-title">Favorites</span>
				<img src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png' ?>"/>
			</div>
			<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
			<div class="ribbon-section">
				<span class="section-title">Logout</span>

				<div class="ribbon-button ribbon-button-large logout-button">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
				</div>
			</div>
			<?php endif; ?>
		</div>
			<?php endif; ?>
		<?php
	elseif (strpos($tabName, 'help-tab-') !== false): ?>
		<?php $tabHelpRecord = HelpTabStorage::model()->findByPk(str_replace('help-tab-', '', $tabName)); ?>
		<?php if (isset($tabHelpRecord)): ?>
		<div class="ribbon-tab help-tab" id="help-tab-<?php echo $tabHelpRecord->id; ?>">
			<span class="ribbon-title"><?php echo $tabHelpRecord->name; ?></span>

			<div class="ribbon-section">
                        <span class="section-title">
                            <?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
							<?php echo Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName; ?>
							<?php endif; ?>
                        </span>
				<img src="<?php echo Yii::app()->baseUrl . $tabHelpRecord->image_path; ?>"/>
			</div>
			<?php
			$pageHelpRecords = HelpPageStorage::model()->findAll(array('order' => '`order`', 'condition' => 'id_tab=:id_tab', 'params' => array(':id_tab' => $tabHelpRecord->id)));
			$selected = true;
			?>
			<?php if (isset($pageHelpRecords)): ?>
			<?php foreach ($pageHelpRecords as $pageHelpRecord): ?>
				<div class="ribbon-section <?php echo!$pageHelpRecord->enabled ? 'disabled' : ''; ?>">
					<span class="section-title"><?php echo $pageHelpRecord->name; ?></span>

					<div class="ribbon-button ribbon-button-large <?php echo $pageHelpRecord->enabled && $selected ? 'sel' : ''; ?> <?php echo!$pageHelpRecord->enabled ? 'disabled' : 'enabled'; ?> help-page"
						 id="<?php echo $pageHelpRecord->id; ?>">
						<img class="ribbon-icon ribbon-normal"
							 src="<?php echo Yii::app()->baseUrl . $pageHelpRecord->image_path; ?>"/>
						<img class="ribbon-icon ribbon-hot"
							 src="<?php echo Yii::app()->baseUrl . $pageHelpRecord->image_path; ?>"/>
						<img class="ribbon-icon ribbon-disabled"
							 src="<?php echo Yii::app()->baseUrl . $pageHelpRecord->image_path; ?>"/>
					</div>
				</div>
				<?php $selected = $pageHelpRecord->enabled ? false : $selected; ?>
				<?php endforeach; ?>
			<?php endif; ?>
			<?php if (isset(Yii::app()->user->firstName) && isset(Yii::app()->user->lastName)): ?>
			<div class="ribbon-section">
				<span class="section-title">Logout</span>

				<div class="ribbon-button ribbon-button-large logout-button">
					<img class="ribbon-icon ribbon-normal"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-hot"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
					<img class="ribbon-icon ribbon-disabled"
						 src="<?php echo Yii::app()->baseUrl . '/images/ribbon/normal/logout.png' ?>"/>
				</div>
			</div>
			<?php endif; ?>
		</div>
			<?php endif; ?>
		<?php endif; ?>
	<?php endforeach; ?>
</div>
<div id="content">
</div>
<div id="content-overlay">
</div>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container">View Options</a>

	<div id="view-dialog-wrapper">
		<div id="view-dialog-container">
		</div>
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
		<li id="increase-text-size" title="Increase Text Size">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/increaseTextSize.png' ?>"
							 alt=""/></a>
		</li>
	</ul>
	<ul>
		<li id="decrease-text-size" title="Decrease Text Size">
			<a href="#"><img src="<?php echo Yii::app()->baseUrl . '/images/minibar/decreaseTextSize.png' ?>"
							 alt=""/></a>
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
</div>
<!------------------------->    