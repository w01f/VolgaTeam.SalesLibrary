<?
	namespace application\models\shortcuts\models\bundle_modal_dialog;

	class FavoritesPageContainer extends TabItemContainer
	{
		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $shortcut \BundleModalDialogShortcut
		 */
		public function __construct($xpath, $contextNode, $shortcut)
		{
			parent::__construct($xpath, $contextNode, $shortcut);
			$this->id = 'bundle-modal-favorites';
			$this->items = \ShortcutBundleModalFavoriteItem::model()->getModels();
		}
	}