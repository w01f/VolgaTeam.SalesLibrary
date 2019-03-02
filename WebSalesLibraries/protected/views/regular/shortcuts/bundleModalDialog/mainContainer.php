<?
	/**
	 * @var $shortcut BundleModalDialogShortcut
	 */
?>
<table id="shortcuts-bundle-modal">
    <tr>
        <td class="left-panel">
            <div class="items-container">
				<? echo $this->renderPartial('bundleModalDialog/leftPanel', array('leftPanel' => $shortcut->leftPanel), true); ?>
            </div>
        </td>
        <td class="tab-page-container">
            <div class="tab-logo-container text-center">
                <img src="<? echo $shortcut->tabPages[0]->imageUrl; ?>" style="height: 100%;">
            </div>
            <ul class="nav nav-tabs" style="margin-top: 20px;">
				<? $hasActive = false; ?>
				<? foreach ($shortcut->tabPages as $tabPage): ?>
                    <li <? if (!$hasActive): ?>class="active"<? endif; ?> style="max-width: 150px;">
                        <a data-toggle="tab" href="#<? echo $tabPage->id; ?>"><? echo $tabPage->title; ?></a>
                    </li>
					<? $hasActive = true; ?>
				<? endforeach; ?>
            </ul>
            <div class="tab-content">
				<? $hasActive = false; ?>
				<? foreach ($shortcut->tabPages as $tabPage): ?>
                    <div class="tab-pane <? if (!$hasActive): ?>active<? endif; ?> items-container"
                         id="<? echo $tabPage->id; ?>">
						<? echo $this->renderPartial('bundleModalDialog/tabPage', array('tabPage' => $tabPage), true); ?>
                        <div class="service-data">
                            <div class="tab-logo"><? echo $tabPage->imageUrl; ?></div>
                        </div>
                    </div>
					<? $hasActive = true; ?>
				<? endforeach; ?>
            </div>
        </td>
    </tr>
</table>
