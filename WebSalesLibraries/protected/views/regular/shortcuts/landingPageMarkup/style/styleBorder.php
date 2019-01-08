<?
	use application\models\shortcuts\models\landing_page\regular_markup\style\BorderStyle;

	/**
	 * @var $blockId string
	 * @var $border BorderStyle
	 */
?>
<? if (isset($border)): ?>
    <style>
        <?echo '#'.$blockId;?> {
            border-top: <? echo $border->size->top; ?>px <? echo Utils::formatColor($border->color); ?> <? echo $border->style;?>;
            border-left: <? echo $border->size->left; ?>px <? echo Utils::formatColor($border->color); ?> <? echo $border->style;?>;
            border-bottom: <? echo $border->size->bottom; ?>px <? echo Utils::formatColor($border->color); ?> <? echo $border->style;?>;
            border-right: <? echo $border->size->right; ?>px <? echo Utils::formatColor($border->color); ?> <? echo $border->style;?>;
        }
        <?if($border->hideCondition->isConfigured):?>
            <?if($border->hideCondition->extraSmall):?>
                @media (max-width: 767px) {
                    <?echo '#'.$blockId;?> {
                        border: none !important;
                    }
                }
            <? endif; ?>
            <?if($border->hideCondition->small):?>
                @media (min-width: 768px) and (max-width: 991px) {
                    <?echo '#'.$blockId;?> {
                        border: none !important;
                    }
                }
            <? endif; ?>
            <?if($border->hideCondition->medium):?>
                @media (min-width: 992px) and (max-width: 1199px) {
                    <?echo '#'.$blockId;?> {
                        border: none !important;
                    }
                }
            <? endif; ?>
            <?if($border->hideCondition->large):?>
                @media (min-width: 1200px) {
                    <?echo '#'.$blockId;?> {
                        border: none !important;
                    }
                }
            <? endif; ?>
        <? endif; ?>
    </style>
<? endif; ?>




