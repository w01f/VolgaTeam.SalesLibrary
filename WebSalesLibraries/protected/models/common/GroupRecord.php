<?php

class GroupRecord
{
    /**
     * @var string id
     * @soap
     */
    public $id;
        /**
     * @var string name
     * @soap
     */
    public $name;
    /**
     * @var boolean selected
     * @soap
     */
    public $selected;    
    /**
     * @var UserRecord[]
     * @soap
     */
    public $users;
	/**
	 * @var boolean selected
	 * @soap
	 */
	public $allUsers;
    /**
     * @var Library[]
     * @soap
     */
    public $libraries;
	/**
	 * @var boolean selected
	 * @soap
	 */
	public $allLibraries;
}
