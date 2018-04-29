<?
	/**
	 * @var $userProfile LinkUserProfileModel
	 */
?>
<div class="user-preferences-editor logger-form" data-log-group="User Preferences" data-log-action="Edit">
	<div class="row">
		<div class="col-xs-12">
			<h3 class="header">
				<span class="title">How do you want to open your files?</span>
			</h3>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-2">
			Power Point
		</div>
		<div class="col-xs-2 text-center">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->powerPointSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-3 text-center">
			<div class="checkbox">
				<label><input id="user-link-preferences-power-point-force-EO-open" class="log-action" type="checkbox"
				              <? if ($userProfile->powerPointSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download
					& Open</label>
			</div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-2">
			Word
		</div>
		<div class="col-xs-2 text-center">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->docSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-3 text-center">
			<div class="checkbox">
				<label><input id="user-link-preferences-doc-force-EO-open" class="log-action" type="checkbox"
				              <? if ($userProfile->docSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
					Open</label>
			</div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-2">
			Excel
		</div>
		<div class="col-xs-2 text-center">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->xlsSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-3 text-center">
			<div class="checkbox">
				<label><input id="user-link-preferences-xls-force-EO-open" class="log-action" type="checkbox"
				              <? if ($userProfile->xlsSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
					Open</label>
			</div>
		</div>
        <div class="col-xs-5 text-left">
            <div class="checkbox">
                <label><input id="user-link-preferences-xls-force-open-gallery" class="log-action" type="checkbox"
				              <? if ($userProfile->xlsSettings->forceOpenGallery == true): ?>checked<? endif; ?>>Open Preview (less than 10 mb)
                </label>
            </div>
        </div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-12">
			<div style="margin-bottom: 20px"></div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-2">
			PDF
		</div>
		<div class="col-xs-2 text-center">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->pdfSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-3 text-center">
			<div class="checkbox">
				<label><input id="user-link-preferences-pdf-force-EO-open" class="log-action" type="checkbox"
				              <? if ($userProfile->pdfSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
					Open</label>
			</div>
		</div>
		<div class="col-xs-5 text-left">
			<div class="checkbox">
				<label><input id="user-link-preferences-pdf-force-web-open" class="log-action" type="checkbox"
				              <? if ($userProfile->pdfSettings->forceWebOpen == true): ?>checked<? endif; ?>>Open New
					Browser Tab
				</label>
			</div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-2">
			Images
		</div>
		<div class="col-xs-2 text-center">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->imageSettings->isDefault()): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-3 text-center">
			<div class="checkbox">
				<label><input id="user-link-preferences-image-force-EO-open" class="log-action" type="checkbox"
				              <? if ($userProfile->imageSettings->forceEOOpen == true): ?>checked<? endif; ?>>Download &
					Open</label>
			</div>
		</div>
		<div class="col-xs-5 text-left">
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
			<span style="color: red">You may need to ALLOW Pop-Ups to view PDFs or Images</span>
			<br>
			<span>Click <a
					href="https://support.google.com/chrome/answer/95472?co=GENIE.Platform%3DDesktop&hl=en" target="_blank">HERE</a> to learn more</span>
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