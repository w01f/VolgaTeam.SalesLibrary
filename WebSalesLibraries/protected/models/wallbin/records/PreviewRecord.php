<?php

	/**
	 * Class PreviewRecord
	 * @property mixed id_container
	 * @property mixed id_library
	 * @property string type
	 * @property string relative_path
	 */
	class PreviewRecord extends CActiveRecord
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
			return '{{preview}}';
		}

		/**
		 * @param $previewContainer
		 */
		public static function updateData($previewContainer)
		{
			if (array_key_exists('pngLinks', $previewContainer))
				if (isset($previewContainer['pngLinks']))
					foreach ($previewContainer['pngLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'png';
						$previewRecord->relative_path = $link;
						if (array_key_exists('thumbsWidth', $previewContainer))
							$previewRecord->thumb_width = $previewContainer['thumbsWidth'];
						if (array_key_exists('thumbsHeight', $previewContainer))
							$previewRecord->thumb_height = $previewContainer['thumbsHeight'];
						$previewRecord->save();
					}
			if (array_key_exists('pngPhoneLinks', $previewContainer))
				if (isset($previewContainer['pngPhoneLinks']))
					foreach ($previewContainer['pngPhoneLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'png_phone';
						$previewRecord->relative_path = $link;
						$previewRecord->save();
					}
			if (array_key_exists('pdfLinks', $previewContainer))
				if (isset($previewContainer['pdfLinks']))
					foreach ($previewContainer['pdfLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'pdf';
						$previewRecord->relative_path = $link;
						$previewRecord->save();
					}
			if (array_key_exists('mp4Links', $previewContainer))
				if (isset($previewContainer['mp4Links']))
					foreach ($previewContainer['mp4Links'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'mp4';
						$previewRecord->relative_path = $link;
						$previewRecord->save();
					}

			if (array_key_exists('mp4ThumbLinks', $previewContainer))
				if (isset($previewContainer['mp4ThumbLinks']))
					foreach ($previewContainer['mp4ThumbLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'mp4 thumb';
						$previewRecord->relative_path = $link;
						$previewRecord->save();
					}
			if (array_key_exists('newOfficeFormatLinks', $previewContainer))
				if (isset($previewContainer['newOfficeFormatLinks']))
					foreach ($previewContainer['newOfficeFormatLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'office';
						$previewRecord->relative_path = $link;
						$previewRecord->save();
					}
			if (array_key_exists('thumbsLinks', $previewContainer))
				if (isset($previewContainer['thumbsLinks']))
					foreach ($previewContainer['thumbsLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'thumbs';
						$previewRecord->relative_path = $link;
						if (array_key_exists('thumbsWidth', $previewContainer))
							$previewRecord->thumb_width = $previewContainer['thumbsWidth'];
						if (array_key_exists('thumbsHeight', $previewContainer))
							$previewRecord->thumb_height = $previewContainer['thumbsHeight'];
						$previewRecord->save();
					}
			if (array_key_exists('thumbsLinks', $previewContainer))
				if (isset($previewContainer['thumbsLinks']))
					foreach ($previewContainer['thumbsLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'thumbs';
						$previewRecord->relative_path = $link;
						if (array_key_exists('thumbsWidth', $previewContainer))
							$previewRecord->thumb_width = $previewContainer['thumbsWidth'];
						if (array_key_exists('thumbsHeight', $previewContainer))
							$previewRecord->thumb_height = $previewContainer['thumbsHeight'];
						$previewRecord->save();
					}
			if (array_key_exists('thumbsPhoneLinks', $previewContainer))
				if (isset($previewContainer['thumbsPhoneLinks']))
					foreach ($previewContainer['thumbsPhoneLinks'] as $link)
					{
						$previewRecord = new PreviewRecord();
						$previewRecord->id_container = $previewContainer['id'];
						$previewRecord->id_library = $previewContainer['libraryId'];
						$previewRecord->type = 'thumbs_phone';
						$previewRecord->relative_path = $link;
						$previewRecord->save();
					}
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}
