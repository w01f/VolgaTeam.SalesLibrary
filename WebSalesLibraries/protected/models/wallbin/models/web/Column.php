<?
	namespace application\models\wallbin\models\web;

	/**
	 * Class Column
	 */
	class Column
	{
		public $parent;
		public $pageId;
		public $libraryId;
		public $name;
		public $order;
		public $backColor;
		public $foreColor;
		public $font;
		public $showText;
		public $alignment;
		public $enableWidget;
		public $widget;
		/**
		 * @var Banner
		 */
		public $banner;
		public $dateModify;

		/**
		 * @param $page
		 */
		public function __construct($page)
		{
			$this->parent = $page;
			$this->name = '';
			$this->order = 0;
			$this->backColor = "000000";
			$this->foreColor = "FFFFFF";
			$this->font = new \Font();
			$this->font->name = "Arial";
			$this->font->size = "14";
			$this->font->isBold = TRUE;
			$this->font->isItalic = FALSE;
			$this->showText = FALSE;
			$this->alignment = 'center';
			$this->enableWidget = FALSE;
		}

		/**
		 * @param $columnRecord
		 */
		public function load($columnRecord)
		{
			$this->pageId = $columnRecord->id_page;
			$this->libraryId = $columnRecord->id_library;
			$this->name = $columnRecord->name;
			$this->order = $columnRecord->order;
			$this->backColor = $columnRecord->back_color;
			$this->foreColor = $columnRecord->fore_color;
			$this->font = new \Font();
			$this->font->name = $columnRecord->font_name;
			$this->font->size = $columnRecord->font_size;
			$this->font->isBold = $columnRecord->font_bold;
			$this->font->isItalic = $columnRecord->font_italic;
			$this->showText = $columnRecord->show_text;
			$this->alignment = $columnRecord->alignment;
			$this->enableWidget = $columnRecord->enable_widget;
			$this->widget = $columnRecord->widget;

			$bannerRecord = \BannerRecord::model()->findByPk($columnRecord->id_banner);
			if ($bannerRecord !== null)
			{
				$this->banner = new Banner();
				$this->banner->load($bannerRecord);
			}
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
		public static function columnComparer($x, $y)
		{
			if ($x->order == $y->order)
				return 0;
			else
				return ($x->order < $y->order) ? -1 : 1;
		}
	}
