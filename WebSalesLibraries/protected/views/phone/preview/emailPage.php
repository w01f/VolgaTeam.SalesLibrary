<?
	/**
	 * @var $previewData FilePreviewData|UrlPreviewData
	 */

	$authorized = false;
	$userId = Yii::app()->user->getId();
	if (isset($userId))
		$authorized = true;

	$logos = QPageRecord::getPageLogoList();
?>
<? if ($authorized && $previewData->allowAddToQuickSite): ?>
	<div id="email-page" data-role="page" data-cache="never" data-dom-cache="false" data-ajax="false">
		<div class="page-header" data-role="header" data-position="fixed" data-theme="a">
			<h3>Email this link</h3>
		</div>
		<div data-role='main' class="main-content">
			<div id="email-tabs" data-role="tabs">
				<div class="navbar" data-role="navbar" data-grid="b">
					<ul>
						<li><a href="#email-tab-settings" class="ui-btn-active">Settings</a></li>
						<li><a href="#email-tab-logo">Logo</a></li>
						<li><a href="#email-tab-security">Security</a></li>
					</ul>
				</div>
				<div id="email-tab-settings" class="ui-content email-tab-content">
					<legend>Page Header Text:</legend>
					<ul class="edit-fields" data-role="listview">
						<li class="edit-field" data-icon="false">
							<label for="email-tab-page-name" class="ui-hidden-accessible"></label>
							<input type="text" id="email-tab-page-name" value="<? echo isset($previewData->fileName) ? $previewData->fileName : $previewData->name; ?>">
						</li>
					</ul>
					<legend>Link Expires:</legend>
					<fieldset data-role="controlgroup">
						<label for="email-tab-expire-7" class="ios-radio ui-icon-ion-checkmark">7 Days</label><input type="radio" id="email-tab-expire-7" name="email-tab-expire-toggle" class="email-tab-expire-toggle" value="7" checked>
						<label for="email-tab-expire-14" class="ios-radio ui-icon-ion-checkmark">14 Days</label><input type="radio" id="email-tab-expire-14" name="email-tab-expire-toggle" class="email-tab-expire-toggle" value="14">
						<label for="email-tab-expire-30" class="ios-radio ui-icon-ion-checkmark">30 Days</label><input type="radio" id="email-tab-expire-30" name="email-tab-expire-toggle" class="email-tab-expire-toggle" value="30">
						<label for="email-tab-expire-never" class="ios-radio ui-icon-ion-checkmark">Never</label><input type="radio" id="email-tab-expire-never" name="email-tab-expire-toggle" class="email-tab-expire-toggle" value="0">
					</fieldset>
					<legend>Link Options:</legend>
					<fieldset data-role="controlgroup">
						<label for="email-tab-options-disable-widgets" class="ios-checkbox ui-icon-ion-checkmark">Disable all widgets</label><input type="checkbox" id="email-tab-options-disable-widgets" class="email-tab-options-toggle">
						<label for="email-tab-options-disable-banners" class="ios-checkbox ui-icon-ion-checkmark">Disable all banners</label><input type="checkbox" id="email-tab-options-disable-banners" class="email-tab-options-toggle">
						<label for="email-tab-options-enable-blue-links" class="ios-checkbox ui-icon-ion-checkmark">Enable blue hyperlinks</label><input type="checkbox" id="email-tab-options-enable-blue-links" class="email-tab-options-toggle">
					</fieldset>
				</div>
				<div id="email-tab-logo" class="ui-content email-tab-content">
					<ul data-role="listview">
						<? $i = 0; ?>
						<? foreach ($logos as $logo): ?>
							<? $selected = $i == 0; ?>
							<li data-icon="false"<? if ($selected): ?> data-theme="e" class="selected"<? endif; ?>>
								<a class="qpage-logo" href="#"><span><img src="<? echo $logo; ?>"></span></a>
							</li>
							<? $i++; ?>
						<? endforeach; ?>
					</ul>
				</div>
				<div id="email-tab-security" class="ui-content email-tab-content">
					<ul class="edit-fields" data-role="listview">
						<li class="edit-field" data-icon="false">
							<fieldset data-role="controlgroup">
								<label for="email-tab-security-email-send" class="ios-checkbox ui-icon-ion-checkmark">Email me on Link Clicks:</label>
								<input type="checkbox" id="email-tab-security-email-send">
								<label for="email-tab-security-email-address" class="ui-hidden-accessible"></label>
								<input id="email-tab-security-email-address" placeholder="Cc Email address…" disabled style="margin-left: 35px">
							</fieldset>
						</li>
						<li class="edit-field" data-icon="false">
							<fieldset data-role="controlgroup">
								<label for="email-tab-security-require-login" class="ios-checkbox ui-icon-ion-checkmark">Require User Log In</label>
								<input type="checkbox" id="email-tab-security-require-login">
								<p style="margin-left: 35px">Only authorized site users can view this link…</p>
							</fieldset>
						</li>
						<li class="edit-field" data-icon="false">
							<fieldset data-role="controlgroup">
								<label for="email-tab-security-access-code-enable" class="ios-checkbox ui-icon-ion-checkmark">Secure Access PIN</label>
								<input type="checkbox" id="email-tab-security-access-code-enable">
								<label for="email-tab-security-access-code" class="ui-hidden-accessible"></label>
								<input type="text" id="email-tab-security-access-code" maxlength="4" placeholder="Type 4 Digit Code…" disabled style="margin-left: 35px">
							</fieldset>
						</li>
					</ul>
				</div>
			</div>
		</div>
		<div class="page-footer main-footer" data-role='footer' data-position="fixed" data-theme="a">
			<div class="ui-grid-a buttons">
				<div class="ui-block-a">
					<a class="button accept" href="#" data-role="button" data-inline="true" data-mini="true" data-theme="d">Send Email</a>
				</div>
				<div class="ui-block-b">
					<a class="button cancel" href="#link-viewer" data-role="button" data-inline="true" data-transition="slidefade" data-direction="reverse" data-mini="true" data-theme="b">Cancel</a>
				</div>
			</div>
		</div>
		<div class="service-data">
			<div class="link-id"><? echo $previewData->linkId; ?></div>
		</div>
	</div>
<? endif; ?>