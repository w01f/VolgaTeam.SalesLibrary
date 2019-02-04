<?

	use application\models\shortcuts\models\landing_page\regular_markup\drop_folder\DropFolderBlock;

	/**
     * @var $contentBlock DropFolderBlock
     */

	$containerId = sprintf('drop-folder-container-%s', $contentBlock->id);
	$folderBlockId = sprintf('drop-folder-%s', $contentBlock->id);

	echo $this->renderPartial('landingPageMarkup/style/styleBorder',
		array(
			'border' => $contentBlock->border,
			'blockId' => $folderBlockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
		array(
			'textAppearance' => $contentBlock->getTextAppearance(),
			'blockId' => $folderBlockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleBackground',
		array(
			'background' => $contentBlock->background,
			'blockId' => $folderBlockId
		)
		, true);
?>
<style>
    <? echo '#'.$folderBlockId;?>
    {
        min-height: <? echo $contentBlock->minHeight;?>px;
    }

    <? echo '#'.$folderBlockId;?> .file-item
    {
        margin: 5px;
        color: #000000;
    }

    <? echo '#'.$folderBlockId;?> .file-item-body .form-control
    {
        outline: none;
        box-shadow: none !important;
        border: 1px solid #ccc !important;
        cursor: pointer;
    }

    <? echo '#'.$folderBlockId;?> .file-item-body .file-name
    {
        float: none;
    }

    <? echo '#'.$folderBlockId;?> .file-item-body .file-delete
    {
        top: 0;
        cursor: pointer;
    }
</style>
<div id="<? echo $containerId; ?>" class="drop-folder-container"
    style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>">
    <ul id="<? echo $folderBlockId; ?>" class="nav nav-pills dropzone">
    </ul>
    <div class="progress" style="position: relative; width: 100%; display: none;">
        <div class="progress-text text-center" style="width: 100%; position: absolute"><span class="file-name">Test</span>: <span class="progress-percent">90</span>%</div>
        <div class="progress-bar" style="width: 0; height: 20px;"></div>
    </div>
    <div class="service-data drop-folder-data">
		<? echo CJSON::encode(array(
			'folderName' => $contentBlock->folderName,
			'defaultMessage' => $contentBlock->hoverText,
			'maxFileSize' => $contentBlock->maxFileSize,
			'maxFileSizeExcessMessage' => $contentBlock->maxFileSizeExcessMessage,
			'allowedFileTypes' =>  $contentBlock->allowedFileTypes,
			'fileTypeDiscardMessage' => $contentBlock->fileTypeDiscardMessage,
			'uploadOnClick' => $contentBlock->uploadOnClick,
		)) ?>
    </div>
</div>