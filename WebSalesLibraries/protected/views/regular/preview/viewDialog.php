<div class="view-dialog-body">
	<?php if (isset($link->originalFormat) && isset($link->availableFormats)): ?>
		<div class="title">
			<div class="link-name">
				<?php echo $link->name; ?>
			</div>
			<br>
			<?php if (isset($link->fileName)): ?>
				<div class="description">
					<?php echo $link->fileName; ?>
				</div><br>
			<?php endif; ?>
		</div>
		<?php if ((($link->originalFormat == 'ppt' || $link->originalFormat == 'doc' || $link->originalFormat == 'pdf') && isset($link->universalPreview)) || $link->originalFormat == 'jpeg' || $link->originalFormat == 'png'): ?>
			<label class="checkbox"> <input class="use-fullscreen" type="checkbox"
											value=""> Open PNG and JPEG images in a new Browser tab </label>
			<br>
		<?php endif; ?>
		<?php if (($link->originalFormat == 'video' || $link->originalFormat == 'wmv' || $link->originalFormat == 'mp4') && !isset($link->universalPreview)): ?>
			<div class="warning">
				This Video is unavailable…<br><br> Ask your Site Administrator to convert this Video to MP4.<br><br> Then the video can be accessed.<br><br>
			</div>
		<?php else: ?>
			<? $logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'fileFormats'; ?>
			<ul class="format-list">
				<?php foreach ($link->availableFormats as $format): ?>
					<?php
					if (!$autorized && ($format == 'email' || $format == 'outlook'))
						continue;
					$imageSource = '';
					switch ($format)
					{
						case 'ppt':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'pptx.png'));
							break;
						case 'doc':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'docx.png'));
							break;
						case 'xls':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'xlsx.png'));
							break;
						case 'png':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'png.png'));
							break;
						case 'jpeg':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'jpeg.png'));
							break;
						case 'pdf':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'pdf.png'));
							break;
						case 'video':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'wmv.png'));
							break;
						case 'mp4':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'mp4.png'));
							break;
						case 'ogv':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'ogv.png'));
							break;
						case 'tab':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'tab.png'));
							break;
						case 'url':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'url.png'));
							break;
						case 'key':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'keynote.png'));
							break;
						case 'email':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'email.png'));
							break;
						case 'outlook':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'email.png'));
							break;
						case 'download':
							$imageSource = 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'download.png'));
							break;
					}
					?>
					<?php if ($imageSource != ''): ?>
						<li class="multi-column" <? if ($link->browser != 'mobile'): ?>rel="tooltip"
							title="<? echo Yii::app()->params['tooltips']['preview_dialog'][$format]; ?>"<?php endif; ?>>
							<img src="<?php echo $imageSource; ?>"/>

							<div class="service-data">
								<div class="link-id"><?php echo $link->id; ?></div>
								<div class="link-name"><?php echo $link->name; ?></div>
								<div class="file-name"><?php echo isset($link->isAttachment) ? $link->name : $link->fileName; ?></div>
								<div class="file-type"><?php echo $link->originalFormat; ?></div>
								<div class="view-type"><?php echo $format; ?></div>
								<div class="tags"><?php echo $link->getTagsString(); ?></div>
								<?php $viewLinks = $link->getViewSource($format); ?>
								<?php if (isset($viewLinks)): ?>
									<div class="links"><?php echo json_encode($viewLinks); ?></div>
								<?php endif; ?>
								<?php if ($format == 'png' || $format == 'jpeg'): ?>
									<?php $thumbsLinks = $link->getViewSource('thumbs'); ?>
									<?php if (isset($thumbsLinks)): ?>
										<div class="thumbs"><?php echo json_encode($thumbsLinks); ?></div>
									<?php endif; ?>
								<?php endif; ?>
							</div>
						</li>
					<?php endif; ?>
				<?php endforeach; ?>
				<?php if (!isset($link->isAttachment) && !$link->forcePreview && $autorized): ?>
					<li class="multi-column" <? if ($link->browser != 'mobile'): ?>rel="tooltip"
						title="<? echo Yii::app()->params['tooltips']['preview_dialog']['favorites']; ?>"<?php endif; ?>>
						<img src="<?php echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'favorites.png')); ?>"/>
						<div class="service-data">
							<div class="link-id"><?php echo $link->id; ?></div>
							<div class="link-name"><?php echo $link->name; ?></div>
							<div class="file-name"><?php echo $link->fileName; ?></div>
							<div class="file-type"><?php echo $link->originalFormat; ?></div>
							<div class="view-type">favorites</div>
							<?php $viewLinks = $link->getViewSource('favorites'); ?>
							<?php if (isset($viewLinks)): ?>
								<div class="links"><?php echo json_encode($viewLinks); ?></div>
							<?php endif; ?>
						</div>
					</li>
				<?php endif; ?>
				<?php if ($link->browser == 'mobile'): ?>
					<li class="multi-column">
						<img src="<?php echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'lp.png')); ?>"/>
						<div class="service-data">
							<div class="link-id"><?php echo $link->id; ?></div>
							<div class="link-name"><?php echo $link->name; ?></div>
							<div class="file-name"><?php echo $link->fileName; ?></div>
							<div class="file-type"><?php echo $link->originalFormat; ?></div>
							<div class="view-type">lp</div>
							<?php $viewLinks = $link->getViewSource('lp'); ?>
							<?php if (isset($viewLinks)): ?>
								<div class="links"><?php echo json_encode($viewLinks); ?></div>
							<?php endif; ?>
						</div>
					</li>
				<?php endif; ?>
			</ul>
		<?php endif; ?>
	<?php else: ?>
		<div class="title">
			<div class="description">
				This link is not available for preview
			</div>
		</div>
	<?php endif; ?>
</div>