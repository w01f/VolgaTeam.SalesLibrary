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
    }
    <?endif;?>

    <?echo '#'.$blockId;?> > .btn:not(:last-child)
    {
        border-right: none !important;
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