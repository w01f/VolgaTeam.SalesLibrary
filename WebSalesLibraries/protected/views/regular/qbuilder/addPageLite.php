<div class="tool-dialog">
	<legend>Email this Link</legend>
	<div class="tabbable">
		<ul id="add-page-tabs" class="nav nav-tabs">
			<li><a href="#add-page-tab-link" data-toggle="tab">Link</a></li>
			<li><a href="#add-page-tab-logo" data-toggle="tab">Logo</a></li>
		</ul>
		<div class="tab-content">
			<div id="add-page-tab-link" class="tab-pane fade">
				<table>
					<tr>
						<td colspan="2">
							<div class="control-group">
								<label class="control-label" for="add-page-name">Link Name:</label>
								<div class="controls">
									<input type="text" class="input-block-level" id="add-page-name" value="<? echo $linkRecord->name; ?>">
								</div>
							</div>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<div class="control-group">
								<label class="control-label">This Link Expires in:</label>
								<div class="controls buttons-area" id="add-page-expires-in">
									<button class="btn active" type="button" value="7">7 Days</button>
									<button class="btn" type="button" value="14">14 Days</button>
									<button class="btn" type="button" value="30">30 Days</button>
									<button class="btn" type="button" value="0">Never</button>
								</div>
							</div>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<label class="checkbox"><input id="add-page-restricted" type="checkbox" value="">Require User Log-in</label>
							<label class="checkbox"><input id="add-page-show-link-to-main-site" type="checkbox" value="">Show Link to Main Site</label>
						</td>
					</tr>
				</table>
			</div>
			<div id="add-page-tab-logo" class="tab-pane fade" style="padding-left: 2px">
				<div class="logo-list" style="height: 246px;">
					<ul class="nav nav-pills">
						<?if (isset($logos)): ?>
							<? $selectedLogo = count($logos) > 0 ? $logos[0] : null; ?>
							<? foreach ($logos as $logo): ?>
								<li>
									<a href="#" <? if ($selectedLogo == $logo): ?>class="opened"<?endif;?>><img src="<? echo $logo; ?>"></a>
								</li>
							<?php endforeach; ?>
						<?php endif;?>
					</ul>
				</div>
			</div>
		</div>
	</div>
	<div class="buttons-area">
		<button class="btn accept-button" type="button">OK</button>
		<button class="btn cancel-button" type="button">Cancel</button>
	</div>
</div>
