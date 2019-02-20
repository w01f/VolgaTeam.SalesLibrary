<?
	/**
	 * @var $login string
	 */
?>
<style>
    .change-password-dialog label {
        font-weight: normal;
    }

    .change-password-dialog .buttons-area .btn {
        width: 100px;
    }

    .change-password-dialog .user-login {
        padding-left: 0;
        background: none;
        -webkit-box-shadow: none;
        border: none;
        box-shadow: none;
    }
</style>
<div class="change-password-dialog">
    <div class="row">
        <div class="col-xs-12">
            <h3 class="header">
                <span class="title">Reset your Account Password</span>
            </h3>
        </div>
    </div>
    <div class="row" style="padding-bottom:30px">
        <div class="col-xs-12">
            <p class="text-muted">This is the password you use to access the site, outside of GrayConnect</p>
        </div>
    </div>
    <form class="form-horizontal">
        <div class="form-group">
            <label class="control-label col-md-5 text-left" for="change-password-dialog-password">Username:</label>
            <div class="col-md-7">
                <p class="form-control user-login"><? echo $login; ?></p>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-5 text-left" for="change-password-dialog-password">Type your new password
                here:</label>
            <div class="col-md-7">
                <input type="text" class="form-control" id="change-password-dialog-password" value="">
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-5 text-left" for="change-password-dialog-password-repeat">Type your
                password again:</label>
            <div class="col-md-7">
                <input type="text" class="form-control" id="change-password-dialog-password-repeat" value="">
            </div>
        </div>
    </form>
    <div class="row" id="change-password-dialog-error-info" style="display: none;">
        <div class="col-xs-12">
            <p class="error-text text-danger text-center">This is the password you use to access the site, outside of GrayConnect</p>
        </div>
    </div>
    <div class="row buttons-area" style="padding-top: 30px;">
        <div class="col-xs-3 col-xs-offset-2">
            <button class="btn btn-default log-action accept-button" type="button">Save</button>
        </div>
        <div class="col-xs-3 col-xs-offset-2">
            <button class="btn btn-default log-action cancel-button" type="button">Cancel</button>
        </div>
    </div>
</div>