<div>
	<? if (isset($page)): ?>
		<div class="form-inline page-title">
			Title: <input type="text" id="page-content-title" class="input-xlarge" value="<? echo $page->title; ?>">
			<div class="create-date">Created: <? echo $page->getCreateDateFormatted(); ?></div>
		</div>
		<legend class="page-url">URL:<a id="page-content-url" href="<? echo $page->getUrl(); ?>" target="_blank"><?echo $page->getUrl();?></a>
		</legend>
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
					<h5>DRAG your Links from the Cart & DROP them into the list below:</h5>
					<div id="page-content-links-title">
						<label>Shared links:<span id="page-content-links-number">#</span></label>
					</div>
				</div>
				<div id="page-content-links-container" class="link-grid-container">
					<? $links = isset($page) ? $page->getPageLinks() : null; ?>
					<? if (isset($links)): ?>
						<?php $this->renderPartial('pageLinks', array('links' => $links), false, true); ?>
					<? endif; ?>
				</div>
			</div>
			<div id="page-content-tab-title">
				<h5 class="checkbox">
					<input type="checkbox" id="page-content-show-description" value="" <?php echo isset($page->subtitle) && $page->subtitle != '' ? 'checked' : '' ?>>This statement will be placed at the TOP of your quickSITE...
				</h5>
				<textarea id="page-content-description"><? echo $page->subtitle; ?></textarea>
			</div>
			<div id="page-content-tab-header">
				<h5 class="checkbox">
					<input type="checkbox" id="page-content-show-header" value="" <?php echo isset($page->header) && $page->header != '' ? 'checked' : '' ?>>The Header Text will be JUST ABOVE the links you are sharing…
				</h5>
				<textarea id="page-content-header-text"><? echo $page->header; ?></textarea>
			</div>
			<div id="page-content-tab-footer">
				<h5 class="checkbox">
					<input type="checkbox" id="page-content-show-footer" value="" <?php echo isset($page->footer) && $page->footer != '' ? 'checked' : '' ?>>The Footer Text will be at the VERY BOTTOM of your quickSITE…
				</h5>
				<textarea id="page-content-footer-text"><? echo $page->footer; ?></textarea>
			</div>
			<div id="page-content-tab-security">
				<? $expDate = $page->getExpirationDateFormatted(); ?>
				<br>
				<form class="form-inline">
					<label class="checkbox"><input type="checkbox" id="page-content-use-expiration-date" value="" <?php echo isset($expDate) && $expDate != '' ? 'checked' : '' ?>><span style="margin-left: 4px;">A. Set Expiration Date</span></label>
					<div id="page-content-expiration-date-container" class="control-group <? if ($page->isExpired()): ?>error<? endif; ?>" <? if (!(isset($expDate) && $expDate != '')): ?>style="display: none"<?endif;?>>
						<div class="input-append date controls" data-date-format="mm/dd/yy" data-date="<? echo $expDate; ?>">
							<input class="input-small" id="page-content-expiration-date" type="text" value="<? echo $expDate; ?>" readonly>
							<span class="add-on"><i class="icon-calendar"></i></span>
						</div>
					</div>
				</form>
				<label class="checkbox"><input type="checkbox" id="page-content-require-login" value="" <?php echo $page->restricted ? 'checked' : '' ?>>B. Require Authorized User login and password to view site</label>
				<form class="form-inline">
					<label class="checkbox"><input id="page-content-access-code-enabled" type="checkbox" value="" <?php echo isset($page->pin_code) && $page->pin_code > 0 ? 'checked' : '' ?>><span style="margin-left: 4px;">C. Create a SECURE ACCESS Pin</span></label>
					<input type="text" maxlength="4" class="input-small" id="page-content-access-code" <? if (!(isset($page->pin_code) && $page->pin_code > 0)): ?>style="display: none;"<?endif;?> value="<?php echo isset($page->pin_code) && $page->pin_code > 0 ? $page->pin_code : '' ?>">
				</form>
				<label class="checkbox"><input type="checkbox" id="page-content-show-link-to-main-site" value="" <?php echo $page->show_site_link ? 'checked' : '' ?>>D. Show Link to MAIN SITE in top ribbon</label>
				<label class="checkbox"><input type="checkbox" id="page-content-show-ticker" value="" <?php echo $page->show_ticker ? 'checked' : '' ?>>E. Show the TICKER at the top of this quickSITE</label>
				<label class="checkbox"><input type="checkbox" id="page-content-disable-widgets" "value="" <?php echo $page->disable_widgets ? 'checked' : '' ?>>F. Disable all Link Widget Icons</label>
				<label class="checkbox"><input type="checkbox" id="page-content-disable-banners" value="" <?php echo $page->disable_banners ? 'checked' : '' ?>>G. Disable all Link Banner Images</label>
				<label class="checkbox"><input type="checkbox" id="page-content-record-activity" value="" <?php echo $page->record_activity ? 'checked' : '' ?>>H. Email me each time someone clicks a link on this quickSITE</label>
			</div>
			<div id="page-content-tab-logo">
				<div class="logo-list">
					<ul class="nav nav-pills">
						<?if (isset($logos)): ?>
							<? $selectedLogo = isset($page) ? $page->getLogo() : null; ?>
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
	<? endif; ?>
</div>