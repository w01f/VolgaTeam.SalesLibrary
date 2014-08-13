<?
	/**
	 * @var $link LibraryLink
	 * @var $authorized boolean
	 */
	$logoFolderPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'fileFormats';
	$itemsCount = count($link->availableFormats);
	if (!isset($link->isAttachment) && !$link->forcePreview && $authorized)
		$itemsCount++;
	if ($link->browser == 'mobile' && !$link->forcePreview)
		$itemsCount++;
	$rowClass = '';
	if ($itemsCount == 1)
		$rowClass = 'col-xs-12';
	else if ($itemsCount == 2)
		$rowClass = 'col-xs-6';
	else
		$rowClass = 'col-xs-4';
	$itemNum = 0;
	$rowClosed = true;
?>
<div class="view-dialog-body<? echo $link->originalFormat == 'url' || $itemsCount > 1 ? ' always-show' : ''; ?>">
	<? if (isset($link->originalFormat) && isset($link->availableFormats)): ?>
		<div class="title">
			<div class="link-name">
				<? echo $link->name; ?>
			</div>
			<? if (isset($link->fileName)): ?>
				<div class="description">
					<? echo $link->fileName; ?>
				</div>
			<? endif; ?>
			<? if (!isset($link->isAttachment)): ?>
				<img class="total-rate" src=""/>
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
			<? if ($link->originalFormat == 'url'): ?>
				<p>This is WebSite Link. Click the button below to visit the site.</p>
			<? endif; ?>
			<div class="container format-list">
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
						<? if ($itemNum == 0 || $itemNum % 3 == 0): ?><div class="row"><? $rowClosed = false; ?><? endif; ?>
						<div class="<? echo $rowClass; ?> text-center">
							<a href="<? echo $format == 'url' ? $link->fileName : '#'; ?>" target="_blank" class="format-item"
							   <? if ($link->browser != 'mobile'): ?>rel="tooltip"
							   title="<? echo Yii::app()->params['tooltips']['preview_dialog'][$format]; ?>"
								<? endif; ?>
								> <img src="<? echo $imageSource; ?>"/>
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
							</a>
						</div>
						<? if ($itemNum == 2 || $itemNum % 3 == 2): ?></div><? $rowClosed = true; ?><? endif; ?>
						<? $itemNum++; ?>
					<? endif; ?>
				<? endforeach; ?>
				<? if (!isset($link->isAttachment) && !$link->forcePreview && $authorized): ?>
					<? if ($itemNum == 0 || $itemNum % 3 == 0): ?><div class="row"><? $rowClosed = false; ?><? endif; ?>
					<div class="<? echo $rowClass; ?> text-center">
						<a href="#" class="format-item"
						   <? if ($link->browser != 'mobile'): ?>rel="tooltip"
						   title="<? echo Yii::app()->params['tooltips']['preview_dialog']['favorites']; ?>"<? endif; ?>
							>
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
						</a>
					</div>
					<? if ($itemNum == 2 || $itemNum % 3 == 2): ?></div><? $rowClosed = true; ?><? endif; ?>
					<? $itemNum++; ?>
				<? endif; ?>
				<? if ($link->browser == 'mobile' && !$link->forcePreview): ?>
					<? if ($itemNum == 0 || $itemNum % 3 == 0): ?><div class="row"><? $rowClosed = false; ?><? endif; ?>
					<div class="<? echo $rowClass; ?> text-center">
						<a href="#" class="format-item">
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
						</a>
					</div>
					<? if ($itemNum == 2 || $itemNum % 3 == 2): ?></div><? $rowClosed = true; ?><? endif; ?>
					<? $itemNum++; ?>
				<? endif; ?>
				<? if (!$rowClosed) echo '</div>'; ?>
				<? if (!isset($link->isAttachment)): ?>
					<div class="row text-center" id="user-link-rate-container">
						<div class="col-xs-12">
							<label for="user-link-rate">Do you LIKE this <? echo $link->originalFormat == 'url' ? 'Web link' : 'file' ?>? </label>
						</div>
						<div class="col-xs-12">
							<input id="user-link-rate" class="rating">
						</div>
						<div class="col-xs-12">
							<label id="user-link-rate-description"></label>
						</div>
					</div>
				<? endif; ?>
			</div>
		<? endif; ?>
	<? else: ?>
		<div class="title">
			<div class="description">
				This link is not available for preview
			</div>
		</div>
	<?endif; ?>
</div>