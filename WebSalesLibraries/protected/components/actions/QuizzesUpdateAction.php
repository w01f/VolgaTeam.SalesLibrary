<?php

	/**
	 * Class QuizzesUpdateAction
	 */
	class QuizzesUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			QuizGroupRecord::clearData();

			$rootFolderPath = \application\models\wallbin\models\web\LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . 'quizzes';
			if (file_exists($rootFolderPath))
			{
				$rootFolder = new DirectoryIterator($rootFolderPath);
				$subFolders = array();
				/** @var $subFolder DirectoryIterator */
				foreach ($rootFolder as $subFolder)
				{
					if ($subFolder->isDir() && !$subFolder->isDot())
					{
						$name = $subFolder->getBasename();
						if ($name != '_gsdata_')
							$subFolders[$name] = $subFolder->getPathname();
					}
				}
				ksort($subFolders);
				$subFolderOrder = 0;
				foreach ($subFolders as $name => $path)
				{
					$this->loadGroup(null, null, $name, $path, $subFolderOrder);
					$subFolderOrder++;
				}
			}
			echo "Job completed...\n";
		}

		/**
		 * @param $parentGroupId string
		 * @param $topLevelGroupId string
		 * @param $name string
		 * @param $path string
		 * @param $order int
		 */
		private function loadGroup($parentGroupId, $topLevelGroupId, $name, $path, $order)
		{
			$quizConfigPath = realpath($path . DIRECTORY_SEPARATOR . 'quiz.xml');
			if (file_exists($quizConfigPath))
				$this->loadQuiz($parentGroupId, $quizConfigPath);
			else
			{
				$quizGroup = new QuizGroupRecord();
				$groupId = uniqid();
				if (!isset($topLevelGroupId))
					$topLevelGroupId = $groupId;
				$quizGroup->id = $groupId;
				$quizGroup->name = $name;
				$quizGroup->order = $order;
				$quizGroup->id_parent = $parentGroupId;
				$quizGroup->id_top_level = $topLevelGroupId;
				$groupConfigPath = realpath($path . DIRECTORY_SEPARATOR . 'config.xml');
				if (file_exists($groupConfigPath))
					$quizGroup->config = file_get_contents($groupConfigPath);
				$quizGroup->save();

				$folderIterator = new DirectoryIterator(realpath($path));
				$subFolders = array();
				foreach ($folderIterator as $subFolder)
				{
					/** @var $subFolder DirectoryIterator */
					if ($subFolder->isDir() && !$subFolder->isDot())
						$subFolders[$subFolder->getBasename()] = $subFolder->getPathname();
				}
				ksort($subFolders);
				$subFolderOrder = 0;
				foreach ($subFolders as $name => $path)
				{
					$this->loadGroup($groupId, $topLevelGroupId, $name, $path, $subFolderOrder);
					$subFolderOrder++;
				}
			}
		}

		/**
		 * @param $parentGroupId string
		 * @param $configPath string
		 */
		private function loadQuiz($parentGroupId, $configPath)
		{
			$quiz = new QuizRecord();
			$quiz->id = uniqid();
			$quiz->id_group = $parentGroupId;
			$quiz->source_path = '/' . str_replace('\\', '/', str_replace(Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR, '', dirname($configPath)));

			$configContent = file_get_contents($configPath);
			$quizConfig = new DOMDocument();
			$quizConfig->loadXML($configContent);
			$xpath = new DomXPath($quizConfig);

			$queryResult = $xpath->query('//Quiz/Title');
			$quiz->name = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$queryResult = $xpath->query('//Quiz/Position');
			$quiz->order = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Quiz/ID');
			$quiz->unique_id = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//Quiz/Pass');
			$quiz->pass_score = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 100;
			$quiz->config = $configContent;
			$quiz->save();
		}
	}