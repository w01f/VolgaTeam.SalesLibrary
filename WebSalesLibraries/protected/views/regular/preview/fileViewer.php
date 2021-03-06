<?
	/**
	 * @var $data FilePreviewData
	 */

	$imageUrlPrefix = Yii::app()->getBaseUrl(true);
?>
<div class="link-viewer<? if ($data->config->enableLogging): ?> logger-form<? endif; ?>" data-log-group="Link"
     data-log-action="Preview Activity">
	<? if ($data->config->allowSave): ?>
		<?
		$headerTextSize = 12;
		if (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl))
			$headerTextSize -= 4;
		?>
        <div class="row tab-above-header" id="tab-above-header-save">
            <div class="col col-xs-<? echo $headerTextSize; ?> text-left">
                <div class="text-label">
                    <img class="text-item file-logo" src="<? echo $data->fileLogo; ?>" style="height: 48px;">
                    <span class="text-item file-name"><? echo $data->fileName; ?></span>
                </div>
            </div>
			<? if (Yii::app()->params['one_drive_links']['enabled'] && !empty($data->oneDriveUrl)): ?>
                <div class="col col-xs-4 text-center">
                    <div class="image-button" title="one drive link">
                        <span class="text-item dropdown">
                             <a href="#" data-toggle="dropdown" class="dropdown-toggle">
                                <img src="<? echo sprintf('%s/images/preview/gallery/button-open-one-drive.png', $imageUrlPrefix); ?>">
                             </a>
                            <ul class="dropdown-menu" style="left: 0;">
                                <li>
                                    <a href="<? echo $data->oneDriveUrl; ?>"
                                       class="log-action log-action one-drive-link-open"
                                       data-log-action="Open OneDrive Link" target="_blank"
                                       title="open link">Open Link</a>
                                </li>
                                <li>
                                    <a href="<? echo $data->oneDriveUrl; ?>" class="log-action one-drive-link-copy"
                                       data-log-action="Copy OneDrive Link"
                                       title="copy link">Copy Link URL</a>
                                </li>
                                <li>
                                    <a href="#" class="log-action one-drive-link-email"
                                       data-log-action="Email OneDrive Link" title="email link">Email Link</a>
                                </li>
                            </ul>
                        </span>
                    </div>
                </div>
			<? endif; ?>
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
                <a class="log-action" href="#link-viewer-tab-save" role="tab" data-toggle="tab">File</a>
            </li>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
            <li>
                <a class="log-action" href="#link-viewer-tab-email" role="tab" data-toggle="tab" data-log-action="Add to QS">Email</a>
            </li>
		<? endif; ?>
    </ul>
    <div class="tab-content">
		<? if ($data->config->allowSave): ?>
            <div role="tabpanel" class="tab-pane" id="link-viewer-tab-save">
				<? echo $this->renderPartial('save', array('data' => $data), true); ?>
            </div>
		<? endif; ?>
		<? if ($data->config->allowEmail): ?>
            <div role="tabpanel" class="tab-pane link-viewer-tab-email" id="link-viewer-tab-email">
				<? echo $this->renderPartial('email', array('data' => $data), true); ?>
            </div>
		<? endif; ?>
    </div>
</div>

