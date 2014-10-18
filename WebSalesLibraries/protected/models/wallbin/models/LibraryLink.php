<?php

	/**
	 * Class LibraryLink
	 */
	class LibraryLink
	{
		public $parent;
		/**
		 * @var string
		 * @soap
		 */
		public $id;
		/**
		 * @var string
		 * @soap
		 */
		public $parentLinkId;
		/**
		 * @var string
		 * @soap
		 */
		public $folderId;
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
		public $fileRelativePath;
		/**
		 * @var string
		 * @soap
		 */
		public $fileName;
		/**
		 * @var string
		 * @soap
		 */
		public $fileExtension;
		/**
		 * @var string
		 * @soap
		 */
		public $fileDate;
		/**
		 * @var int
		 * @soap
		 */
		public $fileSize;
		/**
		 * @var string
		 * @soap
		 */
		public $originalFormat;
		/**
		 * @var string
		 * @soap
		 */
		public $note;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isBold;
		/**
		 * @var int
		 * @soap
		 */
		public $order;
		/**
		 * @var int
		 * @soap
		 */
		public $type;
		/**
		 * @var LineBreak
		 * @soap
		 */
		public $lineBreakProperties;
		/**
		 * @var boolean
		 * @soap
		 */
		public $enableWidget;
		/**
		 * @var string
		 * @soap
		 */
		public $widget;
		/**
		 * @var Banner
		 * @soap
		 */
		public $banner;
		/**
		 * @var string
		 * @soap
		 */
		public $previewId;
		/**
		 * @var LinkSuperFilter[]
		 * @soap
		 */
		public $superFilters;
		/**
		 * @var LinkCategory[]
		 * @soap
		 */
		public $categories;
		/**
		 * @var string
		 * @soap
		 */
		public $tags;
		/**
		 * @var string
		 * @soap
		 */
		public $dateAdd;
		/**
		 * @var string
		 * @soap
		 */
		public $dateModify;
		/**
		 * @var string
		 * @soap
		 */
		public $contentPath;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isRestricted;
		/**
		 * @var boolean
		 * @soap
		 */
		public $noShare;
		/**
		 * @var string
		 * @soap
		 */
		public $assignedUsers;
		/**
		 * @var string
		 * @soap
		 */
		public $deniedUsers;
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
		/**
		 * @var boolean
		 * @soap
		 */
		public $forcePreview;
		public $fileLink;
		public $filePath;
		public $universalPreview;
		public $browser;
		public $availableFormats;
		public $isFavorite;
		public $folderContent;
		public $tooltip;
		public $isFolder;

		/**
		 * @param $folder
		 */
		public function __construct($folder)
		{
			$this->parent = $folder;
			$this->id = uniqid();
			$this->forcePreview = false;
		}

		/**
		 * @param $linkRecord LinkRecord
		 */
		public function load($linkRecord)
		{
			$this->id = $linkRecord->id;
			$this->parentLinkId = $linkRecord->id_parent_link;
			$this->folderId = $linkRecord->id_folder;
			$this->libraryId = $linkRecord->id_library;
			$this->name = $linkRecord->name;
			$this->fileRelativePath = $linkRecord->file_relative_path;
			$this->fileName = $linkRecord->file_name;
			$this->fileExtension = $linkRecord->file_extension;
			$this->fileSize = $linkRecord->file_size;
			$this->note = $linkRecord->note;
			$this->isBold = $linkRecord->is_bold;
			$this->order = $linkRecord->order;
			$this->type = $linkRecord->type;
			$this->enableWidget = $linkRecord->enable_widget;
			$this->widget = $linkRecord->widget;
			$this->isDead = $linkRecord->is_dead;
			$this->isPreviewNotReady = $linkRecord->is_preview_not_ready;
			$this->forcePreview = $linkRecord->force_preview;
			$this->noShare = $linkRecord->no_share;
			$this->isRestricted = $linkRecord->is_restricted;
			$this->originalFormat = $linkRecord->format;

			$lineBreakRecord = LineBreakRecord::model()->findByPk($linkRecord->id_line_break);
			if ($lineBreakRecord !== null)
			{
				$this->lineBreakProperties = new LineBreak();
				$this->lineBreakProperties->load($lineBreakRecord);
			}

			$bannerRecord = BannerRecord::model()->findByPk($linkRecord->id_banner);
			if ($bannerRecord !== null)
			{
				$this->banner = new Banner();
				$this->banner->load($bannerRecord);
			}

			$linkSuperFiltersRecords = LinkSuperFilterRecord::model()->findAll('id_link=?', array($linkRecord->id));
			if ($linkSuperFiltersRecords !== null)
				foreach ($linkSuperFiltersRecords as $linkSuperFiltersRecord)
				{
					$linkSuperFilter = new LinkSuperFilter();
					$linkSuperFilter->load($linkSuperFiltersRecord);
					$this->superFilters[] = $linkSuperFilter;
				}

			$linkCategoryRecords = LinkCategoryRecord::model()->findAll('id_link=?', array($linkRecord->id));
			if ($linkCategoryRecords !== null)
				foreach ($linkCategoryRecords as $linkCategoryRecord)
				{
					$linkCategory = new LinkCategory();
					$linkCategory->load($linkCategoryRecord);
					$this->categories[] = $linkCategory;
				}

			if (isset($linkRecord->id_preview))
			{
				$this->previewId = $linkRecord->id_preview;
				$previewRecords = PreviewRecord::model()->findAll('id_container=?', array($linkRecord->id_preview));
				if (isset($previewRecords) && count($previewRecords) > 0)
				{
					$this->universalPreview = new UniversalPreviewContainer($this->parent->parent->parent);
					$this->universalPreview->load($previewRecords);
				}
			}

			if ($this->type == 5)
			{
				$this->fileName = $this->fileRelativePath;
			}
			else if ($this->type == 6)
			{

			}
			else if ($this->type == 8)
			{
				$this->fileRelativePath = str_replace('\\', '', $this->fileRelativePath);
				$this->fileName = $this->fileRelativePath;
				$this->fileLink = $this->fileRelativePath;
				$this->originalFormat = $this->originalFormat != 'url' && $this->originalFormat != 'url365' ? "url" : $this->originalFormat;
			}
			else
			{
				$this->fileRelativePath = str_replace('\\', '/', $this->fileRelativePath);
				$this->filePath = $this->parent->parent->parent->storagePath . $this->fileRelativePath;
				$this->fileLink = str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->parent->parent->storageLink . $this->fileRelativePath)));
				if (!isset($this->fileSize))
					$this->fileSize = file_exists($this->filePath) ? filesize($this->filePath) : 0;
			}

			$this->isFolder = count(LinkRecord::model()->findAll('id_parent_link=?', array($linkRecord->id))) > 0;

			$this->getFormats();
			$this->getTooltip();
		}

		public function loadFolderContent()
		{
			unset($this->folderContent);
			foreach (LinkRecord::model()->findAll('id_parent_link=? and is_dead=0 and is_preview_not_ready=0', array($this->id)) as $contentRecord)
			{
				$link = new LibraryLink($this->parent);
				$link->browser = $this->browser;
				$link->load($contentRecord);
				$this->folderContent[] = $link;
			}

			if (isset($this->folderContent))
				usort($this->folderContent, "LibraryLink::libraryChildLinkComparer");
		}

		/**
		 * @return string
		 */
		public function getWidget()
		{
			if (isset($this->parentLinkId))
			{
				if (isset($this->originalFormat))
				{
					switch ($this->originalFormat)
					{
						case 'ppt':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'pptx.png'));
						case 'doc':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'docx.png'));
						case 'xls':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'xlsx.png'));
						case 'pdf':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'pdf.png'));
						case 'video':
						case 'wmv':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'wmv.png'));
						case 'mp4':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'mp4.png'));
						case 'png':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'png.png'));
						case 'jpeg':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'jpeg.png'));
						case 'url':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'url.png'));
						case 'url365':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'url365.png'));
						case 'key':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'keynote.png'));
						default:
							if ($this->isFolder)
								return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'folder.png'));
							return $this->parent->parent->parent->getAutoWidget($this->fileExtension);
							break;
					}
				}
			}
			if (isset($this->enableWidget))
				if (isset($this->widget) && $this->widget != '')
					return $this->widget;
			if ($this->isFolder)
				return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folderWidgets' . DIRECTORY_SEPARATOR . 'folder.png'));
			return $this->parent->parent->parent->getAutoWidget($this->fileExtension);
		}

		/**
		 * @return bool
		 */
		public function getIsLineBreak()
		{
			return $this->type == 6 && isset($this->lineBreakProperties);
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
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
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
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
						}
						break;
					case 'xls':
						$this->availableFormats[] = 'xls';
						if (!$this->forcePreview)
						{
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
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
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
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
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
							if ($this->browser != 'phone')
								$this->availableFormats[] = 'download';
						}
						break;
					case 'png':
						$this->availableFormats[] = 'png';
						if (!$this->forcePreview)
						{
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
							$this->availableFormats[] = 'download';
						}
						break;
					case 'jpeg':
						if (!$this->forcePreview)
						{
							$this->availableFormats[] = 'jpeg';
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
							$this->availableFormats[] = 'download';
						}
						break;
					case 'url':
						$this->availableFormats[] = 'url';
						$this->availableFormats[] = 'outlook';
						break;
					case 'url365':
						$this->availableFormats[] = 'url365';
						$this->availableFormats[] = 'outlook';
						break;
					case 'key':
						$this->availableFormats[] = 'key';
						if (!$this->forcePreview)
						{
							if (!$this->noShare)
								$this->availableFormats[] = 'outlook';
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

		/**
		 * @param $format
		 * @return array|null
		 */
		public function getViewSource($format)
		{
			switch ($this->originalFormat)
			{
				case 'ppt':
					switch ($format)
					{
						case 'ppt':
						case 'lp':
							$viewSources[] = array('href' => $this->fileLink);
							break;
						case 'outlook':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
						case 'png':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->pngLinks);
									foreach ($this->universalPreview->pngLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'PNG Viewer - ' . ($this->fileName . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->fileName . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'JPEG Viewer - ' . ($this->fileName . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link);
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
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->fileName . ' - Slide ' . ($i + 1) . ' of ' . $count), 'href' => $link);
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
							$viewSources[] = array('href' => $this->fileLink);
							break;
						case 'outlook':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
						case 'png':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngLinks))
								{
									$i = 0;
									$count = count($this->universalPreview->pngLinks);
									foreach ($this->universalPreview->pngLinks as $link)
									{
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'PNG Viewer - ' . ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => 'JPEG Viewer - ' . ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link);
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
										$viewSources[] = array('id' => 'link' . $this->id . '---' . $i, 'title' => ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link);
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
							$viewSources[] = array('href' => $this->fileLink);
							break;
						case 'outlook':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
					}
					break;
				case 'pdf':
					switch ($format)
					{
						case 'pdf':
						case 'lp':
							$viewSources[] = array('href' => $this->fileLink);
							break;
						case 'outlook':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
						case 'png':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->pngLinks))
								{
									$i = 1;
									$count = count($this->universalPreview->pngLinks);
									foreach ($this->universalPreview->pngLinks as $link)
									{
										$viewSources[] = array('title' => 'PNG Viewer - ' . ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
										$viewSources[] = array('title' => ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link, 'href_mobile' => $link);
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
										$viewSources[] = array('title' => 'JPEG Viewer - ' . ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link);
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
										$viewSources[] = array('title' => ($this->fileName . ' - Page ' . ($i + 1) . ' of ' . $count), 'href' => $link);
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
						case 'jpeg_phone':
						case 'png':
						case 'png_phone':
						case 'lp':
						case 'thumbs':
							$viewSources[] = array('id' => 'link' . $this->id, 'title' => strtoupper(str_replace('_phone', '', $format)) . ' Viewer - ' . $this->fileName, 'href' => $this->fileLink, 'href_mobile' => $this->fileLink);
							break;
						case 'outlook':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
					}
					break;
				case 'key':
					switch ($format)
					{
						case 'key':
						case 'lp':
							$viewSources[] = array('href' => $this->fileLink);
							break;
						case 'outlook':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
					}
					break;
				case 'url':
				case 'url365':
				case 'other':
					$viewSources[] = array('href' => $this->fileLink);
					break;
				case 'video':
					switch ($format)
					{
						case 'video':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->wmvLinks))
									foreach ($this->universalPreview->wmvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName);
								else
									$viewSources[] = array('src' => $this->fileLink, 'href' => $this->fileLink, 'title' => $this->fileName);
							}
							else
								$viewSources[] = array('src' => $this->fileLink, 'href' => $this->fileLink, 'title' => $this->fileName);
							break;
						case 'mp4':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/mp4', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/ogg', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							}
							break;
						case 'tab':
						case 'lp':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/mp4', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							break;
						case 'ogv':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/ogg', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							break;
						case 'outlook':
						case 'download':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
					}
					break;
				case 'wmv':
					switch ($format)
					{
						case 'mp4':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/mp4', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/ogg', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							}
							break;
						case 'video':
							$viewSources[] = array('src' => $this->fileLink, 'href' => $this->fileLink, 'title' => $this->fileName);
							break;
						case 'tab':
						case 'lp':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/mp4', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							break;
						case 'ogv':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/ogg', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							break;
						case 'outlook':
						case 'download':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
					}
					break;
				case 'mp4':
					switch ($format)
					{
						case 'mp4':
						case 'lp':
							$viewSources[] = array('src' => $this->fileLink, 'href' => $this->fileLink, 'title' => $this->fileName, 'type' => 'video/mp4', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							if (isset($this->universalPreview->ogvLinks))
								foreach ($this->universalPreview->ogvLinks as $link)
									$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/ogg', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							break;
						case 'video':
							if (isset($this->universalPreview))
							{
								if (isset($this->universalPreview->wmvLinks))
									foreach ($this->universalPreview->wmvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName);
							}
							break;
						case 'tab':
							$viewSources[] = array('src' => $this->fileLink, 'href' => $this->fileLink, 'title' => $this->fileName, 'type' => 'video/mp4', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->mp4Links))
									foreach ($this->universalPreview->mp4Links as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/mp4', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							break;
						case 'ogv':
							if (isset($this->universalPreview))
								if (isset($this->universalPreview->ogvLinks))
									foreach ($this->universalPreview->ogvLinks as $link)
										$viewSources[] = array('src' => $link, 'href' => $link, 'title' => $this->fileName, 'type' => 'video/ogg', 'swf' => Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf');
							break;
						case 'outlook':
						case 'download':
						case 'favorites':
							$viewSources[] = array('title' => $this->fileName, 'href' => $this->filePath);
							break;
					}
					break;
			}
			if (isset($viewSources))
				return $viewSources;
			return null;
		}

		/**
		 * @param $fileSize
		 * @return string
		 */
		public static function formatFileSize($fileSize)
		{
			$type = '';
			if (isset($fileSize))
			{
				if ($fileSize < 524288000)
				{
					if ($fileSize < 512000)
						$type = 'kb';
					else
						$type = 'mb';
				}
				else
					$type = 'gb';
				switch ($type)
				{
					case "kb":
						$fileSize = $fileSize * .0009765625; // bytes to KB
						break;
					case "mb":
						$fileSize = ($fileSize * .0009765625) * .0009765625; // bytes to MB
						break;
					case "gb":
						$fileSize = (($fileSize * .0009765625) * .0009765625) * .0009765625; // bytes to GB
						break;
				}
			}
			else
				$fileSize = -1;
			if ($fileSize <= 0)
				return '';
			else
				return round($fileSize, 0) . $type;
		}

		/**
		 * @param $format
		 * @return null|string
		 */
		public function getViewSize($format)
		{
			switch ($this->originalFormat)
			{
				case 'ppt':
					switch ($format)
					{
						case 'ppt':
							$fileSize = self::formatFileSize($this->fileSize);
							break;
						case 'png':
							if (isset($this->universalPreview->pngMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pngMaxFileSize);
							break;
						case 'png_phone':
							if (isset($this->universalPreview->pngPhoneMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pngPhoneMaxFileSize);
							break;
						case 'jpeg':
							if (isset($this->universalPreview->jpegMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->jpegMaxFileSize);
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview->jpegPhoneMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->jpegPhoneMaxFileSize);
							break;
						case 'pdf':
							if (isset($this->universalPreview->pdfMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pdfMaxFileSize);
							break;
					}
					break;
				case 'doc':
					switch ($format)
					{
						case 'doc':
							$fileSize = self::formatFileSize($this->fileSize);
							break;
						case 'png':
							if (isset($this->universalPreview->pngMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pngMaxFileSize);
							break;
						case 'png_phone':
							if (isset($this->universalPreview->pngPhoneMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pngPhoneMaxFileSize);
							break;
						case 'jpeg':
							if (isset($this->universalPreview->jpegMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->jpegMaxFileSize);
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview->jpegPhoneMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->jpegPhoneMaxFileSize);
							break;
						case 'pdf':
							if (isset($this->universalPreview->pdfMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pdfMaxFileSize);
							break;
					}
					break;
				case 'xls':
					switch ($format)
					{
						case 'xls':
							$fileSize = self::formatFileSize($this->fileSize);
							break;
					}
					break;
				case 'pdf':
					switch ($format)
					{
						case 'pdf':
							$fileSize = self::formatFileSize($this->fileSize);
							break;
						case 'png':
							if (isset($this->universalPreview->pngMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pngMaxFileSize);
							break;
						case 'png_phone':
							if (isset($this->universalPreview->pngPhoneMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->pngPhoneMaxFileSize);
							break;
						case 'jpeg':
							if (isset($this->universalPreview->jpegMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->jpegMaxFileSize);
							break;
						case 'jpeg_phone':
							if (isset($this->universalPreview->jpegPhoneMaxFileSize))
								$fileSize = self::formatFileSize($this->universalPreview->jpegPhoneMaxFileSize);
							break;
					}
					break;
				case 'jpeg':
				case 'png':
					switch ($format)
					{
						case 'jpeg':
						case 'png':
							$fileSize = self::formatFileSize($this->fileSize);
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

		/**
		 * @return string
		 */
		public function getTagsString()
		{
			if (isset($this->superFilters))
				foreach ($this->superFilters as $superFilter)
					$superFiltersString[] = $superFilter->value;
			if (isset($this->categories))
				foreach ($this->categories as $category)
					$superFiltersString[] = $category->tag;
			if (isset($superFiltersString))
				return implode(', ', $superFiltersString);
			return '';
		}

		/**
		 * @param $x
		 * @param $y
		 * @return int
		 */
		public static function libraryLinkComparer($x, $y)
		{
			if ($x->order == $y->order)
			{
				return strcmp($x->name, $x->name);
			}
			else
				return ($x->order < $y->order) ? -1 : 1;
		}

		/**
		 * @param $x
		 * @param $y
		 * @return int
		 */
		public static function libraryChildLinkComparer($x, $y)
		{
			return strcasecmp($x->name, $y->name);
		}
	}