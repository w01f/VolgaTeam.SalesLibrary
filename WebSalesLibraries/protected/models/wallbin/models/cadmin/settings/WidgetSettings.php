<?
	namespace application\models\wallbin\models\cadmin\settings;

	/**
	 * Class WidgetSettings
	 */
	class WidgetSettings
	{
		const WidgetTypeNoWidget = 1;
		const WidgetTypeAutoWidget = 2;
		const WidgetTypeCustomWidget = 3;

		public $widgetType;
		public $inverted;
		public $originalImage;
		public $displayedImage;
	}