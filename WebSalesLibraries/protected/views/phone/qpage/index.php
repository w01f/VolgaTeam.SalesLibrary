<?
	/** @var $page QPageRecord */
	$cs = Yii::app()->clientScript;
	$cs->registerCoreScript('jquery');
	$cs->registerCoreScript('cookie');
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/jquery.mobile-1.2.0.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/photoswipe.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/libraries.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/email.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/phone/qpage.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/mobile/jquery.mobile-1.2.0.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/lib/klass.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/photoswipe/code.photoswipe.jquery-3.0.5.min.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/link-viewer.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/favorites.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/email.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/wallbin.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/qpage.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);

	$authorized = false;
	$userId = Yii::app()->user->getId();
	if (isset($userId))
		$authorized = true;
?>
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/qpage/';
</script>
<div data-role='page' id="main" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<span class="ui-title  header-title"><? echo $page->title; ?></span>
	</div>
	<div data-role='content' class="page-content">
		<div id="page-id" style="display: none;"><? echo $page->id; ?></div>
		<? if ($page->record_activity): ?>
			<fieldset data-role="controlgroup" data-theme="d">
				<h4>To view the links on this site, enter your email address:</h4>
				<input type="email" id="user-email" name="user-email" placeholder="Email" required data-mini="true" <? if (isset(Yii::app()->user->email)): ?>value="<? echo Yii::app()->user->email; ?>" <? endif; ?>>
			</fieldset>
		<? endif; ?>
		<div class="centered">
			<img src="<? echo $page->logo; ?>"/>
			<h2>
				<?
					$string = $page->subtitle;
					$string = preg_replace('/[\n\r ]*<style[^>]*>(([^<]|[<[^\/]|<\/[^s]|<\/s[^t])*)<\/style>[\n\r ]*/i', '', $string);
					echo strip_tags($string);
				?>
			</h2>
		</div>
		<h4><? echo strip_tags($page->header); ?></h4>
		<? $links = $page->getLibraryLinks() ?>
		<? if (isset($links)): ?>
			<ul data-role="listview" data-theme="c" data-divider-theme="d">
				<? foreach ($links as $link): ?>
					<? echo $this->renderFile(Yii::getPathOfAlias('application.views.phone.wallbin') . '/link.php', array('link' => $link), true); ?>
				<? endforeach; ?>
			</ul>
		<? endif; ?>
		<h4><? echo strip_tags($page->footer); ?></h4>
	</div>
</div>
<div data-role='page' id="link-details" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#main" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"><? echo $page->title; ?></span>
	</div>
	<div data-role='content' class="page-content"></div>
</div>
<div data-role='page' id="preview" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#main" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"><? echo $page->title; ?></span>
	</div>
	<div data-role='content' class="page-content"></div>
</div>
<div data-role='page' id="gallery-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"><? echo $page->title; ?></span>
	</div>
	<div data-role='content' class="page-content">
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4 id="gallery-title"></h4>
			</li>
		</ul>
		<br>
		<ul id="gallery"></ul>
	</div>
</div>
<? if (Yii::app()->params['favorites_tab']['visible'] && $authorized): ?>
	<div data-role='page' class="favorites-tab" id="favorites-add" data-cache="never" data-dom-cache="false" data-ajax="false">
		<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
			<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
			<span class="ui-title header-title"><? echo $page->title; ?></span>
			<ul data-role="listview" data-theme="c" data-divider-theme="c">
				<li data-role="list-divider">
					<h4>
						<table class="link-container">
							<tr>
								<td>Add to Favorites: <span class="name"></span></td>
							</tr>
						</table>
					</h4>
				</li>
			</ul>
		</div>
		<div data-role='content' class="page-content">
			<table class="layout-group">
				<tr>
					<td colspan="2" class="on-left">Name:</td>
				</tr>
				<tr>
					<td colspan="2" class="on-left">
						<label for="favorite-link-name" class="ui-hide-label"></label>
						<input id="favorite-link-name" name="favorite-link-name" type="text" value="" data-mini="true"/><br>
					</td>
				</tr>
				<tr>
					<td class="on-left">Folder:</td>
					<td class="on-right">
						<a id="favorite-folder-select-button" href="#" data-role="button" data-mini="true" data-icon="arrow-d" data-inline="true" data-iconpos="right">Select</a>
					</td>
				</tr>
				<tr>
					<td colspan="2" class="on-left">
						<label for="favorite-folder-name" class="ui-hide-label"></label>
						<input id="favorite-folder-name" name="favorite-folder-name" type="text" value="" data-mini="true"/>
						<br>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<a id="favorite-add-button" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b">Add to Favorites</a>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div data-role="page" id="favorites-success-popup" data-overlay-theme="c">
		<div data-role="header" data-theme="b">
			<span class="ui-title header-title">Add Favorites</span>
		</div>
		<div data-role="content">
			<div>The link has been added to Favorites.</div>
			<br>
			<a href="#preview" data-role="button" data-corners="true" data-shadow="true" data-transition="pop" data-direction="reverse" data-theme="b">Close</a>
		</div>
	</div>
	<div data-role="dialog" id="favorites-folder-list-dialog" data-overlay-theme="c">
		<div data-role="header" data-theme="b">
			<span class="ui-title header-title">Select Folder</span>
		</div>
		<div data-role="content" class="dialog-content"></div>
	</div>
<? endif; ?>
<div data-role="dialog" id="info-dialog" data-overlay-theme="c" data-title="Are you sure?">
	<div data-role="content">
		<h3 class="dialog-title">???</h3>
		<p class="dialog-description">???</p>
		<a href="#" class="dialog-confirm" data-role="button" data-theme="b" data-rel="back">OK</a>
	</div>
</div>
<!--Template for folder links content-->
<div data-role="page" id="link-folder-content-template" data-overlay-theme="c">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"><? echo $page->title; ?></span>
	</div>
	<div data-role='content' class="page-content"></div>
</div><!--Template for folder links content-->