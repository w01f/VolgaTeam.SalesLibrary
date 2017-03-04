<?
	use application\models\wallbin\models\web\Library as Library;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/**
	 * @var $library Library
	 * @var $pageSelectorMode string
	 * @var $pageViewType string
	 * @var $style \application\models\wallbin\models\web\style\WallbinStyle
	 * @var $searchBar SearchBar
	 */

	/** @var LibraryPage $selectedPage */
	$selectedPage = null;
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
<style>
	<?if($style->header->paddingLeft>0):?>
	#content .wallbin-header-container {
		padding-left: <? echo $style->header->paddingLeft;?>px;
	}

	#content .wallbin-header .wallbin-header-cell {
		padding-left: 0;
	}
	<?endif;?>

	#content .wallbin-header .wallbin-header-cell {
		border-bottom: 1px <? echo '#'.$style->header->headerBorderColor?> solid !important;
	}

	<? if (!($searchBar->configured && $style->header->showLogo)): ?>
	 #content .wallbin-header .page-selector-container {
		 padding-top: 25px;
	 }
	<? endif; ?>

	<? if ($searchBar->configured): ?>
	#content .wallbin-header .shortcuts-search-bar-container {
		width: 100%;
		padding-right: 10px;
		padding-top: 25px;
		padding-bottom: 0;
		vertical-align: top;
	}
	<? endif; ?>

	<? if ($style->header->showLogo): ?>
	#content .wallbin-header .shortcuts-search-bar-container {
		padding-left: 20px;
	}
	<? endif; ?>
</style>
<div id="library-update-stamp">
	<span
		class="text">Updated: <? echo date(Yii::app()->params['outputDateFormat'], strtotime($library->lastUpdate)); ?></span>
</div>
<div class="wallbin-header-container">
	<table class="wallbin-header">
		<? if ($searchBar->configured): ?>
			<tr>
				<? if ($style->header->showLogo): ?>
					<td class="wallbin-logo-wrapper">
						<img class="wallbin-logo" src="<? echo $selectedPage->logoContent; ?>">
					</td>
				<? endif; ?>
				<td class="shortcuts-search-bar-container">
					<? echo $this->renderPartial('../shortcuts/searchBar/bar', array('searchBar' => $searchBar), true); ?>
				</td>
			</tr>
		<? endif; ?>
		<tr>
			<? if (!$searchBar->configured && $style->header->showLogo): ?>
				<td class="wallbin-header-cell wallbin-logo-wrapper">
					<img class="wallbin-logo" src="<? echo $selectedPage->logoContent; ?>">
				</td>
			<? endif; ?>
			<td class="wallbin-header-cell page-selector-container<? if (!$style->header->showLogo): ?> page-selector-container-no-logo<? endif; ?>"
			    <? if ($searchBar->configured && $style->header->showLogo): ?>colspan="2" <? endif; ?>>
				<? if ($pageSelectorMode == 'tabs'): ?>
					<div class="tab-pages scroll_tabs_theme_light">
						<? foreach ($library->pages as $page): ?>
							<span class="page-tab-header<? echo $selectedPage->id == $page->id ? ' selected' : ''; ?>">
							<?if(isset($page->settings->icon)):?>
								<i class="icomoon <? echo $page->settings->icon;?>" style="color: <? echo $page->settings->iconColor;?>"></i>
							<?endif;?>
							<? echo $page->name; ?>
								<div class="service-data">
									<div class="encoded-data">
										<? echo CJSON::encode(array(
												'id' => $page->id,
												'name' => $page->name,
												'logoContent' => $style->header->showLogo ? $page->logoContent : ''
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
									'logoContent' => $style->header->showLogo ? $page->logoContent : ''
								)
							)); ?>' <? echo $selectedPage->id == $page->id ? 'selected' : ''; ?>><? echo $page->name; ?></option>
						<? endforeach; ?>
					</select>
				<? endif; ?>
			</td>
		</tr>
	</table>
</div>
<div class="wallbin-container">
	<?
		if ($pageViewType == 'accordion')
		{
			$selectedPage->loadData();
			$this->renderPartial('../wallbin/accordionView', array('libraryPage' => $selectedPage));
		}
		else if ($style->page->enabled)
		{
			$selectedPage->loadData();
			$selectedPage->loadFolders(true);
			$this->renderPartial('../wallbin/columnsView',
				array(
					'libraryPage' => $selectedPage,
					'style' => $style->page
				));
		}
		else
			echo $selectedPage->getCache();
	?>
</div>