<?

	/**
	 * Class LinkBundleRecord
	 * @property mixed id
	 * @property mixed id_bundle
	 * @property mixed id_link
	 * @property mixed id_library
	 * @property mixed order
	 * @property mixed use_as_thumbnail
	 */
	class LinkBundleRecord extends CActiveRecord
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
			return '{{link_bundle}}';
		}

		/**
		 * @param string $linkId
		 * @param string $libraryId
		 * @param array $bundleItemsEncoded
		 */
		public static function updateData($linkId, $libraryId, $bundleItemsEncoded)
		{
			foreach ($bundleItemsEncoded as $bundleItemEncoded)
			{
				/** @var BaseLinkBundleItem $bundleItems */
				$bundleItem = CJSON::decode(CJSON::encode($bundleItemEncoded), false);
				switch ($bundleItem->itemType)
				{
					case 1:
						/** @var LibraryLinkBundleItem $bundleItem */
						$linkBundleRecord = new self();
						$linkBundleRecord->id = uniqid();
						$linkBundleRecord->id_bundle = $linkId;
						$linkBundleRecord->id_library = $libraryId;
						$linkBundleRecord->id_link = $bundleItem->libraryLinkId;
						$linkBundleRecord->order = $bundleItem->collectionOrder;
						$linkBundleRecord->use_as_thumbnail = $bundleItem->useAsThumbnail;
						$linkBundleRecord->save();
						break;
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
