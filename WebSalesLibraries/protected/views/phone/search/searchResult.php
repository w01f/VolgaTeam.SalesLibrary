<? if (isset($searchInfo)): ?>
	<div class="dataset-key"><? echo $searchInfo['datasetKey']; ?></div>
<?php endif; ?>
<?php if (isset($links)): ?>
	<?php foreach ($links as $link): ?>
		<li>
			<a class="file-link" href="#link<?php echo $link['id']; ?>">
				<?php if ($link['hasDetails']): ?>
					<img class="ui-li-has-thumb file-link-detail" src="<? echo Yii::app()->baseUrl . '/images/search/expand-phone.png'; ?>"/>
				<? endif; ?>
				<h3 class="name"><? if (isset($link['name']) && $link['name'] != '') echo $link['name'];
					else if (isset($link['file_name']) && $link['file_name'] != '') echo $link['file_name']; ?></h3>
				<p class="file">
					<?
						if (isset($link['name']) && $link['name'] != '')
							echo $link['file_name'];
						echo '<br>';
						echo $link['library'] . ' - ' . date(Yii::app()->params['outputDateFormat'], strtotime($link['date_modify']));
						if (array_key_exists('tag', $link) && isset($link['tag']) && $link['tag'] != '')
							echo ' - ' . $link['tag'];
					?>
				</p>
			</a>
		</li>
	<?php endforeach; ?>
<?php endif; ?>                    