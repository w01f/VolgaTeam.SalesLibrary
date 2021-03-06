<?
	use application\models\feeds\common\ControlsStyle;

	/**
	 * @var $feedId string
	 * @var $style ControlsStyle
	 */
?>

<style>
	<? echo '#'.$feedId; ?> .feed-controls-container .btn.btn-default
    {
	<?if (!empty($style->regularBackColor)):?>
		background-color: <? echo Utils::formatColorToHex($style->regularBackColor);?> !important;
	<?endif;?>
	<?if (!empty($style->regularTextColor)):?>
		color: <? echo Utils::formatColorToHex($style->regularTextColor);?> !important;
	<?endif;?>
    <?if (!empty($style->borderColor)):?>
        border-color: <? echo Utils::formatColorToHex($style->borderColor);?> !important;
    <?endif;?>
		-webkit-box-shadow: none !important;
		box-shadow: none !important;
        outline: none !important;
	}

	<? echo '#'.$feedId; ?> .feed-controls-container .btn.btn-default.active,
	<? echo '#'.$feedId; ?> .feed-controls-container .btn.btn-default.active:focus,
	<? echo '#'.$feedId; ?> .feed-controls-container .btn.btn-default.active:hover,
	<? echo '#'.$feedId; ?> .feed-controls-container .btn.btn-default.active:focus:hover,
	<? echo '#'.$feedId; ?> .carousel-controls-container .btn.btn-default,
	<? echo '#'.$feedId; ?> .carousel-controls-container .btn.btn-default:hover,
	<? echo '#'.$feedId; ?> .carousel-controls-container .btn.btn-default:focus,
	<? echo '#'.$feedId; ?> .carousel-controls-container .btn.btn-default:focus:hover
    {
	<?if (!empty($style->activeBackColor)):?>
		background-color: <? echo Utils::formatColorToHex($style->activeBackColor);?> !important;
	<?endif;?>
	<?if (!empty($style->activeBackColor)):?>
		color: <? echo Utils::formatColorToHex($style->activeTextColor);?> !important;
	<?endif;?>
    <?if (!empty($style->borderColor)):?>
        border-color: <? echo Utils::formatColorToHex($style->borderColor);?> !important;
    <?endif;?>
		-webkit-box-shadow: none !important;
		box-shadow: none !important;
        outline: none !important;
	}

    <? echo '#'.$feedId; ?> .feed-controls-container .btn,
    <? echo '#'.$feedId; ?> .carousel-controls-container .btn
    {
        line-height: <? echo $style->lineHeight;?>px !important;
        height: <? echo ($style->lineHeight+14);?>px !important;
    }
</style>

