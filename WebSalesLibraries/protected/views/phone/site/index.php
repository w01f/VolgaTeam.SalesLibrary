<?php
$version = '11.0';
$cs = Yii::app()->clientScript;
$cs->registerCoreScript('jquery');
$cs->registerCoreScript('cookie');
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/photoswipe/photoswipe.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/mobiscroll/css/mobiscroll-2.1.custom.min.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/libraries.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/search.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/file-card.css?' . $version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/email.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/photoswipe/lib/klass.min.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/photoswipe/code.photoswipe.jquery-3.0.5.min.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobiscroll/js/mobiscroll-2.1.custom.min.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/login.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/link-viewing.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/libraries.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/search.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/favorites.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/email.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/ribbon.js?' . $version, CClientScript::POS_HEAD);
?>

<?php
$userId = Yii::app()->user->getId();
if (isset($userId))
{
	$availableEmails = UserRecipientStorage::getRecipientsByUser($userId);
}
$logos = QPageStorage::getPageLogoList();
?>

<div data-role='page' id="libraries" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<span class="ui-title">Sales Libraries</span>
	</div>
	<div data-role='content' class="page-content">
		<table id="selectors-container">
			<tr>
				<td>
					<label for="libraries-selector" class="select">Select Sales Library:</label>
					<select id="libraries-selector" name="libraries-selector" data-native-menu="true"> </select>
				</td>
			</tr>
			<tr>
				<td>
					<br> <label for="page-selector" class="select">Select Page:</label>
					<select id="page-selector" name="page-selector" data-native-menu="true"> </select>
				</td>
			</tr>
			<tr>
				<td>
					<br> <br> <a id="load-page-button" href="#" data-role="button" data-theme="b">Load Library</a>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries ui-btn ui-btn-active ui-state-persist" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="folders" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<span class="ui-title header-title"></span>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="links" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#folders" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="link-details" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#links" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="preview" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#links" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="gallery-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
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
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="search-basic" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
		<span class="ui-title">Search</span>

		<div data-role="navbar">
			<ul>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-transition="none">Keyword</a>
				</li>
				<?if (Yii::app()->params['tags']['visible']): ?>
					<li>
						<a class="tab-search-tags" href="#search-tags" data-transition="none">Tag</a>
					</li>
				<? endif;?>
				<li>
					<a class="tab-search-file-types" href="#search-file-types" data-transition="none">File</a>
				</li>
				<li>
					<a class="tab-search-date" href="#search-date" data-transition="none">Date</a>
				</li>
				<li>
					<a class="tab-search-libraries" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations']['tab_name']; ?></a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="search-file-types" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
		<span class="ui-title">Search</span>

		<div data-role="navbar">
			<ul>
				<li>
					<a href="#search-basic" data-transition="none">Keyword</a>
				</li>
				<?if (Yii::app()->params['tags']['visible']): ?>
					<li>
						<a class="tab-search-tags" href="#search-tags" data-transition="none">Tag</a>
					</li>
				<? endif;?>
				<li>
					<a class="tab-search-file-types ui-btn ui-btn-active ui-state-persist" href="#search-file-types" data-transition="none">File</a>
				</li>
				<li>
					<a class="tab-search-date" href="#search-date" data-transition="none">Date</a>
				</li>
				<li>
					<a class="tab-search-libraries" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations']['tab_name']; ?></a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="search-tags" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
		<span class="ui-title">Search</span>

		<div data-role="navbar">
			<ul>
				<li>
					<a href="#search-basic" data-transition="none">Keyword</a>
				</li>
				<li>
					<a class="tab-search-tags  ui-btn ui-btn-active ui-state-persist" href="#search-tags" data-transition="none">Tag</a>
				</li>
				<li>
					<a class="tab-search-file-types" href="#search-file-types" data-transition="none">File</a>
				</li>
				<li>
					<a class="tab-search-date" href="#search-date" data-transition="none">Date</a>
				</li>
				<li>
					<a class="tab-search-libraries" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations']['tab_name']; ?></a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="search-date" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
		<span class="ui-title">Search</span>

		<div data-role="navbar">
			<ul>
				<li>
					<a href="#search-basic" data-transition="none">Keyword</a>
				</li>
				<?if (Yii::app()->params['tags']['visible']): ?>
					<li>
						<a class="tab-search-tags" href="#search-tags" data-transition="none">Tag</a>
					</li>
				<? endif;?>
				<li>
					<a class="tab-search-file-types" href="#search-file-types" data-transition="none">File</a>
				</li>
				<li>
					<a class="tab-search-date ui-btn ui-btn-active ui-state-persist" href="#search-date" data-transition="none">Date</a>
				</li>
				<li>
					<a class="tab-search-libraries" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations']['tab_name']; ?></a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="search-libraries" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="search-button ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-theme="b" data-icon="search">Search</a>
		<span class="ui-title">Search</span>

		<div data-role="navbar">
			<ul>
				<li>
					<a href="#search-basic" data-transition="none">Keyword</a>
				</li>
				<?if (Yii::app()->params['tags']['visible']): ?>
					<li>
						<a class="tab-search-tags" href="#search-tags" data-transition="none">Tag</a>
					</li>
				<? endif;?>
				<li>
					<a class="tab-search-file-types" href="#search-file-types" data-transition="none">File</a>
				</li>
				<li>
					<a class="tab-search-date" href="#search-date" data-transition="none">Date</a>
				</li>
				<li>
					<a class="tab-search-libraries ui-btn ui-btn-active ui-state-persist" href="#search-libraries" data-transition="none"><?php echo Yii::app()->params['stations']['tab_name']; ?></a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search ui-btn ui-btn-active ui-state-persist" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' id="search-result" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#search-basic" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title">Search</span>
	</div>
	<div data-role='content' class="page-content">
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4>
					<table class="layout-group">
						<tr>
							<td id="search-result-links-number" class="on-left">
								Files was not found
							</td>
							<td id="search-result-sort-column-container" class="on-center">
								<select name="search-result-sort-column" id="search-result-sort-column" data-mini="true">
									<option value="link-name" selected>By Name</option>
									<option value="link-type">By Type</option>
									<option value="link-date">By Date</option>
									<option value="library">By Library</option>
									<option value="link-tag">By Tag</option>
								</select>
							</td>
							<td id="search-result-sort-order-container" class="on-right">
								<select name="search-result-sort-order" id="search-result-sort-order" data-role="slider" data-mini="true" data-track-theme="b">
									<option value="asc">Asc</option>
									<option value="desc">Desc</option>
								</select>
							</td>
						</tr>
					</table>
				</h4>
			</li>
		</ul>
		<br>
		<ul id="search-result-body" data-role="listview" data-theme="c" data-divider-theme="d"></ul>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="add-page-info" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4 class="link-container">
					Email Link: <span class="name"></span>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#add-page-info" data-transition="none">Link</a>
				</li>
				<li>
					<a href="#add-page-logo" data-transition="none">Logo</a>
				</li>
				<li>
					<a href="#add-page-options" data-transition="none">Misc</a>
				</li>
				<li>
					<a href="#add-page-tracking" data-transition="none">Track</a>
				</li>
				<li>
					<a href="#add-page-pin" data-transition="none">Pin</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<table class="layout-group">
			<tr>
				<td colspan="2" class="on-left">
					<input type="checkbox" name="add-page-name-enabled" id="add-page-name-enabled" class="custom" data-mini="true" checked/>
					<label for="add-page-name-enabled">Do you want to show a Page Header?</label>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<input id="add-page-name" name="add-page-name" type="text" data-mini="true" value=""/>
				</td>
			</tr>
			<tr>
				<td class="on-left">Expires in:</td>
				<td class="on-right">
					<select id="add-page-expires-in" data-mini="true">
						<option selected value="7">7 days</option>
						<option value="14">14 days</option>
						<option value="30">30 days</option>
						<option value="0">Never</option>
					</select>
				</td>
			</tr>
			<tr>
				<td class="on-left" width="50%">
					<br>
					<a class="add-page-accept" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Send</a>
				</td>
				<td class="on-right" width="50%">
					<br>
					<a id="add-page-info-disclaimer" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Important</a>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="add-page-logo" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4 class="link-container">
					Email Link: <span class="name"></span>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a href="#add-page-info" data-transition="none">Link</a>
				</li>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#add-page-logo" data-transition="none">Logo</a>
				</li>
				<li>
					<a href="#add-page-options" data-transition="none">Misc</a>
				</li>
				<li>
					<a href="#add-page-tracking" data-transition="none">Track</a>
				</li>
				<li>
					<a href="#add-page-pin" data-transition="none">Pin</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<ul data-role="listview" data-theme="c" data-divider-theme="d">
			<?php if (isset($logos)): ?>
				<?php foreach ($logos as $logo): ?>
					<li><a class="qpage-logo" href="#"><img src="<? echo $logo; ?>"></a></li>
				<?php endforeach; ?>
			<?php endif; ?>
		</ul>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="add-page-options" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4 class="link-container">
					Email Link: <span class="name"></span>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a href="#add-page-info" data-transition="none">Link</a>
				</li>
				<li>
					<a href="#add-page-logo" data-transition="none">Logo</a>
				</li>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#add-page-options" data-transition="none">Misc</a>
				</li>
				<li>
					<a href="#add-page-tracking" data-transition="none">Track</a>
				</li>
				<li>
					<a href="#add-page-pin" data-transition="none">Pin</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<table class="layout-group">
			<tr>
				<td colspan="2" class="on-left">
					<input type="checkbox" name="add-page-disable-widgets" id="add-page-disable-widgets" class="custom" data-mini="true"/>
					<label for="add-page-disable-widgets">Disable all Link Widget Icons</label>
					<input type="checkbox" name="add-page-disable-banners" id="add-page-disable-banners" class="custom" data-mini="true"/>
					<label for="add-page-disable-banners">Disable all Link Banner Images</label>
					<input type="checkbox" name="add-page-show-links-as-url" id="add-page-show-links-as-url" class="custom" data-mini="true"/>
					<label for="add-page-show-links-as-url">Display all Links as Blue Hyperlinks</label>
					<br><br>
				</td>
			</tr>
			<tr>
				<td class="on-left" width="50%">
					<a class="add-page-accept" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Send</a>
				</td>
				<td class="on-right" width="50%">
					<a id="add-page-options-disclaimer" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Important</a>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="add-page-tracking" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4 class="link-container">
					Email Link: <span class="name"></span>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a href="#add-page-info" data-transition="none">Link</a>
				</li>
				<li>
					<a href="#add-page-logo" data-transition="none">Logo</a>
				</li>
				<li>
					<a href="#add-page-options" data-transition="none">Misc</a>
				</li>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#add-page-tracking" data-transition="none">Track</a>
				</li>
				<li>
					<a href="#add-page-pin" data-transition="none">Pin</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<table class="layout-group">
			<tr>
				<td colspan="2" class="on-left">
					<input type="checkbox" name="add-page-record-activity" id="add-page-record-activity" class="custom" data-mini="true"/>
					<label for="add-page-record-activity">Send me link notifications</label>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<label for="add-page-activity-email-copy" style="font-size: 10pt;">Cc Email:</label>
					<input type="email" id="add-page-activity-email-copy" name="add-page-activity-email-copy" data-mini="true" disabled>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<input type="checkbox" name="add-page-restricted" id="add-page-restricted" class="custom" data-mini="true"/>
					<label for="add-page-restricted">Require User Login</label>
				</td>
			</tr>
			<tr>
				<td class="on-left" width="50%">
					<a class="add-page-accept" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Send</a>
				</td>
				<td class="on-right" width="50%">
					<a id="add-page-tracking-disclaimer" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Important</a>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="add-page-pin" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4 class="link-container">
					Email Link: <span class="name"></span>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a href="#add-page-info" data-transition="none">Link</a>
				</li>
				<li>
					<a href="#add-page-logo" data-transition="none">Logo</a>
				</li>
				<li>
					<a href="#add-page-options" data-transition="none">Misc</a>
				</li>
				<li>
					<a href="#add-page-tracking" data-transition="none">Track</a>
				</li>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#add-page-pin" data-transition="none">Pin</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<table class="layout-group">
			<tr>
				<td class="on-left" colspan="2">
					<input type="checkbox" name="add-page-access-code-enabled" id="add-page-access-code-enabled" class="custom" data-mini="true"/>
					<label for="add-page-access-code-enabled">ACCESS Pin</label>
				</td>
			</tr>
			<tr>
				<td class="on-left" colspan="2">
					<input type="email" id="add-page-access-code" maxlength=4 data-mini="true" disabled>
					<br><br>
				</td>
			</tr>
			<tr>
				<td class="on-left" width="50%">
					<br>
					<a class="add-page-accept" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Send</a>
				</td>
				<td class="on-right" width="50%">
					<br>
					<a id="add-page-pin-disclaimer" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b" data-mini="true">Important</a>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="email-address" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4>
					<table class="link-container">
						<tr>
							<td>Email Link: <span class="name"></span></td>
						</tr>
					</table>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#email-address" data-transition="none">People</a>
				</li>
				<li>
					<a href="#email-text" data-transition="none">Info</a>
				</li>
				<li>
					<a href="#email-summary" data-transition="none">Send</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<table class="layout-group">
			<tr>
				<td class="on-left">To:</td>
				<td class="on-right">
					<a <?php if (isset($availableEmails)): ?> id="email-to-select-button"<?php endif; ?>
						class="<?php if (!isset($availableEmails)) echo 'ui-disabled'; ?>" href="#" data-role="button" data-mini="true" data-icon="arrow-d" data-inline="true" data-iconpos="right">Select Recipients</a>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<input id="email-to" name="email-to" type="text" value="" data-mini="true"/> <br>
				</td>
			</tr>
			<tr>
				<td class="on-left">Cc:</td>
				<td class="on-right">
					<a <?php if (isset($availableEmails)): ?>id="email-to-copy-select-button"<?php endif; ?>
					   class="<?php if (!isset($availableEmails)) echo 'ui-disabled'; ?>" href="#" data-role="button" data-mini="true" data-icon="arrow-d" data-inline="true" data-iconpos="right">Select Recipients</a>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<input id="email-to-copy" name="email-to-copy" type="text" value="" data-mini="true"/> <br>
				</td>
			</tr>
			<tr>
				<td class="on-left" width="70%">From:</td>
				<td class="on-right">
					<input type="checkbox" name="email-from-copy-me" id="email-from-copy-me" class="custom" data-mini="true"/>
					<label for="email-from-copy-me">Send Copy to Me</label>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<input id="email-from" name="email-to-copy" type="text" data-mini="true" value="<?php echo Yii::app()->user->email; ?>"/>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="email-text" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4>
					<table class="link-container">
						<tr>
							<td>Email Link: <span class="name"></span></td>
						</tr>
					</table>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a href="#email-address" data-transition="none">People</a>
				</li>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#email-text" data-transition="none">Info</a>
				</li>
				<li>
					<a href="#email-summary" data-transition="none">Send</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<table class="layout-group">
			<tr>
				<td colspan="2" class="on-left">Subject Header:</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<input id="email-subject" name="email-subject" type="text" data-mini="true" value="<?php echo Yii::app()->params['email']['send_link']['subject']; ?>"/>
					<br>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">Message Body:</td>
			</tr>
			<tr>
				<td colspan="2" class="on-left">
					<textarea id="email-body" rows="4"><?php echo Yii::app()->params['email']['send_link']['body']; ?></textarea>
					<br>
				</td>
			</tr>
			<tr>
				<td class="on-left">Expires After:</td>
				<td class="on-right">
					<select id="expires-in" data-mini="true">
						<option selected value="7">7 days</option>
						<option value="30">30 days</option>
						<option value="90">90 days</option>
						<option value="">Never</option>
					</select>
				</td>
			</tr>
		</table>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role='page' class="email-tab" id="email-summary" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title header-title"></span>
		<ul data-role="listview" data-theme="c" data-divider-theme="c">
			<li data-role="list-divider">
				<h4>
					<table class="link-container">
						<tr>
							<td>Email Link: <span class="name"></span></td>
						</tr>
					</table>
				</h4>
			</li>
		</ul>
		<div data-role="navbar">
			<ul>
				<li>
					<a href="#email-address" data-transition="none">People</a>
				</li>
				<li>
					<a href="#email-text" data-transition="none">Info</a>
				</li>
				<li>
					<a class="ui-btn ui-btn-active ui-state-persist" href="#email-summary" data-transition="none">Send</a>
				</li>
			</ul>
		</div>
	</div>
	<div data-role='content' class="page-content">
		<div>
			<span>To: </span><span id="email-to-summary"></span>
		</div>
		<br>

		<div>
			<span>Cc: </span><span id="email-to-copy-summary"></span>
		</div>
		<br>

		<div>
			<span>From: </span><span id="email-from-summary"></span>
		</div>
		<br>

		<div>
			<span>Subject: </span><span id="email-subject-summary"></span>
		</div>
		<br>

		<div>
			<span>Message: </span><span id="email-body-summary"></span>
		</div>
		<br>
		<a id="email-send" href="#" data-role="button" data-corners="true" data-shadow="true" data-theme="b">Send Email</a>
	</div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div>
