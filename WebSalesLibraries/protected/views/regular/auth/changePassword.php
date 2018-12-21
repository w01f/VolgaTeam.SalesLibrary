<?
	/**
	 * @var $login string
	 * @var $password string
	 * @var $rememberMe boolean
	 */

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/font-awesome/css/font-awesome.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/material/material.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/vendor-customization.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/login.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/login.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<style>
    .popup-area .mdl-card__actions a
    {
        color: <? echo '#'.Yii::app()->params['login']['theme_color'];?> !important;
    }

    #main-area .content a.mdl-button.mdl-button--accent,
    .modal-dialog .mdl-card__title .modal--logo,
    .modal-dialog .action-button
    {
        background-color: <? echo '#'.Yii::app()->params['login']['theme_color'];?> !important;
    }
</style>
<!-- Banner Area Start -->
<div id="main-area" data-img-src="<? echo Yii::app()->getBaseUrl(true) . '/images/auth/banner-bg.jpg'; ?>">
    <div class="vc-parent">
        <div class="vc-child">
            <div class="content">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="slider-2-content text-center">
                                <h4 class="mdl-typography--text-capitalize"></h4>
                                <a href="#"
                                   class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent"
                                   data-toggle="modal" data-target="#new-password-modal" style="display: none;">
                                    <i class="fa fa-user"></i> Login
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Banner Area End -->

<!-- New password Form Start -->
<div class="popup-area modal fade in" id="new-password-modal" tabindex="-1" role="dialog" style="display: block;">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <div class="modal--logo">
                        <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/auth/logo.svg'; ?>" alt="">
                    </div>
                </div>
                <div class="mdl-card__supporting-text">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="new-password-password">New Password:</label>
                                <input type="password" class="form-control" id="new-password-password" value="">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="new-password-password-confirm">Type Password
                                    Again:</label>
                                <input type="password" class="form-control" id="new-password-password-confirm" value="">
                            </div>
                        </div>
                    </div>
                    <div id="new-password-error-info" class="row error-info" style="display: none;">
                        <div class="col-xs-12 text-center">
                            <p class="bg-danger">Error found</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <button id="new-password-submit"
                                    class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent action-button">
                                Save
                            </button>
                        </div>
                    </div>
					<? if (Yii::app()->params['login']['complex_password']): ?>
                        <div class="row" style="margin-top: 20px;">
                            <div class="col-xs-12">
                                <button id="new-password-requirements-action"
                                        class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent action-button">
                                    Password Requirements
                                </button>
                            </div>
                        </div>
                        <div id="new-password-requirements" class="row" style="display: none;">
                            <div class="col-xs-12 text-center" style="padding-top: 20px">
                                Password MUST Be AT LEAST 8 characters<br><br>
                                Must contain at least 3 of these 4 conditions below:<br><br>
                                <b>1 CAPITAL LETTER</b><br>
                                <b>1 lower case letter</b><br>
                                <b>Number (1, 2, 3…)</b><br>
                                <b>Symbols (!, @, #...)</b>
                            </div>
                        </div>
					<? endif; ?>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- New password Form End -->
<script type="text/javascript">
	$(document).ready(function () {
		$.SalesPortal.Auth.init({
			login: '<? echo $login; ?>',
			password: '<? echo $password; ?>',
			rememberMe: <? echo $rememberMe ? 'true' : 'false'; ?>
		});
	});
</script>


