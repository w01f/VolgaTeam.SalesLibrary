<ul data-role="listview" data-theme="c" data-divider-theme="c">
	<li data-role="list-divider">
		<h4>
			<table class="link-container">
				<tr>
					<td>
                        <span class="name">
                            <?php
							if (isset($link->name) && $link->name != '')
								echo $link->name;
							else if (isset($link->fileName) && $link->fileName != '')
								echo $link->fileName;
							?>
                        </span>
					</td>
				</tr>
				<?php if (isset($link->name) && $link->name != ''): ?>
					<tr>
						<td>
							<span class="file"><?php echo $link->fileName; ?></span>
						</td>
					</tr>
				<?php endif; ?>
			</table>
		</h4>
	</li>
	<?php if ($link->enableFileCard && isset($link->fileCard)): ?>
		<li>
			<a class="file-card-link" href="#file-card<?php echo $link->id; ?>">
				<table class="link-container">
					<tr>
						<td>
							<img src="<? echo Yii::app()->request->getBaseUrl(true) . '/images/search/search-file-card.png'; ?>">
						</td>
						<td>
							<span><?php echo $link->fileCard->title; ?></span>
						</td>
					</tr>
				</table>
			</a>
		</li>
	<?php endif; ?>
	<?php if ($link->enableAttachments && isset($link->attachments)): ?>
		<?php foreach ($link->attachments as $attachment): ?>
			<li>
				<a class="attachment-link" href="#attachment<?php echo $attachment->id; ?>">
					<table class="link-container">
						<tr>
							<td>
								<?php
								$imagePath = '';
								switch ($attachment->originalFormat)
								{
									case 'ppt':
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-powerpoint.png';
										break;
									case 'doc':
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-word.png';
										break;
									case 'xls':
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-excel.png';
										break;
									case 'pdf':
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-pdf.png';
										break;
									case 'video':
									case 'wmv':
									case 'mp4':
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-video.png';
										break;
									case 'url':
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-url.png';
										break;
									case 'key':
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-keynote.png';
										break;
									default:
										$imagePath = Yii::app()->request->getBaseUrl(true) . '/images/search/search-undefined-type.png';
										break;
								}
								?>
								<img src="<?php echo $imagePath; ?>"/>
							</td>
							<td>
								<span><?php echo $attachment->name; ?></span>
							</td>
						</tr>
					</table>
				</a>
			</li>
		<?php endforeach; ?>
	<?php endif; ?>
</ul>