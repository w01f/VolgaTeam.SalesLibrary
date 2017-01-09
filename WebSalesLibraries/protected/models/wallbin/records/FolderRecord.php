<?php

	/**
	 * Class FolderRecord
	 * @property string id
	 * @property string id_library
	 * @property string id_page
	 * @property string name
	 * @property int column_order
	 * @property int row_order
	 * @property string border_color
	 * @property string window_back_color
	 * @property string window_fore_color
	 * @property string header_back_color
	 * @property string header_fore_color
	 * @property string window_font_name
	 * @property int window_font_size
	 * @property boolean window_font_bold
	 * @property boolean window_font_italic
	 * @property boolean window_font_underline
	 * @property string header_font_name
	 * @property int header_font_size
	 * @property boolean header_font_bold
	 * @property boolean header_font_italic
	 * @property boolean header_font_underline
	 * @property string header_alignment
	 * @property string widget
	 * @property boolean enable_widget
	 * @property string date_add
	 * @property string date_modify
	 * @property string folder_settings
	 * @property string widget_settings
	 * @property string banner_settings
	 */
	class FolderRecord extends CActiveRecord
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
		 * @return LinkRecord[]
		 */
		public function getChildLinkIds()
		{
			$criteria = new CDbCriteria;
			$criteria->condition = 't.id=:id and links.id_parent_link is null and links.type<>9 and no_share=0 and is_restricted=0';
			$criteria->params = array(':id' => $this->id);
			$criteria->order = 'links.order ASC';
			/** @var FolderRecord $result */
			$result = $this->with('links')->find($criteria);
			if (isset($result))
				return $result->links;
			return array();
		}

		/**
		 * @param $folder
		 * @param $libraryRootPath
		 */
		public static function updateDataFromSoap($folder, $libraryRootPath)
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
				$folderRecord->window_font_underline = $folder['windowFont']['isUnderlined'];
				$folderRecord->header_font_name = $folder['headerFont']['name'];
				$folderRecord->header_font_size = $folder['headerFont']['size'];
				$folderRecord->header_font_bold = $folder['headerFont']['isBold'];
				$folderRecord->header_font_italic = $folder['headerFont']['isItalic'];
				$folderRecord->header_font_underline = $folder['headerFont']['isUnderlined'];
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

			$linkIds = array();
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
		 * @param string $pageId
		 * @param array $excludeFolderIds
		 */
		public static function clearByIds($pageId, $excludeFolderIds)
		{
			/** @var $folderRecords FolderRecord[] */
			if (count($excludeFolderIds) > 0)
				$folderRecords = self::model()->findAll("id_page = '" . $pageId . "' and id not in ('" . implode("','", $excludeFolderIds) . "')");
			else
				$folderRecords = self::model()->findAll("id_page = '" . $pageId . "'");
			foreach ($folderRecords as $folderRecord)
			{
				LinkRecord::clearByIds($folderRecord->id, array());
				$folderRecord->delete();
			}
		}
	}
