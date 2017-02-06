<?php

	/**
	 * Class ContentController
	 */
	class ContentController extends SoapController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'Font' => 'Font',
						'SoapLibrary' => 'SoapLibrary',
						'SoapLibraryPage' => 'SoapLibraryPage',
						'SoapLibraryPageSettings' => 'SoapLibraryPageSettings',
						'SoapAutoWidget' => 'SoapAutoWidget',
						'SoapBanner' => 'SoapBanner',
						'SoapThumbnail' => 'SoapThumbnail',
						'SoapColumn' => 'SoapColumn',
						'SoapLibraryFolder' => 'SoapLibraryFolder',
						'SoapLibraryLink' => 'SoapLibraryLink',
						'BaseLinkSettings' => 'BaseLinkSettings',
						'VideoLinkSettings' => 'VideoLinkSettings',
						'DocumentLinkSettings' => 'DocumentLinkSettings',
						'HyperLinkSettings' => 'HyperLinkSettings',
						'PowerPointLinkSettings' => 'PowerPointLinkSettings',
						'AppLinkSettings' => 'AppLinkSettings',
						'InternalWallbinLinkSettings' => 'InternalWallbinLinkSettings',
						'InternalLibraryPageLinkSettings' => 'InternalLibraryPageLinkSettings',
						'InternalLibraryFolderLinkSettings' => 'InternalLibraryFolderLinkSettings',
						'InternalLibraryObjectLinkSettings' => 'InternalLibraryObjectLinkSettings',
						'InternalShortcutLinkSettings' => 'InternalShortcutLinkSettings',
						'ExcelLinkSettings' => 'ExcelLinkSettings',
						'QPageLinkSettings' => 'QPageLinkSettings',
						'LinkBundleLinkSettings' => 'LinkBundleLinkSettings',
						'BaseLinkBundleItem' => 'BaseLinkBundleItem',
						'LibraryLinkBundleItem' => 'LibraryLinkBundleItem',
						'UrlLinkBundleItem' => 'UrlLinkBundleItem',
						'LinkBundleLaunchScreenItem' => 'LinkBundleLaunchScreenItem',
						'LinkBundleInfoItem' => 'LinkBundleInfoItem',
						'LinkBundleStrategyItem' => 'LinkBundleStrategyItem',
						'LinkBundleRevenueItem' => 'LinkBundleRevenueItem',
						'LinkBundleRevenueInfoItem' => 'LinkBundleRevenueInfoItem',
						'LineBreak' => 'LineBreak',
						'LinkCategory' => 'LinkCategory',
						'LinkSuperFilter' => 'LinkSuperFilter',
						'UniversalPreviewContainer' => 'UniversalPreviewContainer',
						'References' => 'References',
						'Category' => 'Category',
						'SuperFilter' => 'SuperFilter',
						'LibraryConfig' => 'LibraryConfig',
					),
				),
			);
		}

		/**
		 * @param SoapLibrary $library
		 * @soap
		 */
		public function mockLibrary($library)
		{

		}

		/**
		 * @param VideoLinkSettings $settings
		 * @soap
		 */
		public function mockVideoLinkSettings($settings)
		{

		}

		/**
		 * @param HyperLinkSettings $settings
		 * @soap
		 */
		public function mockHyperLinkSettings($settings)
		{

		}

		/**
		 * @param PowerPointLinkSettings $settings
		 * @soap
		 */
		public function mockPowerPointLinkSettings($settings)
		{

		}

		/**
		 * @param DocumentLinkSettings $settings
		 * @soap
		 */
		public function mockPowerDocumentSettings($settings)
		{

		}

		/**
		 * @param AppLinkSettings $settings
		 * @soap
		 */
		public function mockAppLinkSettings($settings)
		{

		}

		/**
		 * @param InternalWallbinLinkSettings $settings
		 * @soap
		 */
		public function mockInternalWallbinLinkSettings($settings)
		{

		}

		/**
		 * @param InternalLibraryPageLinkSettings $settings
		 * @soap
		 */
		public function mockInternalLibraryPageLinkSettings($settings)
		{

		}

		/**
		 * @param InternalLibraryFolderLinkSettings $settings
		 * @soap
		 */
		public function mockInternalLibraryFolderLinkSettings($settings)
		{

		}

		/**
		 * @param InternalLibraryObjectLinkSettings $settings
		 * @soap
		 */
		public function mockInternalLibraryObjectLinkSettings($settings)
		{

		}

		/**
		 * @param InternalShortcutLinkSettings $settings
		 * @soap
		 */
		public function mockInternalShortcutLinkSettings($settings)
		{

		}

		/**
		 * @param ExcelLinkSettings $settings
		 * @soap
		 */
		public function mockExcelLinkSettings($settings)
		{

		}

		/**
		 * @param QPageLinkSettings $settings
		 * @soap
		 */
		public function mockQPageLinkSettings($settings)
		{

		}

		/**
		 * @param LinkBundleLinkSettings $settings
		 * @soap
		 */
		public function mockLinkBundleLinkSettings($settings)
		{

		}

		/**
		 * @param LibraryLinkBundleItem $item
		 * @soap
		 */
		public function mockLibraryLinkBundleItem($item)
		{

		}

		/**
		 * @param UrlLinkBundleItem $item
		 * @soap
		 */
		public function mockUrlLinkBundleItem($item)
		{

		}

		/**
		 * @param LinkBundleLaunchScreenItem $item
		 * @soap
		 */
		public function mockLinkBundleLaunchScreenItem($item)
		{

		}

		/**
		 * @param LinkBundleInfoItem $item
		 * @soap
		 */
		public function mockLinkBundleInfoItem($item)
		{

		}

		/**
		 * @param LinkBundleStrategyItem $item
		 * @soap
		 */
		public function mockLinkBundleStrategyItem($item)
		{

		}

		/**
		 * @param LinkBundleRevenueItem $item
		 * @soap
		 */
		public function mockLinkBundleRevenueItem($item)
		{

		}
	}
