<?php

	/**
	 * @property mixed id_link
	 * @property mixed id_library
	 * @property mixed group
	 * @property mixed category
	 * @property mixed tag
	 */
	class LinkCategoryRecord extends CActiveRecord
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
			return '{{link_category}}';
		}

		/**
		 * @param $category array
		 */
		public static function updateData($category)
		{
			if(!is_array($category))
				$category = CJSON::decode(CJSON::encode($category),true);

			$categoryRecord = new LinkCategoryRecord();
			$categoryRecord->id_link = $category['linkId'];
			$categoryRecord->id_library = $category['libraryId'];
			$categoryRecord->group = $category['group'];
			$categoryRecord->category = $category['category'];
			$categoryRecord->tag = $category['tag'];
			$categoryRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

	}
