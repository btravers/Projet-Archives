<?php

/**
 * Browser class
 * Browser helper
 */
class Browser
{
	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		$function = new ReflectionMethod(get_called_class(), $request->function);
		return $function->invokeArgs(NULL, $request->arguments);
	}

	/**
	 * Get a Page of a Table
	 */
	public static function getPageTable($idRegister, $page)
	{
		// TODO
		return array();
	}

	/**
	 * Get a Table
	 */
	public static function getTable($idRegister, $page, $year)
	{
		// TODO
		return array();
	}

	/**
	 * Get Sheets between start and end
	 */
	public static function getSheet($idRegister, $start, $end)
	{
		// TODO
		return array();
	}

	/**
	 * Get the Sheet with the matricule number
	 */
	public static function getSheet($idRegister, $matriculeNumber)
	{
		// TODO
		return array();
	}
}