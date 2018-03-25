<?

	use application\models\shortcuts\models\landing_page\regular_markup\toggle_panel\TogglePanelBlock;

	/**
     * @var $contentBlock TogglePanelBlock
	 * @var $screenSettings array
     */

	$blockId = sprintf('toggle-panel-%s', $contentBlock->id);

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
	echo $this->renderPartial('landingPageMarkup/style/styleBackground',
		array(
			'background' => $contentBlock->background,
			'blockId' => $blockId
		)
		, true);
?>
<style>
    <? if (isset($contentBlock->buttonPadding) && $contentBlock->buttonPadding->isConfigured): ?>
    <? echo '#'.$blockId; ?> .toggle-buttons {

        padding-top: <? echo $contentBlock->buttonPadding->top; ?>px !important;
        padding-left: <? echo $contentBlock->buttonPadding->left; ?>px !important;
        padding-bottom: <? echo $contentBlock->buttonPadding->bottom; ?>px !important;
        padding-right: <? echo $contentBlock->buttonPadding->right; ?>px !important;
    }

    <?endif;?>

    <? echo '#'.$blockId; ?> .toggle-button {
        background-color: <?echo Utils::formatColor($contentBlock->buttonStyle->backColorRegular);?> !important;
        border-color: <?echo $contentBlock->buttonStyle->hasBorder? Utils::formatColor($contentBlock->buttonStyle->borderColorRegular):'transparent';?> !important;
    }

    <? echo '#'.$blockId; ?> .toggle-button.toggle-button-active {
        background-color: <?echo Utils::formatColor($contentBlock->buttonStyle->backColorSelected);?> !important;
        border-color: <?echo $contentBlock->buttonStyle->hasBorder? Utils::formatColor($contentBlock->buttonStyle->borderColorSelected):'transparent';?> !important;
    }
</style>
<div class="toggle-panel" id="<? echo $blockId; ?>">
    <div class="row">
        <div class="col-xs-12">
            <div class="btn-group toggle-buttons" role="group">
				<? foreach ($contentBlock->toggleButtons as $button): ?>
					<?
					$buttonTextId = sprintf("toggle-button-%s", $button->id);
					echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
						array(
							'textAppearance' => $button->textAppearance,
							'blockId' => $buttonTextId,
							'selectedClass' => 'toggle-button-active'
						)
						, true);
					?>
                    <button id="<? echo $buttonTextId ?>" type="button" class="btn btn-default toggle-button<? if ($button->isDefault): ?> toggle-button-active<? endif; ?>"
                            data-toggle-tag="<? echo $button->tag; ?>">
						<? echo $button->title; ?>
                    </button>
				<? endforeach; ?>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
	        <? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items, 'screenSettings' => $screenSettings), true); ?>
        </div>
    </div>
</div>