<?php if (isset($searchInfo)): ?>
<div id="search-grid-info">
	<table>
		<tr>
			<td id="search-links-info-count">
				<span><?php echo $searchInfo['count']; ?></span>
			</td>
		</tr>
		<tr>
			<td>
				<?php if (array_key_exists('condition', $searchInfo)): ?>
				<span><?php echo $searchInfo['condition']; ?>; </span>
				<?php endif; ?>
				<?php if (array_key_exists('file_types', $searchInfo)): ?>
				<span><?php echo $searchInfo['file_types']; ?>; </span>
				<?php endif; ?>
				<?php if (array_key_exists('dates', $searchInfo)): ?>
				<span><?php echo $searchInfo['dates']; ?>; </span>
				<?php endif; ?>
			</td>
		</tr>
		<?php if (array_key_exists('categories', $searchInfo)): ?>
		<tr>
			<td>
				<span><?php echo $searchInfo['categories']; ?></span>
			</td>
		</tr>
		<?php endif; ?>
		<?php if (array_key_exists('libraries', $searchInfo)): ?>
		<tr>
			<td>
				<span><?php echo $searchInfo['libraries']; ?></span>
			</td>
		</tr>
		<?php endif; ?>
	</table>
</div>
<?php endif; ?>
<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/linksGrid.php', array('links' => isset($links) ? $links : null), true); ?>
