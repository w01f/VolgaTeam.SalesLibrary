<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\Library as Library;

	/**
	 * Class DataTableHelper
	 */
	class DataTableHelper
	{
		/**
		 * @param $linkQueryResult array
		 * @return array
		 */
		public static function formatRegularData($linkQueryResult)
		{
			$dataset = array();
			if (count($linkQueryResult) > 0)
			{
				$libraryManager = new LibraryManager();
				foreach ($linkQueryResult as $linkRecord)
				{
					if (array_key_exists('type', $linkRecord))
						$type = $linkRecord['type'];
					else
						$type = 9999;

					$record['id'] = $linkRecord['id'];
					$record['name'] = nl2br($linkRecord['name']);

					$record['file_name'] = $linkRecord['file_name'];
					$record['path'] = $linkRecord['path'];
					$record['lib_name'] = $linkRecord['lib_name'];

					$record['tooltip'] = isset($linkRecord['file_name']) && $linkRecord['file_name'] != '' ? $linkRecord['file_name'] : $linkRecord['name'];
					if (isset($linkRecord['format']) && $linkRecord['format'] != 'other')
						$formatKey = $linkRecord['format'];
					else
						$formatKey = $linkRecord['file_extension'];
					if (isset($formatKey) && array_key_exists($formatKey, \Yii::app()->params['tooltips']['wallbin']))
						$record['tooltip'] = $record['tooltip'] . PHP_EOL . \Yii::app()->params['tooltips']['wallbin'][$formatKey];

					$record['date'] = array(
						'display' => date(Yii::app()->params['outputDateFormat'], strtotime($linkRecord['link_date'])),
						'value' => strtotime($linkRecord['link_date'])
					);

					/** @var $library Library */
					$library = $libraryManager->getLibraryById($linkRecord['id_library']);
					$record['library'] = array(
						'id' => $linkRecord['id_library'],
						'name' => isset($library) ? $library->name : ''
					);

					$record['tag'] = $linkRecord['tag'];

					$record['views'] = array(
						'value' => intval($linkRecord['total_views']),
						'display' => $linkRecord['total_views'] == 0 ? '' : $linkRecord['total_views']
					);

					$record['rate'] = array(
						'value' => $linkRecord['rate'],
						'image' => LinkRateRecord::getStarImage(floatval($linkRecord['rate']))
					);

					switch ($linkRecord['format'])
					{
						case 'video':
						case 'wmv':
						case 'mp4':
							$record['file_type'] = 'video';
							break;
						case 'quicksite':
							$record['file_type'] = $linkRecord['format'];
							break;
						default:
							if ($type == 5)
								$record['file_type'] = 'folder';
							if ($type == 8)
								$record['file_type'] = 'url';
							else
								$record['file_type'] = $linkRecord['format'];
							break;
					}

					$extendedProperties = CJSON::decode($linkRecord['extended_properties'], true);
					$record['extended_properties'] = $extendedProperties;

					$record['isHyperlink'] =
						($type == 8 ||
							$type == 10 ||
							$type == 12 ||
							$type == 17 ||
							$type == 18) &&
						$extendedProperties['forcePreview'] == true;

					$fileInfo = FileInfo::fromLinkData($type, $linkRecord['name'], $linkRecord['path'], $library);
					$record['isDraggable'] = $fileInfo->isFile || in_array($linkRecord['format'], array('url', 'quicksite', 'youtube'));

					if ($record['isHyperlink'])
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
					else if (in_array($linkRecord['format'], array('url', 'quicksite', 'youtube')))
					{
						$record['url_header'] = 'URL';
						$record['url'] = $fileInfo->link;
					}
					else
					{
						$record['url_header'] = 'DownloadURL';
						$record['url'] = FileInfo::getFileMIME($linkRecord['format']) . ':' .
							(isset($fileInfo->name) ? $fileInfo->name : $linkRecord['file_name']) . ':' .
							str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', Yii::app()->getBaseUrl(true) . $fileInfo->link);
					}

					$dataset[] = $record;
				}
			}
			return $dataset;
		}
	}