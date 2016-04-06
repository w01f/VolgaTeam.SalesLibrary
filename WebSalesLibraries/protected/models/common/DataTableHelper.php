<?php

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
					$record['name'] = $linkRecord['name'];

					$record['tooltip']= sprintf('%s<br><br>%s',
							isset($linkRecord['file_name']) && $linkRecord['file_name'] != '' ? $linkRecord['file_name'] : $linkRecord['name'],
							Yii::app()->params['tooltips']['wallbin'][$linkRecord['format']]);

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
						default:
							if ($type == 5)
								$record['file_type'] = 'folder';
							else if ($type == 8)
								$record['file_type'] = 'url';
							else
								$record['file_type'] = $linkRecord['format'];
							break;
					}

					$extendedProperties = CJSON::decode($linkRecord['extended_properties'], true);
					$record['extended_properties'] = $extendedProperties;
					$record['url'] = ($type == 8 || $type == 14) && $extendedProperties['forcePreview'] == true ?
						$linkRecord['path'] :
						'';

					$dataset[] = $record;
				}
			}
			return $dataset;
		}
	}