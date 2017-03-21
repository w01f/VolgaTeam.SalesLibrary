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

		public function actionEditConditions()
		{
			$conditionTag = Yii::app()->request->getPost('conditionTag');
			$this->renderPartial('conditions/wrapper', array('conditionTag' => $conditionTag), false, true);
		}

		public function actionSearch()
		{
			$datasetKey = Yii::app()->request->getPost('datasetKey');
			if (!isset($datasetKey))
				$datasetKey = uniqid();

			$conditionsEncoded = Yii::app()->request->getPost('conditions');

			$conditions = isset($conditionsEncoded) ?
				SearchConditions::fromJson($conditionsEncoded) :
				new SearchConditions();

			$resultDataset = SearchHelper::getDatasetByCondition($conditions, $datasetKey);

			$searchResultInfo = array();
			if (isset($conditions->text) && $conditions->text != '')
				$searchResultInfo['condition'] = sprintf('%s (%s)', $conditions->text, ($conditions->textExactMatch ? 'exact match' : 'partial match'));
			if (count($conditions->fileTypes) > 0)
				$searchResultInfo['types'] = implode(', ', $conditions->fileTypes);
			if (isset($conditions->startDate) && isset($conditions->endDate) && $conditions->startDate != '' && $conditions->endDate != '')
				$searchResultInfo['dates'] = sprintf('%s - %s', $conditions->startDate, $conditions->endDate);
			if (count($conditions->superFilters) > 0)
				$searchResultInfo['superTags'] = implode(', ', $conditions->superFilters);
			if (isset($conditions->categories) && count($conditions->categories) > 0)
			{
				$categories = array();
				foreach ($conditions->categories as $category)
					if (count($category->items) > 0)
						$categories[] = implode(', ', $category->items);
				$searchResultInfo['tags'] = implode(', ', $categories);
			}
			if (count($conditions->libraries) > 0)
			{
				$libraries = array();
				foreach ($conditions->libraries as $library)
					$libraries[] = $library->name;
				$searchResultInfo['libraries'] = implode(', ', $libraries);
			}
			$searchResultInfo['found'] = count($resultDataset);

			StatisticActivityRecord::writeCommonActivity('Search', 'Run', $searchResultInfo);

			echo CJSON::encode(array(
				'datasetKey' => $datasetKey,
				'dataset' => $resultDataset
			));
			Yii::app()->end();
		}
	}
