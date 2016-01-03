<?
	/**
	 * @var $library Library
	 * @var $defaultPage LibraryPage
	 * @var $tabPageExisted boolean
	 */

	$libraryManager = new LibraryManager();
	$allLibraries = $libraryManager->getLibraries();
?>
<div data-role='page' id="library" class="shortcut-link-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<a href="#library-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"><? echo $library->alias ?></h1>
		<a href="#library-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
		<a class="content-header vertical" data-rel="popup" data-ajax="false" href="#library-popup-panel-pages">
			<div class="logo"><img src="<? echo $defaultPage->logoContent ?>"></div>
			<div class="title"><? echo $defaultPage->name; ?></div>
		</a>
		<div class="content-data">
			<? echo $this->renderPartial('../wallbin/pageContent', array('page' => $defaultPage)); ?>
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
		<ul data-role="listview">
			<? if (count($allLibraries) > 1): ?>
<!--				<li class="library-action delete-library-page--><?// if ($tabPageExisted == true): ?><!-- active-action--><?// endif; ?><!--">-->
<!--					<h4>-->
<!--						<a data-rel="popup" data-position-to="window" href="#library-popup-delete-tab-page">Remove this Library from the HOME SCREEN</a>-->
<!--					</h4>-->
<!--				</li>-->
<!--				<li class="library-action add-library-page--><?// if ($tabPageExisted != true): ?><!-- active-action--><?// endif; ?><!--">-->
<!--					<h4>-->
<!--						<a data-rel="popup" data-position-to="window" href="#library-popup-add-tab-page">Add this Library to the HOME SCREEN</a>-->
<!--					</h4>-->
<!--				</li>-->
			<? endif; ?>
			<li class="footer-info">
				<div class="ui-grid-a">
					<div class="ui-block-a">
					</div>
					<div class="ui-block-b entities-count">
						<span class="ui-mini"><? echo LinkRecord::getLinksCountByLibrary($library->id); ?> files</span>
					</div>
				</div>
			</li>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" id="library-popup-panel-left">
		<ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
			<li data-icon="false">
				<a class="logout-button" href="#">Log Out</a>
			</li>
			<li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
			<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="library-popup-panel-right">
		<ul data-role="listview">
			<? echo $this->renderPartial('../wallbin/libraryList'); ?>
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="library-popup-panel-pages">
		<ul data-role="listview">
			<li data-role="list-divider" class="header">
				<a href="#" data-ajax="false"><span><? echo $library->alias ?></span></a>
			</li>
			<? foreach ($library->pages as $page): ?>
				<li data-icon="false" class="page-item">
					<a data-ajax="false" href="#"> <span><? echo $page->name; ?></span>
						<div class="service-data">
							<div class="page-id"><? echo $page->id; ?></div>
							<div class="page-logo"><? echo $page->logoContent; ?></div>
						</div>
					</a>
				</li>
			<? endforeach; ?>
			<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
		</ul>
	</div>
	<div id="library-popup-add-tab-page" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1><? echo $library->alias ?></h1>
		</div>
		<div role="main" style="padding:0 20px">
			<p class="warning-text">Add the <? echo $library->alias ?> Library to your HOME SCREEN?</p>
			<div class="ui-grid-a">
				<div class="ui-block-a">
					<a class="accept-action" href="#" data-role="button" data-theme="d">Add</a>     
				</div>
				<div class="ui-block-b">
					<a href="#" data-role="button" data-theme="d" data-rel="back">Cancel</a>
				</div>
			</div>
		</div>
	</div>
	<div id="library-popup-delete-tab-page" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1><? echo $library->alias ?></h1>
		</div>
		<div role="main" style="padding:0 20px">
			<p class="warning-text">REMOVE the <? echo $library->alias ?> Library from your HOME SCREEN?</p>
			<div class="ui-grid-a">
				<div class="ui-block-a">
					<a class="accept-action" href="#" data-role="button" data-theme="d">Remove</a>     
				</div>
				<div class="ui-block-b">
					<a href="#" data-role="button" data-theme="d" data-rel="back">Cancel</a>
				</div>
			</div>
		</div>
	</div>
	<div class="service-data">
		<div class="library-id"><? echo $library->id; ?></div>
	</div>
</div>
