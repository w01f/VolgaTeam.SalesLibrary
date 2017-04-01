<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\TrendingBlock */
?>

<? echo $this->renderPartial('../trending/bar', array(
	'barId' => $contentBlock->id,
	'trendingSettings' => $contentBlock->settings,
	'trendingLinks' => $contentBlock->getTrendingLinks(),
), true); ?>
