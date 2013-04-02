<?php

class UserRecord
{
    /**
     * @var int id
     * @soap
     */
    public $id;    
    /**
     * @var string login
     * @soap
     */
    public $login;
        /**
     * @var string password
     * @soap
     */
    public $password;
    /**
     * @var string first Name
     * @soap
     */    
    public $firstName;
    /**
     * @var string last Name
     * @soap
     */    
    public $lastName;
    /**
     * @var string email
     * @soap
     */        
    public $email;
	/**
	 * @var string email
	 * @soap
	 */
	public $phone;
    /**
     * @var boolean selected
     * @soap
     */
    public $selected;    
    /**
     * @var GroupRecord[]
     * @soap
     */
    public $groups;
	/**
	 * @var boolean selected
	 * @soap
	 */
	public $allGroups;
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
