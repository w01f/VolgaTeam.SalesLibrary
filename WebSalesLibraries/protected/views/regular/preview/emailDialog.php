<?
	/**
	 * @var $data PreviewData
	 */
?>
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link"
     data-log-action="Preview Activity">
	<div class="row tab-above-header" id="tab-above-header-email">
		<span class="header-text">Send this link to your client…</span>
	</div>
	<ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
		<li><a class="log-action" href="#link-viewer-tab-email" role="tab" data-toggle="tab"
		       data-log-action="Add to QS">Email</a></li>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email">
			<? echo $this->renderPartial('email', array('data' => $data), true); ?>
		</div>
	</div>
</div>