<div data-role="dialog" id="email-to-existed-list" data-overlay-theme="c">
	<div data-role="header" data-theme="b">
		<span class="ui-title header-title">Recipients</span>
	</div>
	<div data-role="content">
		<?php if (isset($availableEmails)): ?>
			<fieldset id="email-to-existed-list-container" data-role="controlgroup">
				<?php $i = 0; ?>
				<?php foreach ($availableEmails as $email): ?>
					<input type="checkbox" name="existed-email-to<?php echo $i; ?>" id="existed-email-to<?php echo $i; ?>" class="existed-email-to" class="custom" value="<?php echo $email; ?>"/>
					<label for="existed-email-to<?php echo $i; ?>"><?php echo $email; ?></label>
					<?php $i++; ?>
				<?php endforeach; ?>
			</fieldset>
			<br>
			<a id="email-to-apply-button" href="#email-address" data-role="button" data-corners="true" data-shadow="true" data-transition="pop" data-direction="reverse" data-theme="b" data-icon="check">Apply</a>
		<?php endif; ?>
	</div>
</div>
<div data-role="dialog" id="email-to-copy-existed-list" data-overlay-theme="c">
	<div data-role="header" data-theme="b">
		<span class="ui-title header-title">Recipients</span>
	</div>
	<div data-role="content">
		<?php if (isset($availableEmails)): ?>
			<fieldset id="email-to-copy-existed-list-container" data-role="controlgroup">
				<?php $i = 0; ?>
				<?php foreach ($availableEmails as $email): ?>
					<input type="checkbox" name="existed-email-to-copy<?php echo $i; ?>" id="existed-email-to-copy<?php echo $i; ?>" class="existed-email-to-copy" class="custom" value="<?php echo $email; ?>"/>
					<label for="existed-email-to-copy<?php echo $i; ?>"><?php echo $email; ?></label>
					<?php $i++; ?>
				<?php endforeach; ?>
			</fieldset>
			<br>
			<a id="email-to-copy-apply-button" href="#" data-role="button" data-corners="true" data-shadow="true" data-transition="pop" data-direction="reverse" data-theme="b" data-icon="check">Apply</a>
		<?php endif; ?>
	</div>
