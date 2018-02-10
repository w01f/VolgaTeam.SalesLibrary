<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;


	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	interface IParentMenu
	{
		/** @return TextAppearance */
		public function getTextAppearance();
		/** @return ItemSpacing */
		public function getItemSpacing();
		/** @return boolean */
		public function getShowArrow();
	}