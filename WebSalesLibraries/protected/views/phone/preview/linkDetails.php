<? /** @var $link LibraryLink */ ?>
<ul data-role="listview" data-theme="c" data-divider-theme="c">
	<li data-role="list-divider">
		<h4>
			<table class="link-container">
				<tr>
					<td>
                        <span class="name">
                            <?
								if (isset($link->name) && $link->name != '')
									echo $link->name;
								else if (isset($link->fileName) && $link->fileName != '')
									echo $link->fileName;
							?>
                        </span>
					</td>
				</tr>
				<? if (isset($link->name) && $link->name != ''): ?>
					<tr>
						<td>
							<span class="file"><? echo $link->fileName; ?></span>
						</td>
					</tr>
				<? endif; ?>
			</table>
		</h4>
	</li>
	<? if ($link->enableFileCard && isset($link->fileCard)): ?>
		<li>
			<a class="file-card-link" href="#file-card<? echo $link->id; ?>">
				<h3>
					<img src="<? echo Yii::app()->request->getBaseUrl(true) . '/images/search/search-file-card.png'; ?>" align="middle"/>
					<? echo $link->fileCard->title; ?>
				</h3>
			</a>
		</li>
	<? endif; ?>
	<? if ($link->enableAttachments && isset($link->attachments)): ?>
		<? foreach ($link->attachments as $attachment): ?>
			<li>
				<a class="attachment-link" href="#attachment<? echo $attachment->id; ?>">
					<?
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
					<h3>
						<img src="<? echo $imagePath; ?>" align="middle"/>
						<? echo $attachment->name; ?>
					</h3>
				</a>
			</li>
		<? endforeach; ?>
	<? endif; ?>
</ul>