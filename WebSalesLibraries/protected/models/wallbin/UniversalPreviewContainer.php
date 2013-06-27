<?php
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
		public $pngMaxFileSize;
		/**
		 * @var string[]
		 * @soap
		 */
		public $pngPhoneLinks;
		public $pngPhoneMaxFileSize;
		/**
		 * @var string[]
		 * @soap
		 */
		public $jpegLinks;
		public $jpegMaxFileSize;
		/**
		 * @var string[]
		 * @soap
		 */
		public $jpegPhoneLinks;
		public $jpegPhoneMaxFileSize;
		/**
		 * @var string[]
		 * @soap
		 */
		public $pdfLinks;
		public $pdfMaxFileSize;
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
		public $ogvLinks;
		/**
		 * @var string[]
		 * @soap
		 */
		public $oldOfficeFormatLinks;
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
		 * @var string[]
		 * @soap
		 */
		public $thumbsPhoneLinks;
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

		public function __construct($library)
		{
			$this->parent = $library;
		}

		public function load($previewRecords)
		{
			foreach ($previewRecords as $record)
			{
				$this->id = $record->id_container;
				$this->libraryId = $record->id_library;
				$previewLink = str_replace(' ', '%20', htmlspecialchars(str_replace('\\', '/', $this->parent->storageLink . '/' . $record->relative_path)));
				$previewPath = str_replace('\\', '/', $this->parent->storagePath . DIRECTORY_SEPARATOR . $record->relative_path);
				switch ($record->type)
				{
					case 'png':
						$previewLink .= '?version=' . filemtime($previewPath);
						$this->pngLinks[] = $previewLink;
						$fileSize = file_exists($previewPath) ? filesize($previewPath) : 0;
						if ($this->pngMaxFileSize < $fileSize)
							$this->pngMaxFileSize = $fileSize;
						break;
					case 'png_phone':
						$previewLink .= '?version=' . filemtime($previewPath);
						$this->pngPhoneLinks[] = $previewLink;
						$fileSize = file_exists($previewPath) ? filesize($previewPath) : 0;
						if ($this->pngPhoneMaxFileSize < $fileSize)
							$this->pngPhoneMaxFileSize = $fileSize;
						break;
					case 'jpeg':
						$previewLink .= '?version=' . filemtime($previewPath);
						$this->jpegLinks[] = $previewLink;
						$fileSize = file_exists($previewPath) ? filesize($previewPath) : 0;
						if ($this->jpegMaxFileSize < $fileSize)
							$this->jpegMaxFileSize = $fileSize;
						break;
					case 'jpeg_phone':
						$previewLink .= '?version=' . filemtime($previewPath);
						$this->jpegPhoneLinks[] = $previewLink;
						$fileSize = file_exists($previewPath) ? filesize($previewPath) : 0;
						if ($this->jpegPhoneMaxFileSize < $fileSize)
							$this->jpegPhoneMaxFileSize = $fileSize;
						break;
					case 'pdf':
						$this->pdfLinks[] = $previewLink;
						$fileSize = file_exists($previewPath) ? filesize($previewPath) : 0;
						if ($this->pdfMaxFileSize < $fileSize)
							$this->pdfMaxFileSize = $fileSize;
						break;
					case 'thumbs':
						$previewLink .= '?version=' . filemtime($previewPath);
						$this->thumbsLinks[] = $previewLink;
						break;
					case 'thumbs_phone':
						$previewLink .= '?version=' . filemtime($previewPath);
						$this->thumbsPhoneLinks[] = $previewLink;
						break;
					case 'wmv':
						$this->wmvLinks[] = $previewLink;
						break;
					case 'mp4':
						$this->mp4Links[] = $previewLink;
						break;
					case 'ogv':
						$this->ogvLinks[] = $previewLink;
						break;
					case 'new office':
						$this->newOfficeFormatLinks[] = $previewLink;
						break;
					case 'old office':
						$this->oldOfficeFormatLinks[] = $previewLink;
						break;
				}
				if ($record->thumb_width > 0)
					$thumbsWidth = $record->thumb_width * 0.75;
				if ($record->thumb_height > 0)
					$thumbsHeight = $record->thumb_height * 0.75;
			}
			if (isset($thumbsHeight))
				$this->thumbsHeight = $thumbsHeight;
			if (isset($thumbsWidth))
				$this->thumbsWidth = $thumbsWidth;
			if (isset($this->pngLinks))
				natsort($this->pngLinks);
			if (isset($this->pngPhoneLinks))
				natsort($this->pngPhoneLinks);
			if (isset($this->pdfLinks))
				natsort($this->pdfLinks);
			if (isset($this->jpegLinks))
				natsort($this->jpegLinks);
			if (isset($this->jpegPhoneLinks))
				natsort($this->jpegPhoneLinks);
			if (isset($this->oldOfficeFormatLinks))
				natsort($this->oldOfficeFormatLinks);
			if (isset($this->newOfficeFormatLinks))
				natsort($this->newOfficeFormatLinks);
			if (isset($this->thumbsLinks))
				natsort($this->thumbsLinks);
			if (isset($this->thumbsPhoneLinks))
				natsort($this->thumbsPhoneLinks);
			if (isset($this->mp4Links))
				natsort($this->mp4Links);
			if (isset($this->ogvLinks))
				natsort($this->ogvLinks);
		}

	}
