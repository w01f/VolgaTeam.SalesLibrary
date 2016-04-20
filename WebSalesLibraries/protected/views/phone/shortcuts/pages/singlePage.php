<?
	/**
	 * @var $shortcut PageContentShortcut
	 */

	$this->renderPartial('../site/scripts');
	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/shortcuts-single-page.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<script type="text/javascript">
	$(document).ready(function ()
	{
		$.SalesPortal.ShortcutsSinglePage.init();
	});
</script>
<div class="service-data default-shortcut-data">
	<? echo $shortcut->getMenuItemData(); ?>
</div>
