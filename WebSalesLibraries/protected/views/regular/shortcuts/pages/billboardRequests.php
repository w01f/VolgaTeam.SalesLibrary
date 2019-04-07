<?
	/**
	 * @var $shortcut BillboardRequestsShortcut
	 */

	$showAllItems = !empty($shortcut->selectedItemId);
?>
<div class="billboard-requests-main-page">
    <div class="service-panel left logger-form" data-log-group="Billboard Request" data-log-action="Billboard Request Activity">
        <div class="row service-panel-page item-list">
            <div class="col-xs-12">
                <div class="row item-list-buttons">
                    <div class="button col col-xs-4 text-center">
                        <a class="item-list-add" href="#" target="_self"><span style="color: blue;">new</span></a>
                    </div>
                    <div class="button col col-xs-4 text-center">
                        <a class="item-list-save" href="#" target="_self"><span style="color: blue;">save</span></a>
                    </div>
                    <div class="button col col-xs-4 text-center">
                        <a class="item-list-submit" href="#" target="_self"><span
                                    style="color: green;">submit</span></a>
                    </div>
                </div>
                <div class="row item-list-tabs" data-role="tab">
                    <ul class="nav nav-tabs">
                        <li <?if(!$showAllItems):?>class="active"<?endif;?>><a data-toggle="tab" href="#billboard-requests-item-list-own">my requests</a></li>
                        <li <?if($showAllItems):?>class="active"<?endif;?>><a data-toggle="tab" href="#billboard-requests-item-list-all">all requests</a></li>
                        <li><a data-toggle="tab" href="#billboard-requests-item-list-archive">archives</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane <? if (!$showAllItems): ?>active<? endif; ?>" id="billboard-requests-item-list-own"></div>
                        <div class="tab-pane <? if ($showAllItems): ?>active<? endif; ?>" id="billboard-requests-item-list-all"></div>
                        <div class="tab-pane" id="billboard-requests-item-list-archive"></div>
                    </div>
                </div>
                <div class="row item-list-container logger-form" data-log-group="StarsSteals"
                     data-log-action="Billboard Request Activity">
                </div>
            </div>
        </div>
    </div>
    <div class="content-panel">
        <div class="item-content logger-form" data-log-group="Item" data-log-action="Billboard Request Activity"></div>
        <div class="content-buttons row" style="display: none">
            <div class="col-md-6 text-left">
                <span style="color: red; line-height: 35px">*REMEMBER TO SAVE YOUR CHANGES</span>
            </div>
            <div class="col-md-3 col-sm-6">
                <button type="button" class="btn btn-default btn-block item-save log-action">Save</button>
            </div>
            <div class="col-md-3 col-sm-6">
                <button type="button" class="btn btn-default btn-block item-submit log-action">Submit</button>
            </div>
        </div>
    </div>
</div>