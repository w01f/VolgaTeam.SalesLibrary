<?

	namespace application\models\wallbin\models\cadmin\entities;

	use application\models\wallbin\models\cadmin\settings\LibraryFolderSettings as LibraryFolderSettings;
	use application\models\wallbin\models\cadmin\settings\BannerSettings;
	use application\models\wallbin\models\cadmin\settings\WidgetSettings;

	/**
	 * Class LibraryFolder
	 */
	class LibraryFolder extends VersionedObject
	{
		public $libraryId;
		public $pageId;
		public $name;
		public $rowOrder;
		public $columnOrder;
		public $addDate;
		/** @var  LibraryFolderSettings */
		public $settings;
		/** @var  WidgetSettings */
		public $widget;
		/** @var  BannerSettings */
		public $banner;

		/**
		 * @param $folderRecord \FolderRecord
		 * @return LibraryPage
		 */
		public static function fromFolderRecord($folderRecord)
		{
			$folderInfo = new LibraryFolder();

			$folderInfo->id = $folderRecord->id;
			$folderInfo->pageId = $folderRecord->id_page;
			$folderInfo->libraryId = $folderRecord->id_library;
			$folderInfo->name = $folderRecord->name;
			$folderInfo->rowOrder = $folderRecord->row_order;
			$folderInfo->columnOrder = $folderRecord->column_order;
			$folderInfo->addDate = $folderRecord->date_add;

			if (isset($folderRecord->folder_settings))
				$folderInfo->settings = \CJSON::decode($folderRecord->folder_settings, false);
			else
			{
				$folderSettings = new LibraryFolderSettings();
				$folderSettings->borderColor = $folderRecord->border_color;
				$folderSettings->backgroundWindowColor = $folderRecord->window_back_color;
				$folderSettings->foreWindowColor = $folderRecord->window_fore_color;
				$folderSettings->backgroundHeaderColor = $folderRecord->header_back_color;
				$folderSettings->foreHeaderColor = $folderRecord->header_fore_color;

				$folderSettings->windowFont = new \Font();
				$folderSettings->windowFont->name = $folderRecord->window_font_name;
				$folderSettings->windowFont->size = new \Size($folderRecord->window_font_size);
				$folderSettings->windowFont->isBold = $folderRecord->window_font_bold;
				$folderSettings->windowFont->isItalic = $folderRecord->window_font_italic;
				$folderSettings->windowFont->isUnderlined = false;

				$folderSettings->headerFont = new \Font();
				$folderSettings->headerFont->name = $folderRecord->header_font_name;
				$folderSettings->headerFont->size = new \Size($folderRecord->header_font_size);
				$folderSettings->headerFont->isBold = $folderRecord->header_font_bold;
				$folderSettings->headerFont->isItalic = $folderRecord->header_font_italic;
				$folderSettings->headerFont->isUnderlined = false;

				$folderSettings->headerAlignment = $folderRecord->header_alignment;
				$folderInfo->settings = $folderSettings;
			}

			if (isset($folderRecord->widget_settings))
				$folderInfo->widget = \CJSON::decode($folderRecord->widget_settings, false);
			else
			{
				$widgetSettings = new WidgetSettings();
				$widgetSettings->widgetType = $folderRecord->enable_widget && isset($folderRecord->widget) ?
					WidgetSettings::WidgetTypeCustomWidget :
					WidgetSettings::WidgetTypeNoWidget;
				$widgetSettings->originalImage = $folderRecord->widget;
				$widgetSettings->inverted = false;
				$folderInfo->widget = $widgetSettings;
			}

			if (isset($folderRecord->widget_settings))
				$folderInfo->widget = \CJSON::decode($folderRecord->widget_settings, false);
			else
			{
				$widgetSettings = new WidgetSettings();
				$widgetSettings->widgetType = $folderRecord->enable_widget && isset($folderRecord->widget) ?
					WidgetSettings::WidgetTypeCustomWidget :
					WidgetSettings::WidgetTypeNoWidget;
				$widgetSettings->originalImage = $folderRecord->widget;
				$widgetSettings->inverted = false;
				$folderInfo->widget = $widgetSettings;
			}

			return $folderInfo;
		}
	}