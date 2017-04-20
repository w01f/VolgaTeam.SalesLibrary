<?
	/** @var $margin Padding */
?>
<? if ($margin->isConfigured): ?>
    margin-top: <? echo $margin->top; ?>px !important;
    margin-left: <? echo $margin->left; ?>px !important;
    margin-bottom: <? echo $margin->bottom; ?>px !important;
    margin-right: <? echo $margin->right; ?>px !important;
<? endif; ?>

