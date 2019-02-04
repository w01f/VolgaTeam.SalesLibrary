<?
	/**
	 * @var $shortcut PageContentShortcut
	 */

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/css/bootstrap.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/font-awesome/css/font-awesome.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/material/material.min.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/vendor-customization.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/login.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/bootstrap/js/bootstrap.min.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/login.js?' . Yii::app()->params['version'], CClientScript::POS_END);
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
                            <p>Enter the secure password to view this page:</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="control-label" for="login-shortcut-password">Password:</label>
                                <input type="text" class="form-control" id="login-shortcut-password" value="">
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
            </div>
        </div>
    </div>
</div>
<!-- Login Form End -->

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
                                <label class="control-label" for="contact-station">Your Company:</label>
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
                                SEND
                            </button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-top: 20px">
                            Click the <strong>SEND</strong> Button, and a support technician will review your to resolve the issue.
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
			$.SalesPortal.ShortcutsPublicAuth.init({
				shortcutId: '<? echo $shortcut->id; ?>',
			});
		});
	});
</script>
