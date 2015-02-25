<? /** @var $page PageModel */ ?>
<? foreach ($page->links as $link): ?>
	<? echo $this->renderPartial($link->viewPath, array('link' => $link), true); ?>
<? endforeach; ?>
