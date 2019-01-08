<?

	namespace application\models\drop_folder\models;

	class DropFolderManager
	{
		private $folderPath;

		public $isConfigured;

		public function __construct()
		{
			$this->isConfigured = false;
		}

		/**
		 * @param $folderName string
		 */
		public function init($folderName)
		{
			$folderNameFormatted = str_replace("/", DIRECTORY_SEPARATOR, $folderName);
			$folderPath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . \Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . $folderNameFormatted;
			if (!file_exists($folderPath))
				return;
			$this->folderPath = $folderPath;
			$this->isConfigured = true;
		}

		/**
		 * @return array
		 */
		public function getFiles()
		{
			$fileNames = array();
			$folderIterator = new \DirectoryIterator($this->folderPath);
			foreach ($folderIterator as $folderItem)
			{
				/** @var $folderItem \DirectoryIterator */
				if ($folderItem->isFile() && !$folderItem->isDot())
				{
					$fileName = $folderItem->getBasename();
					$fileNames[] = $fileName;
				}
			}
			return $fileNames;
		}

		/**
		 * @param $fileName string
		 * @param $tempFilePath string
		 */
		public function addFile($fileName, $tempFilePath)
		{
			$filePath = $this->folderPath . DIRECTORY_SEPARATOR . $fileName;
			$fileContent = @file_get_contents($tempFilePath);
			@file_put_contents($filePath, $fileContent);
		}

		/**
		 * @param $fileName string
		 */
		public function deleteFile($fileName)
		{
			$filePath = $this->folderPath . DIRECTORY_SEPARATOR . $fileName;
			@unlink($filePath);
		}

		/**
		 * @param $fileName string
		 */
		public function downloadFile($fileName)
		{
			$filePath = $this->folderPath . DIRECTORY_SEPARATOR . $fileName;
			$fileContent = @file_get_contents($filePath);
			\Yii::app()->getRequest()->sendFile($fileName, $fileContent);
		}
	}