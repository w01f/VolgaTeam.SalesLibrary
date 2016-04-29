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
						'Library' => 'Library',
						'LibraryPage' => 'LibraryPage',
						'AutoWidget' => 'AutoWidget',
						'Banner' => 'Banner',
						'Column' => 'Column',
						'LibraryFolder' => 'LibraryFolder',
						'LibraryLink' => 'LibraryLink',
						'BaseLinkSettings' => 'BaseLinkSettings',
						'VideoLinkSettings' => 'VideoLinkSettings',
						'HyperLinkSettings' => 'HyperLinkSettings',
						'PowerPointLinkSettings' => 'PowerPointLinkSettings',
						'AppLinkSettings' => 'AppLinkSettings',
						'InternalLinkSettings' => 'InternalLinkSettings',
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
		 * @param Library $library
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
	}
