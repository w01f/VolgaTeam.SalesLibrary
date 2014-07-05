<?php

	/**
	 * Class LibraryRecord
	 * @property mixed id
	 * @property string id_group
	 * @property mixed name
	 * @property int order
	 * @property mixed last_update
	 */
	class LibraryRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{library}}';
		}

		/**
		 * @param $library
		 * @param $sourceDate
		 * @param $libraryRootPath
		 * @return bool
		 */
		public static function updateData($library, $sourceDate, $libraryRootPath)
		{
			$needToUpdate = false;
			$needToCreate = false;
			/** @var $libraryRecord LibraryRecord */
			$libraryRecord = LibraryRecord::model()->findByPk($library['id']);
			if ($libraryRecord !== null)
			{
				if ($libraryRecord->last_update != null)
					if ($libraryRecord->last_update != date(Yii::app()->params['mysqlDateFormat'], $sourceDate))
						$needToUpdate = true;
			}
			else
			{
				$libraryRecord = new LibraryRecord();
				$needToCreate = true;
			}

			if ($needToCreate || $needToUpdate)
			{
				Yii::app()->cacheDB->flush();
				self::clearData($library['id']);

				$libraryRecord->id = $library['id'];
				$libraryRecord->name = $library['name'];
				$libraryRecord->last_update = date(Yii::app()->params['mysqlDateFormat'], $sourceDate);
				$libraryRecord->save();

				foreach ($library['autoWidgets'] as $autoWidget)
					AutoWidgetRecord::updateData($autoWidget);

				foreach ($library['previewContainers'] as $previewContainer)
					PreviewRecord::updateData($previewContainer);

				$pageIds = null;
				foreach ($library['pages'] as $page)
				{
					LibraryPageRecord::updateData($page, $libraryRootPath);
					$pageIds[] = $page['id'];
				}
				LibraryPageRecord::clearByIds($library['id'], $pageIds);

				if (array_key_exists('config', $library) && isset($library['config']))
					LibraryConfigRecord::updateData($library['config']);

				echo 'Library ' . ($needToCreate ? 'created' : 'updated') . ': ' . $library['name'] . "\n";
				return true;
			}
			else
				return false;
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			AutoWidgetRecord::clearData($libraryId);
			PreviewRecord::clearData($libraryId);
			LineBreakRecord::clearData($libraryId);
			BannerRecord::clearData($libraryId);
			FileCardRecord::clearData($libraryId);
			AttachmentRecord::clearData($libraryId);
			LinkSuperFilterRecord::clearData($libraryId);
			LinkCategoryRecord::clearData($libraryId);
			ColumnRecord::clearData($libraryId);
			UserLinkRecord::clearData($libraryId);
			UserPageCacheRecord::clearData($libraryId);
			LibraryConfigRecord::clearData($libraryId);
		}
	}
