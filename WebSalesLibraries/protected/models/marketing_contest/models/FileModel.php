<?
	namespace application\models\marketing_contest\models;

	class FileModel
	{
		public $id;
		public $order;
		public $fileName;
		public $fileFormat;
		public $uploadDate;

		/**
		 * @param $fileRecord \MarketingContestFileRecord
		 * @return FileModel
		 */
		public static function fromRecord($fileRecord)
		{
			$model = new self();

			$model->id = $fileRecord->id;
			$model->order = $fileRecord->list_order;
			$model->fileName = $fileRecord->file_name;
			$model->fileFormat = $fileRecord->file_format;
			$model->uploadDate = $fileRecord->upload_date;

			return $model;
		}
	}