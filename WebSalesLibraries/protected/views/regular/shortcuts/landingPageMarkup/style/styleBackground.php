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
        <? if(!empty($background->image)):?>
            <? if(!empty($background->overlayColor)):?>
                background-image: linear-gradient(0deg,<? echo Utils::formatColorToRgba($background->overlayColor,$background->overlayOpacity); ?>,<? echo Utils::formatColorToRgba($background->overlayColor,$background->overlayOpacity); ?>),url("<?echo $background->image;?>");
            <?else:?>
                background-image: url("<?echo $background->image;?>");
            <?endif;?>
            background-size: cover;
        <? elseif($background->gradient!==BackgroundStyle::GradientTypeNone):?>
            background: linear-gradient(<?echo $background->gradient?>, <?echo Utils::formatColorToHex($background->color1);?>, <?echo Utils::formatColorToHex($background->color2);?>);
        <?else: ?>
            background: <? echo Utils::formatColorToHex($background->color1); ?>;
        <?endif;?>
        }
    </style>
<? endif; ?>




