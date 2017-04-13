<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\SpecificLinkFeedBlock */
?>

<? echo $this->renderPartial('../link_feed/specificLinkFeedContainer', array(
	'feedId' => $contentBlock->id,
	'feedSettings' => $contentBlock->settings,
	'feedItems' => $contentBlock->getFeedItems(),
), true); ?>
