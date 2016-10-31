<?
	/**
	 * @var $data UrlPreviewData
	 * */
?>
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link"
     data-log-action="Preview Activity">
	<div class="row tab-above-header" id="tab-above-header-save">
		<div class="col col-xs-12 text-left">
			<div class="text-label">
				<img class="text-item file-logo" src="<? echo $data->fileLogo;?>" style="height: 48px;">
				<span class="text-item file-name"><? echo $data->fileName;?></span>
			</div>
		</div>
	</div>
	<? if ($data->config->allowEmail): ?>
		<div class="row tab-above-header" id="tab-above-header-email-public">
			<span class="header-text">Send this link to your client…</span>
		</div>
		<div class="row tab-above-header" id="tab-above-header-email-protected">
			<span class="header-text">Email a secure link (not for clients)</span>
		</div>
	<? endif; ?>
	<ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
		<li>
			<a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab">Link Options</a>
		</li>
		<? if ($data->config->allowEmail): ?>
			<li>
				<a class="log-action" href="#link-viewer-tab-email-public" role="tab" data-toggle="tab"
				   data-log-action="Add to QS">Public EMAIL</a>
			</li>
			<li>
				<a class="log-action" href="#link-viewer-tab-email-protected" role="tab" data-toggle="tab"
				   data-log-action="Add to QS">Secure EMAIL</a>
			</li>
		<? endif; ?>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane" id="link-viewer-tab-save">
			<? echo $this->renderPartial('save', array('data' => $data), true); ?>
		</div>
		<? if ($data->config->allowEmail): ?>
			<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-public">
				<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => false), true); ?>
			</div>
			<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-protected">
				<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => true), true); ?>
			</div>
		<? endif; ?>
	</div>
</div>

