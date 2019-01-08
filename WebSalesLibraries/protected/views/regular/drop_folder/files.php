<?
	/**
	 * @var $files array
	 * @var $folderName string
	 */
?>
<? foreach ($files as $file): ?>
    <?
	    $fileName = $file;
        $extension = pathinfo($file, PATHINFO_EXTENSION);
    ?>
	<li class="file-item">
		<div class="input-group file-item-body">
            <?
	            $dragUrl = \FileInfo::getFileMIME($extension) . ':' .
		            $fileName . ':' .
		            \Yii::app()->createAbsoluteUrl('dropFolder/downloadFile', array('folderName' => $folderName, 'fileName' => $fileName));
            ?>
			<div class="form-control file-name draggable" draggable="true" data-url-header="DownloadURL" data-url="<? echo $dragUrl; ?>"><? echo $fileName; ?></div>
			<span class="input-group-addon glyphicon glyphicon-remove file-delete"></span>
		</div>
		<div class="service-data">
            <div class="folder-name"><? echo $folderName; ?></div>
			<div class="file-name"><? echo $fileName; ?></div>
		</div>
	</li>
<? endforeach; ?>
