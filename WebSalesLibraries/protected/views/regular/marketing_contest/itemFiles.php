<?

	use application\models\marketing_contest\models\FileModel;

	/**
	 * @var $files FileModel[]
	 */
?>
<? foreach ($files as $file): ?>
	<li class="file-item">
		<div class="input-group file-item-body">
            <?
	            $dragUrl = \FileInfo::getFileMIME($file->fileFormat) . ':' .
		            $file->fileName . ':' .
		            \Yii::app()->createAbsoluteUrl('marketingContest/downloadFile', array('fileId' => $file->id));
            ?>
			<div class="form-control file-name draggable" draggable="true" data-url-header="DownloadURL" data-url="<? echo $dragUrl; ?>"><? echo $file->fileName; ?></div>
			<span class="input-group-addon glyphicon glyphicon-remove file-delete"></span>
		</div>
		<div class="service-data">
			<div class="file-id"><? echo $file->id; ?></div>
		</div>
	</li>
<? endforeach; ?>
