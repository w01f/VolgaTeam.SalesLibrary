<?
	/**
	 * @var $link LibraryLink
	 * @var $authorized boolean
	 */
?>
<div class="view-dialog-body">
	<?
		if (isset($link->originalFormat) && isset($link->availableFormats)): ?>
			<div class="title">
				<div class="link-name">
					<? echo $link->name; ?>
				</div>
				<br>
				<? if (isset($link->fileName)): ?>
					<div class="description">
						<? echo $link->fileName; ?>
					</div><br>
				<? endif; ?>
			</div>
			<? if ((($link->originalFormat == 'ppt' || $link->originalFormat == 'doc' || $link->originalFormat == 'pdf') && isset($link->universalPreview)) || $link->originalFormat == 'jpeg' || $link->originalFormat == 'png'): ?>
				<div class="checkbox">
					<label>
						<input class="use-fullscreen" type="checkbox" value=""> Open PNG and JPEG images in a new Browser tab
					</label>
				</div>
			<? endif; ?>
			<? if (($link->originalFormat == 'video' || $link->originalFormat == 'wmv' || $link->originalFormat == 'mp4') && !isset($link->universalPreview)): ?>
				<div class="warning">
					This Video is unavailable…<br><br> Ask your Site Administrator to convert this Video to MP4.<br><br> Then the video can be accessed.<br><br>
				</div>
			<? else: ?>
				<? $logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'fileFormats'; ?>
				<ul class="format-list">
					<? foreach ($link->availableFormats as $format): ?>
						<?
						if (!$authorized && ($format == 'email' || $format == 'outlook'))
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
						<? if ($imageSource != ''): ?>
							<li class="multi-column"
								<? if ($link->browser != 'mobile'): ?>rel="tooltip"
								title="<? echo Yii::app()->params['tooltips']['preview_dialog'][$format]; ?>"
								<? endif; ?>
								>
								<img src="<? echo $imageSource; ?>"/>

								<div class="service-data">
									<div class="link-id"><? echo $link->id; ?></div>
									<div class="link-name"><? echo $link->name; ?></div>
									<div class="file-name"><? echo isset($link->isAttachment) ? $link->name : $link->fileName; ?></div>
									<div class="file-type"><? echo $link->originalFormat; ?></div>
									<div class="view-type"><? echo $format; ?></div>
									<div class="tags"><? echo isset($link->isAttachment) ? '' : $link->getTagsString(); ?></div>
									<? $viewLinks = $link->getViewSource($format); ?>
									<? if (isset($viewLinks)): ?>
										<div class="links"><? echo json_encode($viewLinks); ?></div>
									<? endif; ?>
									<? if ($format == 'png' || $format == 'jpeg'): ?>
										<? $thumbsLinks = $link->getViewSource('thumbs'); ?>
										<? if (isset($thumbsLinks)): ?>
											<div class="thumbs"><? echo json_encode($thumbsLinks); ?></div>
										<? endif; ?>
									<? endif; ?>
								</div>
							</li>
						<? endif; ?>
					<? endforeach; ?>
					<? if (!isset($link->isAttachment) && !$link->forcePreview && $authorized): ?>
						<li class="multi-column" <? if ($link->browser != 'mobile'): ?>rel="tooltip"
							title="<? echo Yii::app()->params['tooltips']['preview_dialog']['favorites']; ?>"<? endif; ?>>
							<img src="<? echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'favorites.png')); ?>"/>
							<div class="service-data">
								<div class="link-id"><? echo $link->id; ?></div>
								<div class="link-name"><? echo $link->name; ?></div>
								<div class="file-name"><? echo $link->fileName; ?></div>
								<div class="file-type"><? echo $link->originalFormat; ?></div>
								<div class="view-type">favorites</div>
								<? $viewLinks = $link->getViewSource('favorites'); ?>
								<? if (isset($viewLinks)): ?>
									<div class="links"><? echo json_encode($viewLinks); ?></div>
								<? endif; ?>
							</div>
						</li>
					<? endif; ?>
					<? if ($link->browser == 'mobile' && !$link->forcePreview): ?>
						<li class="multi-column">
							<img src="<? echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoFolderPath . DIRECTORY_SEPARATOR . 'lp.png')); ?>"/>
							<div class="service-data">
								<div class="link-id"><? echo $link->id; ?></div>
								<div class="link-name"><? echo $link->name; ?></div>
								<div class="file-name"><? echo $link->fileName; ?></div>
								<div class="file-type"><? echo $link->originalFormat; ?></div>
								<div class="view-type">lp</div>
								<? $viewLinks = $link->getViewSource('lp'); ?>
								<? if (isset($viewLinks)): ?>
									<div class="links"><? echo json_encode($viewLinks); ?></div>
								<? endif; ?>
							</div>
						</li>
					<? endif; ?>
				</ul>
			<? endif; ?>
		<? else: ?>
			<div class="title">
				<div class="description">
					This link is not available for preview
				</div>
			</div>
		<? endif; ?>
</div>