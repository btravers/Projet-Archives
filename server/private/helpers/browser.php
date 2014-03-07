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
	 * Get a page of a table
	 */
	public static function getPageTable($idTable, $page)
	{
		// TODO
		return array();
	}

	/**
	 * Get a table
	 */
	public static function getTable($idTable, $page, $year)
	{
		// TODO
		return array();
	}

	/**
	 * Get sheets between start and end
	 */
	public static function getFiche($idTable, $start, $end)
	{
		// TODO
		return array();
	}

	/**
	 * Get the sheet with the matricule number
	 */
	public static function getFiche($idTable, $numeroMatricule)
	{
		// TODO
		return array();
	}
}