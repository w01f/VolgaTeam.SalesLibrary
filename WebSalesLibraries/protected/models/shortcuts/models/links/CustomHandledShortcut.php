<?

	/**
	 * Class CustomHandledShortcut
	 */
	abstract class CustomHandledShortcut extends BaseShortcut
	{
		public $samePage;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$this->samePage = true;
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="has-custom-handler"></div>';
			$result .= ('<div class="custom-parameters">' . CJSON::encode($this->getShortcutCustomParameters()) . '</div>');
			if ($this->samePage)
				$result .= '<div class="same-page"></div>';
			return $result;
		}

		/**
		 * @return array
		 */
		public function getShortcutCustomParameters()
		{
			return array();
		}
	}