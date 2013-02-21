<?php if (isset($categories) && Yii::app()->params['tags']['visible']): ?>
<table class="layout-group">
	<tr>
		<td class="on-left">
			<a id="search-tags-clear-button" href="#" data-role="button" data-mini="true" data-icon="delete"
			   data-inline="true" data-theme="b">Clear Tags</a>
		</td>
		<td class="on-right">
			<select name="search-tags-exact-match" id="search-tags-exact-match" data-role="slider" data-mini="true"
					data-track-theme="b">
				<option value="true">Exact</option>
				<option value="false">Partial</option>
			</select>
		</td>
	</tr>
</table>
<?php foreach ($categories->groups as $group): ?>
	<div class="search-tags-group" data-role="collapsible" data-collapsed="true" data-inset="false">
		<h3>
			<?php echo $group; ?>
		</h3>
		<fieldset data-role="controlgroup">
			<?php foreach ($categories->getTagsByGroup($group) as $tag): ?>
			<input class="search-tags-item" type="checkbox" name="<?php echo $group . '------' . $tag['tag']; ?>"
				   id="<?php echo $group . '------' . $tag['tag']; ?>" class="custom"/>
			<label for="<?php echo $group . '------' . $tag['tag']; ?>"><?php echo $tag['tag']; ?></label>
			<?php endforeach; ?>
		</fieldset>
	</div>
	<?php endforeach; ?>
<?php endif; ?>
