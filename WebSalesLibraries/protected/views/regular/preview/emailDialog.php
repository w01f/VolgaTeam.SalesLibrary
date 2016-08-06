<?
	/**
	 * @var $data PreviewData
	 * @var $isProtected boolean
	 */
?>
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link"
     data-log-action="Preview Activity">
	<div class="row tab-above-header" id="tab-above-header-email-public">
		<span class="header-text">Send this link to your client…</span>
	</div>
	<div class="row tab-above-header" id="tab-above-header-email-protected">
		<span class="header-text">Email a secure link (not for clients)</span>
	</div>
	<ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
		<li><a class="log-action" href="#link-viewer-tab-email-public" role="tab" data-toggle="tab"
		       data-log-action="Add to QS">Public EMAIL</a></li>
		<li><a class="log-action" href="#link-viewer-tab-email-protected" role="tab" data-toggle="tab"
		       data-log-action="Add to QS">Secure EMAIL</a></li>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-public">
			<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => false), true); ?>
		</div>
		<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email-protected">
			<? echo $this->renderPartial('email', array('data' => $data, 'isProtected' => true), true); ?>
		</div>
	</div>
</div>