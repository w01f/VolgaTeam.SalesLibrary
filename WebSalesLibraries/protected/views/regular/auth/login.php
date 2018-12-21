<?
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

    .modal-dialog .action-button.mdl-button--disabled
    {
        background-color: #f1f1f1 !important;
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
                                   data-toggle="modal" data-target="#login-modal">
                                    <i class="fa fa-user"></i> Login
                                </a>
                                <a href="#"
                                   class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent"
                                   data-toggle="modal" data-target="#new-password-modal" style="display: none;">
                                    <i class="fa fa-user"></i> Login
                                </a>
                                <a href="#"
                                   class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent"
                                   data-toggle="modal" data-target="#recover-password-modal">
                                    <i class="fa fa-lock"></i> Forgot
                                </a>
                                <a href="#"
                                   class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent"
                                   data-toggle="modal" data-target="#contact-modal">
                                    <i class="fa fa-envelope"></i> Contact
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

<!-- Login Form Start -->
<div class="popup-area modal fade" id="login-modal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
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
                                <label class="control-label" for="login-user-name">Username:</label>
                                <input type="text" class="form-control" id="login-user-name" value="">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="login-password">Password:</label>
                                <input type="password" class="form-control" id="login-password" value="">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" id="login-remember-me" checked="checked"> Keep me logged in
                                </label>
                            </div>
                        </div>
                    </div>
                    <div id="login-error-info" class="row error-info" style="display: none;">
                        <div class="col-xs-12 text-center">
                            <p class="bg-danger">Error found</p>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-xs-12">
                            <button id="login-submit" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent action-button">
                                Login
                            </button>
                        </div>
                    </div>
                </div>
                <div class="mdl-card__actions mdl-card--border clearfix">
                    <a href="#" class="pull-left mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect"
                       data-toggle="modal" data-target="#recover-password-modal" data-dismiss="modal">
                        Forgot Password?
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Login Form End -->

<!-- New password Form Start -->
<div class="popup-area modal fade" id="new-password-modal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
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

<!-- Recover Form Start -->
<div class="popup-area modal fade" id="recover-password-modal" tabindex="-1" role="dialog"
     aria-labelledby="signupFormModal">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            <div class="mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <div class="modal--logo">
                        <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/auth/logo.svg'; ?>" alt="">
                    </div>
                </div>
                <div id="recover-password-main-area" class="mdl-card__supporting-text">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="recover-password-email-address">E-mail address:</label>
                                <input type="email" class="form-control" id="recover-password-email-address" value="">
                            </div>
                        </div>
                    </div>
                    <div id="recover-password-error-info" class="row error-info" style="display: none;">
                        <div class="col-xs-12 text-center">
                            <p class="bg-danger">Error found</p>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-xs-12">
                            <button id="recover-password-submit" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent action-button">
                                Recover
                            </button>
                        </div>
                    </div>
                </div>
                <div id="recover-password-success-info-area" class="mdl-card__supporting-text" style="display: none;">
                    <div class="row">
                        <div class="col-xs-12 text-center">
                            A temporary password has been sent<br>Check your inbox or junk mail filter
                        </div>
                    </div>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-xs-12">
                            <button id="recover-password-success-confirm"
                                    class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent action-button">
                                OK
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Recover Form End -->

<!-- Contact Form Start -->
<div class="popup-area modal fade" id="contact-modal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            <div class="mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <div class="modal--logo">
                        <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/auth/logo.svg'; ?>" alt="">
                    </div>
                </div>
                <div class="mdl-card__supporting-text">
                    <div class="row">
                        <div class="col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="contact-full-name">Full Name:</label>
                                <input type="text" class="form-control" id="contact-full-name" value="">
                            </div>
                        </div>
                        <div class="col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="contact-email-address">E-mail address:</label>
                                <input type="email" class="form-control" id="contact-email-address" value="">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="contact-station">Media Property/Station:</label>
                                <input type="text" class="form-control" id="contact-station" value="">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="contact-text">What issues are you having with the
                                    site?</label>
                                <textarea class="form-control" id="contact-text" rows="3"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <button id="contact-submit"
                                    class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent action-button mdl-button--disabled">
                                Send
                            </button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-top: 20px">
                            Click the <strong>SEND</strong> Button, and a support technician will review your account &
                            reset your password.<br><br>
                            Check your <strong>INBOX</strong> or <strong>JUNK MAIL</strong> for the updated account
                            email instructions.
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Contact Form End -->
<script type="text/javascript">
	$(document).ready(function () {
		$(document).ready(function () {
			$.SalesPortal.Auth.init(undefined);
		});
	});
</script>
