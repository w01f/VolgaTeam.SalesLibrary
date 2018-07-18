<?
	/**
	 * @var $data PreviewData
	 */
?>
<ul class="dropdown-menu context-menu-content logger-form" role="menu" data-log-group="Link"
    data-log-action="Context Menu">
	<? foreach ($data->contextActions as $action): ?>
		<? $isDirectUrl = $action->url != '#'; ?>
		<? if ($action->beginGroup): ?>
            <li role="separator" class="divider"></li>
		<? endif; ?>
        <li <? if ($action->onlyWallbinAction): ?>class="only-on-wallbin" <? endif; ?>>
            <a tabindex="-1" href="<? echo $action->url; ?>"
               class="<? echo $isDirectUrl ? 'direct-open' : 'regular-open'; ?> log-action"
			   <? if ($isDirectUrl): ?>target="_blank"<? endif; ?>>
				<? echo $action->text; ?>
                <div class="service-data">
                    <div class="tag"><? echo $action->tag; ?></div>
                </div>
            </a>
        </li>
	<? endforeach; ?>
</ul>