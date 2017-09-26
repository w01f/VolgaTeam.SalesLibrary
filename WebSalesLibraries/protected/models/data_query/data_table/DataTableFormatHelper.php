<?

	namespace application\models\data_query\data_table;

	use application\models\wallbin\models\web\LibraryLink;
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\Library as Library;

	/**
	 * Class DataTableFormatHelper
	 */
	class DataTableFormatHelper
	{
		private static $defaultThumbnailFileKeys = array(
			'aep',
			'aet',
			'ai',
			'ait',
			'eps',
			'gif',
			'pdd',
			'pps',
			'psd',
			'rar',
			'svg',
			'zip',
		);

		/**
		 * @param $linkQueryResult array
		 * @param $baseColumnSettings array
		 * @param $extraColumns array
		 * @return array
		 */
		public static function formatRegularData($linkQueryResult, $baseColumnSettings, $extraColumns = array())
		{
			$dataset = array();
			if (count($linkQueryResult) > 0)
			{
				$libraryManager = new LibraryManager();
				foreach ($linkQueryResult as $linkRecord)
				{
					$record = array();
					$record['id'] = $linkRecord['id'];

					$record['name'] = nl2br($linkRecord['name']);

					$record['file_name'] = $linkRecord['file_name'];
					$record['path'] = $linkRecord['path'];

					/** @var $library Library */
					$library = $libraryManager->getLibraryById($linkRecord['id_library']);
					$record['library'] = array(
						'id' => $linkRecord['id_library'],
						'name' => isset($library) ? $library->name : ''
					);
					$record['lib_name'] = isset($library) ? $library->name : '';

					$record['tooltip'] = isset($linkRecord['file_name']) && $linkRecord['file_name'] != '' ? $linkRecord['file_name'] : $linkRecord['name'];
					if (isset($linkRecord['search_format']) && $linkRecord['search_format'] != 'other')
						$formatKey = $linkRecord['search_format'];
					else
						$formatKey = $linkRecord['file_extension'];
					if (isset($formatKey) && array_key_exists($formatKey, \Yii::app()->params['tooltips']['wallbin']))
						$record['tooltip'] = $record['tooltip'] . PHP_EOL . \Yii::app()->params['tooltips']['wallbin'][$formatKey];

					$type = $linkRecord['type'];
					switch ($linkRecord['search_format'])
					{
						case 'video':
						case 'wmv':
						case 'mp4':
							$record['file_type'] = 'video';
							break;
						case 'quicksite':
							$record['file_type'] = $linkRecord['search_format'];
							break;
						default:
							if ($type == 5)
								$record['file_type'] = 'folder';
							if ($type == 8)
								$record['file_type'] = 'url';
							else
								$record['file_type'] = $linkRecord['search_format'];
							break;
					}

					$record['date'] = array(
						'display' => $type != 6 ? date(\Yii::app()->params['outputDateFormat'], strtotime($linkRecord['link_date'])) : '',
						'value' => strtotime($linkRecord['link_date'])
					);

					$extendedProperties = \BaseLinkSettings::createByContent($linkRecord['extended_properties']);
					$fileInfo = \FileInfo::fromLinkData(
						$record['id'],
						$type,
						$linkRecord['name'],
						$linkRecord['path'],
						$extendedProperties,
						$library);
					$record['isFile'] = $fileInfo->isFile;

					$isHyperlink = LibraryLink::isOpenedAsHyperlink($type, $extendedProperties);

					$isLinkBundle = $linkRecord['original_format'] === 'link bundle';

					$record['isHyperlink'] = $isHyperlink;
					if ($isHyperlink)
					{
						$record['url_header'] = 'URL';
						$record['url'] = $fileInfo->link;

						$currentDomainInfo = parse_url(\Yii::app()->getBaseUrl(true));
						$urlInfo = parse_url($fileInfo->link);

						$domainHost = $currentDomainInfo['host'];
						$urlHost = array_key_exists('host', $urlInfo) ? $urlInfo['host'] : null;
						$urlPath = array_key_exists('path', $urlInfo) ? strtolower($urlInfo['path']) : null;

						$record['isExternalHyperlink'] = $domainHost != $urlHost ||
							(isset($urlPath) &&
								(strpos($urlPath, 'qpage') ||
									strpos($urlPath, 'public_links') ||
									strpos($urlPath, 'getSinglePage')));
					}
					else if ($fileInfo->isFile || $isLinkBundle)
					{
						$record['url_header'] = 'DownloadURL';
						$record['url'] = \FileInfo::getFileMIME($linkRecord['original_format']) . ':' .
							(isset($fileInfo->name) ? $fileInfo->name : $linkRecord['file_name']) . ':' .
							str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $fileInfo->link);
					}

					$record['isDraggable'] = $fileInfo->isFile || $isHyperlink || $isLinkBundle;

					$record['extended_data'] = array();
					foreach ($extraColumns as $column)
						$record['extended_data'][$column] = $linkRecord[$column];

					foreach ($baseColumnSettings as $key => $value)
					{
						/** @var DataTableColumnSettings $columnSettings */
						$columnSettings = $value;
						switch ($key)
						{
							case DataTableQuerySettings::DataTagCategory:
								if ($columnSettings->enable)
									$record['tag'] = $linkRecord['tag'];
								break;
							case DataTableQuerySettings::DataTagRate:
								if ($columnSettings->enable)
									$record['rate'] = array(
										'value' => $linkRecord['rate'],
										'image' => \LinkRateRecord::getStarImage(floatval($linkRecord['rate']))
									);
								break;
							case DataTableQuerySettings::DataTagViewsCount:
								if ($columnSettings->enable)
									$record['views'] = array(
										'value' => intval($linkRecord['total_views']),
										'display' => $linkRecord['total_views'] == 0 ? '' : $linkRecord['total_views']
									);
								break;
							case DataTableQuerySettings::DataTagThumbnail:
								if ($columnSettings->enable)
								{
									$imageUrl = null;
									if (!empty($linkRecord['thumbnail']))
										$imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '//' . 'sd_cache' . '//' . $linkRecord['thumbnail']);
									else
									{
										$imageFileName = null;
										switch ($linkRecord['search_format'])
										{
											case 'youtube':
												if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $fileInfo->link, $match))
												{
													$youTubeId = $match[1];
													$imageUrl = sprintf("https://img.youtube.com/vi/%s/0.jpg", $youTubeId);
												}
												break;
											case 'vimeo':
												if (preg_match('/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|(\w*\/)*review\/|)(\d+)(?:$|\/|\?)/', $fileInfo->link, $match))
												{
													try
													{
														$vimeoId = $match[4];
														$vimeoInfo = \CJSON::decode(@file_get_contents(sprintf("http://vimeo.com/api/v2/video/%s.json", $vimeoId)), true);
														$imageUrl = $vimeoInfo[0]["thumbnail_large"];
													}
													catch (\Exception $ex)
													{
													}
												}
												if (empty($imageUrl))
													$imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/vimeo.png');
												break;
											case 'internal library':
												if (!isset($internalLibraryThumbnailFiles))
												{
													$internalLibraryThumbnailFiles = array();
													$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'datatables' . DIRECTORY_SEPARATOR . 'library';
													if (is_dir($folderPath))
													{
														if ($dh = opendir($folderPath))
														{
															while (($file = readdir($dh)) !== false)
															{
																if ($file !== '.' && $file !== '..')
																	$internalLibraryThumbnailFiles[] = $file;
															}
															closedir($dh);
														}
													}
												}
												if (count($internalLibraryThumbnailFiles) > 0)
													$imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/datatables/library/' . $internalLibraryThumbnailFiles[array_rand($internalLibraryThumbnailFiles)]);
												else
													$imageFileName = $linkRecord['search_format'];
												break;
											case 'internal page':
												if (!isset($internalPageThumbnailFiles))
												{
													$internalPageThumbnailFiles = array();
													$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'datatables' . DIRECTORY_SEPARATOR . 'page';
													if (is_dir($folderPath))
													{
														if ($dh = opendir($folderPath))
														{
															while (($file = readdir($dh)) !== false)
															{
																if ($file !== '.' && $file !== '..')
																	$internalPageThumbnailFiles[] = $file;
															}
															closedir($dh);
														}
													}
												}
												if (count($internalPageThumbnailFiles) > 0)
													$imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/datatables/page/' . $internalPageThumbnailFiles[array_rand($internalPageThumbnailFiles)]);
												else
													$imageFileName = $linkRecord['search_format'];
												break;
											case 'internal window':
												if (!isset($internalWindowThumbnailFiles))
												{
													$internalWindowThumbnailFiles = array();
													$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'datatables' . DIRECTORY_SEPARATOR . 'window';
													if (is_dir($folderPath))
													{
														if ($dh = opendir($folderPath))
														{
															while (($file = readdir($dh)) !== false)
															{
																if ($file !== '.' && $file !== '..')
																	$internalWindowThumbnailFiles[] = $file;
															}
															closedir($dh);
														}
													}
												}
												if (count($internalWindowThumbnailFiles) > 0)
													$imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/datatables/window/' . $internalWindowThumbnailFiles[array_rand($internalWindowThumbnailFiles)]);
												else
													$imageFileName = $linkRecord['search_format'];
												break;
											case 'internal shortcut':
												if (!isset($internalShortcutThumbnailFiles))
												{
													$internalShortcutThumbnailFiles = array();
													$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'internal-links' . DIRECTORY_SEPARATOR . 'datatables' . DIRECTORY_SEPARATOR . 'shortcut';
													if (is_dir($folderPath))
													{
														if ($dh = opendir($folderPath))
														{
															while (($file = readdir($dh)) !== false)
															{
																if ($file !== '.' && $file !== '..')
																	$internalShortcutThumbnailFiles[] = $file;
															}
															closedir($dh);
														}
													}
												}
												if (count($internalShortcutThumbnailFiles) > 0)
													$imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/internal-links/datatables/shortcut/' . $internalShortcutThumbnailFiles[array_rand($internalShortcutThumbnailFiles)]);
												else
													$imageFileName = $linkRecord['search_format'];
												break;
											case 'app':
											case 'doc':
											case 'html5':
											case 'jpeg':
											case 'lan':
											case 'link bundle':
											case 'pdf':
											case 'png':
											case 'ppt':
											case 'quicksite':
											case 'url':
											case 'video':
											case 'xls':
											case 'internal link':
												$imageFileName = $linkRecord['search_format'];
												break;
											default:
												$fileExtension = $linkRecord['file_extension'];
												if (!empty($fileExtension) && in_array($fileExtension, self::$defaultThumbnailFileKeys))
													$imageFileName = $fileExtension;
												break;
										}
										if (!empty($imageFileName))
											$imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . '/images/grid/thumbnail-placeholder/' . $imageFileName . '.png');
									}
									if (!empty($imageUrl))
										$record['thumbnail'] = sprintf('<img src="%s" style="' . ($columnSettings->width > 0 ? ('max-width:' . $columnSettings->width . 'px;') : '') . ($columnSettings->height > 0 ? ('max-height:' . $columnSettings->height . 'px;') : '') . '">', $imageUrl);
									else
										$record['thumbnail'] = 'Not found';
								}
								break;
						}
					}
					$dataset[] = $record;
				}
			}
			return $dataset;
		}

		/**
		 * @param $linkQueryResult array
		 * @param $baseColumnSettings array
		 * @param $extraColumns array
		 * @return array
		 */
		public static function formatExtendedData($linkQueryResult, $baseColumnSettings, $extraColumns)
		{
			return self::formatRegularData($linkQueryResult, $baseColumnSettings, $extraColumns);
		}
	}