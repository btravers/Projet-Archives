<?php

/**
 * Bootstrap class
 * Init application
 * Sets of functions
 */
class Bootstrap
{
	/**
	 * Request manager variable
	 * Entry point of the application
	 */
	private static $requestManager;

	/**
	 * Entry point
	 */
	public static function init()
	{		
		// include main files
		require_once("database.php");
		require_once("request_manager.php");

		Bootstrap::$requestManager = new RequestManager();
		Bootstrap::$requestManager->main();
	}
	
	/**
	 * Load specific module
	 * @see abstract class Module
	 */
	public static function loadModule($module)
	{
		// include module
		switch($module) {
			case Module::Authentificator:
				require_once("modules/authentificator.php");
				break;		
		}
	}
}

abstract class Module
{
	const Authentificator = 0;
}