</div>
<div data-role="page" id="email-success-popup" data-overlay-theme="c">
	<div data-role="header" data-theme="b">
		<span class="ui-title header-title">Email sent</span>
	</div>
	<div data-role="content">
		<div>The email has been sent by the adSALESapps server.</div>
		<br>

		<div>Tell your Recipient they MAY want to check their Spam or Junk mail if they do not receive the link.</div>
		<br>
		<a href="#preview" data-role="button" data-corners="true" data-shadow="true" data-transition="pop" data-direction="reverse" data-theme="b">Close</a>
	</div>
</div>
<? if (Yii::app()->params['favorites_tab']['visible']): ?>
	<div data-role='page' class="favorites-tab" id="favorites-add" data-cache="never" data-dom-cache="false" data-ajax="false">
		<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
			<a class="link back ui-btn-right" href="#preview" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
			<span class="ui-title header-title"></span>
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
		<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
			<div data-role="navbar" data-iconpos="top">
				<ul>
					<li>
						<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
					</li>
					<li>
						<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
					</li>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
					<li>
						<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
					</li>
				</ul>
			</div>
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
<div data-role="dialog" id="confirmation-dialog" data-overlay-theme="c" data-title="Are you sure?">
	<div data-role="content">
		<p class="dialog-description">???</p>

		<h3 class="dialog-title">???</h3>
		<a href="#" class="dialog-confirm" data-role="button" data-theme="b" data-rel="back">Yes</a>
		<a href="#" class="dialog-cancel" data-role="button" data-theme="b" data-rel="back">No</a>
	</div>
