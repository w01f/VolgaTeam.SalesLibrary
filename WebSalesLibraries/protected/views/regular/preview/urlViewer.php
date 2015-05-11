<?
	/**
	 * @var $data UrlPreviewData
	 * */
	$title = $data->isOffice365 ? 'Office 365 Link...' : 'Website URL Link...';
?>
<div class="link-viewer">
	<div class="row tab-above-header active" id="tab-above-header-save">
		<? echo $title; ?>
	</div>
	<div class="row tab-above-header" id="tab-above-header-email">
		<? echo $title; ?>
	</div>
	<ul class="nav nav-tabs" role="tablist" id="link-viewer-body-tabs">
		<li class="active"><a href="#link-viewer-tab-save" role="tab" data-toggle="tab">Link Options</a></li>
		<li><a href="#link-viewer-tab-email" role="tab" data-toggle="tab">Email</a></li>
	</ul>
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane active" id="link-viewer-tab-save">
			<? echo $this->renderPartial('save', array('data' => $data), true); ?>
		</div>
		<div role="tabpanel" class="tab-pane" id="link-viewer-tab-email">
			<? echo $this->renderPartial('email', array('data' => $data), true); ?>
		</div>
	</div>
</div>

