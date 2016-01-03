<?
	/**
	 * @var $data FilePreviewData
	 * */
?>
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<?endif;?>" data-log-group="Link" data-log-action="Preview Activity">
	<? if ($data->config->allowSave): ?>
		<div class="row tab-above-header" id="tab-above-header-save">
			<span class="header-text">Download or Save this file…</span>
		</div>
	<? endif; ?>
	<? if ($data->config->allowEmail): ?>
		<div class="row tab-above-header" id="tab-above-header-email">
			<span class="header-text">Send this link to your client…</span>
		</div>
	<? endif; ?>
	<ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
		<? if ($data->config->allowSave): ?>
			<li>
				<a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab">Save</a>
			</li>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
			<li><a class="log-action" href="#link-viewer-tab-email" role="tab" data-toggle="tab">Email</a></li>
		<? endif; ?>
	</ul>
	<div class="tab-content">
		<? if ($data->config->allowSave): ?>
			<div role="tabpanel" class="tab-pane" id="link-viewer-tab-save">
				<? echo $this->renderPartial('save', array('data' => $data), true); ?>
			</div>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
			<div role="tabpanel" class="tab-pane" id="link-viewer-tab-email">
				<? echo $this->renderPartial('email', array('data' => $data), true); ?>
			</div>
		<? endif; ?>
	</div>
</div>

