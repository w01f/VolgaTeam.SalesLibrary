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
	<?php if ($link->originalFormat == 'ppt' || $link->originalFormat == 'doc' || $link->originalFormat == 'pdf' || $link->originalFormat == 'jpeg' || $link->originalFormat == 'png'): ?>
		<label class="checkbox">
			<input class="use-fullscreen" type="checkbox" value="">
			Open PNG and JPEG images in a new Browser tab
		</label>
		<br>
		<?php endif; ?>
	<?php $viewSource = $link->getViewSource($link->availableFormats[0]); ?>
	<?php if ($link->originalFormat == 'video' && !isset($viewSource)): ?>
		<div class="warning">
			This Video is unavailable…<br><br>
			Ask your Site Administrator to convert this Video to MP4.<br><br>
			Then the video can be accessed.<br><br>
		</div>
		<?php else: ?>
		<ul class="format-list">
			<?php foreach ($link->availableFormats as $format): ?>
			<?php
			$imageSource = '';
			switch ($format)
			{
				case 'ppt':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/pptx.png';
					break;
				case 'doc':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/docx.png';
					break;
				case 'xls':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/xlsx.png';
					break;
				case 'png':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/png.png';
					break;
				case 'jpeg':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/jpeg.png';
					break;
				case 'pdf':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/pdf.png';
					break;
				case 'video':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/wmv.png';
					break;
				case 'mp4':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/mp4.png';
					break;
				case 'ogv':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/ogv.png';
					break;
				case 'tab':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/tab.png';
					break;
				case 'url':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/url.png';
					break;
				case 'email':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/email.png';
					break;
				case 'download':
					$imageSource = Yii::app()->baseUrl . '/images/fileFormats/download.png';
					break;
			}
			?>
			<?php if ($imageSource != ''): ?>
				<li <?if ($link->browser != 'mobile'): ?>rel="tooltip"
						title="<?echo Yii::app()->params['tooltips']['preview_dialog'][$format];?>"<?php endif; ?>>
					<img src="<?php echo $imageSource; ?>"/>
					<div class="service-data">
						<div class="link-id"><?php echo $link->id; ?></div>
						<div class="file-type"><?php echo $link->originalFormat; ?></div>
						<div class="view-type"><?php echo $format; ?></div>
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
			<?php if (!isset($link->isAttachment)): ?>
			<li <?if ($link->browser != 'mobile'): ?>rel="tooltip"
					title="<?echo Yii::app()->params['tooltips']['preview_dialog']['favorites'];?>"<?php endif; ?>>
				<img src="<?php echo Yii::app()->baseUrl . '/images/fileFormats/favorites.png'; ?>"/>
				<div class="service-data">
					<div class="link-id"><?php echo $link->id; ?></div>
					<div class="file-type"><?php echo $link->originalFormat; ?></div>
					<div class="view-type">favorites</div>
					<?php $viewLinks = $link->getViewSource($link->originalFormat); ?>
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