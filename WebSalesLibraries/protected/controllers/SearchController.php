<?php

	/**
	 * Class SearchController
	 */
	class SearchController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'search');
		}

		public function actionGetSearchView()
		{
			$this->renderPartial('searchView', array(), false, true);
		}

		public function actionGetFileTypesView()
		{
			$this->renderPartial('fileTypesView', array(), false, true);
		}

		public function actionGetTagsView()
		{
			$categories = new CategoryManager();
			$categories->loadCategories();

			$this->renderPartial('tagsView', array('categories' => $categories), false, true);
		}

		public function actionGetDateView()
		{
			$this->renderPartial('dateView', array(), false, true);
		}

		public function actionGetLibrariesView()
		{
			$libraryManager = new LibraryManager();
			$libraries = $libraryManager->getLibraries();
			$libraryGroups = $libraryManager->getLibraryGroups();

			if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
			{
				if (count($libraryGroups) > 1 || count($libraries) > 1)
					$checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
				else
					unset(Yii::app()->request->cookies['selectedLibraryIds']);
			}

			foreach ($libraryGroups as $libraryGroup)
				foreach ($libraryGroup->libraries as $library)
					if (isset($checkedLibraryIds))
						$library->selected = in_array($library->id, $checkedLibraryIds);
					else
						$library->selected = true;
			$this->renderPartial('librariesView', array('libraryGroups' => $libraryGroups), false, true);
		}

		public function actionEditConditions()
		{
			$conditionTag = Yii::app()->request->getPost('conditionTag');
			$this->renderPartial('conditions/wrapper', array('conditionTag' => $conditionTag), false, true);
		}

		public function actionSearchByContent()
		{
			$isClear = Yii::app()->request->getPost('isClear');
			if (!isset($isClear))
			{
				$fileTypes = Yii::app()->request->getPost('fileTypes');
				$condition = Yii::app()->request->getPost('condition');
				$startDate = Yii::app()->request->getPost('startDate');
				$endDate = Yii::app()->request->getPost('endDate');
				$onlyWithCategories = filter_var(trim(Yii::app()->request->getPost('onlyWithCategories')), FILTER_VALIDATE_BOOLEAN);
				$sortColumn = Yii::app()->request->getPost('sortColumn');
				$sortDirection = Yii::app()->request->getPost('sortDirection');
				$datasetKey = Yii::app()->request->getPost('datasetKey');
				$baseDatasetKey = Yii::app()->request->getPost('baseDatasetKey');

				$checkedLibraryIds = Yii::app()->request->getPost('libraries');
				if (isset($checkedLibraryIds))
					$checkedLibraryIds = CJSON::decode($checkedLibraryIds);
				else
					$checkedLibraryIds = array();

				$superFilters = Yii::app()->request->getPost('superFilters');
				if (isset($superFilters))
					$superFilters = CJSON::decode($superFilters);

				$categories = Yii::app()->request->getPost('categories');
				if (isset($categories))
					$categories = CJSON::decode($categories);
				$categoriesExactMatch = Yii::app()->request->getPost('categoriesExactMatch');
				if (!isset($categoriesExactMatch))
					$categoriesExactMatch = true;

				if (strtolower(trim(Yii::app()->request->getPost('dateFile'))) == 'true')
					$dateFile = true;
				else
					$dateFile = false;

				if (strtolower(trim(Yii::app()->request->getPost('hideDuplicated'))) == 'true')
					$hideDuplicated = true;
				else
					$hideDuplicated = false;

				if (strtolower(trim(Yii::app()->request->getPost('onlyByName'))) == 'true')
					$onlyByName = true;
				else
					$onlyByName = false;

				if (strtolower(trim(Yii::app()->request->getPost('onlyByContent'))) == 'true')
					$onlyByContent = true;
				else
					$onlyByContent = false;

				if (!isset($datasetKey))
					$datasetKey = uniqid();

				$links = LinkRecord::searchByContentLegacy(
					SearchHelper::prepareTextCondition($condition),
					$fileTypes,
					$startDate,
					$endDate,
					$dateFile,
					$checkedLibraryIds,
					$superFilters,
					$categories,
					$categoriesExactMatch,
					$onlyWithCategories,
					$hideDuplicated,
					$onlyByName,
					$onlyByContent,
					$datasetKey,
					$baseDatasetKey,
					$sortColumn,
					$sortDirection);

				if (!isset($links))
					$links = null;

				$searchInfo['datasetKey'] = $datasetKey;
				if (isset($links))
					$searchInfo['count'] = 'Records: ' . count($links);
				else
					$searchInfo['count'] = 'No Files Meet your Criteria';
				if (isset($condition) && !($condition == '""' || $condition == ''))
					$searchInfo['condition'] = '<b>Keyword (' . (strstr($condition, '"') ? 'Exact match' : 'Partial match') . '):</b> ' . str_replace('"', '', $condition);
				if (isset($fileTypes))
					$searchInfo['file_types'] = '<b>File Types:</b> ' . implode(', ', $fileTypes);
				else
					$searchInfo['file_types'] = '<b>File Types:</b> None';
				if (isset($startDate) && isset($endDate))
					$searchInfo['dates'] = '<b>Dates:</b> ' . $startDate . ' - ' . $endDate;
				else
					$searchInfo['dates'] = '<b>Dates:</b> ALL';
				if (isset($superFilters))
					foreach ($superFilters as $superFilter)
						$categoryTags[] = '<b>' . $superFilter . '</b>';
				if (isset($categories))
				{
					foreach ($categories as $category)
						$groups[] = $category['category'];
					if (isset($groups))
					{
						$groups = array_unique($groups);
						foreach ($groups as $group)
						{
							foreach ($categories as $category)
								if ($category['category'] == $group)
									$tags[] = $category['tag'];
							if (isset($tags))
							{
								$categoryTags[] = '<b>' . $group . ':</b> ' . implode(', ', $tags);
								unset($tags);
							}
						}
					}
				}
				if (isset($categoryTags))
					$searchInfo['categories'] = implode('; ', $categoryTags);
				if (isset($checkedLibraryIds))
				{
					$allLibraryRecords = LibraryRecord::model()->findAll();
					foreach ($checkedLibraryIds as $libraryId)
					{
						/** @var $libraryRecord LibraryRecord */
						$libraryRecord = LibraryRecord::model()->findByPk($libraryId);
						if (isset($libraryRecord))
							$libraries[] = $libraryRecord->name;
					}
					if (isset($libraries))
					{
						if (count($allLibraryRecords) == count($libraries))
							$searchInfo['libraries'] = '<b>Libraries:</b> ALL';
						else
							$searchInfo['libraries'] = '<b>Libraries:</b> ' . implode(", ", $libraries);
					}
					else
						$searchInfo['libraries'] = '<b>Libraries:</b> Not selected';
				}
				else
					$searchInfo['libraries'] = 'Libraries: Not selected';


				if (!isset($searchInfo))
					$searchInfo = null;
				StatisticActivityRecord::WriteActivity('Search', 'Run', array(
					'Condition' => array_key_exists('condition', $searchInfo) ? $searchInfo['condition'] : null,
					'Types' => str_replace('File Types:', '', $searchInfo['file_types']),
					'Dates' => str_replace('Dates:', '', $searchInfo['dates']),
					'Tags' => array_key_exists('categories', $searchInfo) ? $searchInfo['categories'] : null,
					'Libraries' => array_key_exists('libraries', $searchInfo) ? str_replace('Libraries:', '', $searchInfo['libraries']) : null,
					'Found' => isset($links) ? count($links) : null,
				));
				$this->renderPartial('searchResult', array('searchInfo' => $searchInfo, 'links' => $links), false, true);
			}
			else
				$this->renderPartial('searchResult', array(), false, true);
		}

		public function actionSearchJson()
		{
			$datasetKey = Yii::app()->request->getPost('datasetKey');
			if (!isset($datasetKey))
				$datasetKey = uniqid();


			$conditionsEncoded = Yii::app()->request->getPost('conditions');
			$conditions = isset($conditionsEncoded) ?
				CJSON::decode($conditionsEncoded, false) :
				new SearchConditions();

			$resultDataset = SearchHelper::queryLinksByCondition($conditions, $datasetKey);

			$searchResultInfo = array();
			if (isset($conditions->text) && $conditions->text != '')
				$searchResultInfo['Condition'] = sprintf('%s (%s)', $conditions->text, ($conditions->textExactMatch ? 'exact match' : 'partial match'));
			if (count($conditions->fileTypes) > 0)
				$searchResultInfo['Types'] = implode(', ', $conditions->fileTypes);
			if (isset($conditions->startDate) && isset($conditions->endDate) && $conditions->startDate != '' && $conditions->endDate != '')
				$searchResultInfo['Dates'] = sprintf('%s - %s', $conditions->startDate, $conditions->endDate);
			if (count($conditions->superFilters) > 0)
				$searchResultInfo['Super Tags'] = implode(', ', $conditions->superFilters);
			if (count($conditions->categories) > 0)
			{
				$categories = array();
				foreach ($conditions->categories as $category)
					if (count($category->items) > 0)
						$categories[] = implode(', ', $category->items);
				$searchResultInfo['Tags'] = implode(', ', $categories);
			}
			if (count($conditions->libraries) > 0)
			{
				$libraries = array();
				foreach ($conditions->libraries as $library)
					$libraries[] = $library->name;
				$searchResultInfo['Libraries'] = implode(', ', $libraries);
			}
			$searchResultInfo['Found'] = count($resultDataset);

			StatisticActivityRecord::WriteActivity('Search', 'Run', $searchResultInfo);

			echo CJSON::encode(array(
				'datasetKey' => $datasetKey,
				'dataset' => $resultDataset
			));
			Yii::app()->end();
		}
	}
