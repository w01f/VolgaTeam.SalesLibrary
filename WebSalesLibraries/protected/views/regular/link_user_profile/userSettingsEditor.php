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
		<div class="col-xs-4">
			Power Point
		</div>
		<div class="col-xs-4">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->powerPointSettings->forceOpen != true): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-4">
			<div class="checkbox">
				<label><input id="user-link-preferences-power-point-force-open" class="log-action" type="checkbox"
				              <? if ($userProfile->powerPointSettings->forceOpen == true): ?>checked<? endif; ?>>Download & Open</label>
			</div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-4">
			Word
		</div>
		<div class="col-xs-4">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->docSettings->forceOpen != true): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-4">
			<div class="checkbox">
				<label><input id="user-link-preferences-doc-force-open" class="log-action" type="checkbox"
				              <? if ($userProfile->docSettings->forceOpen == true): ?>checked<? endif; ?>>Download & Open</label>
			</div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-4">
			PDF
		</div>
		<div class="col-xs-4">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->pdfSettings->forceOpen != true): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-4">
			<div class="checkbox">
				<label><input id="user-link-preferences-pdf-force-open" class="log-action" type="checkbox"
				              <? if ($userProfile->pdfSettings->forceOpen == true): ?>checked<? endif; ?>>Download & Open</label>
			</div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-4">
			Excel
		</div>
		<div class="col-xs-4">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->xlsSettings->forceOpen != true): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-4">
			<div class="checkbox">
				<label><input id="user-link-preferences-xls-force-open" class="log-action" type="checkbox"
				              <? if ($userProfile->xlsSettings->forceOpen == true): ?>checked<? endif; ?>>Download & Open</label>
			</div>
		</div>
	</div>
	<div class="row settings-row">
		<div class="col-xs-4">
			Images
		</div>
		<div class="col-xs-4">
			<div class="checkbox default">
				<label><input class="log-action" type="checkbox"
				              <? if ($userProfile->imageSettings->forceOpen != true): ?>checked<? endif; ?>>Default</label>
			</div>
		</div>
		<div class="col-xs-4">
			<div class="checkbox">
				<label><input id="user-link-preferences-image-force-open" class="log-action" type="checkbox"
				              <? if ($userProfile->imageSettings->forceOpen == true): ?>checked<? endif; ?>>Download & Open</label>
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