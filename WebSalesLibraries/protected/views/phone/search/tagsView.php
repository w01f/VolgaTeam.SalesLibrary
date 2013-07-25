<?php if (isset($categories) && Yii::app()->params['tags']['visible']): ?>
	<a id="search-tags-clear-button" href="#" data-role="button" data-mini="true" data-icon="delete" data-theme="b">Clear Tags</a>
	<?php if (isset($categories->superFilters)): ?>
		<fieldset id="super-filter-list" data-role="controlgroup">
			<?$i = 0;?>
			<?php foreach ($categories->superFilters as $superFilter): ?>
				<input type="checkbox" name="super-filter-item<? echo $i; ?>" id="super-filter-item<? echo $i; ?>" class="custom super-filter-item" <? if ($superFilter->selected): ?>checked<? endif;?> value="<? echo $superFilter->value; ?>"/>
				<label for="super-filter-item<? echo $i; ?>"><? echo $superFilter->value;?></label>
				<? $i++; ?>
			<?php endforeach; ?>
		</fieldset>
	<?php endif; ?>
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
