<?
	/**
	 * @var $data LinkBundlePreviewData
	 */
?>

<table class="bundle-container">
    <tr>
        <td class="bundle-items-list" rowspan="2">
            <ul class="nav nav-pills">
				<? foreach ($data->bundleItems as $bundleItem): ?>
					<? if ($bundleItem->visible): ?>
                        <li id="bundle-item-<? echo $bundleItem->id; ?>"
                            class="bundle-item<? if ($bundleItem->hasContent): ?> content-item<? endif; ?>"
							<? if (!Yii::app()->browser->isMobile() && !empty($bundleItem->hoverTip)): ?> data-toggle="tooltip" title="<? echo $bundleItem->hoverTip; ?>"<? endif; ?>>
							<? echo $this->renderPartial($bundleItem->contentView, array('itemData' => $bundleItem, 'parentBundleData' => $data), true); ?>
                        </li>
					<? endif; ?>
				<? endforeach; ?>
            </ul>
        </td>
        <td class="bundle-header-container">
            <div class="bundle-header"><? echo $data->name; ?></div>
        </td>
    </tr>
    <tr>
        <td class="link-viewer-container"></td>
    </tr>
</table>
