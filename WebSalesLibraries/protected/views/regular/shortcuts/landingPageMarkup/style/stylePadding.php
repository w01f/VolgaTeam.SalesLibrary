<?
	/** @var $padding Padding */
?>
<? if ($padding->isConfigured): ?>
    padding-top: <? echo $padding->top; ?>px !important;
    padding-left: <? echo $padding->left; ?>px !important;
    padding-bottom: <? echo $padding->bottom; ?>px !important;
    padding-right: <? echo $padding->right; ?>px !important;
<? endif; ?>

