<?php

	/**
	 * Class LibraryFolder
	 */
	class LibraryFolder
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
		public $pageId;
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
		 * @var int
		 * @soap
		 */
		public $rowOrder;
		/**
		 * @var int
		 * @soap
		 */
		public $columnOrder;
		/**
		 * @var string
		 * @soap
		 */
		public $borderColor;
		/**
		 * @var string
		 * @soap
		 */
		public $windowBackColor;
		/**
		 * @var string
		 * @soap
		 */
		public $windowForeColor;
		/**
		 * @var string
		 * @soap
		 */
		public $headerBackColor;
		/**
		 * @var string
		 * @soap
		 */
		public $headerForeColor;
		/**
		 * @var Font
		 * @soap
		 */
		public $windowFont;
		/**
		 * @var Font
		 * @soap
		 */
		public $headerFont;
		/**
		 * @var string
		 * @soap
		 */
		public $headerAlignment;
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
		 * @var LibraryLink[]
		 * @soap
		 */
		public $files;
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
		public $displayLinkWidgets;

		public function __construct($page)
		{
			$this->parent = $page;
		}

		public function load($folderRecord)
		{
			$this->id = $folderRecord->id;
			$this->pageId = $folderRecord->id_page;
			$this->libraryId = $folderRecord->id_library;
			$this->name = $folderRecord->name;
			$this->columnOrder = $folderRecord->column_order;
			$this->rowOrder = $folderRecord->row_order;
			$this->borderColor = $folderRecord->border_color;
			$this->windowBackColor = $folderRecord->window_back_color;
			$this->windowForeColor = $folderRecord->window_fore_color;
			$this->headerBackColor = $folderRecord->header_back_color;
			$this->headerForeColor = $folderRecord->header_fore_color;
			$this->windowFont = new Font();
			$this->windowFont->name = $folderRecord->window_font_name;
			$this->windowFont->size = $folderRecord->window_font_size;
			$this->windowFont->isBold = $folderRecord->window_font_bold;
			$this->windowFont->isItalic = $folderRecord->window_font_italic;
			$this->headerFont = new Font();
			$this->headerFont->name = $folderRecord->header_font_name;
			$this->headerFont->size = $folderRecord->header_font_size;
			$this->headerFont->isBold = $folderRecord->header_font_bold;
			$this->headerFont->isItalic = $folderRecord->header_font_italic;
			$this->headerAlignment = $folderRecord->header_alignment;
			$this->enableWidget = $folderRecord->enable_widget;
			$this->widget = $folderRecord->widget;

			$bannerRecord = BannerRecord::model()->findByPk($folderRecord->id_banner);
			if ($bannerRecord !== null)
			{
				$this->banner = new Banner();
				$this->banner->load($bannerRecord);
			}
		}


		/**
		 * @param $usePermissionsFilter boolean
		 */
		public function loadFiles($usePermissionsFilter)
		{
			unset($this->files);
			$linkRecords = LinkRecord::getLinksByFolder($this->id);
			if ($usePermissionsFilter)
				$linkRecords = LinkRecord::applyPermissionsFilter($linkRecords);
			if (isset($linkRecords))
				foreach ($linkRecords as $linkRecord)
				{
					$link = new LibraryLink($this);
					$link->load($linkRecord);
					if ($usePermissionsFilter)
						$link->isRestricted = false;
					$this->files[] = $link;
				}

			if (isset($this->files))
				usort($this->files, "LibraryLink::libraryLinkComparer");

			$this->displayLinkWidgets = false;
			if (isset($this->files))
				foreach ($this->files as $file)
				{
					$widget = $file->getWidget();
					if ($widget != null && $widget != '')
					{
						$this->displayLinkWidgets = true;
						break;
					}
				}
		}

		/**
		 * @return int
		 */
		public function getRealLinksNumber()
		{
			$count = 0;
			$linkRecords = LinkRecord::getLinksByFolder($this->id);
			$linkRecords = LinkRecord::applyPermissionsFilter($linkRecords);
			if (isset($linkRecords))
				foreach ($linkRecords as $linkRecord)
					switch ($linkRecord->type)
					{
						case 6:
							break;
						case 5:
							$count += LinkRecord::enumFolderContent($linkRecord->id);
							break;
						default:
							$count++;
							break;
					}
			return $count;
		}

		/**
		 * @return null|string
		 */
		public function getWidget()
		{
			if (isset($this->enableWidget))
				if (isset($this->widget))
					if ($this->widget != '')
						return $this->widget;
			return null;
		}

		/**
		 * @param $x
		 * @param $y
		 * @return int
		 */
		public static function libraryFolderComparer($x, $y)
		{
			if ($x->rowOrder == $y->rowOrder)
			{
				if ($x->columnOrder == $y->columnOrder)
					return 0;
				else
					return ($x->columnOrder < $y->columnOrder) ? -1 : 1;
			}
			else
				return ($x->rowOrder < $y->rowOrder) ? -1 : 1;
		}

	}
