<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\Column */
?>
<div class="col-lg-<? echo $contentBlock->size->large; ?> col-md-<? echo $contentBlock->size->medium; ?> col-sm-<? echo $contentBlock->size->small; ?> col-xs-<? echo $contentBlock->size->extraSmall; ?>
    <? if ($contentBlock->hideCondition->large): ?> hidden-lg<? endif; ?>
    <? if ($contentBlock->hideCondition->medium): ?> hidden-md<? endif; ?>
    <? if ($contentBlock->hideCondition->small): ?> hidden-sm<? endif; ?>
    <? if ($contentBlock->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>"
     style="<? echo $this->renderPartial('landingPageMarkup/styleTextAppearance', array('textAppearance' => $contentBlock->getTextAppearance()), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/stylePadding', array('padding' => $contentBlock->padding), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/styleMargin', array('margin' => $contentBlock->margin), true); ?>
     <? echo $this->renderPartial('landingPageMarkup/styleBorder', array('border' => $contentBlock->border), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/blockContainer', array('contentBlocks' => $contentBlock->items), true); ?>
</div>
