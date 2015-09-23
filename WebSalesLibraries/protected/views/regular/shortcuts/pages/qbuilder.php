<?
	/**
	 * @var $shortcut QBuilderShortcut
	 * @var $pages QPageRecord[]
	 * @var $selectedPage QPageRecord
	 * @var $links array
	 */
?>
<div id="main-page">
	<div id="service-panel" class="left">
		<div class="row headers">
			<div class="col-xs-6">
				<button class="btn btn-default btn-block selected" type="button">
					My Sites
					<div class="service-data">
						<div class="tab-id">#page-list</div>
					</div>
				</button>
			</div>
			<div class="col-xs-6">
				<button class="btn btn-default btn-block" type="button">
					Link Cart
					<div class="service-data">
						<div class="tab-id">#link-cart</div>
					</div>
				</button>
			</div>
		</div>
		<div id="page-list" class="row service-panel-page">
			<div class="col-xs-12">
				<div id="page-list-buttons">
					<button type="button" class="btn btn-default btn-block" id="page-list-clear">Clean up my quickSITES</button>
				</div>
				<div id="page-list-container">
					<? $this->renderPartial('../qbuilder/pageList', array('pages' => $pages, 'selectedPage' => $selectedPage), false, true); ?>
				</div>
			</div>
		</div>
		<div id="link-cart" class="row service-panel-page" style="display: none;">
			<div class="col-xs-12">
				<div id="link-cart-buttons">
					<button type="button" class="btn btn-default btn-block" id="link-cart-refresh">Refresh Link Cart</button>
					<button type="button" class="btn btn-default btn-block" id="link-cart-clear">Empty Link Cart</button>
					<button type="button" class="btn btn-default btn-block" id="link-cart-add-all">Add All Links to quickSITE</button>
				</div>
				<div id="link-cart-grid" class="link-grid-container">
					<? $this->renderPartial('../qbuilder/linkCart', array('links' => $links), false, true); ?>
				</div>
			</div>
		</div>
	</div>
	<div id="page-content"></div>
</div>