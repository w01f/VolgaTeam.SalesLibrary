<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\TrendingBlock */
?>

<? echo $this->renderPartial('../link_feed/trendingFeedContainer', array(
	'feedId' => $contentBlock->id,
	'feedSettings' => $contentBlock->settings,
	'feedItems' => $contentBlock->getFeedItems(),
), true); ?>
