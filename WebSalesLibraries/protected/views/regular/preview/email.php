<?
	/**
	 * @var $data PreviewData
	 * @var $isProtected boolean
	 */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);
	$logos = QPageRecord::getPageLogoList();
?>
<div id="email-content-<? echo $isProtected ? 'protected' : 'public'; ?>">
	<div class="row<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link"
	     data-log-action="Email Activity">
		<ul class="nav nav-pills nav-stacked col col-xs-3" role="tablist" id="link-viewer-email-tabs">
			<li class="active">
				<a class="log-action"
				   href="#link-viewer-email-tab-settings-<? echo $isProtected ? 'protected' : 'public'; ?>" role="tab"
				   data-toggle="tab">
					<img src="<? echo sprintf('%s/images/preview/email/settings.png', $imageUrlPrefix); ?>">
					<strong>Link Settings</strong> </a>
			</li>
			<li>
				<a class="log-action"
				   href="#link-viewer-email-tab-logo-<? echo $isProtected ? 'protected' : 'public'; ?>" role="tab"
				   data-toggle="tab">
					<img src="<? echo sprintf('%s/images/preview/email/logo.png', $imageUrlPrefix); ?>">
					<strong>Header Logo</strong> </a>
			</li>
			<? if (!$isProtected): ?>
				<li>
					<a class="log-action"
					   href="#link-viewer-email-tab-security-<? echo $isProtected ? 'protected' : 'public'; ?>"
					   role="tab"
					   data-toggle="tab">
						<img src="<? echo sprintf('%s/images/preview/email/security.png', $imageUrlPrefix); ?>">
						<strong>Security</strong> </a>
				</li>
			<? endif; ?>
		</ul>
		<div class="tab-content col-xs-9">
			<div role="tabpanel" class="tab-pane active"
			     id="link-viewer-email-tab-settings-<? echo $isProtected ? 'protected' : 'public'; ?>">
				<form class="form-horizontal">
					<div class="form-group">
						<div class="checkbox">
							<label> <input id="add-page-name-enabled" class="log-action" type="checkbox" value=""
							               checked>
								<strong>Page Header Text (shown with the link on the page)</strong> </label>
						</div>
						<label for="add-page-name" class="sr-only"></label><input type="text"
						                                                          class="form-control log-action"
						                                                          id="add-page-name"
						                                                          value="<? echo $data->name; ?>">
					</div>
					<div class="form-group title">
						<label class="control-label"><strong>Link Expiration Date:</strong></label>
					</div>
					<div class="form-group" id="add-page-expires-in">
						<div class="col-xs-3">
							<button class="btn btn-default log-action active" type="button" value="7">7 Days</button>
						</div>
						<div class="col-xs-3">
							<button class="btn btn-default log-action" type="button" value="14">14 Days</button>
						</div>
						<div class="col-xs-3">
							<button class="btn btn-default log-action" type="button" value="30">30 Days</button>
						</div>
						<div class="col-xs-3">
							<button class="btn btn-default log-action" type="button" value="0">Never</button>
						</div>
					</div>
					<div class="form-group title">
						<label class="control-label"><strong>Link Options:</strong></label>
					</div>
					<div class="form-group">
						<div class="checkbox">
							<label>
								<input type="checkbox" id="add-page-disable-widgets" class="log-action" value="">
								Disable all Link Widget Icons
							</label>
						</div>
					</div>
					<div class="form-group">
						<div class="checkbox">
							<label>
								<input type="checkbox" id="add-page-disable-banners" class="log-action" value="">Disable
								all Link Banner Images
							</label>
						</div>
					</div>
					<div class="form-group">
						<div class="checkbox">
							<label> <input type="checkbox" id="add-page-show-links-as-url" class="log-action" value="">Display
								all Links as
								<span class="text-primary"><u>Blue Hyperlinks</u></span></label>
						</div>
					</div>
				</form>
			</div>
			<div role="tabpanel" class="tab-pane"
			     id="link-viewer-email-tab-logo-<? echo $isProtected ? 'protected' : 'public'; ?>">
				<div class="checkbox">
					<label>
						<input id="add-page-show-logo" type="checkbox" class="log-action" value="">
						<strong>Show a Logo on your QuickSite:</strong>
					</label>
				</div>
				<div class="logo-list disabled">
					<ul class="nav nav-pills">
						<? foreach ($logos as $logo): ?>
							<li>
								<a href="#" class="log-action" style="margin-right: 40px;"><img src="<? echo $logo; ?>"></a>
							</li>
						<? endforeach; ?>
					</ul>
				</div>
			</div>
			<? if (!$isProtected): ?>
				<div role="tabpanel" class="tab-pane"
				     id="link-viewer-email-tab-security-<? echo $isProtected ? 'protected' : 'public'; ?>">
					<form class="form-horizontal">
						<div class="form-group">
							<div class="checkbox">
								<label>
									<input type="checkbox" id="add-page-record-activity" class="log-action"
									       value=""><strong>Email me each time someone clicks a link on this
										quickSITE</strong>
								</label>
							</div>
						</div>
						<div class="form-group">
							<div class="col-xs-3">
								<p class="form-control-static text-right">Cc Email:</p>
							</div>
							<div class="col-xs-8">
								<label for="add-page-activity-email-copy" class="sr-only"></label>
								<input type="email" id="add-page-activity-email-copy" class="form-control log-action"
								       disabled value="">
							</div>
						</div>
						<div class="form-group title">
							<div class="checkbox">
								<label>
									<input id="add-page-access-code-enabled" type="checkbox" class="log-action"><strong>Create
										a SECURE ACCESS Pin (4 Digit Number)</strong>
								</label>
							</div>
						</div>
						<div class="form-group">
							<div class="col-xs-2">
								<label for="add-page-access-code" class="sr-only"></label>
								<input type="text" maxlength="4" class="form-control log-action"
								       id="add-page-access-code"
								       style="display: none;" value="">
							</div>
						</div>
					</form>
				</div>
			<? endif; ?>
		</div>
	</div>
	<div class="row">
		<div class="col-xs-9">
			<p class="text-danger">
				<? if ($isProtected): ?>
					<strong>You are emailing a SECURE LINK:</strong><br>
					<small>Only authorized users will be able to view this link.</small><br>
					<small>Do NOT send a SECURE LINK to clients.</small>
				<? else: ?>
					<strong>You are emailing a PUBLIC LINK:</strong><br>
					<small>Public links are for clients or other partners who do not have secure user accounts on this site…</small>
				<? endif; ?>
			</p>
		</div>
		<div class="col-xs-3 text-right">
			<button class="btn btn-default send-email log-action" type="button" data-log-action="Email Send">Send
			</button>
		</div>
	</div>
</div>