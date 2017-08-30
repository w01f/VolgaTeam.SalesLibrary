<?

	namespace application\models\wallbin\models\web\style;


	interface IWallbinStyleContainer
	{
		/** @return string */
		public function getStyleContainerId();

		/** @return WallbinStyle */
		public function getStyle();
	}