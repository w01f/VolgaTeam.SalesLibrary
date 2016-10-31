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
					<li class="bundle-item<? if ($bundleItem->hasContent): ?> content-item<? endif; ?><? if ($bundleItem->isDefault): ?> default<? endif; ?>" <? if (!Yii::app()->browser->isMobile() && !empty($bundleItem->hoverTip)): ?> data-toggle="tooltip" title="<? echo $bundleItem->hoverTip; ?>"<? endif; ?>>
						<? echo $this->renderPartial($bundleItem->contentView, array('itemData' => $bundleItem), true); ?>
					</li>
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
