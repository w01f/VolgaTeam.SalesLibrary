<?

	/**
	 * Class UniversalPreviewContainer
	 */
	class UniversalPreviewContainer
	{
		public $parent;
		/**
		 * @var string
		 * @soap
		 */
		public $id;
		/**
		 * @var string
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var string[]
		 * @soap
		 */
		public $pngLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $pngPhoneLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $jpegLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $jpegPhoneLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $pdfLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $wmvLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $mp4Links;
		/**
		 * @var string[]
		 * @soap
		 */
		public $mp4ThumbLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $ogvLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $newOfficeFormatLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $thumbsLinks;
		/**
		 * @var int
		 * @soap
		 */
		public $thumbsWidth;
		/**
		 * @var int
		 * @soap
		 */
		public $thumbsHeight;

		public $pngItems;
		public $pngGalleryItems;
		public $jpegItems;
		public $jpegGalleryItems;
		public $thumbItems;
		public $officeItems;

		public $pdf;
		public $wmv;
		public $ogv;
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
			$this->jpegItems = array();
			$this->jpegGalleryItems = array();
			$this->officeItems = array();
			$this->thumbItems = array();
			foreach ($previewRecords as $record)
			{
				$this->id = $record->id_container;
				$this->libraryId = $record->id_library;
				$previewFile = new PreviewFile();
				$previewFile->link = str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->storageLink . '/' . $record->relative_path)));
				$previewFile->path = str_replace('\\', '/', $this->parent->storagePath . DIRECTORY_SEPARATOR . $record->relative_path);
				$previewFile->name = basename($previewFile->path);
				$previewFile->size = file_exists($previewFile->path) ? filesize($previewFile->path) : 0;
				switch ($record->type)
				{
					case 'png':
						$previewFile->link .= '?version=' . filemtime($previewFile->path);
						$this->pngGalleryItems[] = $previewFile;
						break;
					case 'png_phone':
						$previewFile->link .= '?version=' . filemtime($previewFile->path);
						$this->pngItems[] = $previewFile;
						break;
					case 'jpeg':
						$previewFile->link .= '?version=' . filemtime($previewFile->path);
						$this->jpegGalleryItems[] = $previewFile;
						break;
					case 'jpeg_phone':
						$previewFile->link .= '?version=' . filemtime($previewFile->path);
						$this->jpegItems[] = $previewFile;
						break;
					case 'pdf':
						$this->pdf = $previewFile;
						break;
					case 'thumbs':
						$previewFile->link .= '?version=' . filemtime($previewFile->path);
						$this->thumbItems[] = $previewFile;
						break;
					case 'wmv':
						$this->wmv = $previewFile;
						break;
					case 'mp4':
						$this->mp4 = $previewFile;
						break;
					case 'mp4 thumb':
						$this->mp4Thumb = $previewFile;
						break;
					case 'ogv':
						$this->ogv = $previewFile;
						break;
					case 'office':
					case 'new office':
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
			usort($this->jpegItems, array($sortHelper, 'sort'));
			usort($this->jpegGalleryItems, array($sortHelper, 'sort'));
			usort($this->thumbItems, array($sortHelper, 'sort'));
			usort($this->officeItems, array($sortHelper, 'sort'));
		}
	}
