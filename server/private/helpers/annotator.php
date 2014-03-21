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
	public static function annotate_table($id_session, $idTable, $x, $y, $height, $width, $number)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("message" => "user_not_found");
		} elseif(Database::existPageTable($idTable) == -1) {
			return array("message" => "table_page_not_found");
		} else {			
			Database::exec("INSERT INTO AnnotationPageTable VALUES ('', '" . $idTable . "', '" . $idUser . "', '" . $x . "', '" . $y . "', '" . $width . "', '" . $height . "', '" . $number . "')");
			return array("message" => "registered");
		}
	}

	/**
	 * Annotate a Sheet with a position and a string
	 */
	public static function annotate_sheet($id_session, $idSheet, $idType, $x, $y, $annotation)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("message" => "user_not_found");
		} elseif(Database::existSheet($idSheet) == -1) {
			return array("message" => "sheet_page_not_found");
		} elseif(Database::existType($idType) == -1) {
			return array("message" => "type_not_found");
		} else {
			Database::exec("INSERT INTO AnnotationSheet VALUES ('', '" . $idSheet . "', '" . $idTtype . "', '" . $idUser . "' '" . $x . "', , '" . $y . "', '" . $annotation . "')");
			return array("message" => "registered");
		}
	}

	/**
	 * Delete an annotation on a Table
	 */
	public static function delete_annotation_table($idAnnotationTable)
	{
		if (Database::existAnnotationTable($idAnnotationTable)) {
			return array("message" => "annotation_not_found");
		} else {
			Database::exec("DELETE FROM AnnotationPageTable WHERE id_annotation_page_table = '" . $idAnnotationTable . "'");
			return array("message" => "deleted");
		}
	}

	/**
	 * Delete an annotation on a Sheet
	 */
	public static function delete_annotation_sheet($idAnnotationSheet)
	{
		if (Database::existAnnotationSheet($idAnnotationSheet)) {
			return array("message" => "annotation_not_found");
		} else {
			Database::exec("DELETE FROM AnnotationSheet WHERE id_annotation_sheet = '" . $idAnnotationSheet . "'");
			return array("message" => "deleted");
		}
	}

}