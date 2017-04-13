<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\SearchFeedBlock */
?>

<? echo $this->renderPartial('../link_feed/searchFeedContainer', array(
	'feedId' => $contentBlock->id,
	'feedSettings' => $contentBlock->settings,
	'feedItems' => $contentBlock->getFeedItems(),
), true); ?>
