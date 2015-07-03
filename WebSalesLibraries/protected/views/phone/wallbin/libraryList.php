<?
	$libraryManager = new LibraryManager();
	$libraries = $libraryManager->getLibraries();
?>
<? if (count($libraries) > 1): ?>
	<? foreach ($libraries as $library): ?>
		<li data-icon="false">
			<a data-ajax="false" href="<? echo Yii::app()->createAbsoluteUrl('wallbin/getLibraryPage', array('libraryId' => $library->id)) ?>"><? echo $library->name; ?></a>
		</li>
	<? endforeach; ?>
<? else: ?>
	<? $defaultLibrary = $libraries[0]; ?>
	<? foreach ($defaultLibrary->pages as $page): ?>
		<li data-icon="false">
			<a data-ajax="false" href="<? echo Yii::app()->createAbsoluteUrl('wallbin/getLibraryPage', array('libraryId' => $defaultLibrary->id, 'pageId' => $page->id)) ?>"><? echo $page->name; ?></a>
		</li>
	<? endforeach; ?>
<?endif; ?>
<li data-icon="false">
	<a class="logout-button" href="#">Log Out</a>
</li>
<li data-role="list-divider"><p class="user-info">User: <? echo Yii::app()->user->login; ?></p></li>
<li data-role="list-divider"><p>Copyright 2015 adSALESapps.com</p></li>