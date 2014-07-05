<?php

	/**
	 * Class ContentController
	 */
	class ContentController extends CController
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
						'LineBreak' => 'LineBreak',
						'FileCard' => 'FileCard',
						'Attachment' => 'Attachment',
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
		 * @param References $references
		 * @soap
		 */
		public function mockReferences($references)
		{

		}
	}
