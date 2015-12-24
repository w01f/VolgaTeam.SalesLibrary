<?
	/** @var $shortcut WallbinShortcut */
	$library = $shortcut->library;

	$savedSelectedPageIdTag = sprintf('SelectedLibraryPageId-%s', $library->id);
	if (isset(Yii::app()->request->cookies[$savedSelectedPageIdTag]))
	{
		$selectedPageId = Yii::app()->request->cookies[$savedSelectedPageIdTag]->value;
		foreach ($library->pages as $page)
			if ($page->id == $selectedPageId)
			{
				$selectedPage = $page;
				break;
			}
	}
	if (!isset($selectedPage))
		$selectedPage = $library->pages[0];
?>
<div class="wallbin-header">
	<div class="wallbin-logo-wrapper">
		<img class="wallbin-logo" src="<? echo $selectedPage->logoContent; ?>">
	</div>
	<div class="page-selector-container">
		<? if ($shortcut->pageSelectorMode == 'tabs'): ?>
			<div class="tab-pages scroll_tabs_theme_light">
				<? foreach ($library->pages as $page): ?>
					<span class="page-tab-header<? echo $selectedPage->id == $page->id ? ' selected' : ''; ?>">
					<? echo $page->name; ?>
						<div class="service-data">
							<div class="encoded-data">
								<? echo CJSON::encode(array(
										'id' => $page->id,
										'name' => $page->name,
										'logoContent' => $page->logoContent
									)
								);?>
							</div>
						</div>
				</span>
				<? endforeach; ?>
			</div>
		<? else: ?>
			<select class="selectpicker bootstrapped">
				<? foreach ($library->pages as $page): ?>
					<option value='<? echo base64_encode(CJSON::encode(array(
							'id' => $page->id,
							'name' => $page->name,
							'logoContent' => $page->logoContent
						)
					));?>' <? echo $selectedPage->id == $page->id ? 'selected' : ''; ?>><? echo $page->name; ?></option>
				<? endforeach; ?>
			</select>
		<?endif; ?>
	</div>
</div>
<div class="wallbin-container">
	<?
		if ($shortcut->pageViewType == 'accordion')
		{
			$selectedPage->loadData();
			$this->renderPartial('../wallbin/accordionView', array('libraryPage' => $selectedPage));
		}
		else
			echo $selectedPage->getCache();
	?>
</div>