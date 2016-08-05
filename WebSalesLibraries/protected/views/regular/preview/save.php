<?
	/**
	 * @var $data PreviewData
	 * */
?>
<div class="row action-container">
	<? foreach ($data->dialogActions as $action): ?>
		<div class="action text-button log-action" data-log-action="<? echo $action->tag; ?>">
			<img class="action-logo" src="<? echo $action->logo; ?>">
			<span class="action-text"><strong><? echo $action->text; ?></strong></span>
			<div class="service-data">
				<div class="tag"><? echo $action->tag; ?></div>
			</div>
		</div>
	<? endforeach; ?>
</div>