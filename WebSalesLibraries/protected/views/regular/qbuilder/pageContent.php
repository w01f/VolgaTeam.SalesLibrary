<?
	/**
	 * @var $page QPageRecord
	 */
?>
<div>
	<? if (isset($page)): ?>
		<form class="form-horizontal page-title">
			<div class="form-group">
				<div class="col-xs-1">
					<p class="form-control-static text-left">Title:</p>
				</div>
				<div class="col-xs-6">
					<label for="page-content-title" class="sr-only"></label><input type="text" id="page-content-title" class="form-control log-action" value="<? echo $page->title; ?>">
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
						<a id="page-content-url" class="log-action" href="<? echo $page->getUrl(); ?>" target="_blank"><? echo $page->getUrl(); ?></a>
					</p>
				</div>
			</div>
		</form>
		<div id="page-content-tabs-headers" class="tab-pages scroll_tabs_theme_light">
			<span class="page-tab-header log-action selected">Links
				<span class="service-data">
					<span class="tab-id">#page-content-tab-links</span>
				</span>
			</span>
			<span class="page-tab-header">Title
				<span class="service-data">
					<span class="tab-id">#page-content-tab-title</span>
				</span>
			</span>
			<span class="page-tab-header">Header
				<span class="service-data">
					<span class="tab-id">#page-content-tab-header</span>
				</span>
			</span>
			<span class="page-tab-header">Footer
				<span class="service-data">
					<span class="tab-id">#page-content-tab-footer</span>
				</span>
			</span>
			<span class="page-tab-header">Settings
				<span class="service-data">
					<span class="tab-id">#page-content-tab-security</span>
				</span>
			</span>
			<span class="page-tab-header">Logo
				<span class="service-data">
					<span class="tab-id">#page-content-tab-logo</span>
				</span>
			</span>
		</div>
		<div id="page-content-tabs-content">
			<div id="page-content-tab-links" class="selected">
				<div class="header">
					<h4>DRAG your Links from the Cart & DROP them into the list below:</h4>
				</div>
				<div id="page-content-links-container" class="data-table-content-container">
					<table id="page-links-data-table-content" class="table table-striped table-bordered"></table>
				</div>
			</div>
			<div id="page-content-tab-title">
				<h4 class="checkbox">
					<label><input type="checkbox" id="page-content-show-description" class="log-action" value="" <? echo isset($page->subtitle) && $page->subtitle != '' ? 'checked' : '' ?>> This statement will be placed at the TOP of your quickSITE...</label>
				</h4>
				<label class="sr-only" for="page-content-description"></label><textarea id="page-content-description" class="log-action"><? echo $page->subtitle; ?></textarea>
			</div>
			<div id="page-content-tab-header">
				<h4 class="checkbox">
					<label><input type="checkbox" id="page-content-show-header" class="log-action" value="" <? echo isset($page->header) && $page->header != '' ? 'checked' : '' ?>>The Header Text will be JUST ABOVE the links you are sharing…</label>
				</h4>
				<label class="sr-only" for="page-content-header-text"></label><textarea id="page-content-header-text" class="log-action"><? echo $page->header; ?></textarea>
			</div>
			<div id="page-content-tab-footer">
				<h4 class="checkbox">
					<label><input type="checkbox" id="page-content-show-footer" class="log-action" value="" <? echo isset($page->footer) && $page->footer != '' ? 'checked' : '' ?>>The Footer Text will be at the VERY BOTTOM of your quickSITE…</label>
				</h4>
				<label class="sr-only" for="page-content-footer-text"></label><textarea id="page-content-footer-text" class="log-action"><? echo $page->footer; ?></textarea>
			</div>
			<div id="page-content-tab-security">
				<? $expDate = $page->getExpirationDateFormatted(); ?>
				<h4>Customize your quickSITE with the settings below:</h4>
				<div class="form-horizontal">
					<div class="form-group">
						<div class="col-xs-5">
							<div class="checkbox">
								<label>
									<input type="checkbox" id="page-content-use-expiration-date" class="log-action" value="" <? echo isset($expDate) && $expDate != '' ? 'checked' : '' ?>> A. Expiration Date
								</label>
							</div>
						</div>
						<div class="col-xs-4">
							<div id="page-content-expiration-date-container" class="input-group input-group-sm <? if ($page->isExpired()): ?>has-error<? endif; ?>" <? if (!(isset($expDate) && $expDate != '')): ?>style="display: none"<? endif; ?>>
								<input id="page-content-expiration-date" class="form-control log-action" type="text" placeholder="Select Date..." value="<? echo $expDate; ?>" readonly>
								<div class="input-group-btn">
									<button class="btn btn-default select-date-toggle log-action" type="button">
										<span class="glyphicon glyphicon-calendar"></span></button>
								</div>
							</div>
						</div>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-require-login" class="log-action" value="" <? echo $page->restricted ? 'checked' : '' ?>> B. Require User login and password to view site
						</label>
					</div>
					<div class="form-group">
						<div class="col-xs-5">
							<div class="checkbox">
								<label>
									<input id="page-content-access-code-enabled" class="log-action" type="checkbox" value="" <? echo isset($page->pin_code) && $page->pin_code > 0 ? 'checked' : '' ?>> C. ACCESS Pin (4 Digits)
								</label>
							</div>
						</div>
						<div class="col-xs-4">
							<label class="sr-only" for="page-content-access-code"></label>
							<input type="text" maxlength="4" class="form-control input-sm log-action" id="page-content-access-code" <? if (!(isset($page->pin_code) && $page->pin_code > 0)): ?>style="display: none;"<? endif; ?> value="<? echo isset($page->pin_code) ? $page->pin_code : '' ?>">
						</div>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-disable-widgets" class="log-action" value="" <? echo $page->disable_widgets ? 'checked' : '' ?>> D. Disable Widget Icons
						</label>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-disable-banners" class="log-action" value="" <? echo $page->disable_banners ? 'checked' : '' ?>> E. Disable Banner Images
						</label>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-show-links-as-url" class="log-action" value="" <? echo $page->show_links_as_url ? 'checked' : '' ?>> F. Blue Hyperlinks
						</label>
					</div>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="page-content-record-activity" class="log-action" value="" <? echo $page->record_activity ? 'checked' : '' ?>> G. Email me each time someone clicks a link on this quickSITE
						</label>
					</div>
					<div class="form-group">
						<div class="col-xs-2">
							<p class="form-control-static text-right">Cc Email:</p>
						</div>
						<div class="col-xs-4">
							<label for="page-content-activity-email-copy" class="sr-only"></label>
							<input type="email" id="page-content-activity-email-copy" class="form-control log-action" <? echo !$page->record_activity ? 'disabled' : '' ?> value="<? echo isset($page->activity_email_copy) ? $page->activity_email_copy : '' ?>">
						</div>
					</div>
				</div>
			</div>
			<div id="page-content-tab-logo">
				<? $selectedLogo = isset($page) ? $page->getLogo() : null; ?>
				<div class="header">
					<h4 class="checkbox">
						<label><input type="checkbox" id="page-content-show-logo" class="log-action" value="" <? echo isset($selectedLogo) ? 'checked' : '' ?>>Show a Logo on your QuickSite:</label>
					</h4>
				</div>
				<div class="logo-list<? if (!isset($selectedLogo)): ?> disabled<? endif; ?>">
					<ul class="nav nav-pills">
						<? if (isset($logos)): ?>
							<? foreach ($logos as $logo): ?>
								<li>
									<a href="#" class="log-action<? if ($selectedLogo == $logo): ?> opened<? endif; ?>"><img src="<? echo $logo; ?>"></a>
								</li>
							<? endforeach; ?>
						<? endif; ?>
					</ul>
				</div>
			</div>
		</div>
	<? endif; ?>
</div>