<?
	namespace application\models\wallbin\models\web;

	/**
	 * Class OneDrive
	 */
	class OneDrive
	{
		public $url;
		/**
		 * @param $content string
		 * @return OneDrive
		 */
		public static function createByContent($content)
		{
			if(!empty($content))
				return \CJSON::decode($content, false);
			return new OneDrive();
		}
	}
