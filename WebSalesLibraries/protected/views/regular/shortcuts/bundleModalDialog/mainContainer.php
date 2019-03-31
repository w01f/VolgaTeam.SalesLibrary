<?
	/**
	 * @var $shortcut BundleModalDialogShortcut
	 */
?>
<table id="shortcuts-bundle-modal">
    <tr>
        <td class="left-panel">
            <div class="items-container regular-page">
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
	            <? if (isset($shortcut->favoritesPage)): ?>
                    <li style="max-width: 150px;">
                        <a data-toggle="tab"
                           href="#<? echo $shortcut->favoritesPage->id; ?>"><? echo $shortcut->favoritesPage->title; ?></a>
                    </li>
                <?endif;?>
            </ul>
            <div class="tab-content">
				<? $hasActive = false; ?>
				<? foreach ($shortcut->tabPages as $tabPage): ?>
                    <div class="tab-pane <? if (!$hasActive): ?>active<? endif; ?> items-container regular-page" id="<? echo $tabPage->id; ?>">
                        <div class="page-content">
						    <? echo $this->renderPartial('bundleModalDialog/tabPage', array('tabPage' => $tabPage), true); ?>
                        </div>
                        <div class="service-data">
                            <div class="tab-logo"><? echo $tabPage->imageUrl; ?></div>
                        </div>
                    </div>
					<? $hasActive = true; ?>
				<? endforeach; ?>
	            <? if (isset($shortcut->favoritesPage)): ?>
                    <div class="tab-pane items-container favorites-page" id="<? echo $shortcut->favoritesPage->id; ?>">
                        <div class="page-content">
	                        <? echo $this->renderPartial('bundleModalDialog/tabPage', array('tabPage' => $shortcut->favoritesPage), true); ?>
                        </div>
                        <div class="service-data">
                            <div class="tab-logo"><? echo $shortcut->favoritesPage->imageUrl; ?></div>
                        </div>
                    </div>
	            <?endif;?>
            </div>
        </td>
    </tr>
</table>
