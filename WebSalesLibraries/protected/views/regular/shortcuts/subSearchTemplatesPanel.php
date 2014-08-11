<?
	/**
	 * @var $templates SubSearchTemplate[]
	 * @var $id string
	 */
?>
<div>
	<div class="logo-list">
		<ul class="nav nav-pills">
			<? foreach ($templates as $template): ?>
				<li>
					<a href="#"><img src="<? echo $template->imagePath . '?' . $id; ?>" alt="<? echo $template->tooltip; ?>" data-toggle="tooltip" title="<? echo $template->tooltip; ?>">
						<div class="search-conditions" style="display: none;">
							<? if (isset($template->conditions->text)): ?>
								<div class="search-text"><? echo $template->conditions->text; ?></div>
							<? endif; ?>
							<? if (isset($template->conditions->startDate)): ?>
								<div class="start-date"><? echo $template->conditions->startDate; ?></div>
							<? endif; ?>
							<? if (isset($template->conditions->endDate)): ?>
								<div class="end-date"><? echo $template->conditions->endDate; ?></div>
							<? endif; ?>
							<? if ($template->conditions->dateModified): ?>
								<div class="use-file-date">true</div>
							<? endif; ?>
							<? if (isset($template->conditions->fileTypes)): ?>
								<? foreach ($template->conditions->fileTypes as $fileType): ?>
									<div class="file-type"><? echo $fileType; ?></div>
								<? endforeach; ?>
							<? endif; ?>
							<? if (isset($template->conditions->libraries)): ?>
								<? foreach ($template->conditions->libraries as $library): ?>
									<div class="library"><? echo $library; ?></div>
								<? endforeach; ?>
							<? endif; ?>
							<? if (isset($template->conditions->superFilters)): ?>
								<? foreach ($template->conditions->superFilters as $superFilter): ?>
									<div class="super-filter"><? echo $superFilter; ?></div>
								<? endforeach; ?>
							<? endif; ?>
							<? if (isset($template->conditions->categories)): ?>
								<? foreach ($template->conditions->categories as $category): ?>
									<div class="category"><? echo $category->category; ?>------<? echo $category->tag; ?></div>
								<? endforeach; ?>
							<? endif; ?>
							<? if ($template->conditions->onlyWithCategories): ?>
								<div class="only-with-categories">true</div>
							<? endif; ?>
							<? if ($template->conditions->hideDuplicated): ?>
								<div class="hide-duplicated">true</div>
							<? endif; ?>
							<? if ($template->conditions->searchByName): ?>
								<div class="search-by-name">true</div>
							<? endif; ?>
							<? if ($template->conditions->searchByContent): ?>
								<div class="search-by-content">true</div>
							<? endif; ?>
							<? if (isset($template->conditions->sortColumn)): ?>
								<div class="sort-column"><? echo $template->conditions->sortColumn; ?></div>
							<? endif; ?>
							<? if (isset($template->conditions->sortDirection)): ?>
								<div class="sort-direction"><? echo $template->conditions->sortDirection; ?></div>
							<? endif; ?>
						</div>
					</a>
				</li>
			<? endforeach; ?>
		</ul>
	</div>
</div>