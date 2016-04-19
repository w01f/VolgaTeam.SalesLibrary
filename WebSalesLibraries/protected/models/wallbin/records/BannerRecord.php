<?php

	/**
	 * Class BannerRecord
	 * @property mixed id
	 * @property mixed id_library
	 * @property mixed enabled
	 * @property mixed image
	 * @property mixed show_text
	 * @property mixed image_alignment
	 * @property mixed text
	 * @property mixed fore_color
	 * @property mixed font_name
	 * @property mixed font_size
	 * @property mixed font_bold
	 * @property mixed font_italic
	 * @property mixed date_modify
	 */
	class BannerRecord extends CActiveRecord
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
			return '{{banner}}';
		}

		/**
		 * @param $banner
		 */
		public static function updateData($banner)
		{
			$bannerRecord = self::model()->findByPk($banner['id']);
			if (!isset($bannerRecord))
				$bannerRecord = new BannerRecord();
			$bannerRecord->id = $banner['id'];
			$bannerRecord->id_library = $banner['libraryId'];
			$bannerRecord->enabled = $banner['isEnabled'];
			$bannerRecord->image = $banner['image'];
			$bannerRecord->show_text = $banner['showText'];
			$bannerRecord->image_alignment = $banner['imageAlignment'];
			$bannerRecord->text = $banner['text'];
			$bannerRecord->fore_color = $banner['foreColor'];
			$bannerRecord->font_name = $banner['font']['name'];
			$bannerRecord->font_size = $banner['font']['size'];
			$bannerRecord->font_bold = $banner['font']['isBold'];
			$bannerRecord->font_italic = $banner['font']['isItalic'];
			$bannerRecord->date_modify = date(Yii::app()->params['mysqlDateFormat'], strtotime($banner['dateModify']));
			$bannerRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}
