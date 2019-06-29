<?php
	use application\models\wallbin\models\web\LibraryLink;
	use application\models\wallbin\models\web\LibraryManager;

	/**
	 * Class FolderPreviewData
	 */
	class FileDownloadInfo
	{
		public $name;
		public $relativePath;
		public $fullPath;
		public $size;

		/**
		 * @param $link LibraryLink
		 * @return FileDownloadInfo[]
		 */
		public static function getFolderDownloadInfo($link)
		{
			$downloadInfoList = array();

			$rootPath = realpath($link->filePath);

			/** @var SplFileInfo[] $folderFies */
			$folderFies = new RecursiveIteratorIterator(
				new RecursiveDirectoryIterator($rootPath),
				RecursiveIteratorIterator::LEAVES_ONLY
			);

			foreach ($folderFies as $name => $file)
			{
				if (!$file->isDir())
				{
					$fileInfo = new FileDownloadInfo();
					$fileInfo->name = $file->getBasename();
					$fileInfo->fullPath = $file->getRealPath();
					$fileInfo->relativePath = substr($fileInfo->fullPath, strlen($rootPath) + 1);
					$fileInfo->size = $file->getSize();
					$downloadInfoList[] = $fileInfo;
				}
			}

			return $downloadInfoList;
		}

		/**
		 * @param $link LibraryLink
		 * @return FileDownloadInfo[]
		 */
		public static function getLinkBundleDownloadInfo($link)
		{
			$downloadInfoList = array();

			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($link->libraryId, false);

			/** @var  $linkSettings \LinkBundleLinkSettings */
			$linkBundleSettings = $link->extendedProperties;
			foreach ($linkBundleSettings->bundleItems as $bundleItem)
			{
				if (!$bundleItem->visible) continue;
				switch ($bundleItem->itemType)
				{
					case 1:
						/** @var \LibraryLinkBundleItem $bundleItem */
						$bundleLinkRecord = LinkRecord::getLinkById($bundleItem->libraryLinkId);
						$fileInfo = FileInfo::fromLinkRecord($bundleLinkRecord, $library);
						if ($fileInfo->isFile)
							$downloadInfoList[] = self::getFileDownloadInfo($fileInfo);
						else
						{
							/** @var \LinkInternalLinkRecord $internalLinkRecord */
							$internalLinkRecord = \LinkInternalLinkRecord::model()->find('id_internal=?', array($bundleItem->libraryLinkId));
							if (isset($internalLinkRecord))
							{
								/** @var \LibraryLinkBundleItem $bundleItem */
								$bundleInternalLinkRecord = LinkRecord::getLinkById($internalLinkRecord->id_original);
								if (isset($bundleInternalLinkRecord))
								{
									$internalLibrary = $libraryManager->getLibraryById($bundleInternalLinkRecord->id_library, false);
									$fileInfo = FileInfo::fromLinkRecord($bundleInternalLinkRecord, $internalLibrary);
									if ($fileInfo->isFile)
										$downloadInfoList[] = self::getFileDownloadInfo($fileInfo);
								}
							}
						}
						break;
				}
			}

			return $downloadInfoList;
		}

		/**
		 * @param $fileInfo FileInfo
		 * @return FileDownloadInfo
		 */
		public static function getFileDownloadInfo($fileInfo)
		{
			$downloadInfo = new FileDownloadInfo();
			$downloadInfo->name = $fileInfo->name;
			$downloadInfo->fullPath = $fileInfo->path;
			$downloadInfo->relativePath = $fileInfo->name;
			$downloadInfo->size = $fileInfo->size;
			return $downloadInfo;
		}

		/**
		 * @param $downloadInfo FileDownloadInfo[]
		 * @return int
		 */
		public static function getTotalSize($downloadInfo)
		{
			$totalSize = 0;
			foreach ($downloadInfo as $infoItem)
				$totalSize += $infoItem->size;
			return $totalSize;
		}
	}