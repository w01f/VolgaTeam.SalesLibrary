<?
	$isEOBrowser = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile($cs->getCoreScriptUrl() . '/jui/css/metro/jquery-ui.min.css');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/onemenu/css/onemenu.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/metro-menu/customized/css/metro-menu.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/icomoon/style.css?' . Yii::app()->params['icomoon']['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/font-awesome/css/font-awesome.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap-link-feed.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/datepicker/css/daterangepicker.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/css/cubeportfolio.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/carousel/load/skin_modern_silver.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/carousel/load/html_content.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/css/froala_editor.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/css/froala_style.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/star-rating/css/star-rating.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/combobox/css/bootstrap-select.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/scroll-tabs/css/scrolltabs.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/newsbox/css/jquery.bootstrap.newsbox.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/layout.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/vendor-customization.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/main-menu.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/action-menu.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/navigation-panel.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/tool-dialog.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/logo-list.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/wallbin/folder-links.css?' . Yii::app()->params['version']);
	if ($isEOBrowser)
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/wallbin/folder-links-eo.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/wallbin/banner.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/wallbin/thumbnail.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-video.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-link-bundle.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-gallery.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-save.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-email.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-bar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-special.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/link-viewer/link-viewer-rate.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/data-table.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-search-link.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-search-bar.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-search-app.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-wallbin.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-qbuilder.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-quizzes.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-favorites.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-landing-page.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/shortcuts/shortcuts-user-preferences.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/page-list.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qbuilder/page-content.css?' . Yii::app()->params['version']);

	$cs->registerCoreScript('jquery.ui');
	$cs->registerCoreScript('cookie');
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/onemenu/js/masonry.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/onemenu/js/onemenu.dev.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/metro-menu/customized/js/metro-menu.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/jquery.fancybox.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/datepicker/js/moment.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/datepicker/js/daterangepicker.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/gesture-handler/jquery.hammer.min.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/touch-punch/jquery.ui.touch-punch.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/touch-swipe/jquery.touchSwipe.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/cubeportfolio/js/jquery.cubeportfolio.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/carousel/java/FWDUltimate3DCarousel.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/js/froala_editor.min.js', CClientScript::POS_BEGIN);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/js/plugins/tables.min.js', CClientScript::POS_BEGIN);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/js/plugins/lists.min.js', CClientScript::POS_BEGIN);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/js/plugins/colors.min.js', CClientScript::POS_BEGIN);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/js/plugins/font_family.min.js', CClientScript::POS_BEGIN);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/js/plugins/font_size.min.js', CClientScript::POS_BEGIN);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/froala-editor/js/plugins/block_styles.min.js', CClientScript::POS_BEGIN);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/star-rating/js/star-rating.min.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/combobox/js/bootstrap-select.min.js', CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/scroll-tabs/js/dragdivscroll.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/scroll-tabs/js/jquery.scrolltabs.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/progress-bar/progress-bar.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/newsbox/js/jquery.bootstrap.newsbox.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/link-viewer-data.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/search-processor.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/logger.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/form-logger.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/menu-manager.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/login.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/overlay.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/modal-dialog.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/wallbin-manager.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/quiz-manager.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/data-table.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/link-rate.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/quiz-manager.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/content-manager.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/history-manager.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-file.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-document.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-video.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-youtube.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-vimeo.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-lan.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-app-link.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-internal-link.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-image.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-link-bundle.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-gallery.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-email.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-bar.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-zip-download-files.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-search-link.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-search-app.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-search-bar.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-grid.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-carousel.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-landing-page.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-landing-page-feed-common.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-landing-page-feed-horizontal.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-landing-page-feed-vertical.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-landing-page-masonry.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-wallbin.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-library-page.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-library-window.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-download.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-qbuilder.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-quizzes.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-favorites.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-super-group.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-user-preferences.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-navigation-panel.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/page-list.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/link-cart.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qbuilder/page-content.js?' . Yii::app()->params['version'], CClientScript::POS_END);

	if (Yii::app()->browser->isMobile())
	{
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/native/css/datatables.min.css?' . Yii::app()->params['version']);
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/native/js/datatables.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	}
	else
	{
		$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/bootstrap/css/datatables.min.css?' . Yii::app()->params['version']);
		$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/bootstrap/js/datatables.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	}
?>
    <style>
        .menu-back-colored {
            background-color: <?echo Utils::formatColor(Yii::app()->params['menu']['BarColor']);?> !important;
        }

        #shortcut-action-menu,
        #shortcut-action-menu .main-level,
        #shortcut-action-menu .level {
            background-color: <? echo Utils::formatColor(Yii::app()->params['menu']['BarColor']);?>;
        }

        #shortcut-action-menu .main-level .shortcut-menu-header:hover {
            background-color: <? echo Utils::formatColor(Yii::app()->params['menu']['BarColor']);?>;
        }

        #main-menu .shortcut-menu-group-item {
            margin-right: <? echo Yii::app()->params['menu']['IconSeparation'];?>px;
        }
    </style>
    <script type="text/javascript">
		$.Editable.DEFAULTS.key = '<?echo Yii::app()->params['froala_editor']['key'];?>';
    </script>

<? if (array_key_exists('refresh_popup', Yii::app()->params->getKeys()) && Yii::app()->params['refresh_popup']['enabled'] === true): ?>
    <script type="text/javascript">
		$(document).ready(function ()
		{
			setTimeout($.SalesPortal.MainMenu.checkIfShortcutsUpdated, 60000);
		});
    </script>
<? endif; ?>