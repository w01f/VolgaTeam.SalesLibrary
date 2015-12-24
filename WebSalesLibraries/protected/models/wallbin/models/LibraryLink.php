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
		 * @var LinkSettings
		 * @soap
		 */
		public $extendedProperties;
		/**
		 * @var LineBreak
		 * @soap
		 */
		public $lineBreakProperties;
		/**
		 * @var int
		 * @soap
		 */
		public $widgetType;
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
		public $isDead;
		public $fileLink;
		public $filePath;
		public $universalPreview;
		public $browser;
		public $isFavorite;
		public $folderContent;
		public $tooltip;
		public $isFolder;
		public $isLineBreak;
		public $isDirectUrl;

		/**
		 * @param $folder
		 */
		public function __construct($folder)
		{
			$this->parent = $folder;
			$this->id = uniqid();
			$this->extendedProperties = new LinkSettings();
			$this->extendedProperties->forcePreview = false;
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
			$this->order = $linkRecord->order;
			$this->type = $linkRecord->type;
			$this->widgetType = $linkRecord->widget_type;
			$this->widget = $linkRecord->widget;
			$this->isDead = $linkRecord->is_dead;

			$this->originalFormat = $linkRecord->format;
			if ($this->type == 8)
				$this->originalFormat = $this->originalFormat != 'url' && $this->originalFormat != 'url365' ? "url" : $this->originalFormat;

			$this->extendedProperties = CJSON::decode($linkRecord->properties, false);

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
			}
			else
			{
				$this->fileRelativePath = str_replace('\\', '/', $this->fileRelativePath);
				$this->filePath = $this->parent->parent->parent->storagePath . $this->fileRelativePath;
				$this->fileLink = str_replace('&', '%26', str_replace('&amp;', '%26', str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->parent->parent->storageLink . $this->fileRelativePath)))));
				if (!isset($this->fileSize))
					$this->fileSize = file_exists($this->filePath) ? filesize($this->filePath) : 0;
			}

			$this->isFolder = count(LinkRecord::model()->findAll('id_parent_link=?', array($linkRecord->id))) > 0;
			$this->isLineBreak = isset($this->lineBreakProperties);
			$this->isDirectUrl = $this->type == 8 && $this->extendedProperties->forcePreview;

			$this->getTooltip();
		}

		public function loadFolderContent()
		{
			unset($this->folderContent);
			$this->folderContent = array();
			foreach (LinkRecord::model()->findAll('id_parent_link=? and is_dead=0 and is_preview_not_ready=0', array($this->id)) as $contentRecord)
			{
				$link = new LibraryLink($this->parent);
				$link->load($contentRecord);
				$this->folderContent[] = $link;
			}

			usort($this->folderContent, "LibraryLink::libraryChildLinkComparer");
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
					switch ($this->originalFormat)
					{
						case 'ppt':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'pptx.png'));
						case 'doc':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'docx.png'));
						case 'xls':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'xlsx.png'));
						case 'pdf':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'pdf.png'));
						case 'video':
						case 'wmv':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'wmv.png'));
						case 'mp4':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'mp4.png'));
						case 'png':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'png.png'));
						case 'jpeg':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'jpeg.png'));
						case 'url':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'url.png'));
						case 'url365':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'url365.png'));
						case 'mp3':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'mp3.png'));
						case 'key':
							return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'keynote.png'));
						default:
							if ($this->isFolder)
								return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'folder.png'));
							return $this->parent->parent->parent->getAutoWidget($this->fileExtension);
							break;
					}
				}
				return null;
			}
			else
			{
				if ($this->isFolder)
					return base64_encode(file_get_contents(realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'folder-file-icons' . DIRECTORY_SEPARATOR . 'folder.png'));
				else
				{
					switch ($this->widgetType)
					{
						case 1:
							return null;
						case 2:
							return $this->parent->parent->parent->getAutoWidget($this->fileExtension);
						case 3:
							return isset($this->widget) && $this->widget != '' ? $this->widget : null;
							break;
						default:
							return null;
					}
				}
			}
		}

		/**
		 * @return bool
		 */
		public function getIsLineBreak()
		{
			return $this->type == 6 && isset($this->lineBreakProperties);
		}

		private function getTooltip()
		{
			$tooltipList = array();

			$isLineBreak = $this->getIsLineBreak();

			if (!$isLineBreak && isset($this->extendedProperties->hoverNote) && $this->extendedProperties->hoverNote != '')
			{
				$tooltipList[] = $this->extendedProperties->hoverNote;
				if (!$this->isFolder && isset($this->fileName))
					$tooltipList[] = $this->fileName;
				else if (!$this->getIsLineBreak())
					$tooltipList[] = $this->name;
			}
			else if ($isLineBreak && isset($this->lineBreakProperties->note) && $this->lineBreakProperties->note != '')
				$tooltipList[] = $this->lineBreakProperties->note;

			if ($this->isFolder)
				$tooltipList[] = 'Folder';
			else if (isset($this->originalFormat) && array_key_exists($this->originalFormat, Yii::app()->params['tooltips']['wallbin']))
				$tooltipList[] = Yii::app()->params['tooltips']['wallbin'][$this->originalFormat];

			if (count($tooltipList) > 0)
				$this->tooltip = implode('<br><br>', $tooltipList);
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

		/**
		 * @param $isQuickSite boolean
		 * @return PreviewData
		 */
		public function getPreviewData($isQuickSite)
		{
			return PreviewData::getInstance($this, $isQuickSite);
		}

		/**
		 * @return string
		 */
		public function getLinkData()
		{
			$result = '';
			$result .= '<div class="activity-data">' .
				CJSON::encode(array(
					'title' => $this->name,
					'fileName' => $this->fileName,
					'format' => $this->originalFormat
				)) .
				'</div>';
			return $result;
		}
	}