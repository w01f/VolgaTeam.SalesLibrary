<?
	/**
	 * @var $searchInfo array
	 * @var $links array
	 */
?>
<? if (isset($searchInfo)): ?>
	<div class="search-grid-info<? if (isset($links)): ?> has-result<? endif; ?>">
		<table>
			<tr>
				<td id="search-links-info-count">
					<span><? echo $searchInfo['count']; ?></span>
				</td>
			</tr>
			<tr>
				<td>
					<? if (array_key_exists('condition', $searchInfo)): ?>
						<span><? echo $searchInfo['condition']; ?>; </span>
					<? endif; ?>
					<? if (array_key_exists('file_types', $searchInfo)): ?>
						<span><? echo $searchInfo['file_types']; ?>; </span>
					<? endif; ?>
					<? if (array_key_exists('dates', $searchInfo)): ?>
						<span><? echo $searchInfo['dates']; ?>; </span>
					<? endif; ?>
				</td>
			</tr>
			<? if (array_key_exists('categories', $searchInfo)): ?>
				<tr>
					<td>
						<span><? echo $searchInfo['categories']; ?></span>
					</td>
				</tr>
			<? endif; ?>
			<? if (array_key_exists('libraries', $searchInfo)): ?>
				<tr>
					<td>
						<span><? echo $searchInfo['libraries']; ?></span>
					</td>
				</tr>
			<? endif; ?>
		</table>
	</div>
<? endif; ?>
<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/linksGrid.php', array('links' => isset($links) ? $links : null, 'datasetKey' => isset($searchInfo) ? $searchInfo['datasetKey'] : null), true); ?>
