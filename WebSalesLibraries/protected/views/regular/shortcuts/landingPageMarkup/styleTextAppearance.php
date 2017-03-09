<?
	/** @var $textAppearance \application\models\shortcuts\models\landing_page\regular_markup\TextAppearance */
?>
<? if (isset($textAppearance)): ?>
	<? if (isset($textAppearance->color)): ?>
        color: <? echo '#' . $textAppearance->color; ?> !important;
	<? endif; ?>
	<? if ($textAppearance->lineHeight > 0): ?>
        line-height: <? echo $textAppearance->lineHeight; ?>px !important;
	<? endif; ?>
	<? if (isset($textAppearance->alignment)): ?>
        text-align: <? echo $textAppearance->alignment; ?> !important;
	<? endif; ?>
	<? if (isset($textAppearance->font)): ?>
        font-family: <? echo FontReplacementHelper::replaceFont($textAppearance->font->name); ?> !important;
        font-size: <? echo $textAppearance->font->size; ?>pt !important;
        font-weight: <? echo $textAppearance->font->isBold ? 'bold' : 'normal'; ?> !important;
        font-style: <? echo $textAppearance->font->isItalic ? 'italic' : 'normal'; ?> !important;
        text-decoration: <? echo $textAppearance->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
	<? endif; ?>
<? endif; ?>

