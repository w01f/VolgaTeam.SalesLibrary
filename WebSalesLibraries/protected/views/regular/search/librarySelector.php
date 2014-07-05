<?
	/**
	 * @var $libraries Library[]
	 */
?>
<table class="library-checkbox-list-buttons">
	<tr>
		<td class="left"><input type="submit" id="library-select-all" value="Select All"/></td>
		<td class="right"><input type="submit" id="library-clear-all" value="Clear All"/></td>
	</tr>
</table>
<ul id="library-checkbox-list">
	<?
		foreach ($libraries as $library)
		{
			echo CHtml::openTag('li');
			echo CHtml::checkBox($library['id'], $library['selected']);
			echo CHtml::label($library['name'], $library['id']);
			echo CHtml::closeTag('li');
		}
	?>
</ul>
<table class="library-checkbox-list-buttons">
	<tr>
		<td class="left"><input type="submit" id="library-select-save" value="Apply"/></td>
		<td class="right"><input type="submit" id="library-select-cancel" value="Cancel"/></td>
	</tr>
</table>        

