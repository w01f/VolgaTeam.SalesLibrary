<?
	/** @var $page PageModel    * */
	echo $this->renderPartial('searchBar', array('searchBar' => $page->searchBar, 'pageId' => $page->id), true);
?>
<div class="shortcuts-page-content" id="shortcuts-page-content-<? echo $page->idTab; ?>">
	<? echo $this->renderPartial($page->viewPath . '/container', array('page' => $page), true); ?>
</div>
