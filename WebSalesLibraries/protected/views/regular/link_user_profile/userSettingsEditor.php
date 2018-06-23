<?
	/**
	 * @var $userProfile UserProfileModel
	 */
?>
<div class="user-preferences-editor logger-form" data-log-group="User Preferences" data-log-action="Edit">
	<div class="row">
		<div class="col-xs-12">
			<h3 class="header">
				<span class="title">Site Preferences & Default Settings</span>
			</h3>
		</div>
	</div>
    <div class="row">
        <div class="col-xs-12">
            <div id="user-preferences-tabs-headers" class="tab-pages scroll_tabs_theme_light">
                <span class="page-tab-header log-action selected">Files
                    <span class="service-data">
                        <span class="tab-id">#user-preferences-links</span>
                    </span>
                </span>
                <span class="page-tab-header">Email
                    <span class="service-data">
                        <span class="tab-id">#user-preferences-email</span>
                    </span>
                </span>
                <span class="page-tab-header">QuickSites
                    <span class="service-data">
                        <span class="tab-id">#user-preferences-qpage</span>
                    </span>
                </span>
            </div>
        </div>
    </div>
    <div id="user-preferences-tabs-content" style="height: 250px;">
        <div id="user-preferences-links" class="selected">
            <div class="row link-settings-row">
                <div class="col-xs-2">
                    Power Point
                </div>
                <div class="col-xs-2 text-center">
                    <div class="checkbox default">
                        <label><input class="log-action" type="checkbox"
                                      <? if ($userProfile->powerPointSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
                    </div>
                </div>
                <? if (Yii::app()->params['one_drive_links']['enabled']): ?>
                    <div class="col-xs-2 text-center">
                        <div class="checkbox">
                            <label><input id="user-link-preferences-power-point-open-one-drive" class="log-action"
                                          type="checkbox"
                                          <? if ($userProfile->powerPointSettings->forceOneDriveOpen == true): ?>checked<? endif; ?>>OneDrive
                            </label>
                        </div>
                    </div>
                <? endif; ?>
                <div class="col-xs-3 text-center">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-power-point-force-EO-open" class="log-action" type="checkbox"
                                      <? if ($userProfile->powerPointSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download
                            & Open</label>
                    </div>
                </div>
            </div>
            <div class="row link-settings-row">
                <div class="col-xs-2">
                    Word
                </div>
                <div class="col-xs-2 text-center">
                    <div class="checkbox default">
                        <label><input class="log-action" type="checkbox"
                                      <? if ($userProfile->docSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
                    </div>
                </div>
                <? if (Yii::app()->params['one_drive_links']['enabled']): ?>
                    <div class="col-xs-2 text-center">
                        <div class="checkbox">
                            <label><input id="user-link-preferences-doc-open-one-drive" class="log-action" type="checkbox"
                                          <? if ($userProfile->docSettings->forceOneDriveOpen == true): ?>checked<? endif; ?>>OneDrive
                            </label>
                        </div>
                    </div>
                <? endif; ?>
                <div class="col-xs-3 text-center">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-doc-force-EO-open" class="log-action" type="checkbox"
                                      <? if ($userProfile->docSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
                            Open</label>
                    </div>
                </div>
            </div>
            <div class="row link-settings-row">
                <div class="col-xs-2">
                    Excel
                </div>
                <div class="col-xs-2 text-center">
                    <div class="checkbox default">
                        <label><input class="log-action" type="checkbox"
                                      <? if ($userProfile->xlsSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
                    </div>
                </div>
                <? if (Yii::app()->params['one_drive_links']['enabled']): ?>
                    <div class="col-xs-2 text-center">
                        <div class="checkbox">
                            <label><input id="user-link-preferences-xls-open-one-drive" class="log-action" type="checkbox"
                                          <? if ($userProfile->xlsSettings->forceOneDriveOpen == true): ?>checked<? endif; ?>>OneDrive
                            </label>
                        </div>
                    </div>
                <? endif; ?>
                <div class="col-xs-3 text-center">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-xls-force-EO-open" class="log-action" type="checkbox"
                                      <? if ($userProfile->xlsSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
                            Open</label>
                    </div>
                </div>
                <div class="col-xs-3 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-xls-force-open-gallery" class="log-action" type="checkbox"
                                      <? if ($userProfile->xlsSettings->forceOpenGallery == true): ?>checked<? endif; ?>>Preview (less 10 mb)
                        </label>
                    </div>
                </div>
            </div>
            <div class="row link-settings-row">
                <div class="col-xs-12">
                    <div style="margin-bottom: 20px"></div>
                </div>
            </div>
            <div class="row link-settings-row">
                <div class="col-xs-2">
                    PDF
                </div>
                <div class="col-xs-2 text-center">
                    <div class="checkbox default">
                        <label><input class="log-action" type="checkbox"
                                      <? if ($userProfile->pdfSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
                    </div>
                </div>
                <? if (Yii::app()->params['one_drive_links']['enabled']): ?>
                    <div class="col-xs-2 text-center">
                        <div class="checkbox">
                            <label><input id="user-link-preferences-pdf-open-one-drive" class="log-action" type="checkbox"
                                          <? if ($userProfile->pdfSettings->forceOneDriveOpen == true): ?>checked<? endif; ?>>OneDrive
                            </label>
                        </div>
                    </div>
                <? endif; ?>
                <div class="col-xs-3 text-center">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-pdf-force-EO-open" class="log-action" type="checkbox"
                                      <? if ($userProfile->pdfSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
                            Open</label>
                    </div>
                </div>
                <div class="col-xs-3 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-pdf-force-web-open" class="log-action" type="checkbox"
                                      <? if ($userProfile->pdfSettings->forceWebOpen == true): ?>checked<? endif; ?>>Open New
                            Browser Tab
                        </label>
                    </div>
                </div>
            </div>
            <div class="row link-settings-row">
                <div class="col-xs-2">
                    Images
                </div>
                <div class="col-xs-2 text-center">
                    <div class="checkbox default">
                        <label><input class="log-action" type="checkbox"
                                      <? if ($userProfile->imageSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
                    </div>
                </div>
                <? if (Yii::app()->params['one_drive_links']['enabled']): ?>
                    <div class="col-xs-2 text-center">
                        <div class="checkbox">
                            <label><input id="user-link-preferences-image-open-one-drive" class="log-action" type="checkbox"
                                          <? if ($userProfile->imageSettings->forceOneDriveOpen == true): ?>checked<? endif; ?>>OneDrive
                            </label>
                        </div>
                    </div>
                <? endif; ?>
                <div class="col-xs-3 text-center">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-image-force-EO-open" class="log-action" type="checkbox"
                                      <? if ($userProfile->imageSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
                            Open</label>
                    </div>
                </div>
                <div class="col-xs-3 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-image-force-web-open" class="log-action" type="checkbox"
                                      <? if ($userProfile->imageSettings->forceWebOpen == true): ?>checked<? endif; ?>>Open New
                            Browser Tab
                        </label>
                    </div>
                </div>
            </div>
            <div class="row popup-blocker-warning">
                <div>
                    <img src="<? echo Yii::app()->getBaseUrl(true) . '/images/popup-blocker-warning.png' ?>" style="height: 48px">
                </div>
                <div style="width: 100%;padding-left: 10px;">
                    <span style="color: red">You may need to ALLOW Pop-Ups to view OneDrive links, PDFs or Images</span>
                    <br>
                    <span>Click <a
                            href="https://support.google.com/chrome/answer/95472?co=GENIE.Platform%3DDesktop&hl=en" target="_blank">HERE</a> to learn more</span>
                </div>
            </div>
        </div>
        <div id="user-preferences-email">
            <div class="row">
                <div class="col-xs-12">
                    <br>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-email-show-links-as-url" class="log-action" type="checkbox"
		                              <? if ($userProfile->defaultEmailSettings->showLinksAsUrl == true): ?>checked<? endif; ?>>Display all Links as <span class="text-primary"><u>Blue Hyperlinks</u></span></label>
                    </div>
                </div>
                <div class="col-xs-8">
                    <div class="row">
                        <div class="col-xs-3 text-left">
                            Link Expires:
                        </div>
                        <div class="col-xs-2 text-left">
                            <div class="checkbox email-expires-value">
                                <label><input id="user-link-preferences-email-expires-7" class="log-action" type="checkbox"
                                              <? if ($userProfile->defaultEmailSettings->expiresInDays == 7): ?>checked<? endif; ?>>7 days</label>
                            </div>
                        </div>
                        <div class="col-xs-2 text-left">
                            <div class="checkbox email-expires-value">
                                <label><input id="user-link-preferences-email-expires-14" class="log-action" type="checkbox"
                                              <? if ($userProfile->defaultEmailSettings->expiresInDays == 14): ?>checked<? endif; ?>>14 days</label>
                            </div>
                        </div>
                        <div class="col-xs-2 text-left">
                            <div class="checkbox email-expires-value">
                                <label><input id="user-link-preferences-email-expires-30" class="log-action" type="checkbox"
                                              <? if ($userProfile->defaultEmailSettings->expiresInDays == 30): ?>checked<? endif; ?>>30 days</label>
                            </div>
                        </div>
                        <div class="col-xs-2 text-left">
                            <div class="checkbox email-expires-value">
                                <label><input id="user-link-preferences-email-expires-0" class="log-action" type="checkbox"
			                                  <? if ($userProfile->defaultEmailSettings->expiresInDays == 0): ?>checked<? endif; ?>>Never</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <br>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-email-disable-widgets" class="log-action" type="checkbox"
		                              <? if ($userProfile->defaultEmailSettings->disableWidgets == true): ?>checked<? endif; ?>>Disable all Link Widget Icons</label>
                    </div>
                </div>
                <div class="col-xs-8">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="checkbox">
                                <label><input id="user-link-preferences-email-disable-banners" class="log-action" type="checkbox"
                                              <? if ($userProfile->defaultEmailSettings->disableBanners == true): ?>checked<? endif; ?>>Disable all Link Banner Images</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <br>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-email-auto-launch" class="log-action" type="checkbox"
				                      <? if ($userProfile->defaultEmailSettings->autoLaunch == true): ?>checked<? endif; ?>>Auto Launch</label>
                    </div>
                </div>
                <div class="col-xs-8">
                    <div class="row">
                        <div class="col-xs-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="user-preferences-qpage">
            <div class="row">
                <div class="col-xs-12">
                    <br>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-qpage-require-login" class="log-action" type="checkbox"
				                      <? if ($userProfile->defaultQPageSettings->requireLogin == true): ?>checked<? endif; ?>>Require User login and password to view site</label>
                    </div>
                </div>
                <div class="col-xs-6 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-qpage-disable-banners" class="log-action" type="checkbox"
			                          <? if ($userProfile->defaultQPageSettings->disableBanners == true): ?>checked<? endif; ?>>Disable Banner Images</label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <br>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-qpage-require-pin-code" class="log-action" type="checkbox"
				                      <? if ($userProfile->defaultQPageSettings->requirePinCode == true): ?>checked<? endif; ?>>ACCESS Pin (4 Digits)</label>
                    </div>
                </div>
                <div class="col-xs-6 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-qpage-show-links-as-url" class="log-action" type="checkbox"
				                      <? if ($userProfile->defaultQPageSettings->showLinksAsUrl == true): ?>checked<? endif; ?>>Blue Hyperlinks</label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <br>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-qpage-disable-widgets" class="log-action" type="checkbox"
				                      <? if ($userProfile->defaultQPageSettings->disableWidgets == true): ?>checked<? endif; ?>>Disable Widget Icons</label>
                    </div>
                </div>
                <div class="col-xs-6 text-left">
                    <div class="checkbox">
                        <label><input id="user-link-preferences-qpage-auto-launch" class="log-action" type="checkbox"
				                      <? if ($userProfile->defaultQPageSettings->autoLaunch == true): ?>checked<? endif; ?>>Auto Launch</label>
                    </div>
                </div>
            </div>
        </div>
    </div>
	<div class="row buttons-area">
		<div class="col-xs-3 col-xs-offset-2">
			<button class="btn btn-default log-action accept-button" type="button">Save</button>
		</div>
		<div class="col-xs-3 col-xs-offset-2">
			<button class="btn btn-default log-action cancel-button" type="button">Cancel</button>
		</div>
	</div>
</div>