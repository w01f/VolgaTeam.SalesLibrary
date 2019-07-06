<?php
	use application\models\data_query\conditions\ConditionalQueryHelper;
	use application\models\data_query\conditions\TableQueryConditions;

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
				TableQueryConditions::fromJson($conditionsEncoded) :
				new TableQueryConditions();

			$resultDataset = ConditionalQueryHelper::getDatasetByCondition($conditions, $datasetKey);

			echo CJSON::encode(array(
				'datasetKey' => $datasetKey,
				'dataset' => $resultDataset
			));
			Yii::app()->end();
		}
	}
