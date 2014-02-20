<?php

	class QuizzesUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			QuizGroupStorage::clearData();

			$rootFolderPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'quizzes';
			if (file_exists($rootFolderPath))
			{
				$rootFolder = new DirectoryIterator($rootFolderPath);
				$subFolders = array();
				foreach ($rootFolder as $subFolder)
				{
					if ($subFolder->isDir() && !$subFolder->isDot())
						$subFolders[$subFolder->getBasename()] = $subFolder->getPathname();
				}
				ksort($subFolders);
				$subFolderOrder = 0;
				foreach ($subFolders as $name => $path)
				{
					$this->loadGroup(null, $name, $path, $subFolderOrder);
					$subFolderOrder++;
				}
			}
			echo "Job completed...\n";
		}

		private function loadGroup($parentGroupId, $name, $path, $order)
		{
			$quizConfigPath = realpath($path . DIRECTORY_SEPARATOR . 'quiz.xml');
			if (file_exists($quizConfigPath))
				$this->loadQuiz($parentGroupId, $quizConfigPath);
			else
			{
				$quizGroup = new QuizGroupStorage();
				$groupId = uniqid();
				$quizGroup->id = $groupId;
				$quizGroup->name = $name;
				$quizGroup->order = $order;
				$quizGroup->id_parent = $parentGroupId;
				$quizGroup->save();

				$folderIterator = new DirectoryIterator(realpath($path));
				$subFolders = array();
				foreach ($folderIterator as $subFolder)
				{
					if ($subFolder->isDir() && !$subFolder->isDot())
						$subFolders[$subFolder->getBasename()] = $subFolder->getPathname();
				}
				ksort($subFolders);
				$subFolderOrder = 0;
				foreach ($subFolders as $name => $path)
				{
					$this->loadGroup($groupId, $name, $path, $subFolderOrder);
					$subFolderOrder++;
				}
			}
		}

		private function loadQuiz($parentGroupId, $configPath)
		{
			$quiz = new QuizStorage();
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
			$quiz->config = $configContent;
			$quiz->save();
		}
	}