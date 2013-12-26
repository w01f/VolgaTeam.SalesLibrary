<?php
	class QuizzesUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			QuizGroupStorage::clearData();

			$rootFolderPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'quizzes';
			$subFolderOrder = 0;
			if (file_exists($rootFolderPath))
			{
				$rootFolder = new DirectoryIterator($rootFolderPath);
				foreach ($rootFolder as $subFolder)
				{
					if ($subFolder->isDir() && !$subFolder->isDot())
						$this->loadGroup(null, $subFolder, $subFolderOrder);
					$subFolderOrder++;
				}
			}
			echo "Job completed...\n";
		}

		private function loadGroup($parentGroupId, $folder, $order)
		{
			$path = $folder->getPathname();
			$quizConfigPath = realpath($path . DIRECTORY_SEPARATOR . 'quiz.xml');
			if (file_exists($quizConfigPath))
				$this->loadQuiz($parentGroupId, $quizConfigPath);
			else
			{
				$quizGroup = new QuizGroupStorage();
				$groupId = uniqid();
				$quizGroup->id = $groupId;
				$quizGroup->name = $folder->getBasename();
				$quizGroup->order = $order;
				$quizGroup->id_parent = $parentGroupId;
				$quizGroup->save();

				$subFolderOrder = 0;
				$folderIterator = new DirectoryIterator($path);
				foreach ($folderIterator as $subFolder)
				{
					if ($subFolder->isDir() && !$subFolder->isDot())
						$this->loadGroup($groupId, $subFolder, $subFolderOrder);
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