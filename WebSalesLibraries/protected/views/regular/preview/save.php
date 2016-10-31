<?
	/**
	 * @var $data PreviewData
	 * */
	$currentColumn = 0
?>
<div class="row action-container">
	<? for ($actionIndex = 0; $actionIndex < count($data->dialogActions); $actionIndex += 2): ?>
		<div class="row">
			<div class="col col-xs-6 text-center">
				<button type="button" class="btn btn-default action log-action"
				        data-log-action="<? echo $data->dialogActions[$actionIndex]->tag; ?>">
					<img class="action-logo" src="<? echo $data->dialogActions[$actionIndex]->logo; ?>">
					<span class="service-data">
						<span class="tag"><? echo $data->dialogActions[$actionIndex]->tag; ?></span>
					</span>
				</button>
			</div>
			<div class="col col-xs-6 text-center">
				<? if (($actionIndex + 1) < count($data->dialogActions)): ?>
					<button type="button" class="btn btn-default action log-action"
					        data-log-action="<? echo $data->dialogActions[$actionIndex + 1]->tag; ?>">
						<img class="action-logo" src="<? echo $data->dialogActions[$actionIndex + 1]->logo; ?>">
					<span class="service-data">
						<span class="tag"><? echo $data->dialogActions[$actionIndex + 1]->tag; ?></span>
					</span>
					</button>
				<? endif; ?>
			</div>
		</div>
	<? endfor; ?>
</div>