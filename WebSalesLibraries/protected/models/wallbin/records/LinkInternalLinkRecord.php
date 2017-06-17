<?

	/**
	 * Class LinkInternalLinkRecord
	 * @property mixed id
	 * @property mixed id_internal
	 * @property mixed id_original
	 * @property mixed id_library
	 */
	class LinkInternalLinkRecord extends CActiveRecord
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
			return '{{link_internal_link}}';
		}

		/**
		 * @param string $linkId
		 * @param string $libraryId
		 * @param array $settingsArray
		 */
		public static function updateData($linkId, $libraryId, $settingsArray)
		{
			/** @var InternalLibraryObjectLinkSettings $linkSettings */
			$linkSettings = BaseLinkSettings::createByContent(CJSON::encode($settingsArray));
			if ($linkSettings->internalLinkType == 4)
			{
				$libraryName = str_replace("'", "''", $linkSettings->libraryName);
				$pageName = str_replace("'", "''", $linkSettings->pageName);
				$windowName = str_replace("'", "''", $linkSettings->windowName);
				$linkName = str_replace("'", "''", $linkSettings->linkName);

				if (isset($libraryName) && isset($pageName) && isset($linkName) && isset($windowName))
				{
					$linkRecord = Yii::app()->db->createCommand()
						->select("l.*")
						->from('tbl_link l')
						->join('tbl_folder f', 'f.id = l.id_folder')
						->join('tbl_page p', 'p.id = f.id_page')
						->join('tbl_library lb', 'lb.id = p.id_library')
						->where("(l.file_name='" . $linkName . "' or l.name='" . $linkName . "') and f.name='" . $windowName . "' and p.name='" . $pageName . "' and lb.name='" . $libraryName . "'")
						->queryRow();
					if ($linkRecord != false)
					{
						$linkRecord = (object)$linkRecord;
						/** @var LibraryLinkBundleItem $bundleItem */
						$internalLinkRecord = new self();
						$internalLinkRecord->id = uniqid();
						$internalLinkRecord->id_internal = $linkId;
						$internalLinkRecord->id_original = $linkRecord->id;
						$internalLinkRecord->id_library = $libraryId;
						$internalLinkRecord->save();
					}
				}
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
