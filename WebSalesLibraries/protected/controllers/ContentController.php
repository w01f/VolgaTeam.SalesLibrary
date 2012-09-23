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
                    'UniversalPreviewContainer' => 'UniversalPreviewContainer',
                ),
            ),
        );
    }

    /**
     * @param Library Library
     * @soap
     */
    public function mockup($library)
    {
    }
}

?>