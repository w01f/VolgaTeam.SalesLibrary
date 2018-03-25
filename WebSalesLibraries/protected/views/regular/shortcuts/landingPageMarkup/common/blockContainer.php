<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * @var $contentBlocks ContentBlock[]
	 * @var $screenSettings array
	 */

	foreach ($contentBlocks as $contentBlock)
	{
		$viewName = $contentBlock->getViewName();
		echo $this->renderPartial('landingPageMarkup/' . $viewName, array('contentBlock' => $contentBlock, 'screenSettings' => $screenSettings), true);
	}
?>
