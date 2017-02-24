<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\Library as Library;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/**
	 * @var $library Library
	 * @var $defaultPage LibraryPage
     * @var $openOnSamePage bool
	 */

	$libraryManager = new LibraryManager();
	$allLibraries = $libraryManager->getLibraries();
?>
<div data-role='page' id="library" class="shortcut-link-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<a href="#library-popup-panel-left" class="navigation-panel-toggle" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<h1 class="header-title"><? echo $library->alias ?></h1>
		<a href="#library-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
	</div>
	<div data-role='content' class="main-content">
        <table class="content-header">
            <tr>
                <td class="title">
                    <a data-rel="popup" data-ajax="false" href="#library-popup-panel-pages">
                        <div>Change page:</div>
                        <div class="page-name"><? echo $defaultPage->name; ?></div>
                    </a>
                </td>
                <td class="back">
					<? if ($openOnSamePage): ?>
                        <a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
					<? endif; ?>
                </td>
            </tr>
        </table>
		<div class="content-data">
			<? echo $this->renderPartial('../wallbin/pageContent', array('page' => $defaultPage)); ?>
		</div>
	</div>
	<div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
		<ul data-role="listview">
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
        <ul class="navigation-items-container" data-role="listview">
		</ul>
	</div>
	<div data-role="panel" data-display="overlay" data-position="right" id="library-popup-panel-right">
        <ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
            <li data-icon="false">
                <a class="logout-button" href="#">Log Out</a>
            </li>
            <li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
            <li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
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
	<div class="service-data">
		<div class="library-id"><? echo $library->id; ?></div>
	</div>
</div>
