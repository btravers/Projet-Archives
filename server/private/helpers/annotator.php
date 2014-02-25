<?php

/**
 * Authentificator class
 * Authentificator helper
 */
class Annotator
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
	 * Annotate a Table with a position and a number
	 */
	public static function annotate_table($id_session, $idTable, $page, $x, $y, $height, $width, $number)
	{
		$idUser = Database::getUser($sessionId);
		if($idUser != -1) {
			Database::exec("INSERT INTO AnnotationPageTable VALUES ('', '" . $idTable . "', '" . $idUser . "', '" . $x . "', , '" . $y . "', '" . $width . "', '" . $height . "', '" . $number . "')");
			return array("message" => "registered");
		} else {
			return array("message" => "user_not_found");
		}
	}

	/**
	 * Annotate a Sheet with a position and a string
	 */
	public static function annotate_sheet($id_session, $idSheet, $idType, $x, $y, $annotation)
	{
		$idUser = Database::getUser($sessionId);
		if($idUser != -1) {
			Database::exec("INSERT INTO AnnotationSheet VALUES ('', '" . $idSheet . "', '" . $idTtype . "', '" . $idUser . "' '" . $x . "', , '" . $y . "', '" . $annotation . "')");
			return array("message" => "registered");
		} else {
			return array("message" => "user_not_found");
		}
	}

}