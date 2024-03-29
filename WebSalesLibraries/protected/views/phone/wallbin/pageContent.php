<?

	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/**
	 * @var $page LibraryPage
	 */
?>
<div data-role="collapsibleset" data-theme="a" data-content-theme="a" data-inset="false">
	<? if (isset($page->folders)): ?>
		<? foreach ($page->folders as $folder): ?>
            <div data-role="collapsible">
                <h3><? echo $folder->name; ?></h3>
                <div class="folder-content"></div>
                <div class="service-data">
                    <div class="folder-id"><? echo $folder->id; ?></div>
                </div>
            </div>
		<? endforeach; ?>
	<? endif; ?>
</div>