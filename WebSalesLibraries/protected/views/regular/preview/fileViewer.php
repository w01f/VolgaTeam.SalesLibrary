<?
	/**
	 * @var $data FilePreviewData
	 * */
?>
<div class="link-viewer<? if ($data->userAuthorized): ?> logger-form<?endif;?>" data-log-group="Link" data-log-action="Preview Activity">
	<div class="row tab-above-header active" id="tab-above-header-save">
		Download or Save this file…
	</div>
	<? if ($data->allowAddToQuickSite): ?>
		<div class="row tab-above-header" id="tab-above-header-email">
			Send this link to your client…
		</div>
	<? endif; ?>
	<ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
		<li class="active">
			<a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab" data-log-action="Download File">Save</a>
		</li>
		<? if ($data->allowAddToQuickSite): ?>
			<li>
				<a class="log-action" href="#link-viewer-tab-email" role="tab" data-toggle="tab" data-log-action="Add to QS">Email</a>
			</li>
		<? endif; ?>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane active" id="link-viewer-tab-save">
			<? echo $this->renderPartial('save', array('data' => $data), true); ?>
		</div>
		<? if ($data->allowAddToQuickSite): ?>
			<div role="tabpanel" class="tab-pane" id="link-viewer-tab-email">
				<? echo $this->renderPartial('email', array('data' => $data), true); ?>
			</div>
		<? endif; ?>
	</div>
</div>

