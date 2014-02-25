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
	 * @see abstract class HelperEnum
	 */
	public static function loadHelper($helper)
	{
		// include helper
		switch($helper) {
			case HelperEnum::Authentificator:
				require_once("helpers/authentificator.php");
				$function = new ReflectionMethod('Authentificator::exec');
				return $function->getClosure();
			case HelperEnum::Annotator:
				require_once("helpers/annotator.php");
				$function = new ReflectionMethod('Annotator::exec');
				return $function->getClosure();
			default:
				require_once("helpers/error.php");
				$function = new ReflectionMethod('Error::exec');
				return $function->getClosure();
		}
	}
}

abstract class HelperEnum
{
	const Authentificator = 0;
	const Annotator = 1;
	const Error = 100;
}