<?
	use application\models\wallbin\models\web\Library as Library;

	/**
	 * @var $library Library
	 * @var $pageSelectorMode string
	 * @var $pageViewType string
	 * @var $showLogo bool
	 */
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
<div id="library-update-stamp">
	<span
		class="text">Updated: <? echo date(Yii::app()->params['outputDateFormat'], strtotime($library->lastUpdate)); ?></span>
</div>
<div class="wallbin-header">
	<div class="wallbin-logo-wrapper">
		<? if ($showLogo): ?>
			<img class="wallbin-logo" src="<? echo $selectedPage->logoContent; ?>">
		<? endif; ?>
	</div>
	<div class="page-selector-container<? if (!$showLogo): ?> page-selector-container-no-logo<? endif; ?>">
		<? if ($pageSelectorMode == 'tabs'): ?>
			<div class="tab-pages scroll_tabs_theme_light">
				<? foreach ($library->pages as $page): ?>
					<span class="page-tab-header<? echo $selectedPage->id == $page->id ? ' selected' : ''; ?>">
					<? echo $page->name; ?>
						<div class="service-data">
							<div class="encoded-data">
								<? echo CJSON::encode(array(
										'id' => $page->id,
										'name' => $page->name,
										'logoContent' => $showLogo ? $page->logoContent : ''
									)
								); ?>
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
							'logoContent' => $showLogo ? $page->logoContent : ''
						)
					)); ?>' <? echo $selectedPage->id == $page->id ? 'selected' : ''; ?>><? echo $page->name; ?></option>
				<? endforeach; ?>
			</select>
		<? endif; ?>
	</div>
</div>
<div class="wallbin-container">
	<?
		if ($pageViewType == 'accordion')
		{
			$selectedPage->loadData();
			$this->renderPartial('../wallbin/accordionView', array('libraryPage' => $selectedPage));
		}
		else
			echo $selectedPage->getCache();
	?>
</div>