<?php
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $page QPageRecord
	 */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/qpage/page-content.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/qpage/qpage-controller.js?' . Yii::app()->params['version'], CClientScript::POS_END);
	$authorized = UserIdentity::isUserAuthorized();

	if ($page->is_email)
		$headerText = trim(strip_tags($page->subtitle));
	else
		$headerText = trim(strip_tags($page->title));

	if (!empty($headerText))
	{
		if (count($menuGroups) > 0)
			$this->renderPartial('../menu/mainMenu', array(
				'menuGroups' => $menuGroups,
				'headerText' => $headerText,
				'showMainSiteUrl' => true,
				'mainSiteUrl' => $page->getUrl(),
				'mainSiteName' => sprintf('Back to: %s', $headerText),
				'mainSiteTarget' => '_self'));
		else
			$this->renderPartial('../menu/singlePageMenu', array('headerText' => $headerText, 'showMainSiteUrl' => false));
	}
?>
<? if (empty($headerText)): ?>
    <style>
        body {
            padding-top: 0 !important;
        }
    </style>
<? endif; ?>
<table id="content">
    <tr>
        <td class="content-inner">
            <div class="content-scrollable-area">
                <div id="page-id" style="display: none;"><? echo $page->id; ?></div>
				<? if (isset($page->logo) || $page->record_activity): ?>
                    <div class="qpage-header">
                        <div>
							<? if (isset($page->logo)): ?>
                                <img src="<? echo $page->logo; ?>">
							<? endif; ?>
                        </div>
                        <div class="email logger-form">
							<? if ($page->record_activity): ?>
                                <div class="row">
                                    <div class="col-xs-8">
                                        <p class="email-description">
                                            <strong>To view the links on this site, enter your email address:</strong>
                                        </p>
                                    </div>
                                    <div class="col-xs-4">
                                        <input type="email" id="user-email" class="form-control" placeholder="Email"
                                               required
										       <? if (isset(Yii::app()->user->email)): ?>value="<? echo Yii::app()->user->email; ?>" <? endif; ?>>
                                    </div>
                                </div>
							<? endif; ?>
                        </div>
                    </div>
				<? endif; ?>
                <div class="padding">
					<? if (!$page->is_email && !empty($page->subtitle)): ?>
                        <div id="page-title"><? echo $page->subtitle; ?></div>
					<? endif; ?>
					<? if (!empty($page->header)): ?>
                        <div id="page-header"><? echo nl2br($page->header); ?></div>
					<? endif; ?>
                    <div id="page-links-container" class="folder-links-container">
						<? if ($page->show_links_as_url): ?>
                        <ul class="nav nav-pills nav-stacked">
							<? endif; ?>
							<? $links = $page->getLibraryLinks() ?>
							<? if (isset($links)): ?>
								<?php foreach ($links as $link): ?>
									<? if ($page->show_links_as_url): ?>
										<? if ($link->isLineBreak): ?>
											<? $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php',
												array(
													'link' => $link,
													'disableBanner' => $page->disable_banners,
													'disableWidget' => $page->disable_widgets,
													'authorized' => $authorized
												)); ?>
										<? elseif ($link->name != ''): ?>
											<? $this->renderPartial('blueHyperlink', array(
												'link' => $link,
												'authorized' => $authorized,
												'disableWidget' => $page->disable_widgets,
											)); ?>
										<? endif; ?>
									<? else: ?>
										<? $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php',
											array(
												'link' => $link,
												'disableBanner' => $page->disable_banners,
												'disableWidget' => $page->disable_widgets,
												'authorized' => $authorized
											)); ?>
									<? endif; ?>
								<?php endforeach; ?>
							<? endif; ?>
							<? if ($page->show_links_as_url): ?>
                        </ul>
					<? endif; ?>
                    </div>
                    <div id="page-footer"><? echo nl2br($page->footer); ?></div>
                </div>
            </div>
        </td>
    </tr>
</table>