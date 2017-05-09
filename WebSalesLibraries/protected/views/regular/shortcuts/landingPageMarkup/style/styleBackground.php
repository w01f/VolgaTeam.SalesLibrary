<?
	use application\models\shortcuts\models\landing_page\regular_markup\style\BackgroundStyle;

	/**
	 * @var $blockId string
	 * @var $background BackgroundStyle
	 */
?>
<? if (isset($background)): ?>
    <style>
        <?echo '#'.$blockId;?>
        {
        <?if($background->gradient!==BackgroundStyle::GradientTypeNone):?>
            background: linear-gradient(<?echo $background->gradient?>, <?echo Utils::formatColor($background->color1);?>, <?echo Utils::formatColor($background->color2);?>);
        <?else: ?>
            background: <? echo Utils::formatColor($background->color1); ?>;
        <?endif;?>
        }
    </style>
<? endif; ?>




