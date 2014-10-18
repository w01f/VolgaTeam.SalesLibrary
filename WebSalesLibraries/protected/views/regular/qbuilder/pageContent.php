<div>
	<? if (isset($page)): ?>
		<form class="form-horizontal page-title">
			<div class="form-group">
				<div class="col-xs-1">
					<p class="form-control-static text-left">Title:</p>
				</div>
				<div class="col-xs-6">
					<label for="page-content-title" class="sr-only"></label><input type="text" id="page-content-title" class="form-control" value="<? echo $page->title; ?>">
				</div>
				<div class="col-xs-5">
					<p class="form-control-static text-right">Created: <? echo $page->getCreateDateFormatted(); ?></p>
				</div>
			</div>
			<div class="form-group">
				<div class="col-xs-1">
					<p class="form-control-static text-left">URL:</p>
				</div>
				<div class="col-xs-11">
					<p class="form-control-static text-left">
						<a id="page-content-url" href="<? echo $page->getUrl(); ?>" target="_blank"><? echo $page->getUrl(); ?></a>
					</p>
				</div>
			</div>
		</form>
		<div id="page-content-tabs">
			<ul>
				<li><a href="#page-content-tab-links">Links</a></li>
				<li><a href="#page-content-tab-title">Title</a></li>
				<li><a href="#page-content-tab-header">Header</a></li>
				<li><a href="#page-content-tab-footer">Footer</a></li>
				<li><a href="#page-content-tab-security">Settings</a></li>
				<li><a href="#page-content-tab-logo">Logo</a></li>
			</ul>
			<div id="page-content-tab-links">
				<div class="header">
					<h4>DRAG your Links from the Cart & DROP them into the list below:</h4>
					<div id="page-content-links-title">
						Shared links:<span id="page-content-links-number">#</span>
					</div>
				</div>
				<div id="page-content-links-container" class="link-grid-container">
					<? $links = isset($page) ? $page->getPageLinks() : null; ?>
					<? if (isset($links)): ?>
						<? $this->renderPartial('pageLinks', array('links' => $links), false, true); ?>
					<? endif; ?>
				</div>
			</div>
			<div id="page-content-tab-title">
				<h4 class="checkbox">
					<label><input type="checkbox" id="page-content-show-description" value="" <? echo isset($page->subtitle) && $page->subtitle != '' ? 'checked' : '' ?>> This statement will be placed at the TOP of your quickSITE...</label>
				</h4>
				<label class="sr-only" for="page-content-description"></label><textarea id="page-content-description"><? echo $page->subtitle; ?></textarea>
			</div>
			<div id="page-content-tab-header">
				<h4 class="checkbox">
					<label><input type="checkbox" id="page-content-show-header" value="" <? echo isset($page->header) && $page->header != '' ? 'checked' : '' ?>>The Header Text will be JUST ABOVE the links you are sharing…</label>
				</h4>
				<label class="sr-only" for="page-content-header-text"></label><textarea id="page-content-header-text"><? echo $page->header; ?></textarea>
			</div>
			<div id="page-content-tab-footer">
				<h4 class="checkbox">
					<label><input type="checkbox" id="page-content-show-footer" value="" <? echo isset($page->footer) && $page->footer != '' ? 'checked' : '' ?>>The Footer Text will be at the VERY BOTTOM of your quickSITE…</label>
				</h4>
				<label class="sr-only" for="page-content-footer-text"></label><textarea id="page-content-footer-text"><? echo $page->footer; ?></textarea>
			</div>
			<div id="page-content-tab-security">
				<? $expDate = $page->getExpirationDateFormatted(); ?>
				<h4>Customize your quickSITE with the settings below:</h4>
				<div class="form-horizontal">
					<div class="form-group">
						<div class="col-xs-3">
							<div class="checkbox">
								<label>
									<input type="checkbox" id="page-content-use-expiration-date" value="" <? echo isset($expDate) && $expDate != '' ? 'checked' : '' ?>> A. Set Expiration Date
								</label>
							</div>
						</div>
						<div class="col-xs-3">
							<div id="page-content-expiration-date-container" class="input-group input-group-sm <? if ($page->isExpired()): ?>has-error<? endif; ?>" <? if (!(isset($expDate) && $expDate != '')): ?>style="display: none"<? endif; ?>>
								<input id="page-content-expiration-date" class="form-control" type="text" readonly placeholder="Select Date..." value="<? echo $expDate; ?>">
								<div class="input-group-btn">
									<button id="select-date-range" class="btn btn-default" type="button">
										<span class="glyphicon glyphicon-calendar"></span></button>
								</div>
							</div>
						</div>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-require-login" value="" <? echo $page->restricted ? 'checked' : '' ?>> B. Require Authorized User login and password to view site
						</label>
					</div>
					<div class="form-group">
						<div class="col-xs-4">
							<div class="checkbox">
								<label>
									<input id="page-content-access-code-enabled" type="checkbox" value="" <? echo isset($page->pin_code) && $page->pin_code > 0 ? 'checked' : '' ?>> C. Create a SECURE ACCESS Pin (4 Digits)
								</label>
							</div>
						</div>
						<div class="col-xs-1">
							<label class="sr-only" for="page-content-access-code"></label>
							<input type="text" maxlength="4" class="form-control input-sm" id="page-content-access-code" <? if (!(isset($page->pin_code) && $page->pin_code > 0)): ?>style="display: none;"<? endif; ?> value="<? echo isset($page->pin_code) ? $page->pin_code : '' ?>">
						</div>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-disable-widgets" "value="" <? echo $page->disable_widgets ? 'checked' : '' ?>> D. Disable all Link Widget Icons
						</label>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-disable-banners" value="" <? echo $page->disable_banners ? 'checked' : '' ?>> E. Disable all Link Banner Images
						</label>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-show-links-as-url" value="" <? echo $page->show_links_as_url ? 'checked' : '' ?>> F. Display all Links as Blue Hyperlinks
						</label>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-record-activity" value="" <? echo $page->record_activity ? 'checked' : '' ?>> G. Email me each time someone clicks a link on this quickSITE
						</label>
					</div>
					<div class="form-group">
						<div class="col-xs-2">
							<p class="form-control-static text-right">Cc Email:</p>
						</div>
						<div class="col-xs-4">
							<label for="page-content-activity-email-copy" class="sr-only"></label>
							<input type="email" id="page-content-activity-email-copy" class="form-control" <? echo !$page->record_activity ? 'disabled' : '' ?> value="<? echo isset($page->activity_email_copy) ? $page->activity_email_copy : '' ?>">
						</div>
					</div>
				</div>
			</div>
			<div id="page-content-tab-logo">
				<div class="header">
					<h4>Select a Logo that will appear at the VERY top of this quickSITE:</h4>
				</div>
				<div class="logo-list">
					<ul class="nav nav-pills">
						<? if (isset($logos)): ?>
							<? $selectedLogo = isset($page) ? $page->getLogo() : null; ?>
							<? foreach ($logos as $logo): ?>
								<li>
									<a href="#" <? if ($selectedLogo == $logo): ?>class="opened"<? endif; ?>><img src="<? echo $logo; ?>"></a>
								</li>
							<? endforeach; ?>
						<? endif; ?>
					</ul>
				</div>
			</div>
		</div>
	<? endif; ?>
</div>