<?
	/**
	 * @var $tabPages array
	 * @var $librariesPopupId string
	 */

	$libraryManager = new LibraryManager();
	$libraries = $libraryManager->getLibraries();
?>
<? foreach ($tabPages as $tabName => $tabIndex): ?>
	<? $url = TabPages::getTabUrl($tabName); ?>
	<? if ($tabName == 'home_tab'): ?>
		<li data-icon="false">
			<a data-ajax="false" href="<? echo count($libraries) > 1 ? ('#' . $librariesPopupId) : $url; ?>"><? echo Yii::app()->params['home_tab']['name'] ?></a>
		</li>
	<? elseif ($tabName == 'search_full_tab'): ?>
		<li data-icon="false">
			<a class="not-working" data-ajax="false" href="<? echo $url; ?>"><? echo Yii::app()->params['search_full_tab']['name'] ?></a>
		</li>
	<?
	elseif ($tabName == 'favorites_tab'): ?>
		<li data-icon="false">
			<a class="not-working" data-ajax="false" href="<? echo $url; ?>"><? echo Yii::app()->params['favorites_tab']['name'] ?></a>
		</li>
	<?
	elseif (strpos($tabName, 'shortcuts-tab-') !== false): ?>
		<?
		/** @var $tabShortcutsRecord ShortcutsTabRecord */
		$tabShortcutsRecord = ShortcutsTabRecord::model()->findByPk(str_replace('shortcuts-tab-', '', $tabName));
		?>
		<? if (isset($tabShortcutsRecord)): ?>
			<li data-icon="false">
				<a data-ajax="false" href="<? echo $url; ?>"><? echo $tabShortcutsRecord->name; ?></a>
			</li>
		<? endif; ?>
	<? endif; ?>
<? endforeach; ?>