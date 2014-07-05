<?
	/** @var $selectedPage LibraryPage */
	$page = $selectedPage->getCache();
	if (isset(Yii::app()->browser) && Yii::app()->browser->isMobile())
		$page = str_replace("title=", "t1=", $page);
	echo $page;
