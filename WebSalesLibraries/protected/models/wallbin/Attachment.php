<?php
	class Attachment
	{
		public $parent;
		public $id;
		/**
		 * @var string
		 * @soap
		 */
		public $linkId;
		/**
		 * @var string
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var string
		 * @soap
		 */
		public $name;
		/**
		 * @var string
		 * @soap
		 */
		public $path;
		/**
		 * @var string
		 * @soap
		 */
		public $originalFormat;
		/**
		 * @var string
		 * @soap
		 */
		public $previewId;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isDead;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isPreviewNotReady;
		public $fullPath;
		public $link;
		public $availableFormats;
		public $browser;
		public $universalPreview;
		public $fileSize;
		public $isAttachment;
		public $tooltip;
		public $forcePreview;

		public function __construct($link)
		{
			$this->parent = $link;
			$this->isAttachment = true;
			$this->forcePreview = false;
		}

		public function load($attachmentRecord)
		{
			$this->id = $attachmentRecord->id;
			$this->linkId = $attachmentRecord->id_link;
			$this->libraryId = $attachmentRecord->id_library;
			$this->name = $attachmentRecord->name;
			$this->path = $attachmentRecord->path;
			$this->originalFormat = $attachmentRecord->format;
			$this->isDead = $attachmentRecord->is_dead;
			$this->isPreviewNotReady = $attachmentRecord->is_preview_not_ready;
			if ($this->originalFormat != 'url')
			{
				$this->link = str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->parent->parent->parent->storageLink . '/' . $this->path)));
				$this->fullPath = $this->parent->parent->parent->parent->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $this->path);
			}
			else
			{
				$this->link = $attachmentRecord->path;
				$this->fullPath = '';
			}
			$this->fileSize = file_exists($this->fullPath) ? filesize($this->fullPath) : 0;

			if (isset($attachmentRecord->id_preview))
			{
				$this->previewId = $attachmentRecord->id_preview;
				$previewRecords = PreviewStorage::model()->findAll('id_container=?', array($attachmentRecord->id_preview));
				if (isset($previewRecords) && count($previewRecords) > 0)
				{
					$this->universalPreview = new UniversalPreviewContainer($this->parent->parent->parent->parent);
					$this->universalPreview->load($previewRecords);
				}
			}
			$this->getFormats();
			$this->getTooltip();
		}

		public function getFormats()
		{
			if (isset($this->originalFormat))
			{
				switch ($this->originalFormat)
				{
					case 'ppt':
						$this->availableFormats[] = 'ppt';
						if (!$this->forcePreview)
						{
							if (isset($this->universalPreview))
							{
								$this->availableFormats[] = 'pdf';
								$this->availableFormats[] = 'png';
								$this->availableFormats[] = 'jpeg';
							}
							$this->availableFormats[] = 'email';
						}
						break;
					case 'doc':
						$this->availableFormats[] = 'doc';
						if (!$this->forcePreview)
						{
							if (isset($this->universalPreview))
							{
								$this->availableFormats[] = 'pdf';
								$this->availableFormats[] = 'png';
								$this->availableFormats[] = 'jpeg';
							}

							$this->availableFormats[] = 'email';
						}
						break;
					case 'xls':
						$this->availableFormats[] = 'xls';
						if (!$this->forcePreview)
						{
							$this->availableFormats[] = 'email';
						}
						break;
					case 'pdf':
						$this->availableFormats[] = 'pdf';
						if (!$this->forcePreview)
						{
							if (isset($this->universalPreview))
							{
								$this->availableFormats[] = 'png';
								$this->availableFormats[] = 'jpeg';
							}

							$this->availableFormats[] = 'email';
						}
						break;
					case 'video':
					case 'wmv':
					case 'mp4':
						if (isset($this->universalPreview))
							switch ($this->browser)
							{
								case 'phone':
									$this->availableFormats[] = 'tab';
									break;
								case 'mobile':
									$this->availableFormats[] = 'mp4';
									if (!$this->forcePreview)
										$this->availableFormats[] = 'tab';
									break;
								case 'ie':
									$this->availableFormats[] = 'mp4';
									if (!$this->forcePreview)
										$this->availableFormats[] = 'video';
									break;
								case 'webkit':
									$this->availableFormats[] = 'mp4';
									if (!$this->forcePreview)
										$this->availableFormats[] = 'tab';
									break;
								case 'firefox':
									$this->availableFormats[] = 'mp4';
									if (!$this->forcePreview)
										$this->availableFormats[] = 'ogv';
									break;
								case 'opera':
									$this->availableFormats[] = 'mp4';
									if (!$this->forcePreview)
									{
										$this->availableFormats[] = 'tab';
										$this->availableFormats[] = 'ogv';
									}
									break;
								default:
									$this->availableFormats[] = 'mp4';
									if (!$this->forcePreview)
									{
										$this->availableFormats[] = 'video';
										$this->availableFormats[] = 'ogv';
										$this->availableFormats[] = 'tab';
									}
									break;
							}
						if (!$this->forcePreview)
						{
							$this->availableFormats[] = 'email';
							if ($this->browser != 'phone')
								$this->availableFormats[] = 'download';
						}
						break;
					case 'png':
						$this->availableFormats[] = 'png';
						if (!$this->forcePreview)
						{
							$this->availableFormats[] = 'email';
						}
						break;
					case 'jpeg':
						$this->availableFormats[] = 'jpeg';
						if (!$this->forcePreview)
						{
							$this->availableFormats[] = 'email';
						}
						break;
					case 'url':
						$this->availableFormats[] = 'url';
						break;
					case 'key':
						$this->availableFormats[] = 'key';
						if (!$this->forcePreview)
						{
							$this->availableFormats[] = 'email';
						}
						break;
					default:
						$this->originalFormat = 'other';
						if (isset($this->fileLink))
							if ($this->fileLink != '')
								$this->availableFormats[] = 'url';
						break;
				}
			}
		}

		public function getTooltip()
		{
			if (isset($this->originalFormat) && array_key_exists($this->originalFormat, Yii::app()->params['tooltips']['wallbin']))
				$this->tooltip = Yii::app()->params['tooltips']['wallbin'][$this->originalFormat];
		}

		public function getViewSource($format)
		{
			switch ($this->originalFormat)
			{
				case 'ppt':
					switch ($format)
					{
						case 'ppt':
						case 'lp':
							$viewSources[] = array('href' => $this->link);
							break;
						case 'email':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
						case 'png':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->pngLinks);
									foreach ($this->universalPreview->pngLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'PNG Viewer - ' . ($this->name . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
										$i++;
									}
								}
							break;
						case 'png_phone':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngPhoneLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->pngPhoneLinks);
									foreach ($this->universalPreview->pngPhoneLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->name . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
										$i++;
									}
								}
							break;
						case 'jpeg':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->jpegLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->jpegLinks);
									foreach ($this->universalPreview->jpegLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'JPEG Viewer - ' . ($this->name . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link);
										$i++;
									}
								}
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->jpegPhoneLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->jpegPhoneLinks);
									foreach ($this->universalPreview->jpegPhoneLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->name . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link);
										$i++;
									}
								}
							break;
						case 'pdf':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pdfLinks))
									foreach ($this->universalPreview->pdfLinks as $link)
										$viewSources[] = array('href' => $link);
							break;
						case 'thumbs':
							if (isset($this->universalPreview))
							{
								if ($this->browser == 'phone')
								{
									if (isset($this->universalPreview->thumbsPhoneLinks))
										foreach ($this->universalPreview->thumbsPhoneLinks as $link)
											$viewSources[] = array('href' => $link);
								}
								else
								{
									if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
										foreach ($this->universalPreview->thumbsLinks as $link)
											$viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
								}
							}
							break;
					}
					break;
				case 'doc':
					switch ($format)
					{
						case 'doc':
						case 'lp':
							$viewSources[] = array('href' => $this->link);
							break;
						case 'email':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
						case 'png':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->pngLinks);
									foreach ($this->universalPreview->pngLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'PNG Viewer - ' . ($this->name . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
										$i++;
									}
								}
							break;
						case 'png_phone':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngPhoneLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->pngPhoneLinks);
									foreach ($this->universalPreview->pngPhoneLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->name . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
										$i++;
									}
								}
							break;
						case 'jpeg':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->jpegLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->jpegLinks);
									foreach ($this->universalPreview->jpegLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'JPEG Viewer - ' . ($this->name . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link);
										$i++;
									}
								}
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->jpegPhoneLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->jpegPhoneLinks);
									foreach ($this->universalPreview->jpegPhoneLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->name . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link);
										$i++;
									}
								}
							break;
						case 'pdf':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pdfLinks))
									foreach ($this->universalPreview->pdfLinks as $link)
										$viewSources[] = array('href' => $link);
							break;
						case 'thumbs':
							if (isset($this->universalPreview))
							{
								if ($this->browser == 'phone')
								{
									if (isset($this->universalPreview->thumbsPhoneLinks))
										foreach ($this->universalPreview->thumbsPhoneLinks as $link)
											$viewSources[] = array('href' => $link);
								}
								else
								{
									if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
										foreach ($this->universalPreview->thumbsLinks as $link)
											$viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
								}
							}
							break;
					}
					break;
				case 'xls':
					switch ($format)
					{
						case 'xls':
						case 'lp':
							$viewSources[] = array('href' => $this->link);
							break;
						case 'email':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
					}
					break;
				case 'pdf':
					switch ($format)
					{
						case 'pdf':
						case 'lp':
							$viewSources[] = array('href' => $this->link);
							break;
						case 'email':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
						case 'png':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngLinks))
								{
									$i = 1;
									$count = count($this->universalPreview->pngLinks);
									foreach ($this->universalPreview->pngLinks as $link)
									{
										$viewSources[] = array('title' => 'PNG Viewer - ' . ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
										$i++;
									}
								}
							break;
						case 'png_phone':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngPhoneLinks))
								{
									$i = 1;
									$count = count($this->universalPreview->pngPhoneLinks);
									foreach ($this->universalPreview->pngPhoneLinks as $link)
									{
										$viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
										$i++;
									}
								}
							break;
						case 'jpeg':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->jpegLinks))
								{
									$i = 1;
									$count = count($this->universalPreview->jpegLinks);
									foreach ($this->universalPreview->jpegLinks as $link)
									{
										$viewSources[] = array('title' => 'JPEG Viewer - ' . ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
										$i++;
									}
								}
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->jpegPhoneLinks))
								{
									$i = 1;
									$count = count($this->universalPreview->jpegPhoneLinks);
									foreach ($this->universalPreview->jpegPhoneLinks as $link)
									{
										$viewSources[] = array('title' => ($this->name . ' - Page ' . $i . ' of ' . $count), 'href' => $link);
										$i++;
									}
								}
							break;
						case 'thumbs':
							if (isset($this->universalPreview))
							{
								if ($this->browser == 'phone')
								{
									if (isset($this->universalPreview->thumbsPhoneLinks))
										foreach ($this->universalPreview->thumbsPhoneLinks as $link)
											$viewSources[] = array('href' => $link);
								}
								else
								{
									if (isset($this->universalPreview->thumbsLinks) && isset($this->universalPreview->thumbsWidth) && isset($this->universalPreview->thumbsHeight))
										foreach ($this->universalPreview->thumbsLinks as $link)
											$viewSources[] = array('href' => $link, 'width' => $this->universalPreview->thumbsWidth, 'height' => $this->universalPreview->thumbsHeight);
								}
							}
							break;
					}
					break;
				case 'jpeg':
				case 'png':
					switch ($format)
					{
						case 'jpeg':
						case 'png':
						case 'lp':
							$viewSources[] = array('href' => $this->link);
							break;
						case 'email':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
					}
					break;
				case 'url':
				case 'other':
					$viewSources[] = array('href' => $this->link);
					break;
				case 'key':
					switch ($format)
					{
						case 'key':
						case 'lp':
							$viewSources[] = array('href' => $this->link);
							break;
						case 'email':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
					}
					break;
				case 'video':
					switch ($format)
					{
						case 'video':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->wmvLinks))
									foreach ($this->universalPreview->wmvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name);
								else
									$viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name);
							}
							else
								$viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name);
							break;
						case 'email':
						case 'download':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
						case 'mp4':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							}
							break;
						case 'tab':
						case 'lp':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
						case 'ogv':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
					}
					break;
				case 'wmv':
					switch ($format)
					{
						case 'video':
							$viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name);
							break;
						case 'email':
						case 'download':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
						case 'mp4':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							}
							break;
						case 'tab':
						case 'lp':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
						case 'ogv':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
					}
					break;
				case 'mp4':
					switch ($format)
					{
						case 'mp4':
						case 'lp':
							$viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							if (isset($this->universalPreview->ogvLinks))
								foreach ($this->universalPreview->ogvLinks as $link)
									$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
						case 'video':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->wmvLinks))
									foreach ($this->universalPreview->wmvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name);
							}
							break;
						case 'tab':
							$viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
						case 'ogv':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->name, 'type' => 'video/ogg', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
						case 'email':
						case 'download':
						case 'favorites':
							$viewSources[] = array('title' => $this->name, 'href' => $this->path);
							break;
						default:
							$viewSources[] = array('src' => $this->link, 'href' => $this->link, 'title' => $this->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf');
							break;
					}
					break;
			}
			if (isset($viewSources))
				return $viewSources;
			return null;
		}

		public function getViewSize($format)
		{
			switch ($this->originalFormat)
			{
				case 'ppt':
				case 'pptx':
					switch ($format)
					{
						case 'ppt':
						case 'pptx':
							$fileSize = LibraryLink::formatFileSize($this->fileSize);
							break;
						case 'png':
							if (isset($this->universalPreview->pngMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pngMaxFileSize);
							break;
						case 'png_phone':
							if (isset($this->universalPreview->pngPhoneMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pngPhoneMaxFileSize);
							break;
						case 'jpeg':
							if (isset($this->universalPreview->jpegMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->jpegMaxFileSize);
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview->jpegPhoneMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->jpegPhoneMaxFileSize);
							break;
						case 'pdf':
							if (isset($this->universalPreview->pdfMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pdfMaxFileSize);
							break;
					}
					break;
				case 'doc':
				case 'docx':
					switch ($format)
					{
						case 'doc':
						case 'docx':
							$fileSize = LibraryLink::formatFileSize($this->fileSize);
							break;
						case 'png':
							if (isset($this->universalPreview->pngMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pngMaxFileSize);
							break;
						case 'png_phone':
							if (isset($this->universalPreview->pngPhoneMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pngPhoneMaxFileSize);
							break;
						case 'jpeg':
							if (isset($this->universalPreview->jpegMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->jpegMaxFileSize);
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview->jpegPhoneMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->jpegPhoneMaxFileSize);
							break;
						case 'pdf':
							if (isset($this->universalPreview->pdfMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pdfMaxFileSize);
							break;
					}
					break;
				case 'xls':
				case 'xlsx':
					switch ($format)
					{
						case 'xls':
						case 'xlsx':
							$fileSize = LibraryLink::formatFileSize($this->fileSize);
							break;
					}
					break;
				case 'pdf':
					switch ($format)
					{
						case 'pdf':
							$fileSize = LibraryLink::formatFileSize($this->fileSize);
							break;
						case 'png':
							if (isset($this->universalPreview->pngMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pngMaxFileSize);
							break;
						case 'png_phone':
							if (isset($this->universalPreview->pngPhoneMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->pngPhoneMaxFileSize);
							break;
						case 'jpeg':
							if (isset($this->universalPreview->jpegMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->jpegMaxFileSize);
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview->jpegPhoneMaxFileSize))
								$fileSize = LibraryLink::formatFileSize($this->universalPreview->jpegPhoneMaxFileSize);
							break;
					}
					break;
				case 'jpeg':
				case 'png':
					switch ($format)
					{
						case 'jpeg':
						case 'png':
							$fileSize = LibraryLink::formatFileSize($this->fileSize);
							break;
					}
					break;
				case 'key':
					switch ($format)
					{
						case 'key':
							$fileSize = self::formatFileSize($this->fileSize);
							break;
					}
					break;
			}
			if (isset($fileSize))
				return $fileSize;
			return null;
		}

		public static function attachmentComparer($x, $y)
		{
			if ($x->originalFormat == $y->originalFormat)
				return 0;
			else
				return ($x->originalFormat < $y->originalFormat) ? -1 : 1;
		}

	}
