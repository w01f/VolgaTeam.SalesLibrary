<?
	/**
	 * @var $data PreviewData
	 * */
?>
<div class="row action-container">
	<? foreach ($data->actions as $action): ?>
		<div class="action text-button">
			<img class="action-logo" src="<? echo $action->logo; ?>">
			<span class="action-text"><strong><? echo $action->text; ?></strong></span>
			<div class="service-data" style="display: none">
				<div class="tag"><? echo $action->tag; ?></div>
			</div>
		</div>
	<? endforeach; ?>
</div>