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
			return self::fromLinkData(
				$linkRecord->type,
				$linkRecord->name,
				$linkRecord->file_relative_path,
				BaseLinkSettings::createByLink($linkRecord),
				$parentLibrary);
		}

		/**
		 * @param int $type
		 * @param string $name
		 * @param string $relativePath
		 * @param BaseLinkSettings $extendedProperties
		 * @param Library $parentLibrary
		 * @return FileInfo
		 */
		public static function fromLinkData($type, $name, $relativePath, $extendedProperties, $parentLibrary)
		{
			$fileInfo = new FileInfo();
			$fileInfo->isFile = false;
			switch ($type)
			{
				case 5:
					$fileInfo->name = str_replace('\\', '', $relativePath);
					$fileInfo->path = str_replace('//', DIRECTORY_SEPARATOR, str_replace('\\', DIRECTORY_SEPARATOR, $parentLibrary->storagePath . $relativePath));
					break;
				case 6:
					break;
				case 8:
				case 17:
				case 18:
					$fileInfo->name = $fileInfo->link = str_replace('\\', '', $relativePath);
					break;
				case 9:
				case 15:
					$fileInfo->name = $name;
					$fileInfo->link = $relativePath;
					break;
				case 14:
				case 20:
					$fileInfo->name = $name;
					$fileInfo->link = str_replace('\\', '', $relativePath);
					break;
				case 16:
					$fileInfo->name = $name;
					/** @var InternalLibraryFolderLinkSettings|InternalLibraryObjectLinkSettings|InternalLibraryPageLinkSettings|InternalShortcutLinkSettings|InternalWallbinLinkSettings $internalLinkSettings */
					$internalLinkSettings = $extendedProperties;
					if ($internalLinkSettings->internalLinkType == 5)
					{
						/** @var InternalShortcutLinkSettings */
						$shortcutLinkSettings = $internalLinkSettings;
						$fileInfo->link = PageContentShortcut::createShortcutUrl($shortcutLinkSettings->shortcutId, $shortcutLinkSettings->openOnSamePage);
					}
					break;
				default:
					$fileInfo->path = str_replace('//', DIRECTORY_SEPARATOR, str_replace('\\', DIRECTORY_SEPARATOR, $parentLibrary->storagePath . $relativePath));
					$fileInfo->link = Utils::formatUrl($parentLibrary->storageLink . $relativePath);
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

		/**
		 * @param $fileSize
		 * @return string
		 */
		public static function formatFileSize($fileSize)
		{
			$type = '';
			if (isset($fileSize))
			{
				if ($fileSize < 1073741824)
				{
					if ($fileSize < 1048576)
						$type = 'kb';
					else
						$type = 'mb';
				}
				else
					$type = 'gb';
				switch ($type)
				{
					case "kb":
						$fileSize = $fileSize * .0009765625; // bytes to KB
						break;
					case "mb":
						$fileSize = ($fileSize * .0009765625) * .0009765625; // bytes to MB
						break;
					case "gb":
						$fileSize = (($fileSize * .0009765625) * .0009765625) * .0009765625; // bytes to GB
						break;
				}
			}
			else
				$fileSize = -1;
			if ($fileSize <= 0)
				return '';
			else
				return round($fileSize, 0) . $type;
		}
	}