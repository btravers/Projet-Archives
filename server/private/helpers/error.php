<?php

/**
 * Error class
 * Error helper
 */
class Error
{
	const HelperNotFound = 0;

	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		echo "error " . $request->function;
	}
}