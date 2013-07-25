<?php
class LinkSuperFilter
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
    public $value;

    public function __construct()
    {
    }

    public function load($linkSuperFilterRecord)
    {
        $this->linkId = $linkSuperFilterRecord->id_link;
        $this->libraryId = $linkSuperFilterRecord->id_library;
        $this->category = $linkSuperFilterRecord->value;
    }
}
