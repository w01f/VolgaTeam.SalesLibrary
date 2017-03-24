<?php

	/**
	 * Class LineBreakRecord
	 * @property mixed id
	 * @property mixed id_library
	 * @property mixed note
	 * @property mixed fore_color
	 * @property mixed font_name
	 * @property mixed font_size
	 * @property mixed font_bold
	 * @property mixed font_italic
	 * @property mixed font_underline
	 * @property mixed date_modify
	 */
	class LineBreakRecord extends CActiveRecord
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
			return '{{line_break}}';
		}

		/**
		 * @param $lineBreak
		 */
		public static function updateData($lineBreak)
		{
			$lineBreakRecord = new LineBreakRecord();
			$lineBreakRecord->id = $lineBreak['id'];
			$lineBreakRecord->id_library = $lineBreak['libraryId'];
			$lineBreakRecord->note = $lineBreak['note'];
			$lineBreakRecord->fore_color = $lineBreak['foreColor'];
			$lineBreakRecord->font_name = $lineBreak['font']['name'];
			$lineBreakRecord->font_size = $lineBreak['font']['size'];
			$lineBreakRecord->font_bold = $lineBreak['font']['isBold'];
			$lineBreakRecord->font_italic = $lineBreak['font']['isItalic'];
			$lineBreakRecord->font_underline = $lineBreak['font']['isUnderlined'];
			$lineBreakRecord->date_modify = date(Yii::app()->params['mysqlDateTimeFormat'], strtotime($lineBreak['dateModify']));
			$lineBreakRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

	}
