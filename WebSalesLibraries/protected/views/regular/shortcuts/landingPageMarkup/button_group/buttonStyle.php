<?

	use application\models\shortcuts\models\landing_page\regular_markup\button_group\ButtonItem;

	/**
	 * @var $buttonItem ButtonItem
	 * @var $blockId string
     */

	echo $this->renderPartial('landingPageMarkup/style/styleBorder',
		array(
			'border' => $buttonItem->parentGroup->border,
			'blockId' => $blockId
		)
		, true);

	$textAppearance = $buttonItem->textAppearance;
?>
<style>
    <? echo '#'.$blockId; ?>,
    <? echo '#'.$blockId; ?>:focus,
    <? echo '#'.$blockId; ?>:active
    {
        <? if (isset($textAppearance->color)): ?>
            color: <? echo Utils::formatColor($textAppearance->color); ?> !important;
        <? endif; ?>
        <? if ($textAppearance->lineHeight > 0): ?>
            line-height: <? echo $textAppearance->lineHeight; ?>px !important;
        <? endif; ?>
        <? if (isset($textAppearance->alignment)): ?>
            text-align: <? echo $textAppearance->alignment; ?> !important;
        <? endif; ?>
        <? if (isset($textAppearance->font)): ?>
            font-family: <? echo FontReplacementHelper::replaceFont($textAppearance->font->name); ?> !important;
            font-size: <? echo $textAppearance->font->size->extraSmall; ?>pt !important;
            font-weight: <? echo $textAppearance->font->isBold ? 'bold' : 'normal'; ?> !important;
            font-style: <? echo $textAppearance->font->isItalic ? 'italic' : 'normal'; ?> !important;
            text-decoration: <? echo $textAppearance->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
        <? endif; ?>
        <? if (isset($buttonItem->backgroundColor)): ?>
            background-color: <? echo Utils::formatColor($buttonItem->backgroundColor); ?> !important;
        <? endif; ?>
    }

    <? echo '#'.$blockId; ?>:hover
    {
     <? if (isset($textAppearance->hoverColor)): ?>
        color: <? echo Utils::formatColor($textAppearance->hoverColor); ?> !important;
     <? endif; ?>
     <? if (isset($buttonItem->backgroundHoverColor)): ?>
         background-color: <? echo Utils::formatColor($buttonItem->backgroundHoverColor); ?> !important;
     <? endif; ?>
    }


    @media (min-width: 768px)
    {
        <? echo '#'.$blockId; ?>
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->small; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 992px)
    {
        <? echo '#'.$blockId; ?>
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->medium; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 1200px)
    {
        <? echo '#'.$blockId; ?>
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->large; ?>pt !important;
            <? endif; ?>
        }
    }
</style>