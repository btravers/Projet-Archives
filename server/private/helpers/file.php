<?php

/**
 * File class
 * File manager helper
 */
class File
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
	 * Print file content
	 * Extension must be .JPG and / must be replaced by -
	 */
	public static function print_file($path) {
		$path = str_replace("-", "/", $path);
		
		if(file_exists("../private/" . $path) && substr($path, strlen($path) - 4, strlen($path)) == ".JPG") {
			readfile("../private/" . $path);			
		}
		
		exit();
	}
}