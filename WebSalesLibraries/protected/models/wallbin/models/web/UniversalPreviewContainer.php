<?

	/**
	 * Class UniversalPreviewContainer
	 */
	class UniversalPreviewContainer
	{
		public $parent;
		public $id;
		public $libraryId;
		/**
		 * @var string[]
		 */
		public $pngLinks;
		/**
		 * @var string[]
		 */
		public $pngPhoneLinks;
		/**
		 * @var string[]
		 */
		public $pdfLinks;
		/**
		 * @var string[]
		 */
		public $mp4Links;
		/**
		 * @var string[]
		 */
		public $mp4ThumbLinks;
		/**
		 * @var string[]
		 */
		public $newOfficeFormatLinks;
		/**
		 * @var string[]
		 */
		public $thumbsLinks;
		/**
		 * @var string[]
		 */
		public $thumbsPhoneLinks;
		/**
		 * @var int
		 */
		public $thumbsWidth;
		/**
		 * @var int
		 */
		public $thumbsHeight;

		public $pngItems;
		public $pngGalleryItems;
		public $thumbItems;
		public $thumbPhoneItems;
		public $officeItems;

		public $pdf;
		public $mp4;
		public $mp4Thumb;

		/**
		 * @param $library
		 */
		public function __construct($library)
		{
			$this->parent = $library;
		}

		/**
		 * @param $previewRecords
		 */
		public function load($previewRecords)
		{
			$this->pngItems = array();
			$this->pngGalleryItems = array();
			$this->officeItems = array();
			$this->thumbItems = array();
			$this->thumbPhoneItems = array();
			foreach ($previewRecords as $record)
			{
				$this->id = $record->id_container;
				$this->libraryId = $record->id_library;
				$previewFile = new PreviewFile();
				$previewFile->link = Utils::formatUrl($this->parent->storageLink . '/' . $record->relative_path);
				$previewFile->path = str_replace('//', DIRECTORY_SEPARATOR, str_replace('\\', DIRECTORY_SEPARATOR, $this->parent->storagePath . DIRECTORY_SEPARATOR . $record->relative_path));
				$previewFile->name = basename($previewFile->path);
				$previewFile->size = file_exists($previewFile->path) ? filesize($previewFile->path) : 0;
				$fileTime = file_exists($previewFile->path) ? filemtime($previewFile->path) : 'time_undefined';
				switch ($record->type)
				{
					case 'png':
						$previewFile->link .= '?version=' . $fileTime;
						$this->pngGalleryItems[] = $previewFile;
						break;
					case 'png_phone':
						$previewFile->link .= '?version=' . $fileTime;
						$this->pngItems[] = $previewFile;
						break;
					case 'pdf':
						$this->pdf = $previewFile;
						break;
					case 'thumbs':
						$previewFile->link .= '?version=' . $fileTime;
						$this->thumbItems[] = $previewFile;
						break;
					case 'thumbs_phone':
						$previewFile->link .= '?version=' . $fileTime;
						$this->thumbPhoneItems[] = $previewFile;
						break;
					case 'mp4':
						$this->mp4 = $previewFile;
						break;
					case 'mp4 thumb':
						$this->mp4Thumb = $previewFile;
						break;
					case 'office':
						$this->officeItems[] = $previewFile;
						break;
				}
				if ($record->thumb_width > 0)
					$this->thumbsWidth = $record->thumb_width * 0.75;
				if ($record->thumb_height > 0)
					$this->thumbsHeight = $record->thumb_height * 0.75;
			}
			$sortHelper = new ObjectSortHelper('link', 'asc');
			usort($this->pngItems, array($sortHelper, 'sort'));
			usort($this->pngGalleryItems, array($sortHelper, 'sort'));
			usort($this->thumbItems, array($sortHelper, 'sort'));
			usort($this->thumbPhoneItems, array($sortHelper, 'sort'));
			usort($this->officeItems, array($sortHelper, 'sort'));
		}
	}
