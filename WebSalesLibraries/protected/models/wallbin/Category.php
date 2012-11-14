<?php
class Category
{
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

    public function load($categoryRecord)
    {
        $this->category = $categoryRecord->category;
        $this->tag = $categoryRecord->tag;
    }
}

?>