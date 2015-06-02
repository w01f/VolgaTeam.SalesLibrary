<?
	$libraryManager = new LibraryManager();
?>
<? foreach ($libraryManager->getLibraries() as $library): ?>
	<li data-icon="false">
		<a data-ajax="false" href="#"><? echo $library->name; ?></a>
	</li>
<? endforeach; ?>