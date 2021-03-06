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
			case HelperEnum::Browser:
				require_once("helpers/browser.php");
				$function = new ReflectionMethod('Browser::exec');
				return $function->getClosure();
			case HelperEnum::Finder:
				require_once("helpers/finder.php");
				$function = new ReflectionMethod('Finder::exec');
				return $function->getClosure();
			case HelperEnum::File:
				require_once("helpers/file.php");
				$function = new ReflectionMethod('File::exec');
				return $function->getClosure();
			case HelperEnum::Shortcut:
				require_once("helpers/shortcut.php");
				$function = new ReflectionMethod('Shortcut::exec');
				return $function->getClosure();
			case HelperEnum::Bookmark:
				require_once("helpers/bookmark.php");
				$function = new ReflectionMethod('Bookmark::exec');
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
	const Browser = 2;
	const Finder = 3;
	const File = 4;
	const Shortcut = 5;
	const Bookmark = 6;
	const Error = 100;
}