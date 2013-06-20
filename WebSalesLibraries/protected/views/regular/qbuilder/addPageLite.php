<div class="tool-dialog">
	<legend>
		<div>Email this Link:</div>
		<div><? echo $linkRecord->file_name != '' ? $linkRecord->file_name : $linkRecord->name; ?></div>
	</legend>
	<div class="tabbable">
		<ul id="add-page-tabs" class="nav nav-tabs">
			<li><a href="#add-page-tab-link" data-toggle="tab">Link</a></li>
			<li><a href="#add-page-tab-logo" data-toggle="tab">Logo</a></li>
			<li><a href="#add-page-tab-settings" data-toggle="tab">Settings</a></li>
		</ul>
		<div class="tab-content">
			<div id="add-page-tab-link" class="tab-pane fade">
				<table>
					<tr>
						<td colspan="2">
							<div class="control-group">
								<label class="checkbox control-label" for="add-page-name"><input id="add-page-name-enabled" type="checkbox" value="" checked><strong>Page Header Text (shown with the link on the page)</strong></label>
								<div class="controls">
									<input type="text" class="input-block-level" id="add-page-name" value="<? echo $linkRecord->name; ?>">
								</div>
							</div>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<div class="control-group">
								<label class="control-label"><strong>This Link Expires in:</strong></label>
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
							<label class="control-label"><strong>Link Security:</strong></label>
							<form class="form-inline">
								<label class="checkbox" style="padding-right: 20px;"><input id="add-page-restricted" type="checkbox" value="">Require User Log-in</label>
								<label class="checkbox"><input id="add-page-show-link-to-main-site" type="checkbox" value="">Show Link to Main Site</label>
							</form>
						</td>
					</tr>
					<tr>
						<td colspan="2" class="error-message">
							<p class="text-left">
								<strong>Important Info you should KNOW about EMAILING LINKS</strong><br>
								<small>You are Sending a WEB LINK to this file over the internet. The Recipient will receive an email with the website Link. Tell your recipient to click this link to view or download this file…</small>
							</p>
						</td>
					</tr>
				</table>
			</div>
			<div id="add-page-tab-logo" class="tab-pane fade" style="padding-left: 2px">
				<div class="logo-list" style="height: 313px;">
					<ul class="nav nav-pills">
						<?if (isset($logos)): ?>
							<? $selectedLogo = count($logos) > 0 ? $logos[0] : null; ?>
							<? foreach ($logos as $logo): ?>
								<li>
									<a href="#" <? if ($selectedLogo == $logo): ?>class="opened"<?endif;?> style="margin-right: 50px;"><img src="<? echo $logo; ?>"></a>
								</li>
							<?php endforeach; ?>
						<?php endif;?>
					</ul>
				</div>
			</div>
			<div id="add-page-tab-settings" class="tab-pane fade">
				<div style="height: 315px;">
					<form class="form-inline" style="margin-bottom:10px;">
						<label class="checkbox" style="min-height: 30px;"><input id="add-page-access-code-enabled" type="checkbox"><span style="margin-left: 4px;">Create a SECURE ACCESS Pin (4 Digit Number)</span></label>
						<input type="text" maxlength="4" class="input-small" id="add-page-access-code" style="display: none;" value="">
					</form>
					<label class="checkbox"><input type="checkbox" id="add-page-show-ticker" value="">Show the TICKER at the top of this quickSITE</label>
					<br>
					<label class="checkbox"><input type="checkbox" id="add-page-disable-widgets" "value="">Disable all Link Widget Icons</label>
					<br>
					<label class="checkbox"><input type="checkbox" id="add-page-disable-banners" value="">Disable all Link Banner Images</label>
					<br>
					<label class="checkbox"><input type="checkbox" id="add-page-record-activity" value="">Email me each time someone clicks a link on this quickSITE</label>
				</div>
			</div>
		</div>
	</div>
	<div class="buttons-area">
		<button class="btn accept-button" type="button">OK</button>
		<button class="btn cancel-button" type="button">Cancel</button>
	</div>
</div>
