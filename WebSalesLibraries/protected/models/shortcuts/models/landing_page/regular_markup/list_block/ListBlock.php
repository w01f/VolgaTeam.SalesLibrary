<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\list_block;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;

	/**
	 * Class ListBlock
	 */
	class ListBlock extends BlockContainer
	{
		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'list';
		}
	}