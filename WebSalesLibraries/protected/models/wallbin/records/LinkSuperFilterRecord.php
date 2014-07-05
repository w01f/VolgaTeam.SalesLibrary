<?php

	/**
	 * Class LinkSuperFilterRecord
	 * @property mixed id_link
	 * @property mixed id_library
	 * @property mixed value
	 */
	class LinkSuperFilterRecord extends CActiveRecord
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
			return '{{link_super_filter}}';
		}

		/**
		 * @param $superFilter
		 */
		public static function updateData($superFilter)
		{
			$superFilterRecord = new LinkSuperFilterRecord();
			$superFilterRecord->id_link = $superFilter['linkId'];
			$superFilterRecord->id_library = $superFilter['libraryId'];
			$superFilterRecord->value = $superFilter['value'];
			$superFilterRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

	}
