<?
	namespace application\models\shortcuts\models\landing_page\regular_markup;

	/**
	 * Class Row
	 */
	class Row extends BlockContainer
	{
		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'row';
		}
	}