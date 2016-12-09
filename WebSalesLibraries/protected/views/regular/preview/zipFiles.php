<?
	/** @var $files SplFileInfo[] */
?>
<div class="tool-dialog logger-form" data-log-group="Link" data-log-action="Preview Activity">
	<legend>Select files you want to download</legend>
	<div class="buttons-area" style="padding-top: 5px !important; margin-bottom: 10px;">
		<button id="zip-files-select-all" class="btn btn-default log-action" type="button" style="width: 130px;">
			Select All
		</button>
		<button id="zip-files-clear-all" class="btn btn-default log-action" type="button" style="width: 130px;">Clear
			All
		</button>
	</div>
	<div class="zip-files-list" style="height: 246px;">
		<ul class="nav nav-pills nav-stacked">
				<? foreach ($files as $file): ?>
					<li>
						<div class="checkbox">
							<label> <input class="zip-files-item log-action" type="checkbox"
							               value="<? echo $page->id; ?>">
								<? echo $file->getBasename(); ?>
							</label>
						</div>
					</li>
				<? endforeach; ?>
		</ul>
	</div>
	<div class="buttons-area">
		<button class="btn btn-default log-action accept-button" type="button" style="width: 130px;">Delete Selected
		</button>
		<button class="btn btn-default log-action cancel-button" type="button" style="width: 130px;">Cancel</button>
	</div>
</div>