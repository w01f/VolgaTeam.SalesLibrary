<?
	/** @var $folderName string */
	/** @var $folderFileInfoList FileDownloadInfo[] */
?>
<style>
	.zip-folder-form .total-files-info {
		margin-top: 10px;
		margin-bottom: 10px;
	}

	.zip-folder-form .zip-folder-list {
		height: 400px;
		background-color: #ffffff;
		border: 1px solid #cecece;
		padding-left: 10px;
		margin-top: 10px;
		margin-bottom: 30px;
	}

	.zip-folder-form .accept-button,
	.zip-folder-form .cancel-button {
		width: 150px;
	}
</style>
<div class="zip-folder-form logger-form" data-log-group="Link" data-log-action="Preview Activity">
	<legend>Preparing your download…</legend>
	<div class="row">
		<div class="col-xs-6 text-left">
			<button id="zip-folder-select-all" class="btn btn-default log-action" type="button" style="width: 130px;">
				Select All
			</button>
		</div>
		<div class="col-xs-6 text-right">
			<button id="zip-folder-clear-all" class="btn btn-default log-action" type="button" style="width: 130px;">
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
	<div class="zip-folder-list">
		<ul class="nav nav-pills nav-stacked">
			<? foreach ($folderFileInfoList as $fileInfo): ?>
				<li>
					<div class="checkbox">
						<label> <input class="zip-folder-item log-action" type="checkbox"
						               value="<? echo base64_encode(CJSON::encode($fileInfo)); ?>" checked>
							<? echo sprintf('%s (%s)', $fileInfo->name, FileInfo::formatFileSize($fileInfo->size)); ?>
						</label>
					</div>
				</li>
			<? endforeach; ?>
		</ul>
	</div>
	<div class="row">
		<div class="col-xs-6 text-left">
			<button class="btn btn-default log-action accept-button" type="button">Download Selected
			</button>
		</div>
		<div class="col-xs-6 text-right">
			<button class="btn btn-default log-action cancel-button" type="button">Cancel</button>
		</div>
	</div>
	<div class="service-data">
		<div class="folder-name"><? echo $folderName; ?></div>
	</div>
</div>