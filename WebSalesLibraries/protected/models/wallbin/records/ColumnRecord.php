<?php

	/**
	 * Class ColumnRecord
	 * @property mixed id_page
	 * @property mixed id_library
	 * @property mixed name
	 * @property mixed order
	 * @property mixed back_color
	 * @property mixed fore_color
	 * @property mixed font_size
	 * @property mixed font_name
	 * @property mixed font_bold
	 * @property mixed font_italic
	 * @property mixed show_text
	 * @property mixed alignment
	 * @property mixed enable_widget
	 * @property mixed widget
	 * @property mixed date_modify
	 * @property mixed id_banner
	 */
	class ColumnRecord extends CActiveRecord
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
			return '{{column}}';
		}

		/**
		 * @param $column
		 */
		public static function updateData($column)
		{
			$columnRecord = new ColumnRecord();
			$columnRecord->id_page = $column['pageId'];
			$columnRecord->id_library = $column['libraryId'];
			$columnRecord->name = $column['name'];
			$columnRecord->order = $column['order'];
			$columnRecord->back_color = $column['backColor'];
			$columnRecord->fore_color = $column['foreColor'];
			$columnRecord->font_name = $column['font']['name'];
			$columnRecord->font_size = $column['font']['size'];
			$columnRecord->font_bold = $column['font']['isBold'];
			$columnRecord->font_italic = $column['font']['isItalic'];
			$columnRecord->show_text = $column['showText'];
			$columnRecord->alignment = $column['alignment'];
			$columnRecord->enable_widget = $column['enableWidget'];
			$columnRecord->widget = $column['widget'];
			$columnRecord->date_modify = date(Yii::app()->params['mysqlDateFormat'], strtotime($column['dateModify']));;

			$columnRecord->id_banner = $column['banner']['id'];
			BannerRecord::updateData($column['banner']);

			$columnRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

	}
