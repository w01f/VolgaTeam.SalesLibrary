<?
	namespace application\models\wallbin\models\web;

	/**
	 * Class LibraryFolder
	 */
	class LibraryFolder
	{
		public $parent;
		public $id;
		public $pageId;
		public $libraryId;
		public $name;
		public $rowOrder;
		public $columnOrder;
		public $borderColor;
		public $windowBackColor;
		public $windowForeColor;
		public $headerBackColor;
		public $headerForeColor;
		/**
		 * @var \Font
		 */
		public $windowFont;
		/**
		 * @var \Font
		 * @soap
		 */
		public $headerFont;
		public $headerAlignment;
		public $enableWidget;
		public $widget;
		public $banner;
		/**
		 * @var LibraryLink[]
		 */
		public $files;
		public $dateAdd;
		public $dateModify;
		public $headerHeight;

		public function __construct($page)
		{
			$this->parent = $page;
		}

		/**
		 * @param $folderRecord
		 */
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
			$this->windowFont = new \Font();
			$this->windowFont->name = $folderRecord->window_font_name;
			$this->windowFont->size = $folderRecord->window_font_size;
			$this->windowFont->isBold = $folderRecord->window_font_bold;
			$this->windowFont->isItalic = $folderRecord->window_font_italic;
			$this->windowFont->isUnderlined = $folderRecord->window_font_underline;
			$this->headerFont = new \Font();
			$this->headerFont->name = $folderRecord->header_font_name;
			$this->headerFont->size = $folderRecord->header_font_size;
			$this->headerFont->isBold = $folderRecord->header_font_bold;
			$this->headerFont->isItalic = $folderRecord->header_font_italic;
			$this->headerFont->isUnderlined = $folderRecord->header_font_underline;
			$this->headerAlignment = $folderRecord->header_alignment;
			$this->enableWidget = $folderRecord->enable_widget;
			$this->widget = $folderRecord->widget;

			$bannerRecord = \BannerRecord::model()->findByPk($folderRecord->id_banner);
			if ($bannerRecord !== null)
			{
				$this->banner = new Banner();
				$this->banner->load($bannerRecord);
			}

			$headerImage = null;
			$widget = $this->getWidget();
			if (isset($widget) && $widget != '')
				$headerImage = @imagecreatefromstring(base64_decode($widget));
			if (isset($headerImage))
				$this->headerHeight = @imagesy($headerImage) + 2;
			else
				$this->headerHeight = 34;
		}


		/**
		 * @param $usePermissionsFilter boolean
		 */
		public function loadFiles($usePermissionsFilter)
		{
			unset($this->files);
			$this->files = array();
			$linkRecords = \LinkRecord::getLinksByFolder($this->id);
			if ($usePermissionsFilter)
				$linkRecords = \LinkRecord::applyPermissionsFilter($linkRecords);
			if (isset($linkRecords))
				foreach ($linkRecords as $linkRecord)
				{
					$link = new LibraryLink($this);
					$link->load($linkRecord);
					if ($usePermissionsFilter)
						$link->extendedProperties->isRestricted = false;
					if (!$usePermissionsFilter || !$link->isAppLink || ($link->isAppLink && \Yii::app()->browser->getBrowser() == \Browser::BROWSER_EO))
						$this->files[] = $link;
				}

			usort($this->files, "application\\models\\wallbin\\models\\web\\LibraryLink::libraryLinkComparer");
		}

		/**
		 * @return int
		 */
		public function getRealLinksNumber()
		{
			$count = 0;
			$linkRecords = \LinkRecord::getLinksByFolder($this->id);
			$linkRecords = \LinkRecord::applyPermissionsFilter($linkRecords);
			if (isset($linkRecords))
				foreach ($linkRecords as $linkRecord)
					switch ($linkRecord->type)
					{
						case 6:
							break;
						case 5:
							$count += \LinkRecord::enumFolderContent($linkRecord->id);
							break;
						default:
							$count++;
							break;
					}
			return $count;
		}

		/**
		 * @return string
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
