<?

	/**
	 * Class PublicPageContentShortcutLoginModel
	 */
	class PublicPageContentShortcutLoginModel
	{
		public $shortcutId;
		public $password;

		/**
		 * @param string $encodedContent
		 * @return PublicPageContentShortcutLoginModel
		 */
		public static function fromJson($encodedContent)
		{
			$instance = new self();
			\Utils::loadFromJson($instance, $encodedContent);
			return $instance;
		}
	}