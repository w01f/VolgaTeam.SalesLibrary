<?php
$version = '6.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->clientScript->getCoreScriptUrl() . '/jui/css/base/jquery-ui.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/datepicker/css/datepicker.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/cleditor/jquery.cleditor.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/tool-dialog.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/page-list.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/link-cart.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/links-grid.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/logo-list.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/page-content.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/main-page.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/qbuilder/ribbon.css?' . $version);
$cs->registerCoreScript('jquery.ui');
$cs->registerCoreScript('cookie');
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/bootstrap-datepicker.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/cleditor/jquery.cleditor.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/context-menu/bootstrap.contextmenu.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/touch-punch/jquery.ui.touch-punch.min.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/overlay.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/page-list.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/link-cart.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/page-content.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/main-page.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/scaling.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/qbuilder/ribbon.js?' . $version, CClientScript::POS_HEAD);

$showPageList = isset(Yii::app()->request->cookies['showQPageList']->value) ? Yii::app()->request->cookies['showQPageList']->value == "true" : true;
$showLinlCart = isset(Yii::app()->request->cookies['showLinkCart']->value) ? Yii::app()->request->cookies['showLinkCart']->value == "true" : true;
?>
<div id="ribbon">
	<div class="ribbon-window-title"></div>
	<div class="ribbon-tab">
		<span class="ribbon-title">adSALESapps.com</span>
		<div class="ribbon-section">
			<span class="section-title">quickSITES Beta</span>
			<img src="<?php echo Yii::app()->baseUrl . '/images/rbntab2logo.png?' . $version; ?>"/>
		</div>
		<div class="ribbon-section">
			<span class="section-title">Site List</span>
			<div class="ribbon-button ribbon-button-large <? if ($showPageList): ?>sel<? endif; ?>" id="page-list-button" rel="tooltip" title="quickSITES Panel">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-list.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-list.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-list.png' ?>"/>
			</div>
		</div>
		<div class="ribbon-section">
			<span class="section-title">Link Cart</span>
			<div class="ribbon-button ribbon-button-large <? if ($showLinlCart): ?>sel<? endif; ?>" id="link-cart-button" rel="tooltip" title="Link Cart">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/link-cart.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/link-cart.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/link-cart.png' ?>"/>
			</div>
		</div>
		<div class="ribbon-section">
			<span class="section-title">quickSITE</span>
			<div class="ribbon-button ribbon-button-large" id="page-add-button" rel="tooltip" title="Add New quickSITE">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-add.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-add.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-add.png' ?>"/>
				<span>Add</span>
			</div>
			<div class="ribbon-button ribbon-button-large" id="page-delete-button" rel="tooltip" title="DELETE Selected quickSITE">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-delete.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-delete.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-delete.png' ?>"/>
				<span>Delete</span>
			</div>
			<div class="ribbon-button ribbon-button-large" id="page-save-button" rel="tooltip" title="SAVE this quickSITE">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-save.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-save.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-save.png' ?>"/>
				<span>Save</span>
			</div>
			<div class="ribbon-button ribbon-button-large" id="page-preview-button" rel="tooltip" title="PREVIEW this quickSITE">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-preview.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-preview.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-preview.png' ?>"/>
				<span>Preview</span>
			</div>
			<div class="ribbon-button ribbon-button-large" id="page-email-outlook-button" rel="tooltip" title="Send URL with Outlook">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-email.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-email.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/page-email.png' ?>"/>
				<span>Email</span>
			</div>
		</div>
		<?
		$popupLink = '';
		if (Yii::app()->browser->isMobile())
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
		<div class="ribbon-section">
			<span class="section-title">Enable Popups</span>
			<a class="ribbon-button ribbon-button-large" href="<?php echo Yii::app()->getBaseUrl(true) . '/sd_cache/popups/' . $popupLink; ?>" target="_blanck">
				<img class="ribbon-icon ribbon-normal" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/popup-help.png' ?>"/>
				<img class="ribbon-icon ribbon-hot" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/popup-help.png' ?>"/>
				<img class="ribbon-icon ribbon-disabled" src="<?php echo Yii::app()->baseUrl . '/images/qpages/ribbon/popup-help.png' ?>"/>
			</a>
		</div>
	</div>
</div>
<div id="content"></div>
<div id="content-overlay"></div>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container">View Options</a>

	<div id="view-dialog-wrapper">
		<div id="view-dialog-container"></div>
	</div>
</div>
<!------------------------->