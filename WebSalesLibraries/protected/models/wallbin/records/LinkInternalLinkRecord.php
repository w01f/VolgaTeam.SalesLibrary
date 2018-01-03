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

				if (isset($libraryName) && isset($pageName) && isset($windowName) && isset($linkName))
				{
					$linkRecord = LinkRecord::getLinkByName($libraryName, $pageName, $windowName, $linkName);
					if (isset($linkRecord))
					{
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
