<?
	namespace application\models\data_query\data_table;

	use application\models\data_query\common\DataColumnSettings;
	use application\models\data_query\common\QuerySettings;
	use application\models\wallbin\models\web\LibraryLink;
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\Library as Library;

	/**
	 * Class DataTableHelper
	 */
	class DataTableHelper
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

					$fileInfo = \FileInfo::fromLinkData(
						$record['id'],
						$type,
						$linkRecord['name'],
						$linkRecord['path'],
						\BaseLinkSettings::createByContent($linkRecord['extended_properties']),
						$library);
					$record['isFile'] = $fileInfo->isFile;

					$extendedProperties = \BaseLinkSettings::createByContent($linkRecord['extended_properties']);
					$isHyperlink = LibraryLink::isOpenedAsHyperlink($type, $extendedProperties);
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
					else if ($fileInfo->isFile)
					{
						$record['url_header'] = 'DownloadURL';
						$record['url'] = \FileInfo::getFileMIME($linkRecord['original_format']) . ':' .
							(isset($fileInfo->name) ? $fileInfo->name : $linkRecord['file_name']) . ':' .
							str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', \Yii::app()->getBaseUrl(true) . $fileInfo->link);
					}

					$record['isDraggable'] = $fileInfo->isFile || $isHyperlink;

					$record['extended_data'] = array();
					foreach ($extraColumns as $column)
						$record['extended_data'][$column] = $linkRecord[$column];

					foreach ($baseColumnSettings as $key => $value)
					{
						/** @var DataColumnSettings $columnSettings */
						$columnSettings = $value;
						switch ($key)
						{
							case QuerySettings::DataTagCategory:
								if ($columnSettings->enable)
									$record['tag'] = $linkRecord['tag'];
								break;
							case QuerySettings::DataTagRate:
								if ($columnSettings->enable)
									$record['rate'] = array(
										'value' => $linkRecord['rate'],
										'image' => \LinkRateRecord::getStarImage(floatval($linkRecord['rate']))
									);
								break;
							case QuerySettings::DataTagViewsCount:
								if ($columnSettings->enable)
									$record['views'] = array(
										'value' => intval($linkRecord['total_views']),
										'display' => $linkRecord['total_views'] == 0 ? '' : $linkRecord['total_views']
									);
								break;
							case QuerySettings::DataTagThumbnail:
								if ($columnSettings->enable)
								{
									$imageUrl = null;
									if (!empty($linkRecord['thumbnail']))
										$imageUrl = \Utils::formatUrl($library->storageLink . '//' . $linkRecord['thumbnail']);
									else
									{
										$imageFileName = null;
										switch ($linkRecord['search_format'])
										{
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
											case 'vimeo':
											case 'xls':
											case 'youtube':
											case 'internal library':
											case 'internal page':
											case 'internal shortcut':
											case 'internal window':
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