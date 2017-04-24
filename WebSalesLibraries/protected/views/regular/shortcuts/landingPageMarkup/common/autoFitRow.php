<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\AutoFitRow;

	/** @var $contentBlock AutoFitRow */

	$blockId = sprintf('auto-fit-row-%s', $contentBlock->id);

	echo $this->renderPartial('landingPageMarkup/style/styleBorder',
		array(
			'border' => $contentBlock->border,
			'blockId' => $blockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
		array(
			'textAppearance' => $contentBlock->getTextAppearance(),
			'blockId' => $blockId
		)
		, true);

	$columnWidthLg = floor(100 / 12);
	if ($contentBlock->itemsPerRow->large < 12)
		$columnWidthLg = floor(100 / $contentBlock->itemsPerRow->large);

	$columnWidthMd = $columnWidthLg;
	if ($contentBlock->itemsPerRow->medium < $contentBlock->itemsPerRow->large)
		$columnWidthMd = floor(100 / $contentBlock->itemsPerRow->medium);

	$columnWidthSm = $columnWidthMd;
	if ($contentBlock->itemsPerRow->small < $contentBlock->itemsPerRow->medium)
		$columnWidthSm = floor(100 / $contentBlock->itemsPerRow->small);

	$columnWidthXs = $columnWidthSm;
	if ($contentBlock->itemsPerRow->extraSmall < $contentBlock->itemsPerRow->small)
		$columnWidthXs = floor(100 / $contentBlock->itemsPerRow->extraSmall);
?>
<style>
    <? echo '#'.$blockId; ?>
    .auto-fit-column-xs {
        width: <?echo $columnWidthXs;?>%;
    }

    @media (min-width: 768px) {
    <? echo '#'.$blockId; ?> .auto-fit-column-sm {
        width: <?echo $columnWidthSm;?>%;
    }
    }

    @media (min-width: 992px) {
    <? echo '#'.$blockId; ?> .auto-fit-column-md {
        width: <?echo $columnWidthMd;?>%;
    }
    }

    @media (min-width: 1200px) {
    <? echo '#'.$blockId; ?> .auto-fit-column-lg {
        width: <?echo $columnWidthLg;?>%;
    }
    }
</style>
<div id="<? echo $blockId; ?>" class="row"
     style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
    <? $i=1;?>
	<? foreach ($contentBlock->items as $childBlock): ?>
        <?
            $columnId = sprintf('auto-fit-column-%s-%s', $contentBlock->id,$i);
            echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
                array(
                    'textAppearance' => $contentBlock->getTextAppearance(),
                    'blockId' => $columnId
                )
                , true);
        ?>
        <div class="auto-fit-column auto-fit-column-xs auto-fit-column-sm auto-fit-column-md auto-fit-column-lg">
			<?
				$viewName = $childBlock->getViewName();
				echo $this->renderPartial('landingPageMarkup/' . $viewName, array('contentBlock' => $childBlock), true);
			?>
        </div>
		<? $i++;?>
	<? endforeach; ?>
</div>
