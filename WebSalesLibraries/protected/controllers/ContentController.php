<?php
	/**
	 * Class ContentController
	 */
	class ContentController extends SoapController
	{
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
						'SoapAutoWidget' => 'SoapAutoWidget',
						'SoapBanner' => 'SoapBanner',
						'SoapColumn' => 'SoapColumn',
						'SoapLibraryFolder' => 'SoapLibraryFolder',
						'SoapLibraryLink' => 'SoapLibraryLink',
						'BaseLinkSettings' => 'BaseLinkSettings',
						'VideoLinkSettings' => 'VideoLinkSettings',
						'HyperLinkSettings' => 'HyperLinkSettings',
						'PowerPointLinkSettings' => 'PowerPointLinkSettings',
						'AppLinkSettings' => 'AppLinkSettings',
						'InternalLinkSettings' => 'InternalLinkSettings',
						'ExcelLinkSettings' => 'ExcelLinkSettings',
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
		 * @param AppLinkSettings $settings
		 * @soap
		 */
		public function mockAppLinkSettings($settings)
		{

		}

		/**
		 * @param InternalLinkSettings $settings
		 * @soap
		 */
		public function mockInternalLinkSettings($settings)
		{

		}

		/**
		 * @param ExcelLinkSettings $settings
		 * @soap
		 */
		public function mockExcelLinkSettings($settings)
		{

		}
	}
