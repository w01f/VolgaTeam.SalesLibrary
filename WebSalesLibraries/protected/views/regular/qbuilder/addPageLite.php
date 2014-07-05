<?
	/**
	 * @var $linkRecord LinkRecord
	 * @var $logos array
	 */
?>
<div class="tool-dialog">
	<h4>Email this Link:</h4>
	<h4><? echo $linkRecord->file_name != '' ? $linkRecord->file_name : $linkRecord->name; ?></h4>
	<div class="tabbable">
		<ul id="add-page-tabs" class="nav nav-tabs">
			<li><a href="#add-page-tab-link" data-toggle="tab">Link</a></li>
			<li><a href="#add-page-tab-logo" data-toggle="tab">Logo</a></li>
			<li><a href="#add-page-tab-settings" data-toggle="tab">Misc</a></li>
			<li><a href="#add-page-tab-tracking" data-toggle="tab">Tracking</a></li>
			<li><a href="#add-page-tab-pin" data-toggle="tab">Pin</a></li>
		</ul>
		<div class="tab-content">
			<div id="add-page-tab-link" class="tab-pane fade">
				<form class="form-horizontal" style="height: 315px;">
					<div class="form-group">
						<div class="checkbox">
							<label> <input id="add-page-name-enabled" type="checkbox" value="" checked>
								<strong>Page Header Text (shown with the link on the page)</strong> </label>
						</div>
						<label for="add-page-name" class="sr-only"></label><input type="text" class="form-control" id="add-page-name" value="<? echo $linkRecord->name; ?>">
					</div>
					<div class="form-group">
						<label class="control-label"><strong>This Link Expires in:</strong></label>
					</div>
					<div class="form-group" id="add-page-expires-in">
						<div class="col-xs-3">
							<button class="btn btn-default active" type="button" value="7">7 Days</button>
						</div>
						<div class="col-xs-3">
							<button class="btn btn-default" type="button" value="14">14 Days</button>
						</div>
						<div class="col-xs-3">
							<button class="btn btn-default" type="button" value="30">30 Days</button>
						</div>
						<div class="col-xs-3">
							<button class="btn btn-default" type="button" value="0">Never</button>
						</div>
					</div>
					<div class="form-group">
						<p class="form-control-static text-danger">
							<strong>Important Info you should KNOW about EMAILING LINKS</strong><br>
							<small>You are Sending a WEB LINK to this file over the internet. The Recipient will receive an email with the website Link. Tell your recipient to click this link to view or download this file…</small>
						</p>
					</div>
				</form>
			</div>
			<div id="add-page-tab-logo" class="tab-pane fade" style="padding-left: 2px">
				<div class="logo-list" style="height: 318px;">
					<ul class="nav nav-pills">
						<? if (isset($logos)): ?>
							<? $selectedLogo = count($logos) > 0 ? $logos[0] : null; ?>
							<? foreach ($logos as $logo): ?>
								<li>
									<a href="#" <? if ($selectedLogo == $logo): ?>class="opened"<? endif; ?> style="margin-right: 50px;"><img src="<? echo $logo; ?>"></a>
								</li>
							<? endforeach; ?>
						<? endif; ?>
					</ul>
				</div>
			</div>
			<div id="add-page-tab-settings" class="tab-pane fade">
				<form class="form-horizontal" style="height: 315px;">
					<div class="form-group">
						<div class="checkbox">
							<label>
								<input type="checkbox" id="add-page-disable-widgets" "value=""> Disable all Link Widget Icons
							</label>
						</div>
					</div>
					<div class="form-group">
						<div class="checkbox">
							<label>
								<input type="checkbox" id="add-page-disable-banners" value="">Disable all Link Banner Images
							</label>
						</div>
					</div>
					<div class="form-group">
						<div class="checkbox">
							<label>
								<input type="checkbox" id="add-page-show-links-as-url" value="">Display all Links as Blue Hyperlinks
							</label>
						</div>
					</div>
				</form>
			</div>
			<div id="add-page-tab-tracking" class="tab-pane fade">
				<form class="form-horizontal" style="height: 315px;">
					<div class="form-group">
						<div class="checkbox">
							<label>
								<input type="checkbox" id="add-page-record-activity" value="">Email me each time someone clicks a link on this quickSITE
							</label>
						</div>
					</div>
					<div class="form-group">
						<div class="col-xs-3">
							<p class="form-control-static text-right">Cc Email:</p>
						</div>
						<div class="col-xs-8">
							<label for="add-page-activity-email-copy" class="sr-only"></label>
							<input type="email" id="add-page-activity-email-copy" class="form-control" disabled value="">
						</div>
					</div>
					<div class="form-group">
						<div class="checkbox">
							<label> <input id="add-page-restricted" type="checkbox" value="">Require User Log-in
							</label>
						</div>
						<p class="form-control-static text-danger text-left" style="padding-left: 15px">
							<small>(If you select this option, only Users who already have an official username and password can access this site)</small>
						</p>
					</div>
				</form>
			</div>
			<div id="add-page-tab-pin" class="tab-pane fade">
				<form class="form-horizontal" style="height: 315px;">
					<div class="form-group">
						<div class="checkbox">
							<label>
								<input id="add-page-access-code-enabled" type="checkbox">Create a SECURE ACCESS Pin (4 Digit Number)
							</label>
						</div>
					</div>
					<div class="form-group">
						<div class="col-xs-2">
							<label for="add-page-access-code" class="sr-only"></label>
							<input type="text" maxlength="4" class="form-control" id="add-page-access-code" style="display: none;" value="">
						</div>
					</div>
				</form>
			</div>
		</div>
	</div>
	<div class="buttons-area">
		<button class="btn btn-default accept-button" type="button">Send</button>
		<button class="btn btn-default cancel-button" type="button">Cancel</button>
	</div>
</div>
