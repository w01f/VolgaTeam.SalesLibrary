<?
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;

	/** @var $folder LibraryFolder */
?>
<div data-role="collapsibleset" data-theme="a" data-content-theme="a" data-inset="true">
	<? foreach ($folder->files as $link): ?>
		<? if ($link->isFolder): ?>
			<div class="folder-link" data-role="collapsible">
				<h3><? echo nl2br($link->name); ?></h3>
				<div class="link-folder-content"></div>
				<div class="service-data">
					<div class="link-id"><? echo $link->id; ?></div>
				</div>
			</div>
		<? elseif ($link->isDirectUrl): ?>
			<div class="collapsible-link direct-link" data-role="collapsible" data-collapsed-icon="none" data-expanded-icon="none">
				<h3><? echo nl2br($link->name); ?></h3>
				<div class="service-data">
					<div class="url"><? echo $link->fileLink; ?></div>
					<div class="link-id"><? echo $link->id; ?></div>
				</div>
			</div>
		<? elseif (!$link->isLineBreak): ?>
			<div class="collapsible-link regular-link" data-role="collapsible" data-collapsed-icon="none" data-expanded-icon="none">
				<h3><? echo nl2br($link->name); ?></h3>
				<div class="service-data">
					<div class="link-id"><? echo $link->id; ?></div>
				</div>
			</div>
		<? endif; ?>
	<? endforeach; ?>
</div>