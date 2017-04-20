<?
	/** @var $border BorderStyle */

	use application\models\shortcuts\models\landing_page\regular_markup\common\BorderStyle;
?>
<? if (isset($border)): ?>
    border-top: <? echo $border->size->top; ?>px <? echo '#' . $border->color; ?> solid !important;
    border-left: <? echo $border->size->left; ?>px <? echo '#' . $border->color; ?> solid !important;
    border-bottom: <? echo $border->size->bottom; ?>px <? echo '#' . $border->color; ?> solid !important;
    border-right: <? echo $border->size->right; ?>px <? echo '#' . $border->color; ?> solid !important;
<? endif; ?>

