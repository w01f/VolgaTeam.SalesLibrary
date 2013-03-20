<tr class="link-details-container">
	<td class="link-id-column"></td>
	<td class="details-button"></td>
	<td class="library-column"></span></td>
	<td colspan="3">
		<table class="link-details">
			<?php if (isset($link)): ?>
			<?php $clickClass = ' click-no-mobile'; ?>
			<?php if ($link->enableFileCard && isset($link->fileCard)): ?>
				<tr class="file-card">
					<td class="link-id-column"><?echo $link->id;?></td>
					<td class="link-type-column<?echo $clickClass;?>"><img src="images/search/search-file-card.png" alt=""></td>
					<td class="link-name-column<?echo $clickClass;?>"><?echo $link->fileCard->title;?></td>
				</tr>
				<? endif; ?>
			<?php if ($link->enableAttachments && isset($link->attachments)): ?>
				<?php foreach ($link->attachments as $attachment): ?>
					<tr class="attachment">
						<td class="link-id-column"><?echo $attachment->id;?></td>
						<td class="link-type-column<?echo $clickClass;?>">
							<?
							switch ($attachment->originalFormat)
							{
								case 'ppt':
									echo CHtml::tag('img', array('src' => 'images/search/search-powerpoint.png', 'alt' => ''));
									break;
								case 'doc':
									echo CHtml::tag('img', array('src' => 'images/search/search-word.png', 'alt' => ''));
									break;
								case 'xls':
									echo CHtml::tag('img', array('src' => 'images/search/search-excel.png', 'alt' => ''));
									break;
								case 'pdf':
									echo CHtml::tag('img', array('src' => 'images/search/search-pdf.png', 'alt' => ''));
									break;
								case 'video':
								case 'wmv':
								case 'mp4':
									echo CHtml::tag('img', array('src' => 'images/search/search-video.png', 'alt' => ''));
									break;
								case 'url':
									if (Yii::app()->browser->isMobile())
									{
										echo CHtml::tag('img', array('src' => 'images/search/search-url-safari.png', 'alt' => ''));
									}
									else
									{
										$browser = Yii::app()->browser->getBrowser();
										switch ($browser)
										{
											case 'Internet Explorer':
												echo CHtml::tag('img', array('src' => 'images/search/search-url-ie.png', 'alt' => ''));
												break;
											case 'Chrome':
												echo CHtml::tag('img', array('src' => 'images/search/search-url-chrome.png', 'alt' => ''));
												break;
											case 'Safari':
												echo CHtml::tag('img', array('src' => 'images/search/search-url-safari.png', 'alt' => ''));
												break;
											case 'Firefox':
												echo CHtml::tag('img', array('src' => 'images/search/search-url-firefox.png', 'alt' => ''));
												break;
											case 'Opera':
												echo CHtml::tag('img', array('src' => 'images/search/search-url-opera.png', 'alt' => ''));
												break;
											default:
												echo CHtml::tag('img', array('src' => 'images/search/search-url.png', 'alt' => ''));
												break;
										}
									}
									break;
								default:
									echo CHtml::tag('img', array('src' => 'images/search/search-undefined-type.png', 'alt' => ''));
									break;
							}
							?>
						</td>
						<td class="link-name-column<?echo $clickClass;?>"><?echo $attachment->name;?></td>
					</tr>
					<? endforeach; ?>
				<? endif; ?>
			<? endif;?>
		</table>
	</td>
</tr>



