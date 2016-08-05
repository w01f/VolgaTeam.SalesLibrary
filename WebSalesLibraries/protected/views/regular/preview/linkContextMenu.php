<?
	/**
	 * @var $data PreviewData
	 */
?>
<ul class="dropdown-menu context-menu-content logger-form" role="menu" data-log-group="Link" data-log-action="Context Menu">
	<? foreach ($data->contextActions as $action): ?>
		<li>
			<a tabindex="-1" href="#" class="log-action">
				<? echo $action->text; ?>
				<div class="service-data">
					<div class="tag"><? echo $action->tag; ?></div>
				</div>
			</a>
		</li>
	<? endforeach; ?>
</ul>