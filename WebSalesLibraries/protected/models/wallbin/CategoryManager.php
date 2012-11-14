<?php
class CategoryManager
{
    public $categories;
    public $groups;
    public function loadCategories()
    {
        $categoryRecords = CategoryStorage::getData();
        if (isset($categoryRecords))
        {
            foreach ($categoryRecords as $categoryRecord)
            {
                $category = new Category();
                $category->category = $categoryRecord->category;
                $category->tag = $categoryRecord->tag;
                $this->categories[] = $category;

                if (!(isset($this->groups) && in_array($category->category, $this->groups)))
                    $this->groups[] = $category->category;
            }
        }
    }

    public function getTagsByGroup($group)
    {
        if (isset($this->groups) && isset($this->categories))
        {
            foreach ($this->categories as $category)
                if ($category->category == $group)
                    $tags[] = $category->tag;
            if (isset($tags))
                return $tags;
        }
    }

}

?>
