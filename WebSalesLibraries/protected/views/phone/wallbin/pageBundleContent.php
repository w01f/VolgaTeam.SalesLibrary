<?
	/**
	 * @var $wallbinId string
	 * @var $wallbinName string
	 * @var $pageItems LibraryPageBundleItem[]
	 * @var $defaultPageItem LibraryPageBundleItem
	 * @var $openOnSamePage bool
	 */

	$pageIds = array();
	foreach ($pageItems as $item)
		$pageIds[] = $item->libraryPage->id;
	$linksCount = LinkRecord::getLinksCountByPageIds($pageIds);
?>
<div data-role='page' id="wallbin-<? echo $wallbinId; ?>" class="shortcut-link-page" data-cache="never" data-dom-cache="false" data-ajax="false">
    <div class="service-data">
        <div class="activity-data">
	        <? echo CJSON::encode(array('type' => 'Shortcut', 'subType' => 'Open Shortcut', 'data' => array('file' => $wallbinName))); ?>
        </div>
    </div>
    <div data-role='header' class="page-header" data-position="fixed" data-theme="a">
        <a href="#wallbin-<? echo $wallbinId; ?>-popup-panel-left" class="navigation-panel-toggle" data-icon="ion-navicon-round"
           data-iconpos="notext"></a>
        <h1 class="header-title"><? echo $wallbinName ?></h1>
        <a href="#wallbin-<? echo $wallbinId; ?>-popup-panel-right" class="ui-btn-right" data-icon="ion-navicon-round"
           data-iconpos="notext"></a>
    </div>
    <div data-role='content' class="main-content">
        <table class="content-header">
            <tr>
                <td class="title">
                    <a data-rel="popup" data-ajax="false" href="#wallbin-<? echo $wallbinId; ?>-popup-panel-pages">
                        <div>Change page:</div>
                        <div class="page-name"><? echo $defaultPageItem->name; ?></div>
                    </a>
                </td>
                <td class="back">
					<? if ($openOnSamePage): ?>
                        <a href="#" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true"
                           data-transition="slidefade" data-direction="reverse">BACK</a>
					<? endif; ?>
                </td>
            </tr>
        </table>
        <div class="content-data">
			<? echo $this->renderPartial('../wallbin/pageContent', array('page' => $defaultPageItem->libraryPage)); ?>
        </div>
    </div>
    <div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
        <ul data-role="listview">
            <li class="footer-info">
                <div class="ui-grid-a">
                    <div class="ui-block-a">
                    </div>
                    <div class="ui-block-b entities-count">
                        <span class="ui-mini"><? echo $linksCount; ?> files</span>
                    </div>
                </div>
            </li>
        </ul>
    </div>
    <div data-role="panel" data-display="overlay" id="wallbin-<? echo $wallbinId; ?>-popup-panel-left">
        <ul class="navigation-items-container navigation-items-container-main" data-role="listview">
        </ul>
    </div>
    <div data-role="panel" data-display="overlay" data-position="right" id="wallbin-<? echo $wallbinId; ?>-popup-panel-right">
        <ul data-role="listview">
			<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
            <li data-icon="false">
                <a class="logout-button" href="#">Log Out</a>
            </li>
            <li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p>
            </li>
            <li data-role="list-divider"><p>Copyright 2018 adSALESapps.com</p></li>
        </ul>
    </div>
    <div data-role="panel" data-display="overlay" data-position="right" id="wallbin-<? echo $wallbinId; ?>-popup-panel-pages">
        <ul data-role="listview">
            <li data-role="list-divider" class="header">
                <a href="#" data-ajax="false"><span><? echo $wallbinName ?></span></a>
            </li>
			<? foreach ($pageItems as $item): ?>
                <li data-icon="false" class="page-item">
                    <a data-ajax="false" href="#"> <span><? echo $item->name; ?></span>
                        <div class="service-data">
                            <div class="page-id"><? echo $item->libraryPage->id; ?></div>
                            <div class="page-logo"><? echo $item->libraryPage->logoContent; ?></div>
                        </div>
                    </a>
                </li>
			<? endforeach; ?>
            <li data-role="list-divider"><p>Copyright 2018 adSALESapps.com</p></li>
        </ul>
    </div>
</div>
