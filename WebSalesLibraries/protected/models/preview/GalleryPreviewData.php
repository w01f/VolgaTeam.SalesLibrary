<?

	/**
	 * Class GalleryPreviewData
	 */
	abstract class GalleryPreviewData extends FilePreviewData
	{
		public $galleryEnabled;

		public $pagesInPng;
		public $pagesInJpeg;
		public $galleryPagesInPng;
		public $galleryPagesInJpeg;
		public $thumbnails;

		public $thumbWidth;
		public $thumbHeight;

		public $singlePage;

		protected $pageItemName;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);

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
					$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), $link->fileExtension);
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
					$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), $link->fileExtension);
					$pageItem->itemIndexInfo = sprintf('%s %s of %s', $this->pageItemName, ($i + 1), $count);
					$pageItem->href = $previewFile->link;
					$pageItem->path = $previewFile->path;
					$pageItem->size = FilePreviewData::formatFileSize($previewFile->size);
					$this->galleryPagesInPng[] = $pageItem;
					$i++;
				}
			}

			$this->pagesInJpeg = array();
			if (isset($link->universalPreview->jpegItems))
			{
				$count = count($link->universalPreview->jpegItems);
				$i = 0;
				foreach ($link->universalPreview->jpegItems as $previewFile)
				{
					$pageItem = new PagePreviewItem();
					$pageItem->id = sprintf('link%s---%s', $link->id, $i);
					$pageItem->index = $i;
					$pageItem->title = sprintf('%s - %s %s of %s', $this->fileName, $this->pageItemName, ($i + 1), $count);
					$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), $link->fileExtension);
					$pageItem->itemIndexInfo = sprintf('%s %s of %s', $this->pageItemName, ($i + 1), $count);
					$pageItem->href = $previewFile->link;
					$pageItem->path = $previewFile->path;
					$pageItem->size = FilePreviewData::formatFileSize($previewFile->size);
					$this->pagesInJpeg[] = $pageItem;
					$i++;
				}
			}

			$this->galleryPagesInJpeg = array();
			if (isset($link->universalPreview->jpegGalleryItems))
			{
				$count = count($link->universalPreview->jpegGalleryItems);
				$i = 0;
				foreach ($link->universalPreview->jpegGalleryItems as $previewFile)
				{
					$pageItem = new PagePreviewItem();
					$pageItem->id = sprintf('link%s---%s', $link->id, $i);
					$pageItem->index = $i;
					$pageItem->title = sprintf('%s - %s %s of %s', $this->fileName, $this->pageItemName, ($i + 1), $count);
					$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), $link->fileExtension);
					$pageItem->itemIndexInfo = sprintf('%s %s of %s', $this->pageItemName, ($i + 1), $count);
					$pageItem->href = $previewFile->link;
					$pageItem->path = $previewFile->path;
					$pageItem->size = FilePreviewData::formatFileSize($previewFile->size);
					$this->galleryPagesInJpeg[] = $pageItem;
					$i++;
				}
			}

			if (Yii::app()->browser->isMobile())
				$this->thumbnails = $link->universalPreview->thumbPhoneItems;
			else
				$this->thumbnails = $link->universalPreview->thumbItems;

			$this->galleryEnabled = count($this->pagesInPng) > 0 && count($this->pagesInJpeg) > 0;
		}

		/**
		 * @param $format string
		 * @return array
		 */
		public function getFullScreenGalleryImages($format)
		{
			$galleryLinks = array();
			$originalImages = array();
			switch ($format)
			{
				case 'png':
					$originalImages = $this->galleryPagesInPng;
					break;
				case 'jpeg':
					$originalImages = $this->galleryPagesInJpeg;
					break;
			}

			for ($i = 0; $i < count($originalImages); $i++)
			{
				$galleryLink = array('image' => $originalImages[$i]->href,
					'title' => $originalImages[$i]->title,
					'thumb' => $this->thumbnails[$i]->link);
				$galleryLinks[] = $galleryLink;
			}
			return $galleryLinks;
		}
	}