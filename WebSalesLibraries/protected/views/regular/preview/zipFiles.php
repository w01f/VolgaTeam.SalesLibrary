<?
	/** @var $zipName string */
	/** @var $downloadInfoList FileDownloadInfo[] */
?>
<style>
    .zip-files-form .total-files-info {
        margin-top: 10px;
        margin-bottom: 10px;
    }

    .zip-files-form .zip-files-list {
        height: 400px;
        background-color: #ffffff;
        border: 1px solid #cecece;
        padding-left: 10px;
        margin-top: 10px;
        overflow-y: auto;
    }

    .zip-files-form .zip-name {
        margin-top: 15px;
        color: #999999;
    }

    .zip-files-form .action-buttons {
        margin-top: 15px;
    }

    .zip-files-form .accept-button,
    .zip-files-form .cancel-button {
        width: 150px;
    }
</style>
<div class="zip-files-form logger-form" data-log-group="Link" data-log-action="Preview Activity">
    <legend>Preparing your download…</legend>
    <div class="row">
        <div class="col-xs-6 text-left">
            <button id="zip-files-select-all" class="btn btn-default log-action" type="button" style="width: 130px;">
                Select All
            </button>
        </div>
        <div class="col-xs-6 text-right">
            <button id="zip-files-clear-all" class="btn btn-default log-action" type="button" style="width: 130px;">
                Clear
                All
            </button>
        </div>
    </div>
    <div class="row total-files-info">
        <div class="col-xs-6 text-left">
            Total files: <span class="total-files-count"></span>
        </div>
        <div class="col-xs-6 text-right">
            Total size: <span class="total-files-size"></span>
        </div>
    </div>
    <div class="zip-files-list">
        <ul class="nav nav-pills nav-stacked">
			<? foreach ($downloadInfoList as $fileInfo): ?>
                <li>
                    <div class="checkbox">
                        <label> <input class="zip-files-item log-action" type="checkbox"
                                       value="<? echo base64_encode(CJSON::encode($fileInfo)); ?>" checked>
							<? echo sprintf('%s (%s)', $fileInfo->name, FileInfo::formatFileSize($fileInfo->size)); ?>
                        </label>
                    </div>
                </li>
			<? endforeach; ?>
        </ul>
    </div>
    <div class="row">
        <div class="col-xs-12 zip-name">
			<? echo $zipName; ?>
        </div>
    </div>
    <div class="row action-buttons">
        <div class="col-xs-6 text-left">
            <button class="btn btn-default log-action accept-button" type="button">Download Selected
            </button>
        </div>
        <div class="col-xs-6 text-right">
            <button class="btn btn-default log-action cancel-button" type="button">Cancel</button>
        </div>
    </div>
    <div class="service-data">
        <div class="zip-name"><? echo $zipName; ?></div>
    </div>
</div>