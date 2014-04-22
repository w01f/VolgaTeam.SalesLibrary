<?
	$searchBar = $pageRecord->getSearchBar();
	$homeBar = $pageRecord->getHomeBar();
	echo $this->renderPartial('homeBar', array('homeBar' => $homeBar, 'enableSearchBar' => $searchBar->configured), true);
	echo $this->renderPartial('searchBar', array('searchBar' => $searchBar, 'pageId' => $pageRecord->id), true);
?>
<div class="shortcuts-page-content" id="shortcuts-page-content-<? echo $pageRecord->id_tab; ?>">
	<? if (isset($linkRecords) && count($linkRecords) > 0): ?>
		<div class="shortcuts-links-container">
			<ul class="shortcuts-links">
				<? foreach ($linkRecords as $linkRecord): ?>
					<?
					$linkContent = '';
					switch ($linkRecord->type)
					{
						case 'mp4':
							$linkContent = $this->renderPartial('videoLink', array('link' => $linkRecord->GetModel()), true);
							break;
						case 'url':
						case 'file':
						case 'window':
						case 'quicklist':
						case 'search':
							$linkContent = $this->renderPartial('directLink', array('link' => $linkRecord->GetModel()), true);
							break;
						case 'page':
							$linkContent = $this->renderPartial('pageLink', array('link' => $linkRecord->GetModel()), true);
							break;
						case 'libraryfile':
							$linkContent = $this->renderPartial('libraryFileLink', array('link' => $linkRecord->GetModel()), true);
							break;
						case 'none':
							$linkContent = $this->renderPartial('emptyLink', array('link' => $linkRecord->GetModel()), true);
							break;
					}
					?>
					<li class="cbp-item identity cbp-l-grid-masonry-height4">
						<? echo $linkContent; ?>
					</li>
				<? endforeach; ?>
			</ul>
		</div>
	<? endif; ?>
</div>
