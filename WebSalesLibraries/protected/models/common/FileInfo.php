<?
	use application\models\wallbin\models\web\Library as Library;
	/**
	 * Class FileInfo
	 */
	class FileInfo
	{
		public $name;
		public $path;
		public $link;
		public $size;

		public $isFile;

		/**
		 * @param LinkRecord $linkRecord
		 * @param Library $parentLibrary
		 * @return FileInfo
		 */
		public static function fromLinkRecord($linkRecord, $parentLibrary)
		{
			return self::fromLinkData($linkRecord->type, $linkRecord->name, $linkRecord->file_relative_path, $parentLibrary);
		}

		/**
		 * @param int $type
		 * @param string $name
		 * @param string $relativePath
		 * @param Library $parentLibrary
		 * @return FileInfo
		 */
		public static function fromLinkData($type, $name, $relativePath, $parentLibrary)
		{
			$fileInfo = new FileInfo();
			$fileInfo->isFile = false;
			switch ($type)
			{
				case 5:
					$fileInfo->name = $relativePath;
					break;
				case 6:
				case 16:
					break;
				case 8:
					$fileInfo->name = $fileInfo->link = str_replace('\\', '', $relativePath);
					break;
				case 9:
				case 15:
					$fileInfo->name = $name;
					$fileInfo->link = $relativePath;
					break;
				case 14:
					$fileInfo->name = $name;
					$fileInfo->link = str_replace('\\', '', $relativePath);
					break;
				default:
					$fileInfo->path = $parentLibrary->storagePath . str_replace('\\', '/', $relativePath);
					$fileInfo->link = Utils::formatUrl($parentLibrary->storageLink . str_replace('\\', '/', $relativePath));
					$fileInfo->size = file_exists($fileInfo->path) ? filesize($fileInfo->path) : 0;
					$fileInfo->isFile = true;
					break;
			}
			return $fileInfo;
		}

		/**
		 * @param string $format
		 * @return string
		 */
		public static function getFileMIME($format)
		{
			switch ($format)
			{
				case 'pdf':
					return 'application/pdf';
				default:
					return 'application/octet-stream';
			}
		}
	}