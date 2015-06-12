<?
	$libraryManager = new LibraryManager();
?>
<? foreach ($libraryManager->getLibraries() as $library): ?>
	<li data-icon="false">
		<a data-ajax="false" href="<? echo Yii::app()->createAbsoluteUrl('wallbin/getLibraryPage', array('libraryId' => $library->id)) ?>"><? echo $library->name; ?></a>
	</li>
<? endforeach; ?>