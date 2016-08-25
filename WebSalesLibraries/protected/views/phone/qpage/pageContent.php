<?
	/**
	 * @var $page QPageRecord
	 * @var $parentId string
	 */
	$authorized = UserIdentity::isUserAuthorized();
?>
<div data-role='page' id="quicksite" class="shortcut-link-page" data-cache="never" data-dom-cache="false" data-ajax="false">
	<div data-role='header' class="page-header" data-position="fixed" data-theme="a">
		<? if ($authorized): ?>
			<a href="#quicksite-popup-panel-left" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
		<h1 class="header-title">Quicksite</h1>
		<? if ($authorized): ?>
			<a href="#quicksite-popup-panel-right" data-icon="ion-navicon-round" data-iconpos="notext"></a>
		<? endif; ?>
	</div>
	<div data-role='content' class="main-content">
		<table class="content-header qpage-header">
			<tr>
				<td class="logo">
					<img src="<? echo $page->logo; ?>">
				</td>
				<? if (isset($parentId)): ?>
					<td class="back">
						<a href="#<? echo $parentId; ?>" data-role="button" data-icon="ion-arrow-left-a" data-mini="true" data-inline="true" data-transition="slidefade" data-direction="reverse">BACK</a>
					</td>
				<? else: ?>
					<td class="title gray">
						<? echo $page->title; ?>
					</td>
				<?endif; ?>
			</tr>
		</table>
		<div class="content-data qpage-data">
			<h3>
				<?
					$string = $page->subtitle;
					$string = preg_replace('/[\n\r ]*<style[^>]*>(([^<]|[<[^\/]|<\/[^s]|<\/s[^t])*)<\/style>[\n\r ]*/i', '', $string);
					echo $string;
				?>
			</h3>
			<p><? echo $page->header; ?></p>
			<? if ($page->record_activity): ?>
				<label for="user-email">To view the links on this site, enter your email address:</label>
				<input type="email" id="user-email" name="user-email" placeholder="Email Address" required data-mini="true" <? if (isset(Yii::app()->user->email)): ?>value="<? echo Yii::app()->user->email; ?>" <? endif; ?>>
			<? endif; ?>
			<div class="qpage-files">
				<h3>Files:</h3>
				<? $links = $page->getLibraryLinks(); ?>
				<? foreach ($links as $link): ?>
					<? if (!in_array($link->type, array(5, 6))): ?>
						<div><a class="file-link" href="#"><span><? echo nl2br($link->name); ?></span>
								<div class="service-data">
									<div class="link-id"><? echo $link->id; ?></div>
								</div>
							</a></div>
					<? endif; ?>
				<? endforeach; ?>
			</div>
			<p><? echo strip_tags($page->footer); ?></p>
		</div>
	</div>
	<div id="email-warning-dialog" data-role="popup" data-theme="a" data-overlay-theme="d" data-dismissible="false">
		<div data-role="header" data-theme="d">
			<h1>Email</h1>
		</div>
		<div role="main" style="padding:0 40px">
			<p>Enter your email address to view this link...</p>
			<a href="#" data-role="button" data-theme="d" data-rel="back">OK</a>     
		</div>
	</div>
	<? if ($authorized): ?>
		<div data-role="panel" data-display="overlay" id="quicksite-popup-panel-left">
			<ul data-role="listview">
				<? echo $this->renderPartial('../shortcuts/groups/groupList'); ?>
				<li data-icon="false">
					<a class="logout-button" href="#">Log Out</a>
				</li>
				<li data-role="list-divider"><p class="user-info">User: <? echo UserIdentity::getCurrentUserLogin(); ?></p></li>
				<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>
			</ul>
		</div>
		<div data-role="panel" data-display="overlay" data-position="right" id="quicksite-popup-panel-right">
			<ul data-role="listview">
				<? echo $this->renderPartial('../wallbin/libraryList'); ?>
			</ul>
		</div>
	<? endif; ?>
</div>
