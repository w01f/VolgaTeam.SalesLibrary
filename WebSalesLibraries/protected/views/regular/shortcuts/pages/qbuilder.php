<?
	/**
	 * @var $shortcut QBuilderShortcut
	 */
?>
<div id="main-page">
    <div id="service-panel" class="left logger-form" data-log-group="QBuilder" data-log-action="QBuilder Activity">
        <div class="row headers">
            <div class="col-xs-6" style="padding-left: 10px">
                <button class="btn btn-default btn-block log-action<? if (!$shortcut->showLinkCart): ?> selected<? endif; ?>"
                        type="button">
                    My Sites
                    <span class="service-data">
						<span class="tab-id">#page-list</span>
					</span>
                </button>
            </div>
            <div class="col-xs-6" style="padding-right: 10px">
                <button class="btn btn-default btn-block log-action<? if ($shortcut->showLinkCart): ?> selected<? endif; ?>"
                        type="button">
                    Link Cart
                    <span class="service-data">
						<span class="tab-id">#link-cart</span>
					</span>
                </button>
            </div>
        </div>
        <div id="page-list" class="row service-panel-page"<? if ($shortcut->showLinkCart): ?> style="display: none;"<? endif; ?>>
            <div class="col-xs-12">
                <div id="page-list-buttons">
                    <button type="button" class="btn btn-default btn-block log-action" id="page-list-clear">Clean up my
                        quickSITES
                    </button>
                </div>
                <div id="page-list-container" class="logger-form" data-log-group="QBuilder"
                     data-log-action="QBuilder Activity">
                </div>
            </div>
        </div>
        <div id="link-cart" class="row service-panel-page"<? if (!$shortcut->showLinkCart): ?> style="display: none;"<? endif; ?>>
            <div class="col-xs-12">
                <div id="link-cart-buttons">
                    <button type="button" class="btn btn-default btn-block log-action" id="link-cart-refresh">Refresh
                        Link Cart
                    </button>
                    <button type="button" class="btn btn-default btn-block log-action" id="link-cart-clear">Empty Link
                        Cart
                    </button>
                    <button type="button" class="btn btn-default btn-block log-action" id="link-cart-add-new-page">
                        Create Quicksite with these links
                    </button>
                    <button type="button" class="btn btn-default btn-block log-action" id="link-cart-add-all">Add All
                        Links to quickSITE
                    </button>
                </div>
                <div id="link-cart-grid" class="data-table-content-container">
                    <table id="linc-cart-data-table-content" class="table table-striped table-bordered"></table>
                </div>
            </div>
        </div>
    </div>
    <div id="page-content" class="logger-form" data-log-group="Link" data-log-action="QBuilder Activity"></div>
</div>