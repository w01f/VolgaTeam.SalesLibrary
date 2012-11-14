<?php
class ContentController extends CController
{
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
                    'UniversalPreviewContainer' => 'UniversalPreviewContainer',
                    'Category' => 'Category',
                ),
            ),
        );
    }

    /**
     * @param Library Library
     * @soap
     */
    public function mockupLibrary($library)
    {
        
    }
    
        /**
     * @param Category[] Categories
     * @soap
     */
    public function mockupCategories($categories)
    {
        
    }
}

?>