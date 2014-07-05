<table id="search-container">
	<tr>
		<?
			$sideBarVisible = true;
			if (isset(Yii::app()->request->cookies['sideBarVisible']->value))
				$sideBarVisible = Yii::app()->request->cookies['sideBarVisible']->value == "true";
		?>
		<td id="right-navbar" <? if (!$sideBarVisible): ?>style="display: none;"<? endif; ?>>
			<div><?php $this->widget('application.components.widgets.SearchControlPanel', array()); ?></div>
		</td>
		<td id="search-result">
			<div><?php $this->renderPartial('searchResult', array('links' => null), false, true); ?></div>
		</td>
	</tr>
</table>
