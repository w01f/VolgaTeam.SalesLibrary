<?
	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * @var $blockId string
	 * @var $selectedClass string
     * @var $textAppearance TextAppearance
     */
?>
<? if (isset($textAppearance)): ?>
    <style>
        <?echo '#'.$blockId;?> {
            <? if($textAppearance->wrapText): ?>;
                white-space: normal;
            <? endif; ?>
            <? if (isset($textAppearance->color)): ?>
                color: <? echo Utils::formatColor($textAppearance->color); ?> !important;
            <? endif; ?>
            <? if (isset($textAppearance->hoverColor)): ?>
                -moz-transition: all .2s ease-in;
                -o-transition: all .2s ease-in;
                -webkit-transition: all .2s ease-in;
                transition: all .2s ease-in;
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
        }
        <? if (isset($textAppearance->hoverColor)): ?>
            <?echo '#'.$blockId;?>:hover {
                color: <? echo Utils::formatColor($textAppearance->hoverColor); ?> !important;
            }
        <? endif; ?>
        <? if (isset($textAppearance->selectedColor) && isset($selectedClass)): ?>
            <?echo '#'.$blockId;?>.<? echo $selectedClass;?> {
                color: <? echo Utils::formatColor($textAppearance->selectedColor); ?> !important;
            }
        <? endif; ?>
    </style>
<? endif; ?>

