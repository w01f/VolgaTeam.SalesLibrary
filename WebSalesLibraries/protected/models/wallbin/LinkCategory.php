<?php
class LinkCategory
{
    /**
     * @var string
     * @soap
     */
    public $linkId;
    /**
     * @var string
     * @soap
     */
    public $libraryId;
    /**
     * @var string
     * @soap
     */
    public $category;
    /**
     * @var string
     * @soap
     */
    public $tag;
    
    public function __construct()
    {
    }

    public function load($linkCategoryRecord)
    {
        $this->linkId = $linkCategoryRecord->id_link;
        $this->libraryId = $linkCategoryRecord->id_library;
        $this->category = $linkCategoryRecord->category;
        $this->tag = $linkCategoryRecord->tag;
    }
}

?>