<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class GalleryPreviewData
	 */
	abstract class GalleryPreviewData extends FilePreviewData
	{
		public $galleryEnabled;

		public $pagesInPng;
		public $galleryPagesInPng;
		public $thumbnails;

		public $thumbWidth;
		public $thumbHeight;

		public $singlePage;

		protected $pageItemName;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->pageItemName = '';
			switch ($this->format)
			{
				case 'ppt':
					$this->pageItemName = 'Slide';
					break;
				case 'doc':
				case 'pdf':
					$this->pageItemName = 'Page';
					break;
			}

			$this->thumbWidth = $link->universalPreview->thumbsWidth;
			$this->thumbHeight = $link->universalPreview->thumbsHeight;

			$this->singlePage = count($link->universalPreview->pngItems) < 2;

			$this->pagesInPng = array();
			if (isset($link->universalPreview->pngItems))
			{
				$count = count($link->universalPreview->pngItems);
				$i = 0;
				foreach ($link->universalPreview->pngItems as $previewFile)
				{
					$pageItem = new PagePreviewItem();
					$pageItem->id = sprintf('link%s---%s', $link->id, $i);
					$pageItem->index = $i;
					$pageItem->title = sprintf('%s - %s %s of %s', $this->fileName, $this->pageItemName, ($i + 1), $count);
					$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), pathinfo($previewFile->path, PATHINFO_EXTENSION));
					$pageItem->itemIndexInfo = sprintf('%s %s of %s', $this->pageItemName, ($i + 1), $count);
					$pageItem->href = $previewFile->link;
					$pageItem->path = $previewFile->path;
					$pageItem->size = FilePreviewData::formatFileSize($previewFile->size);
					$this->pagesInPng[] = $pageItem;
					$i++;
				}
			}

			$this->galleryPagesInPng = array();
			if (isset($link->universalPreview->pngGalleryItems))
			{
				$count = count($link->universalPreview->pngGalleryItems);
				$i = 0;
				foreach ($link->universalPreview->pngGalleryItems as $previewFile)
				{
					$pageItem = new PagePreviewItem();
					$pageItem->id = sprintf('link%s---%s', $link->id, $i);
					$pageItem->index = $i;
					$pageItem->title = sprintf('%s - %s %s of %s', $this->fileName, $this->pageItemName, ($i + 1), $count);
					$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), pathinfo($previewFile->path, PATHINFO_EXTENSION));
					$pageItem->itemIndexInfo = sprintf('%s %s of %s', $this->pageItemName, ($i + 1), $count);
					$pageItem->href = $previewFile->link;
					$pageItem->path = $previewFile->path;
					$pageItem->size = FilePreviewData::formatFileSize($previewFile->size);
					$this->galleryPagesInPng[] = $pageItem;
					$i++;
				}
			}

			if (Yii::app()->browser->isMobile())
				$this->thumbnails = $link->universalPreview->thumbPhoneItems;
			else
				$this->thumbnails = $link->universalPreview->thumbItems;

			$this->galleryEnabled = count($this->pagesInPng) > 0;
		}
	}