<? if (Yii::app()->params['jqm_theme']['enabled'] === true): ?>
	<style>
		#jqm-markup .ui-page-theme-a a,
		#jqm-markup .ui-bar-a a,
		#jqm-markup .ui-body-a a,
		#jqm-markup .ui-group-theme-a a,
		#jqm-markup .ui-page-theme-a a:visited,
		#jqm-markup .ui-bar-a a:visited,
		#jqm-markup .ui-body-a a:visited,
		#jqm-markup .ui-group-theme-a a:visited,
		#jqm-markup .ui-page-theme-a a:active,
		#jqm-markup .ui-bar-a a:active,
		#jqm-markup .ui-body-a a:active,
		#jqm-markup .ui-group-theme-a a:active,
		#jqm-markup .ui-page-theme-a .ui-btn,
		#jqm-markup .ui-bar-a .ui-btn,
		#jqm-markup .ui-body-a .ui-btn,
		#jqm-markup .ui-group-theme-a .ui-btn,html body .ui-btn.ui-btn-a,
		#jqm-markup .ui-page-theme-a .ui-btn:visited,
		#jqm-markup .ui-bar-a .ui-btn:visited,
		#jqm-markup .ui-body-a .ui-btn:visited,
		#jqm-markup .ui-group-theme-a .ui-btn:visited,
		#jqm-markup .ui-btn.ui-btn-a:visited,
		#jqm-markup .ui-page-theme-a .ui-btn:hover,
		#jqm-markup .ui-bar-a .ui-btn:hover,
		#jqm-markup .ui-body-a .ui-btn:hover,
		#jqm-markup .ui-group-theme-a .ui-btn:hover,
		#jqm-markup .ui-btn.ui-btn-a:hover,
		#jqm-markup .ui-page-theme-a .ios-checkbox.ui-checkbox-on:after,
		#jqm-markup .ui-bar-a .ios-checkbox.ui-checkbox-on:after,
		#jqm-markup .ui-body-a .ios-checkbox.ui-checkbox-on:after,
		#jqm-markup .ui-group-theme-a .ios-checkbox.ui-checkbox-on:after,
		#jqm-markup .ios-checkbox.ui-btn.ui-checkbox-on.ui-btn-a:after,
		#jqm-markup .ui-card.ui-card-a,
		#jqm-markup .ui-listview.ui-group-theme-a a
		{
			color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-a a:hover,
		#jqm-markup .ui-bar-a a:hover,
		#jqm-markup .ui-body-a a:hover,
		#jqm-markup .ui-group-theme-a a:hover,
		#jqm-markup .ui-page-theme-a .ui-btn:active,
		#jqm-markup .ui-bar-a .ui-btn:active,
		#jqm-markup .ui-body-a .ui-btn:active,
		#jqm-markup .ui-group-theme-a .ui-btn:active,
		#jqm-markup .ui-btn.ui-btn-a:active,
		#jqm-markup .ui-listview.ui-group-theme-a a:hover
		{
			color: <? echo '#'.Yii::app()->params['jqm_theme']['minor_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-a .ui-btn.ui-btn-active,
		#jqm-markup .ui-bar-a .ui-btn.ui-btn-active,
		#jqm-markup .ui-body-a .ui-btn.ui-btn-active,
		#jqm-markup .ui-group-theme-a .ui-btn.ui-btn-active,
		#jqm-markup .ui-btn.ui-btn-a.ui-btn-active,
		#jqm-markup .ui-page-theme-a .ui-flipswitch-active,
		#jqm-markup .ui-bar-a .ui-flipswitch-active,
		#jqm-markup .ui-body-a .ui-flipswitch-active,
		#jqm-markup .ui-group-theme-a .ui-flipswitch-active,
		#jqm-markup .ui-flipswitch.ui-bar-a.ui-flipswitch-active,
		#jqm-markup .ui-page-theme-a .ui-slider-track .ui-btn-active,
		#jqm-markup .ui-bar-a .ui-slider-track .ui-btn-active,
		#jqm-markup .ui-body-a .ui-slider-track .ui-btn-active,
		#jqm-markup .ui-group-theme-a .ui-slider-track .ui-btn-active,
		#jqm-markup div.ui-slider-track.ui-body-a .ui-btn-active
		{
			background-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			color: <? echo '#'.Yii::app()->params['jqm_theme']['minor_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-a .ui-radio-on:after,
		#jqm-markup .ui-bar-a .ui-radio-on:after,
		#jqm-markup .ui-body-a .ui-radio-on:after,
		#jqm-markup .ui-group-theme-a .ui-radio-on:after,
		#jqm-markup .ui-btn.ui-radio-on.ui-btn-a:after,
		#jqm-markup .ui-circle
		{
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}

		/*+++++++++++++++++++++++++++++++++++++++++++++++++*/

		#jqm-markup .ui-bar-d,
		#jqm-markup .ui-page-theme-d .ui-bar-inherit,
		#jqm-markup .ui-bar-d .ui-bar-inherit,
		#jqm-markup .ui-body-d .ui-bar-inherit,
		#jqm-markup .ui-group-theme-d .ui-bar-inherit,
		#jqm-markup .ui-body-d,
		#jqm-markup .ui-page-theme-d .ui-body-inherit,
		#jqm-markup .ui-bar-d .ui-body-inherit,
		#jqm-markup .ui-body-d .ui-body-inherit,
		#jqm-markup .ui-group-theme-d .ui-body-inherit,
		#jqm-markup .ui-panel-page-container-d,
		#jqm-markup .ui-card.ui-card-d
		{
			background: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			color: <? echo '#'.Yii::app()->params['jqm_theme']['minor_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-d a:hover,
		#jqm-markup .ui-bar-d a:hover,
		#jqm-markup .ui-body-d a:hover,
		#jqm-markup .ui-group-theme-d a:hover,
		#jqm-markup .ui-page-theme-d .ui-btn.ui-btn-active,
		#jqm-markup .ui-bar-d .ui-btn.ui-btn-active,
		#jqm-markup .ui-body-d .ui-btn.ui-btn-active,
		#jqm-markup .ui-group-theme-d .ui-btn.ui-btn-active,
		#jqm-markup .ui-btn.ui-btn-d.ui-btn-active,
		#jqm-markup .ui-page-theme-d .ui-checkbox-on:after,
		#jqm-markup .ui-bar-d .ui-checkbox-on:after,
		#jqm-markup .ui-body-d .ui-checkbox-on:after,
		#jqm-markup .ui-group-theme-d .ui-checkbox-on:after,
		#jqm-markup .ui-btn.ui-checkbox-on.ui-btn-d:after,
		#jqm-markup .ui-page-theme-d .ui-flipswitch-active,
		#jqm-markup .ui-bar-d .ui-flipswitch-active,
		#jqm-markup .ui-body-d .ui-flipswitch-active,
		#jqm-markup .ui-group-theme-d .ui-flipswitch-active,
		#jqm-markup .ui-flipswitch.ui-bar-d.ui-flipswitch-active,
		#jqm-markup .ui-page-theme-d .ui-slider-track .ui-btn-active,
		#jqm-markup .ui-bar-d .ui-slider-track .ui-btn-active,
		#jqm-markup .ui-body-d .ui-slider-track .ui-btn-active,
		#jqm-markup .ui-group-theme-d .ui-slider-track .ui-btn-active,
		#jqm-markup div.ui-slider-track.ui-body-d .ui-btn-active,
		#jqm-markup .ui-listview.ui-group-theme-d a:hover
		{
			color: <? echo '#'.Yii::app()->params['jqm_theme']['minor_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-d .ui-btn,
		#jqm-markup .ui-bar-d .ui-btn,
		#jqm-markup .ui-body-d .ui-btn,body .ui-group-theme-d .ui-btn,
		#jqm-markup .ui-btn.ui-btn-d,
		#jqm-markup .ui-page-theme-d .ui-btn:visited,
		#jqm-markup .ui-bar-d .ui-btn:visited,
		#jqm-markup .ui-body-d .ui-btn:visited,
		#jqm-markup .ui-group-theme-d .ui-btn:visited,
		#jqm-markup .ui-btn.ui-btn-d:visited,
		#jqm-markup .ui-page-theme-d .ui-btn:hover,
		#jqm-markup .ui-bar-d .ui-btn:hover,
		#jqm-markup .ui-body-d .ui-btn:hover,
		#jqm-markup .ui-group-theme-d .ui-btn:hover,
		#jqm-markup .ui-btn.ui-btn-d:hover
		{
			background: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			color: <? echo '#'.Yii::app()->params['jqm_theme']['minor_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-d .ui-btn:active,
		#jqm-markup .ui-bar-d .ui-btn:active,
		#jqm-markup .ui-body-d .ui-btn:active,
		#jqm-markup .ui-group-theme-d .ui-btn:active,
		#jqm-markup .ui-btn.ui-btn-d:active
		{
			background: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			color: <? echo '#'.Yii::app()->params['jqm_theme']['minor_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-d .ui-flipswitch-active .ui-flipswitch-on,
		#jqm-markup .ui-bar-d .ui-flipswitch-active .ui-flipswitch-on,
		#jqm-markup .ui-body-d .ui-flipswitch-active .ui-flipswitch-on,
		#jqm-markup .ui-group-theme-d .ui-flipswitch-active .ui-flipswitch-on,
		#jqm-markup .ui-flipswitch.ui-bar-d.ui-flipswitch-active .ui-flipswitch-on,
		#jqm-markup .ui-page-theme-d .ui-clear,
		#jqm-markup .ui-bar-d .ui-clear,
		#jqm-markup .ui-body-d .ui-clear,
		#jqm-markup .ui-group-theme-d .ui-clear,
		#jqm-markup .ui-btn-d.ui-clear,
		#jqm-markup .ui-page-theme-d .ui-clear:visited,
		#jqm-markup .ui-bar-d .ui-clear:visited,
		#jqm-markup .ui-body-d .ui-clear:visited,
		#jqm-markup .ui-group-theme-d .ui-clear:visited,
		#jqm-markup .ui-btn-d.ui-clear:visited,
		#jqm-markup .ui-listview.ui-group-theme-d a
		{
			color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-d .ui-radio-on:after,
		#jqm-markup .ui-bar-d .ui-radio-on:after,
		#jqm-markup .ui-body-d .ui-radio-on:after,
		#jqm-markup .ui-group-theme-d .ui-radio-on:after,
		#jqm-markup .ui-btn.ui-radio-on.ui-btn-d:after
		{
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}

		#jqm-markup .ui-page-theme-d .ui-outline,
		#jqm-markup .ui-bar-d .ui-outline,
		#jqm-markup .ui-body-d .ui-outline,
		#jqm-markup .ui-group-theme-d .ui-outline,
		#jqm-markup .ui-btn-d.ui-outline,
		#jqm-markup .ui-page-theme-d .ui-outline:visited,
		#jqm-markup .ui-bar-d .ui-outline:visited,
		#jqm-markup .ui-body-d .ui-outline:visited,
		#jqm-markup .ui-group-theme-d .ui-outline:visited,
		#jqm-markup .ui-btn-d.ui-outline:visited
		{
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}

		/*++++++++++++++++++++++++++++++++++++++++++++++*/

		#email-tab-security .edit-field label.ios-checkbox,
		.link-viewer-page .action span,
		.login-content .intro-message,
		.search-results-page .content-data table td.link-name .name
		{
			color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}

		#email-page .navbar .ui-state-active a,
		#search-tabs .navbar .ui-state-active a
		{
			background-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}

		#search-tab-libraries .advanced-filter-button
		{
			border-color: <? echo '#'.Yii::app()->params['jqm_theme']['major_color']; ?> !important;
		}
	</style>
<?endif;?>
