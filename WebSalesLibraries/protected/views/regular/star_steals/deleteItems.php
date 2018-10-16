<?
	/**
	 * @var $items \application\models\star_steals\models\ItemListModel
	 */
?>
<div class="tool-dialog logger-form" data-log-group="StarsSteals" data-log-action="StarsSteals Activity">
    <legend>Select items you want to delete</legend>
    <div class="buttons-area" style="padding-top: 5px !important; margin-bottom: 10px;">
        <button id="delete-items-select-all" class="btn btn-default log-action" type="button" style="width: 130px;">
            Select All
        </button>
        <button id="delete-items-clear-all" class="btn btn-default log-action" type="button" style="width: 130px;">Clear
            All
        </button>
    </div>
    <div class="delete-items-list" style="height: 246px;">
        <ul class="nav nav-pills nav-stacked">
			<? foreach ($items as $item): ?>
                <li>
                    <div class="checkbox">
                        <label> <input class="delete-items-item log-action" type="checkbox"
                                       value="<? echo $item->id; ?>">
							<? echo $item->title; ?>
                        </label>
                    </div>
                </li>
			<? endforeach; ?>
        </ul>
    </div>
    <div class="buttons-area">
        <button class="btn btn-default log-action accept-button" type="button" style="width: 130px;">Delete Selected
        </button>
        <button class="btn btn-default log-action cancel-button" type="button" style="width: 130px;">Cancel</button>
    </div>
</div>