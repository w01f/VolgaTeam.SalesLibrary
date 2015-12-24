<div class="tool-dialog logger-form" data-log-group="QBuilder" data-log-action="QBuilder Activity">
	<legend>Select quickSITES you want to delete</legend>
	<div class="buttons-area" style="padding-top: 5px !important; margin-bottom: 10px;">
		<button id="delete-pages-select-all" class="btn btn-default log-action" type="button" style="width: 130px;">Select All</button>
		<button id="delete-pages-clear-all" class="btn btn-default log-action" type="button" style="width: 130px;">Clear All</button>
	</div>
	<div class="delete-pages-list" style="height: 246px;">
		<ul class="nav nav-pills nav-stacked">
			<? if (isset($pages)): ?>
				<? foreach ($pages as $page): ?>
					<li>
						<div class="checkbox">
							<label> <input class="delete-pages-item log-action" type="checkbox" value="<? echo $page->id; ?>">
								<? echo $page->title; ?>
							</label>
						</div>
					</li>
				<?php endforeach; ?>
			<?php endif; ?>
		</ul>
	</div>
	<div class="buttons-area">
		<button class="btn btn-default log-action accept-button" type="button" style="width: 130px;">Delete Selected</button>
		<button class="btn btn-default log-action cancel-button" type="button" style="width: 130px;">Cancel</button>
	</div>
</div>