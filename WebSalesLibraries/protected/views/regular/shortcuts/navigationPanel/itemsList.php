<?
	/** @var NavigationPanel $navigationPanel */
?>
<style>
	#content .navigation-panel {
		background-color: #<?echo $navigationPanel->backColor;?>;
		border-right: #<?echo $navigationPanel->dividerColor;?> solid <?echo $navigationPanel->dividerWidth;?>px;
	}

	#content .navigation-panel ul {
		overflow-y: <?if($navigationPanel->showScroll):?>auto<?else:?>hidden <?endif;?>;
	}

	#content .navigation-panel li a:hover,
	#content .navigation-panel li a:focus:hover
	{
		background-color: #<?echo $navigationPanel->hoverColor;?>;
	}

	#content .navigation-panel li a {
		padding-bottom: <?echo $navigationPanel->itemsGap;?>px;
	}

	#content .navigation-panel li .item-title {
		color: #<?echo $navigationPanel->textColor;?>;
		font-size: <?echo $navigationPanel->textSize;?>px;
	}

	#content .navigation-panel li .item-icon {
		margin-bottom: <?echo $navigationPanel->imagePadding;?>px;
	}
</style>
<ul class="nav nav-pills">
	<? foreach ($navigationPanel->items as $navigationItem): ?>
		<li class="navigation-item" <? if (!Yii::app()->browser->isMobile() && !empty($navigationItem->tooltip)): ?> title="<? echo $navigationItem->tooltip; ?>"<? endif; ?>>
			<? echo $this->renderPartial('navigationPanel/' . $navigationItem->contentView, array('itemData' => $navigationItem), true); ?>
		</li>
	<? endforeach; ?>
</ul>
