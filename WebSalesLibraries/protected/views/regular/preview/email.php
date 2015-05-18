<?
	/**
	 * @var $data DocumentPreviewData
	 * */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);
	$logos = QPageRecord::getPageLogoList();
	$selectedLogo = count($logos) > 0 ? $logos[0] : null;
?>
<div class="row">
	<ul class="nav nav-pills nav-stacked col col-xs-3" role="tablist" id="link-viewer-email-tabs">
		<li class="active">
			<a href="#link-viewer-email-tab-settings" role="tab" data-toggle="tab">
				<img src="<? echo sprintf('%s/images/preview/email/settings.png', $imageUrlPrefix); ?>">
				<strong>Link Settings</strong> </a>
		</li>
		<li>
			<a href="#link-viewer-email-tab-logo" role="tab" data-toggle="tab">
				<img src="<? echo sprintf('%s/images/preview/email/logo.png', $imageUrlPrefix); ?>">
				<strong>Header Logo</strong> </a>
		</li>
		<li>
			<a href="#link-viewer-email-tab-security" role="tab" data-toggle="tab">
				<img src="<? echo sprintf('%s/images/preview/email/security.png', $imageUrlPrefix); ?>">
				<strong>Security</strong> </a>
		</li>
	</ul>
	<div class="tab-content col-xs-9">
		<div role="tabpanel" class="tab-pane active" id="link-viewer-email-tab-settings">
			<form class="form-horizontal">
				<div class="form-group">
					<div class="checkbox">
						<label> <input id="add-page-name-enabled" type="checkbox" value="" checked>
							<strong>Page Header Text (shown with the link on the page)</strong> </label>
					</div>
					<label for="add-page-name" class="sr-only"></label><input type="text" class="form-control" id="add-page-name" value="<? echo $data->name; ?>">
				</div>
				<div class="form-group title">
					<label class="control-label"><strong>Link Expiration Date:</strong></label>
				</div>
				<div class="form-group" id="add-page-expires-in">
					<div class="col-xs-3">
						<button class="btn btn-default active" type="button" value="7">7 Days</button>
					</div>
					<div class="col-xs-3">
						<button class="btn btn-default" type="button" value="14">14 Days</button>
					</div>
					<div class="col-xs-3">
						<button class="btn btn-default" type="button" value="30">30 Days</button>
					</div>
					<div class="col-xs-3">
						<button class="btn btn-default" type="button" value="0">Never</button>
					</div>
				</div>
				<div class="form-group title">
					<label class="control-label"><strong>Link Options:</strong></label>
				</div>
				<div class="form-group">
					<div class="checkbox">
						<label>
							<input type="checkbox" id="add-page-disable-widgets" "value=""> Disable all Link Widget Icons
						</label>
					</div>
				</div>
				<div class="form-group">
					<div class="checkbox">
						<label>
							<input type="checkbox" id="add-page-disable-banners" value="">Disable all Link Banner Images
						</label>
					</div>
				</div>
				<div class="form-group">
					<div class="checkbox">
						<label> <input type="checkbox" id="add-page-show-links-as-url" value="">Display all Links as
							<span class="text-primary"><u>Blue Hyperlinks</u></span></label>
					</div>
				</div>
			</form>
		</div>
		<div role="tabpanel" class="tab-pane" id="link-viewer-email-tab-logo">
			<div class="logo-list">
				<ul class="nav nav-pills">
					<? foreach ($logos as $logo): ?>
						<li>
							<a href="#" <? if ($selectedLogo == $logo): ?>class="opened"<? endif; ?> style="margin-right: 40px;"><img src="<? echo $logo; ?>"></a>
						</li>
					<? endforeach; ?>
				</ul>
			</div>
		</div>
		<div role="tabpanel" class="tab-pane" id="link-viewer-email-tab-security">
			<form class="form-horizontal">
				<div class="form-group">
					<div class="checkbox">
						<label>
							<input type="checkbox" id="add-page-record-activity" value=""><strong>Email me each time someone clicks a link on this quickSITE</strong>
						</label>
					</div>
				</div>
				<div class="form-group">
					<div class="col-xs-3">
						<p class="form-control-static text-right">Cc Email:</p>
					</div>
					<div class="col-xs-8">
						<label for="add-page-activity-email-copy" class="sr-only"></label>
						<input type="email" id="add-page-activity-email-copy" class="form-control" disabled value="">
					</div>
				</div>
				<div class="form-group title">
					<div class="checkbox">
						<label>
							<input id="add-page-restricted" type="checkbox" value=""><strong>Require User Log-in</strong></label>
					</div>
					<p class="form-control-static text-danger text-left" style="padding-left: 15px">
						<small>(If you select this option, only Users who already have an official username and password can access this site)</small>
					</p>
				</div>
				<div class="form-group title">
					<div class="checkbox">
						<label>
							<input id="add-page-access-code-enabled" type="checkbox"><strong>Create a SECURE ACCESS Pin (4 Digit Number)</strong>
						</label>
					</div>
				</div>
				<div class="form-group">
					<div class="col-xs-2">
						<label for="add-page-access-code" class="sr-only"></label>
						<input type="text" maxlength="4" class="form-control" id="add-page-access-code" style="display: none;" value="">
					</div>
				</div>
			</form>
		</div>
	</div>
</div>
<div class="row">
	<div class="col-xs-9">
		<p class="text-danger">
			<strong>Important Info you should KNOW about EMAILING LINKS</strong><br>
			<small>You are Sending a WEB LINK to this file over the internet. The Recipient will receive an email with the website Link. Tell your recipient to click this link to view or download this file…</small>
		</p>
	</div>
	<div class="col-xs-3 text-right">
		<button class="btn btn-default send-email" type="button">Send</button>
	</div>
</div>