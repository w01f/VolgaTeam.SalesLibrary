<?

	use application\models\shortcuts\models\landing_page\regular_markup\menu_stripe\MenuStripeItem;

	/**
	 * @var $menuItem MenuStripeItem
	 * @var $blockId string
     */

	$textAppearance = $menuItem->textAppearance;
?>
<style>
    <? echo '#'.$blockId; ?> a
    {
        <? if (isset($textAppearance->color)): ?>
            color: <? echo Utils::formatColorToHex($textAppearance->color); ?> !important;
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
    }
    <? if (isset($textAppearance->hoverColor)): ?>
        <? echo '#'.$blockId; ?> a:hover
        {
          color: <? echo Utils::formatColorToHex($textAppearance->hoverColor); ?> !important;
        }
    <? endif; ?>

    @media (min-width: 768px)
    {
        <? echo '#'.$blockId; ?> a
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->small; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 992px)
    {
        <? echo '#'.$blockId; ?> a
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->medium; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 1200px)
    {
        <? echo '#'.$blockId; ?> a
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->large; ?>pt !important;
            <? endif; ?>
        }
    }
</style>