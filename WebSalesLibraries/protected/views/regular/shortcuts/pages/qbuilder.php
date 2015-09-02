<?
	/**
	 * @var $shortcut QBuilderShortcut
	 * @var $pages QPageRecord[]
	 * @var $selectedPage QPageRecord
	 * @var $links array
	 */
?>
<table id="main-page">
	<tr>
		<td id="page-list">
			<div id="page-list-buttons">
				<button type="button" class="btn btn-default btn-block" id="page-list-clear">Clean up my quickSITES</button>
			</div>
			<div id="page-list-container">
				<? $this->renderPartial('../qbuilder/pageList', array('pages' => $pages, 'selectedPage' => $selectedPage), false, true); ?>
			</div>
		</td>
		<td id="page-content">
		</td>
		<td id="link-cart">
			<div id="link-cart-buttons">
				<button type="button" class="btn btn-default btn-block" id="link-cart-refresh">Refresh Link Cart</button>
				<button type="button" class="btn btn-default btn-block" id="link-cart-clear">Empty Link Cart</button>
				<button type="button" class="btn btn-default btn-block" id="link-cart-add-all">Add All Links to quickSITE</button>
			</div>
			<div id="link-cart-grid" class="link-grid-container">
				<? $this->renderPartial('../qbuilder/linkCart', array('links' => $links), false, true); ?>
			</div>
		</td>
	</tr>
</table>