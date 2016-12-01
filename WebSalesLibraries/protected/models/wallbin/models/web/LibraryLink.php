<?
	namespace application\models\wallbin\models\web;

	/**
	 * Class LibraryLink
	 */
	class LibraryLink
	{
		public $parent;
		public $id;
		public $parentLinkId;
		public $folderId;
		public $libraryId;
		public $name;
		public $fileRelativePath;
		public $fileName;
		public $fileExtension;
		public $fileDate;
		public $fileSize;
		public $originalFormat;
		public $searchFormat;
		public $order;
		public $type;
		/**
		 * @var \BaseLinkSettings|\VideoLinkSettings|\HyperLinkSettings|\DocumentLinkSettings|\PowerPointLinkSettings|\AppLinkSettings|\InternalWallbinLinkSettings|\InternalLibraryPageLinkSettings|\InternalLibraryFolderLinkSettings|\InternalLibraryObjectLinkSettings|\InternalShortcutLinkSettings|\QPageLinkSettings|\LinkBundleLinkSettings
		 */
		public $extendedProperties;
		/**
		 * @var \LineBreak
		 */
		public $lineBreakProperties;
		public $widgetType;
		public $widget;
		/**
		 * @var Banner
		 */
		public $banner;
		public $previewId;
		/**
		 * @var \LinkSuperFilter[]
		 */
		public $superFilters;
		/**
		 * @var \LinkCategory[]
		 */
		public $categories;
		public $tags;
		public $dateAdd;
		public $dateModify;
		public $contentPath;
		public $isDead;
		public $fileLink;
		public $filePath;
		public $universalPreview;
		public $browser;
		public $folderContent;
		public $tooltip;

		public $isFavorite;
		public $isFolder;
		public $isLineBreak;
		public $isDirectUrl;
		public $isExternalUrl;
		public $isAppLink;

		/**
		 * @param $folder
		 */
		public function __construct($folder)
		{
			$this->parent = $folder;
			$this->id = uniqid();
			$this->extendedProperties = new \BaseLinkSettings();
		}

		/**
		 * @param $linkRecord \LinkRecord
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
			$this->order = $linkRecord->order;
			$this->type = $linkRecord->type;
			$this->widgetType = $linkRecord->widget_type;
			$this->widget = $linkRecord->widget;
			$this->isDead = $linkRecord->is_dead;
			$this->originalFormat = $linkRecord->original_format;
			$this->searchFormat = $linkRecord->search_format;
			$this->extendedProperties = \BaseLinkSettings::createByLink($linkRecord);

			$lineBreakRecord = \LineBreakRecord::model()->findByPk($linkRecord->id_line_break);
			if ($lineBreakRecord !== null)
			{
				$this->lineBreakProperties = new \LineBreak();
				$this->lineBreakProperties->load($lineBreakRecord);
			}

			$bannerRecord = \BannerRecord::model()->findByPk($linkRecord->id_banner);
			if ($bannerRecord !== null)
			{
				$this->banner = new Banner();
				$this->banner->load($bannerRecord);
			}

			$linkSuperFiltersRecords = \LinkSuperFilterRecord::model()->findAll('id_link=?', array($linkRecord->id));
			if ($linkSuperFiltersRecords !== null)
				foreach ($linkSuperFiltersRecords as $linkSuperFiltersRecord)
				{
					$linkSuperFilter = new \LinkSuperFilter();
					$linkSuperFilter->load($linkSuperFiltersRecord);
					$this->superFilters[] = $linkSuperFilter;
				}

			$linkCategoryRecords = \LinkCategoryRecord::model()->findAll('id_link=?', array($linkRecord->id));
			if ($linkCategoryRecords !== null)
				foreach ($linkCategoryRecords as $linkCategoryRecord)
				{
					$linkCategory = new \LinkCategory();
					$linkCategory->load($linkCategoryRecord);
					$this->categories[] = $linkCategory;
				}

			if (isset($linkRecord->id_preview))
			{
				$this->previewId = $linkRecord->id_preview;
				$previewRecords = \PreviewRecord::model()->findAll('id_container=?', array($linkRecord->id_preview));
				if (isset($previewRecords) && count($previewRecords) > 0)
				{
					$this->universalPreview = new \UniversalPreviewContainer($this->parent->parent->parent);
					$this->universalPreview->load($previewRecords);
				}
			}

			$fileInfo = \FileInfo::fromLinkRecord($linkRecord, $this->parent->parent->parent);
			$this->fileName = isset($fileInfo->name) ? $fileInfo->name : $this->fileName;
			$this->filePath = isset($fileInfo->path) ? $fileInfo->path : $this->filePath;
			$this->fileLink = isset($fileInfo->link) ? $fileInfo->link : $this->fileLink;
			$this->fileSize = !isset($this->fileSize) ? $fileInfo->size : $this->fileSize;

			$this->isFolder = $this->originalFormat == 'folder' || count(\LinkRecord::model()->findAll('id_parent_link=?', array($linkRecord->id))) > 0;
			$this->isLineBreak = $this->originalFormat == 'line break' || ($this->type == 6 && isset($this->lineBreakProperties));
			$this->isAppLink = $this->type == 15;

			$this->isDirectUrl = self::isHyperlink($this->type, $this->extendedProperties);

			$this->isExternalUrl = false;
			if ($this->isDirectUrl)
			{
				$currentDomainInfo = parse_url(\Yii::app()->getBaseUrl(true));
				$urlInfo = parse_url($this->fileLink);

				$domainHost = $currentDomainInfo['host'];
				$urlHost = array_key_exists('host', $urlInfo) ? $urlInfo['host'] : null;
				$urlPath = array_key_exists('path', $urlInfo) ? strtolower($urlInfo['path']) : null;

				$this->isExternalUrl = $domainHost != $urlHost ||
					(isset($urlPath) &&
						(strpos($urlPath, 'qpage') ||
							strpos($urlPath, 'public_links') ||
							strpos($urlPath, 'getSinglePage')));
			}

			$this->getTooltip();
		}

		public function loadFolderContent()
		{
			unset($this->folderContent);
			$this->folderContent = array();
			foreach (\LinkRecord::model()->findAll('id_parent_link=? and is_dead=0 and is_preview_not_ready=0', array($this->id)) as $contentRecord)
			{
				/** @var  $contentRecord \LinkRecord */
				$link = new LibraryLink($this->parent);
				$link->load($contentRecord);
				$this->folderContent[] = $link;
			}

			usort($this->folderContent, "application\\models\\wallbin\\models\\web\\LibraryLink::libraryChildLinkComparer");
		}

		/**
		 * @return string
		 */
		public function getWidget()
		{
			if (isset($this->parentLinkId) && isset($this->originalFormat))
			{
				if (isset($this->originalFormat))
				{
					$fileName = null;
					switch ($this->originalFormat)
					{
						case 'ppt':
							$fileName = 'pptx.png';
							break;
						case 'doc':
							$fileName = 'docx.png';
							break;
						case 'xls':
							$fileName = 'xlsx.png';
							break;
						case 'pdf':
							$fileName = 'pdf.png';
							break;
						case 'video':
						case 'wmv':
							$fileName = 'wmv.png';
							break;
						case 'mp4':
							$fileName = 'mp4.png';
							break;
						case 'png':
							$fileName = 'png.png';
							break;
						case 'jpeg':
							$fileName = 'jpeg.png';
							break;
						case 'url':
						case 'youtube':
						case 'quicksite':
						case 'html5':
						case 'app':
						case 'internal library':
						case 'internal page':
						case 'internal window':
						case 'internal link':
						case 'internal shortcut':
							$fileName = 'url.png';
							break;
						case 'mp3':
							$fileName = 'mp3.png';
							break;
						case 'key':
							$fileName = 'keynote.png';
							break;
						case 'folder':
							$fileName = 'folder.png';
							break;
						default:
							if ($this->isFolder)
								$fileName = 'folder.png';
							break;
					}
					if (isset($fileName))
					{
						$backColor = $this->parent->windowBackColor;
						$colorPrefix = \Utils::isColorLight($backColor) ? 'white' : 'black';
						return base64_encode(file_get_contents(realpath(\Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . $colorPrefix . DIRECTORY_SEPARATOR . $fileName));
					}
					else
						return $this->parent->parent->parent->getAutoWidget($this->fileExtension);
				}
				return null;
			}
			else
			{
				$widget = null;
				switch ($this->widgetType)
				{
					case 1:
						$widget = null;
						break;
					case 2:
						$widget = $this->parent->parent->parent->getAutoWidget($this->fileExtension);
						break;
					case 3:
						$widget = isset($this->widget) && $this->widget != '' ? $this->widget : null;
						break;
					default:
						$widget = null;
				}
				if (!isset($widget) && $this->isFolder)
				{
					$backColor = $this->parent->windowBackColor;
					$colorPrefix = \Utils::isColorLight($backColor) ? 'white' : 'black';
					$widget = base64_encode(file_get_contents(realpath(\Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . $colorPrefix . DIRECTORY_SEPARATOR . 'folder.png'));
				}
				return $widget;
			}
		}

		/**
		 * @return bool
		 */
		public function getIsLineBreak()
		{
			return $this->type == 6 && isset($this->lineBreakProperties);
		}

		/**
		 * @return string
		 */
		private function getTooltip()
		{
			$tooltipList = array();

			if ($this->isLineBreak)
			{
				if (!empty($this->lineBreakProperties->note))
					$tooltipList[] = $this->lineBreakProperties->note;
			}
			else
			{
				if (!empty($this->extendedProperties->hoverNote))
					$tooltipList[] = $this->extendedProperties->hoverNote;

				if (!isset($this->extendedProperties->showOnlyCustomHoverNote) || !$this->extendedProperties->showOnlyCustomHoverNote)
				{
					if (!$this->isFolder && isset($this->fileName))
						$tooltipList[] = $this->fileName;
					else
						$tooltipList[] = $this->name;
					if ($this->isFolder)
						$tooltipList[] = 'Folder';
					else
					{
						if (isset($this->originalFormat) && $this->originalFormat != 'other')
							$formatKey = $this->originalFormat;
						else
							$formatKey = $this->fileExtension;
						if (isset($formatKey) && array_key_exists($formatKey, \Yii::app()->params['tooltips']['wallbin']))
							$tooltipList[] = \Yii::app()->params['tooltips']['wallbin'][$formatKey];
					}
				}
			}
			if (count($tooltipList) > 0)
				$this->tooltip = implode(PHP_EOL, $tooltipList);
			else
				$this->tooltip = null;
		}

		/**
		 * @return string
		 */
		public function getTagsString()
		{
			if (isset($this->superFilters))
				foreach ($this->superFilters as $superFilter)
					$tagsArray[] = $superFilter->value;
			if (isset($this->categories))
				foreach ($this->categories as $category)
					$tagsArray[] = $category->tag;
			if (isset($tagsArray))
				return implode(', ', $tagsArray);
			return '';
		}

		/**
		 * @param $isQuickSite boolean
		 * @return \PreviewData
		 */
		public function getPreviewData($isQuickSite)
		{
			return \PreviewData::getInstance($this, $isQuickSite);
		}

		/**
		 * @return string
		 */
		public function getLinkData()
		{
			$result = '';

			if (isset($this->fileLink))
			{
				if (in_array($this->originalFormat, array('url', 'quicksite', 'html5', 'youtube')))
				{
					$downloadHeader = 'URL';
					$downloadLink = $this->fileLink;
				}
				else
				{
					$downloadHeader = 'DownloadURL';
					$downloadLink = \FileInfo::getFileMIME($this->originalFormat) . ':' .
						$this->fileName . ':' .
						str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $this->fileLink);
				}
				$result .= '<div class="download-header">' . $downloadHeader . '</div>';
				$result .= '<div class="download-link">' . $downloadLink . '</div>';
			}

			$result .= '<div class="activity-data">' .
				\CJSON::encode(array(
					'id' => $this->id,
					'title' => $this->name,
					'fileName' => $this->fileName,
					'format' => $this->originalFormat
				)) .
				'</div>';
			return $result;
		}

		/**
		 * @return int
		 */
		public function getTotalViews()
		{
			/** @var int $totalLinkViews */
			$totalLinkViews = \StatisticLinkRecord::model()->count('id_link=?', array($this->id));
			if ($this->originalFormat == 'quicksite')
			{
				/** @var  $quickSiteSettings \QPageLinkSettings */
				$quickSiteSettings = $this->extendedProperties;
				/** @var int $quickSiteViews */
				$quickSiteViews = \StatisticQPageRecord::model()->count('id_qpage=?', array($quickSiteSettings->qpageId));
				if ($quickSiteViews > 0)
					$totalLinkViews += $quickSiteViews;
			}
			return $totalLinkViews;
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
			return strnatcasecmp($x->name, $y->name);
		}

		/**
		 * @param $type int
		 * @param $extendedProperties \BaseLinkSettings
		 * @return boolean
		 */
		public static function isHyperlink($type, $extendedProperties)
		{
			switch ($type)
			{
				case 8:
				case 10:
				case 12:
				case 17:
				case 18:
					/** @var \VideoLinkSettings|\DocumentLinkSettings|\HyperLinkSettings $urlLinkSettings */
					$urlLinkSettings = $extendedProperties;
					return $urlLinkSettings->forcePreview;
				case 16:
					/** @var \InternalLibraryFolderLinkSettings|\InternalLibraryObjectLinkSettings|\InternalLibraryPageLinkSettings|\InternalShortcutLinkSettings|\InternalWallbinLinkSettings $internalLinkSettings */
					$internalLinkSettings = $extendedProperties;
					if ($internalLinkSettings->internalLinkType == 5)
					{
						/** @var \InternalShortcutLinkSettings $shortcutLinkSettings */
						$shortcutLinkSettings = $internalLinkSettings;
						return !$shortcutLinkSettings->openOnSamePage;
					}
					return false;
				default:
					return false;
			}
		}
	}