</div>
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
		<span class="ui-title header-title"></span>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<? if (Yii::app()->params['favorites_tab']['visible']): ?>
					<li>
						<a class="tab-favorites" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
					</li>
				<? endif; ?>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div><!--Template for folder links content--><!--Template for favorites content-->
<div data-role='page' id="favorites-template" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="b">
		<a class="link back ui-btn-right" href="#" data-role="button" data-mini="true" data-corners="true" data-shadow="true" data-transition="slidefade" data-direction="reverse" data-theme="b">Back</a>
		<span class="ui-title">Favorites</span>
	</div>
	<div data-role='content' class="page-content"></div>
	<div class="page-footer" data-role='footer' data-id="ribbon" data-position="fixed" data-theme="b">
		<div data-role="navbar" data-iconpos="top">
			<ul>
				<li>
					<a class="tab-libraries" href="#libraries" data-icon="grid" data-transition="slidefade" data-direction="reverse"> Libraries </a>
				</li>
				<li>
					<a class="tab-search" href="#search-basic" data-icon="search" data-transition="slidefade"> Search </a>
				</li>
				<li>
					<a class="tab-favorites ui-btn ui-btn-active ui-state-persist" href="#favorites" data-icon="star" data-transition="slidefade"> Favs </a>
				</li>
				<li>
					<a class="logout-button" href="#logout" data-icon="delete" data-transition="slidefade"> Log Out </a>
				</li>
			</ul>
		</div>
	</div>
</div><!--Template for favorites content-->