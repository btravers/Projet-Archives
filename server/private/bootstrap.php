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
		require_once("helpers/error.php");
		require_once("request.php");
		require_once("request_manager.php");

		Bootstrap::$requestManager = new RequestManager();
		Bootstrap::$requestManager->main();
	}
	
	/**
	 * Load specific helper
	 * @see abstract class Helper
	 */
	public static function loadHelper($helper)
	{
		// include helper
		switch($helper) {
			case Helper::Authentificator:
				require_once("helpers/authentificator.php");
				break;		
		}
	}
}

abstract class Helper
{
	const Authentificator = 0;
	const Error = 100;
}