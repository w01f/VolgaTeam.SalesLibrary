<table id="main-page">
	<tr>
		<td id="page-list">
			<div id="page-list-buttons">
				<button type="button" class="btn btn-block" id="page-list-clear">Clean up my quickSITES</button>
			</div>
			<div id="page-list-container">
				<?php $this->renderPartial('pageList', array('pages' => $pages, 'selectedPage' => $selectedPage), false, true); ?>
			</div>
		</td>
		<td id="page-content">
			<?php $this->renderPartial('pageContent', array('page' => $selectedPage, 'logos' => $logos), false, true); ?>
		</td>
		<td id="link-cart">
			<div id="link-cart-buttons">
				<button type="button" class="btn btn-block" id="link-cart-refresh">Refresh Link Cart</button>
				<button type="button" class="btn btn-block" id="link-cart-clear">Empty Link Cart</button>
				<button type="button" class="btn btn-block" id="link-cart-add-all">Add All Links to quickSITE</button>
			</div>
			<div id="link-cart-grid" class="link-grid-container">
				<?php $this->renderPartial('linkCart', array('links' => $links), false, true); ?>
			</div>
		</td>
	</tr>
</table>