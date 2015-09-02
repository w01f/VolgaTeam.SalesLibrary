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
			if (isset($conditions->categories) && count($conditions->categories) > 0)
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
