<?php
class CategoryManager
{
    public $categories;
    public $groups;
    public function loadCategories()
    {
        $cookieId = 'selectedCategories';
        if (isset(Yii::app()->request->cookies['selectedRibbonTabId']->value))
            $cookieId.=Yii::app()->request->cookies['selectedRibbonTabId']->value;
        if (isset(Yii::app()->request->cookies[$cookieId]->value))
            $selectedCategories = CJSON::decode(Yii::app()->request->cookies[$cookieId]->value);

        $categoryRecords = CategoryStorage::getData();
        if (isset($categoryRecords))
        {
            foreach ($categoryRecords as $categoryRecord)
            {
                $category = new Category();
                $category->category = $categoryRecord->category;
                $category->tag = $categoryRecord->tag;
                $category->selected = false;
                if (isset($selectedCategories))
                    foreach ($selectedCategories as $selectedCategory)
                        if ($selectedCategory['category'] == $category->category && $selectedCategory['tag'] == $category->tag)
                        {
                            $category->selected = true;
                            break;
                        }
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
                    $tags[] = array('tag' => $category->tag, 'selected' => $category->selected);
            if (isset($tags))
                return $tags;
        }
        return null;
    }

}
