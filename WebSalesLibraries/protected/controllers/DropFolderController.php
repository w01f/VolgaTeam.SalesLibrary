<?

	/**
	 * Class DropFolderController
	 */
	class DropFolderController extends IsdController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'drop_folder');
		}

		public function actionGetFiles()
		{
			$folderName = Yii::app()->request->getPost('folderName');
			$dropFolderManager = new \application\models\drop_folder\models\DropFolderManager();
			$dropFolderManager->init($folderName);
			if ($dropFolderManager->isConfigured)
			{
				$files = $dropFolderManager->getFiles();
				if (count($files) > 0)
					$this->renderPartial('files', array(
						'files' => $files,
						'folderName' => $folderName,
					), false, true);
			}
			echo '';
		}

		public function actionUploadFile()
		{
			$folderName = Yii::app()->request->getQuery('folderName');
			$dropFolderManager = new \application\models\drop_folder\models\DropFolderManager();
			$dropFolderManager->init($folderName);
			if ($dropFolderManager->isConfigured && count($_FILES) > 0)
			{
				$filesForUploading = array();
				if (is_array($_FILES['file']['name']))
					for ($i = 0; $i < count($_FILES['file']['name']); $i++)
						$filesForUploading[] = array(
							'name' => $_FILES['file']['name'][$i],
							'tmp_name' => $_FILES['file']['tmp_name'][$i]
						);
				else
					$filesForUploading[] = array(
						'name' => $_FILES['file']['name'],
						'tmp_name' => $_FILES['file']['tmp_name']
					);

				foreach ($filesForUploading as $fileForUploading)
					$dropFolderManager->addFile(
						$fileForUploading['name'],
						$fileForUploading['tmp_name']);
			}
			Yii::app()->end();
		}

		public function actionDownloadFile()
		{
			$folderName = Yii::app()->request->getQuery('folderName');
			if (empty($folderName))
				$folderName = Yii::app()->request->getPost('folderName');
			$fileName = Yii::app()->request->getQuery('fileName');
			if (empty($fileName))
				$fileName = Yii::app()->request->getPost('fileName');
			$dropFolderManager = new \application\models\drop_folder\models\DropFolderManager();
			$dropFolderManager->init($folderName);
			$dropFolderManager->downloadFile($fileName);
			Yii::app()->end();
		}

		public function actionDeleteFile()
		{
			$folderName = Yii::app()->request->getPost('folderName');
			$fileName = Yii::app()->request->getPost('fileName');
			$dropFolderManager = new \application\models\drop_folder\models\DropFolderManager();
			$dropFolderManager->init($folderName);
			$dropFolderManager->deleteFile($fileName);
			echo CJSON::encode(array("success" => true));
		}
	}
