<? if (isset($tickerLink)): ?>
	<? if ($tickerLink->type == 'text'): ?>
		<li><a class="ticker-link text" href="#" target="_self"><?echo $tickerLink->text;?></a></li>
	<? endif; ?>
	<? if ($tickerLink->type == 'url'): ?>
		<li>
			<a class="ticker-link url" href="<? echo $tickerLink->getDetailByKey('path'); ?>" target="_blank"><?echo $tickerLink->text;?></a>
		</li>
	<? endif; ?>
	<? if ($tickerLink->type == 'link'): ?>
		<?
		$libraryName = $tickerLink->getDetailByKey('library');
		$pageName = $tickerLink->getDetailByKey('page');
		$linkName = $tickerLink->getDetailByKey('link');
		$link = LinkStorage::getLinkByLibraryAndPageAndName($libraryName, $pageName, $linkName);
		if (isset($link))
			$id = $link['link_id'];
		?>
		<? if (isset($id)): ?>
			<li><a class="ticker-link link" href="#<? echo $id; ?>" target="_self"><?echo $tickerLink->text;?></a></li>
		<? else: ?>
			<li>
				<a class="ticker-link text" href="#" target="_self"><?echo $tickerLink->text;?> (Associated library link was not found)</a>
			</li>
		<? endif; ?>
	<? endif; ?>
	<? if ($tickerLink->type == 'video'): ?>
		<?
		$relativePath = $tickerLink->getDetailByKey('path');
		if (isset($relativePath))
		{
			$relativeLink = str_replace('//', '/', str_replace('\\', '/', ('/' . $relativePath)));
			$tickerRoot = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['tickerRoot'];
			$fileLink = str_replace(' ', '%20', htmlspecialchars($tickerRoot . $relativeLink));
		}
		?>
		<? if (isset($fileLink)): ?>
			<li>
				<a class="ticker-link video" href="<? echo $fileLink; ?>" target="_self"><?echo $tickerLink->text;?>
					<div class="service-data" style="display: none;">
						<div class="link-name"><?php echo $relativePath; ?></div>
						<div class="file-name"><?php echo $relativePath; ?></div>
						<div class="file-type">mp4</div>
						<div class="view-type">mp4</div>
						<?php $viewLinks[] = array('src' => $fileLink, 'href' => $fileLink, 'title' => $relativePath, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf'); ?>
						<?php if (isset($viewLinks)): ?>
							<div class="links"><?php echo json_encode($viewLinks); ?></div>
						<?php endif; ?>
					</div>
				</a>
			</li>
		<? else: ?>
			<li>
				<a class="ticker-link text" href="#" target="_self"><?echo $tickerLink->text;?> (Associated video link was not found)</a>
			</li>
		<? endif; ?>
	<? endif; ?>
	<? if ($tickerLink->type == 'file'): ?>
		<?
		$relativePath = $tickerLink->getDetailByKey('path');
		if (isset($relativePath))
		{
			$relativeLink = str_replace('//', '/', str_replace('\\', '/', ('/' . $relativePath)));
			$tickerRoot = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['tickerRoot'];
			$fileLink = str_replace(' ', '%20', htmlspecialchars($tickerRoot . $relativeLink));
		}
		?>
		<? if (isset($fileLink)): ?>
			<li><a class="ticker-link file" href="<? echo $fileLink; ?>" target="_blank"><?echo $tickerLink->text;?></a>
			</li>
		<? else: ?>
			<li>
				<a class="ticker-link text" href="#" target="_self"><?echo $tickerLink->text;?> (Associated file link was not found)</a>
			</li>
		<? endif; ?>
	<? endif; ?>
<? endif; ?>