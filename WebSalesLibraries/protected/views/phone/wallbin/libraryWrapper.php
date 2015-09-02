<?
	/**
	 * @var $library Library
	 * @var $defaultPage LibraryPage
	 * @var $tabPageExisted boolean
	 */

	$this->renderPartial('../site/scripts');
?>
	<script type="text/javascript">
		$(document).ready(function ()
		{
			$.SalesPortal.Wallbin.init();
		});
	</script>
<? $this->renderPartial('../wallbin/libraryContent', array('library' => $library, 'defaultPage' => $defaultPage, 'tabPageExisted' => $tabPageExisted)); ?>