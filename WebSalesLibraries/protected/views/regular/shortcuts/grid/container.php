<? /** @var $page PageModel * */ ?>
<? if (count($page->links) > 0): ?>
	<div class="shortcuts-links-grid">
		<ul class="shortcuts-links">
			<? foreach ($page->links as $link): ?>
				<li class="cbp-item identity cbp-l-grid-masonry-height4">
					<? echo $this->renderPartial($page->viewPath . '/' . $link->viewPath, array('link' => $link), true); ?>
				</li>
			<? endforeach; ?>
		</ul>
	</div>
<? endif; ?>