<?
	use application\models\shortcuts\models\landing_page\regular_markup\button_group\ButtonGroupBlock;

	/** @var $contentBlock ButtonGroupBlock */

	$blockId = sprintf('button-group-%s', $contentBlock->id);

	$textAppearance = $contentBlock->textAppearance;
?>
<style>
    <?if($contentBlock->height > 0):?>
    <?echo '#'.$blockId;?>
    {
        min-height: <? echo $contentBlock->height; ?>px;
        height: <? echo $contentBlock->height; ?>px;
        <? if ($contentBlock->fixedSize->extraSmall>0): ?>
            max-width: <? echo $contentBlock->fixedSize->extraSmall; ?>px;
        <? endif; ?>
    }
    <?endif;?>

    <?echo '#'.$blockId;?> > .btn:not(:last-child)
    {
        border-right: none !important;
    }

    @media (min-width: 768px)
    {
        <?echo '#'.$blockId;?>
        {
            <? if ($contentBlock->fixedSize->small>0): ?>
                max-width: <? echo $contentBlock->fixedSize->small; ?>px;
            <? endif; ?>
        }
    }

    @media (min-width: 992px)
    {
        <?echo '#'.$blockId;?>
        {
            <? if ($contentBlock->fixedSize->medium>0): ?>
                max-width: <? echo $contentBlock->fixedSize->medium; ?>px;
            <? endif; ?>
        }
    }

    @media (min-width: 1200px)
    {
        <?echo '#'.$blockId;?>
        {
            <? if ($contentBlock->fixedSize->large>0): ?>
                max-width: <? echo $contentBlock->fixedSize->large; ?>px;
            <? endif; ?>
        }
    }
</style>
<div id="<? echo $blockId; ?>" class="btn-group btn-group-justified landing-page-button-group">
	<? foreach ($contentBlock->buttons as $buttonItem): ?>
		<?
		switch ($buttonItem->type)
		{
			case 'url':
			case 'undefined':
				echo $this->renderPartial('landingPageMarkup/button_group/urlItem', array('buttonItem' => $buttonItem), true);
				break;
			case 'shortcut':
				echo $this->renderPartial('landingPageMarkup/button_group/shortcutItem', array('buttonItem' => $buttonItem), true);
				break;
		}
		?>
	<? endforeach; ?>
</div>