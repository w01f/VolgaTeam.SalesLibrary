<?
	/** @var $contentBlocks \application\models\shortcuts\models\landing_page\regular_markup\ContentBlock[] */
	foreach ($contentBlocks as $contentBlock)
	{
		$viewName = $contentBlock->getViewName();
		echo $this->renderPartial('landingPageMarkup/' . $viewName, array('contentBlock' => $contentBlock), true);
	}
?>
