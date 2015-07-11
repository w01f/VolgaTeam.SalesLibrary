<?
	/**
	 * @var $folders FavoritesFolder[]
	 * @var $links array
	 * @var $topLevel boolean
	 */
?>
<div data-role="collapsibleset" data-theme="a" data-content-theme="a" data-inset="<? echo !$topLevel; ?>">
	<? foreach ($folders as $folder): ?>
		<div class="folder" data-role="collapsible">
			<h3>
				<span style="float: left; line-height: 1.8em;"><? echo $folder->name; ?></span>
				<a style="float: right" class="delete-button" href="#" data-role="button" data-inline="true" data-iconpos="notext" data-theme="a" data-icon="delete"></a>
			</h3>
			<div class="folder-content"></div>
			<div class="service-data">
				<div class="folder-id"><? echo $folder->id; ?></div>
			</div>
		</div>
	<? endforeach; ?>
	<? foreach ($links as $link): ?>
		<div class="link" data-role="collapsible" data-collapsed-icon="none" data-expanded-icon="none">
			<h3>
				<span <? if ($topLevel): ?> class="top-level"<? endif; ?> style="float: left; line-height: 1.8em;">
							<?
								if (isset($link['name']) && $link['name'] != '')
									echo $link['name'];
								elseif (isset($link['file_name']) && $link['file_name'] != '')
									echo $link['file_name'];
							?>
				</span>
				<a style="float: right" class="delete-button" href="#" data-role="button" data-inline="true" data-iconpos="notext" data-theme="a" data-icon="delete"></a>
			</h3>
			<div class="service-data">
				<div class="link-id"><? echo $link['id']; ?></div>
			</div>
		</div>
	<? endforeach; ?>
</div>