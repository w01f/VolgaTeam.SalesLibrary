<?
	/**
	 * @var $link LibraryLink
	 * @var $authorized boolean
	 * @var $isQuickSite boolean
	 */
?>
<ul data-role="listview" data-theme="c" data-divider-theme="c">
	<? if (isset($link->originalFormat) && isset($link->availableFormats)): ?>
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
					<? if (isset($link->name) && $link->name != '' && isset($link->fileName) && $link->fileName != ''): ?>
						<tr>
							<td>
								<span class="file"><? echo $link->fileName; ?></span>
							</td>
						</tr>
					<? endif; ?>
				</table>
				<? if (($link->originalFormat == 'ppt' || $link->originalFormat == 'doc' || $link->originalFormat == 'pdf') && !$this->isTabletMobileView): ?>
					<hr align="center" width="100%" size="2" color="#b0b0b0"/>
					<div data-role="navbar" class="res-selector">
						<ul>
							<li><a href="#" class="low-res-button ui-btn-active" data-corners="true"
								   data-shadow="true">LOW Res</a></li>
							<li><a href="#" class="hi-res-button" data-corners="true" data-shadow="true">HIGH Res</a>
							</li>
						</ul>
					</div>
				<? endif; ?>
			</h4>
		</li>
		<? $viewSource = $link->getViewSource($link->availableFormats[0]); ?>
		<? if (($link->originalFormat == 'video' || $link->originalFormat == 'wmv' || $link->originalFormat == 'mp4') && !isset($viewSource)): ?>
			<li>
				<div class="warning">
					This Video is unavailable…<br><br> Ask your Site Administrator to convert this Video to MP4.<br><br> Then the video can be accessed.<br><br>
				</div>
			</li>
		<? else: ?>
			<?
			foreach ($link->availableFormats as $format):
				if ((!$authorized || $isQuickSite) && $format == 'outlook')
					continue;
				$imageSource = '';
				$imageTitle = '';
				switch ($format)
				{
					case 'ppt':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/pptx.png';
						$imageTitle = 'PowerPoint';
						break;
					case 'doc':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/docx.png';
						$imageTitle = 'Word';
						break;
					case 'xls':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/xlsx.png';
						$imageTitle = 'Excel';
						break;
					case 'png':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/png.png';
						$imageTitle = 'PNG';
						break;
					case 'jpeg':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/jpeg.png';
						$imageTitle = 'JPEG';
						break;
					case 'pdf':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/pdf.png';
						$imageTitle = 'PDF';
						break;
					case 'tab':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/tab.png';
						$imageTitle = 'QuickTime';
						break;
					case 'url':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/url.png';
						$imageTitle = 'Web Url';
						break;
					case 'url365':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/url365.png';
						$imageTitle = 'Office 365 Url';
						break;
					case 'mp3':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/mp3.png';
						$imageTitle = 'MP3 Track';
						break;
					case 'key':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/keynote.png';
						$imageTitle = 'Apple Keynote';
						break;
					case 'outlook':
						$imageSource = Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/email.png';
						$imageTitle = 'Email Link';
						break;
				}
				$fileSize = $link->getViewSize($format);
				$fileSizePhone = $link->getViewSize($format . '_phone');
				$viewLinks = $link->getViewSource($format);
				if (isset($viewLinks) && $imageSource != '' && $imageTitle != ''):
					?>
					<li>
						<a class="preview-link" href="#">
							<table class="link-container">
								<tr>
									<td><img src="<? echo $imageSource; ?>"/></td>
									<td>
										<span><? echo $imageTitle; ?></span>
										<? if (isset($fileSize)): ?>
											<span class="file-size <? echo isset($fileSizePhone) ? 'regular' : ''; ?>"> (<? echo $fileSize; ?>
												)</span>
										<? endif; ?>
										<? if (isset($fileSizePhone)): ?>
											<span class="file-size phone"> (<? echo $fileSizePhone; ?>)</span>
										<? endif; ?>
										<div class="item-content">
											<div class="link-id"><? echo $link->id; ?></div>
											<div class="link-name"><? echo $link->name; ?></div>
											<div class="file-name"><? echo $link->fileName; ?></div>
											<div class="file-type"><? echo $link->originalFormat; ?></div>
											<div class="view-type"><? echo $format; ?></div>
											<div class="file-size"><? echo $link->originalFormat; ?></div>
											<?
												echo CHtml::openTag('div', array('class' => 'links'));
												if ($format == 'png' || $format == 'jpeg'):
													$thumbsLinks = $link->getViewSource('thumbs');
													$viewPhoneLinks = $format == 'png' ? $link->getViewSource('png_phone') : $link->getViewSource('jpeg_phone');
													if (isset($thumbsLinks)):
														$i = 0;
														foreach ($viewLinks as $viewLink):
															?>
															<li class="hi-res">
																<a href="<? echo $viewLink['href']; ?>"
																   rel="external"><img
																		src="<? echo !$this->isTabletMobileView ? $thumbsLinks[$i]['href'] : $viewPhoneLinks[$i]['href']; ?>"
																		alt="<? echo $viewLink['title']; ?>"/></a>
															</li>
															<li class="low-res">
																<a href="<? echo $viewPhoneLinks[$i]['href']; ?>"
																   rel="external"><img
																		src="<? echo $thumbsLinks[$i]['href']; ?>"
																		alt="<? echo $viewLink['title']; ?>"/></a>
															</li>
															<?
															$i++;
														endforeach;
													endif;
												else:
													echo json_encode($viewLinks);
												endif;
												echo CHtml::closeTag('div');
											?>
										</div>
									</td>
								</tr>
							</table>
						</a>
					</li>
				<? endif; ?>
			<? endforeach; ?>
			<? if (!$link->extendedProperties->forcePreview && $authorized): ?>
				<li>
					<a class="preview-link" href="#">
						<table class="link-container">
							<tr>
								<td><img
										src="<? echo Yii::app()->request->getBaseUrl(true) . '/images/fileFormats_phone/favorites.png'; ?>"/>
								</td>
								<td>
									<span>Add to Favorites</span>

									<div class="item-content">
										<div class="link-id"><? echo $link->id; ?></div>
										<div class="link-name"><? echo $link->name; ?></div>
										<div class="file-name"><? echo $link->fileName; ?></div>
										<div class="file-type"><? echo $link->originalFormat; ?></div>
										<div class="view-type">favorites</div>
										<?
											$viewLinks = $link->getViewSource('favorites');
											if (isset($viewLinks)):
												echo CHtml::openTag('div', array('class' => 'links'));
												echo json_encode($viewLinks);
												echo CHtml::closeTag('div');
											endif;
										?>
									</div>
								</td>
							</tr>
						</table>
					</a>
				</li>
			<? endif; ?>
		<? endif; ?>
	<? endif; ?>
</ul>
