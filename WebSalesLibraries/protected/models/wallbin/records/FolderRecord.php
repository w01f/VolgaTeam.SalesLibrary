<?php

	/**
	 * Class FolderRecord
	 * @property mixed id
	 * @property mixed id_library
	 * @property mixed id_page
	 * @property mixed name
	 * @property mixed column_order
	 * @property mixed row_order
	 * @property mixed border_color
	 * @property mixed window_back_color
	 * @property mixed window_fore_color
	 * @property mixed header_back_color
	 * @property mixed header_fore_color
	 * @property mixed window_font_name
	 * @property mixed window_font_size
	 * @property mixed window_font_bold
	 * @property mixed window_font_italic
	 * @property mixed header_font_name
	 * @property mixed header_font_size
	 * @property mixed header_font_bold
	 * @property mixed header_font_italic
	 * @property mixed header_alignment
	 * @property mixed widget
	 * @property mixed enable_widget
	 * @property mixed date_add
	 * @property bool|string date_modify
	 */
	class FolderRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return FolderRecord
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
			return '{{folder}}';
		}

		/**
		 * @return array
		 */
		public function relations()
		{
			return array(
				'links' => array(self::HAS_MANY, 'LinkRecord', 'id_folder'),
			);
		}

		/**
		 * @param $folderId
		 * @return array
		 */
		public function getChildLinkIds($folderId)
		{
			$linkIds = array();
			$criteria = new CDbCriteria;
			$criteria->condition = 't.id=:id and links.id_parent_link is null and links.type<>9 and no_share=0 and is_restricted=0';
			$criteria->params = array(':id' => $folderId);
			$criteria->order = 'links.order ASC';
			$result = $this->with('links')->find($criteria);
			if (isset($result))
				foreach ($result->links as $link)
					$linkIds[] = $link->id;
			return $linkIds;
		}

		/**
		 * @param $folder
		 * @param $libraryRootPath
		 */
		public static function updateData($folder, $libraryRootPath)
		{
			$needToUpdate = false;
			$needToCreate = false;
			/** @var $folderRecord FolderRecord */
			$folderRecord = self::model()->findByPk($folder['id']);
			$folderDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($folder['dateModify']));
			if ($folderRecord !== null)
			{
				if ($folderRecord->date_modify != null)
					if ($folderRecord->date_modify != $folderDate)
						$needToUpdate = true;
			}
			else
			{
				$folderRecord = new FolderRecord();
				$needToCreate = true;
			}
			if ($needToCreate || $needToUpdate)
			{
				$folderRecord->id = $folder['id'];
				$folderRecord->id_page = $folder['pageId'];
				$folderRecord->id_library = $folder['libraryId'];
				$folderRecord->name = $folder['name'];
				$folderRecord->column_order = $folder['columnOrder'];
				$folderRecord->row_order = $folder['rowOrder'];
				$folderRecord->border_color = $folder['borderColor'];
				$folderRecord->window_back_color = $folder['windowBackColor'];
				$folderRecord->window_fore_color = $folder['windowForeColor'];
				$folderRecord->header_back_color = $folder['headerBackColor'];
				$folderRecord->header_fore_color = $folder['headerForeColor'];
				$folderRecord->window_font_name = $folder['windowFont']['name'];
				$folderRecord->window_font_size = $folder['windowFont']['size'];
				$folderRecord->window_font_bold = $folder['windowFont']['isBold'];
				$folderRecord->window_font_italic = $folder['windowFont']['isItalic'];
				$folderRecord->header_font_name = $folder['headerFont']['name'];
				$folderRecord->header_font_size = $folder['headerFont']['size'];
				$folderRecord->header_font_bold = $folder['headerFont']['isBold'];
				$folderRecord->header_font_italic = $folder['headerFont']['isItalic'];
				$folderRecord->header_alignment = $folder['headerAlignment'];
				$folderRecord->enable_widget = $folder['enableWidget'];
				$folderRecord->widget = $folder['widget'];
				$folderRecord->date_add = date(Yii::app()->params['mysqlDateFormat'], strtotime($folder['dateAdd']));
				$folderRecord->date_modify = $folderDate;

				echo 'Window ' . ($needToCreate ? 'created' : 'updated') . ': ' . $folder['name'] . "\n";
			}
			$folderRecord->id_banner = $folder['banner']['id'];
			$folderRecord->save();

			BannerRecord::updateData($folder['banner']);

			$linkIds = null;
			foreach ($folder['files'] as $link)
			{
				LinkRecord::updateData($link, $libraryRootPath);
				$linkIds[] = $link['id'];
			}
			LinkRecord::clearByIds($folder['id'], $linkIds);
		}

		/**
		 * @param $libraryId
		 */
		public static function clearByLibrary($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

		/**
		 * @param $pageId
		 * @param $folderIds
		 */
		public static function clearByIds($pageId, $folderIds)
		{
			/** @var $folderRecords FolderRecord[] */
			if (isset($folderIds))
				$folderRecords = self::model()->findAll("id_page = '" . $pageId . "' and id not in ('" . implode("','", $folderIds) . "')");
			else
				$folderRecords = self::model()->findAll("id_page = '" . $pageId . "'");
			foreach ($folderRecords as $folderRecord)
			{
				LinkRecord::clearByIds($folderRecord->id, null);
				$folderRecord->delete();
			}
		}
	}
