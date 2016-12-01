<?
	/** @var $shortcut GridBundleShortcut */
?>
<? if ($shortcut->searchBar->configured): ?>
	<style>
		#content .shortcuts-search-bar-container
		{
			width: 100%;
			padding: 20px 20px 20px;
		}
	</style>
	<div class="shortcuts-search-bar-container">
		<? echo $this->renderPartial('searchBar/bar', array('searchBar' => $shortcut->searchBar), true);	?>
	</div>
<? endif; ?>
<div class="shortcuts-page-content">
	<? if (count($shortcut->links) > 0): ?>
		<div class="shortcuts-links-grid">
			<ul class="shortcuts-links">
				<? foreach ($shortcut->links as $link): ?>
					<li class="cbp-item identity cbp-l-grid-masonry-height4">
						<a class="cbp-caption shortcuts-link" href="<? echo $link->getSourceLink(); ?>" target="_blank">
							<div class="cbp-caption-defaultWrap">
								<img src="<? echo $link->imageContent; ?>" alt="" width="100%">
							</div>
							<div class="cbp-caption-activeWrap">
								<div class="cbp-l-caption-alignCenter">
									<div class="cbp-l-caption-body">
										<div class="cbp-l-caption-title"><? echo $link->title; ?></div>
										<div class="cbp-l-caption-desc"><? echo $link->description; ?></div>
									</div>
								</div>
							</div>
							<div class="service-data">
								<? echo $link->getMenuItemData(); ?>
							</div>
						</a>
					</li>
				<? endforeach; ?>
			</ul>
		</div>
	<? endif; ?>
</div>