<?

	/**
	 * Class MenuItem
	 */
	class MenuItem
	{
		/** @var $shortcut BaseShortcut */
		public $shortcut;

		/** @var $appearance ShortcutAppearance */
		public $appearance;

		/**
		 * @param $shortcut BaseShortcut
		 * @param $group ShortcutGroup
		 */
		public function __construct($shortcut, $group)
		{
			$this->shortcut = $shortcut;

			$this->appearance = new ShortcutAppearance();
			$this->appearance->size = isset($shortcut->appearance->size) ? $shortcut->appearance->size : $group->defaultItemAppearance->size;
			$this->appearance->textSize = isset($shortcut->appearance->textSize) ? $shortcut->appearance->textSize : $group->defaultItemAppearance->textSize;
			$this->appearance->iconSize = isset($shortcut->appearance->iconSize) ? $shortcut->appearance->iconSize : $group->defaultItemAppearance->iconSize;
			$this->appearance->textAlign = isset($shortcut->appearance->textAlign) ? $shortcut->appearance->textAlign : $group->defaultItemAppearance->textAlign;
			$this->appearance->backColor = isset($shortcut->appearance->backColor) ? $shortcut->appearance->backColor : $group->defaultItemAppearance->backColor;
			$this->appearance->textColor = isset($shortcut->appearance->textColor) ? $shortcut->appearance->textColor : $group->defaultItemAppearance->textColor;
			$this->appearance->iconColor = isset($shortcut->appearance->iconColor) ? $shortcut->appearance->iconColor : $group->defaultItemAppearance->iconColor;
			$this->appearance->shadowColor = isset($shortcut->appearance->shadowColor) ? $shortcut->appearance->shadowColor : $group->defaultItemAppearance->shadowColor;
			$this->appearance->useGradient = isset($shortcut->appearance->useGradient) ? $shortcut->appearance->useGradient : $group->defaultItemAppearance->useGradient;
		}
	}