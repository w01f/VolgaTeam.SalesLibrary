<?
	/**
	 * Class InternalShortcutPreviewInfo
	 */
	class InternalShortcutPreviewInfo
	{
		public $internalLinkType;
		public $shortcutId;

		/**
		 * @param $linkSettings InternalShortcutLinkSettings
		 */
		public function __construct($linkSettings)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;
			$this->shortcutId = $linkSettings->shortcutId;
		}

		/**
		 * @param $isPhone boolean
		 * @return string
		 */
		public function getShortcutData($isPhone)
		{
			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->findByPk($this->shortcutId);
			/** @var  $shortcut BaseShortcut */
			$shortcut = $shortcutRecord->getModel($isPhone);
			return $shortcut->getMenuItemData();
		}
	}