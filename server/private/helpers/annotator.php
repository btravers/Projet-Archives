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
			Database::exec("INSERT INTO AnnotationPageTable VALUES ('', ?, ?, ?, ?, ?, ?, ?)", array($idTable, $idUser, $x, $y, $width, $height, $number));
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
			Database::exec("INSERT INTO AnnotationSheet VALUES ('', ?, ?, ?, ?, ?, ?)", array($idSheet, $idTtype, $idUser, $x, $y, $annotation));
			return array("message" => "registered");
		}
	}

	/**
	 * Delete an annotation on a Table
	 */
	public static function delete_annotation_table($idAnnotationTable)
	{
		if (Database::existAnnotationTable($idAnnotationTable == -1)) {
			return array("message" => "annotation_not_found");
		} else {
			Database::exec("DELETE FROM AnnotationPageTable WHERE id_annotation_page_table = ?", array($idAnnotationTable));
			return array("message" => "deleted");
		}
	}

	/**
	 * Delete an annotation on a Sheet
	 */
	public static function delete_annotation_sheet($idAnnotationSheet)
	{
		if (Database::existAnnotationSheet($idAnnotationSheet) == -1) {
			return array("message" => "annotation_not_found");
		} else {
			Database::exec("DELETE FROM AnnotationSheet WHERE id_annotation_sheet = ?",array($idAnnotationSheet));
			return array("message" => "deleted");
		}
	}

	/**
	 * Update an annotation on a Table
	 */
	public static function update_annotation_table($id_session, $idAnnotationTable, $x, $y, $height, $width, $number)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("message" => "user_not_found");
		} elseif(Database::existAnnotationTable($idAnnotationTable == -1)) {
			return array("message" => "annotation_not_found");
		} else {
			Database::exec("UPDATE AnnotationPageTable SET x = ?, y = ?, width = ?, height = ?, id_number = ? WHERE id_annotation_page_table = ?", array($x, $y, $width, $height, $number, $idAnnotationTable));
			return array("message" => "updated");
		}
	}


	/**
	 * Update an annotation on a Sheet
	 */
	public static function update_annotation_table($id_session, $idAnnotationSheet, $idType, $x, $y, $annotation)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("message" => "user_not_found");
		} elseif (Database::existAnnotationSheet($idAnnotationSheet) == -1) {
			return array("message" => "annotation_not_found");
		} else {
			Database::exec("UPDATE AnnotationSheet SET x = ?, y = ?, id_type = ?, text = ? WHERE id_annotation_sheet = ?", array($x, $y, $type, $annotation, $idAnnotationSheet));
			return array("message" => "updated");
		}
	}
	
	public static function get_annotation_sheet($idSession, $idAnnotationSheet)
	{
		$idUser = Database::getUser($idSession);
	}
	
	public static function get_annotation_table($idSession, $idTable)
	{
		$idUser = Database::getUser($idSession);
	}
}