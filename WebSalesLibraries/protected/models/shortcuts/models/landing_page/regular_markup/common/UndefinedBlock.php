<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class UndefinedBlock
	 */
	class UndefinedBlock extends ContentBlock
	{
		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'undefined-block';
		}
	}