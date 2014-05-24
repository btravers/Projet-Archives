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
			return array("helper" => "annotator", "message" => "user_not_found");
		} else if(!Database::existPageTable($idTable)) {
			return array("helper" => "annotator", "message" => "table_page_not_found");
		} else {
			$query = "SELECT Sheet.id_sheet FROM PageTable, Sheet, AnnotationSheet "
					. "WHERE id_page_table = ? "
					. "AND PageTable.id_register = Sheet.id_register "
					. "AND Sheet.id_sheet = AnnotationSheet.id_sheet "
					. "AND id_type = 3 "
					. "AND AnnotationSheet.text = ?";
			$result = Database::query($query, array($idTable, $number));

			$idSheet = -1;
			if (count($result) > 0) {
				$idSheet = $result[0][0];
			}

			Database::exec("INSERT INTO AnnotationPageTable VALUES ('', ?, ?, ?, ?, ?, ?, ?, ?)", array($idTable, $idUser, $x, $y, $width, $height, $number, $idSheet));
			
			$result = Database::query("SELECT * FROM AnnotationPageTable WHERE id_page_table = ? AND id_user = ? AND x = ? AND y = ? AND width = ? AND height = ? AND id_number = ? AND id_sheet = ?", array($idTable, $idUser, $x, $y, $width, $height, $number, $idSheet));
			if (count($result) == 0) {
				return array("helper" => "annotator", "message" => "registration_error");
			} else {
				return array("helper" => "annotator", "message" => "registered");
			}
		}
	}

	/**
	 * Annotate a Sheet with a position and a string
	 */
	public static function annotate_sheet($id_session, $idSheet, $idType, $x, $y, $annotation)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("helper" => "annotator", "message" => "user_not_found");
		} else if(!Database::existSheet($idSheet)) {
			return array("helper" => "annotator", "message" => "sheet_page_not_found");
		} else if(!Database::existType($idType)) {
			return array("helper" => "annotator", "message" => "type_not_found");
		} else {
			// Updates page table annotations if idType is matricule number
			if($idType == 3) {
				$query = "SELECT AnnotationPageTable.id_annotation_page_table FROM PageTable, Sheet, AnnotationPageTable "
					. "WHERE Sheet.id_sheet = ? "
					. "AND PageTable.id_register = Sheet.id_register "
					. "AND PageTable.id_page_table = AnnotationPageTable.id_page_table "
					. "AND AnnotationPageTable.id_number = ? "
					. "AND AnnotationPageTable.id_sheet = '-1'";

				$result = Database::query($query, array($idSheet, $annotation));
				foreach ($result as $annotationPageTable) {
					Database::exec("UPDATE AnnotationPageTable SET id_sheet = ? WHERE id_annotation_page_table = ?", array($idSheet, $annotationPageTable[0]));
				}
			}

			Database::exec("INSERT INTO AnnotationSheet VALUES ('', ?, ?, ?, ?, ?, ?)", array($idSheet, $idType, $idUser, $x, $y, $annotation));
			
			$result = Database::query("SELECT * FROM AnnotationSheet WHERE id_sheet = ? AND id_user = ? AND id_type = ? AND x = ? AND y = ? AND text = ?", array($idSheet, $idUser, $idType, $x, $y, $annotation));
			if (count($result) == 0) {
				return array("helper" => "annotator", "message" => "registration_error");
			} else {
				return array("helper" => "annotator", "message" => "registered");
			}
		}
	}

	/**
	 * Delete an annotation on a Table
	 */
	public static function delete_annotation_table($id_session, $idAnnotationTable)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("helper" => "annotator", "message" => "user_not_found");
		} else if (!Database::existAnnotationTable($idAnnotationTable)) {
			return array("helper" => "annotator", "message" => "annotation_not_found");
		} else {
			Database::exec("DELETE FROM AnnotationPageTable WHERE id_annotation_page_table = ?", array($idAnnotationTable));
			return array("helper" => "annotator", "message" => "deleted");
		}
	}

	/**
	 * Delete an annotation on a Sheet
	 */
	public static function delete_annotation_sheet($id_session, $idAnnotationSheet)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("helper" => "annotator", "message" => "user_not_found");
		} else if (!Database::existAnnotationSheet($idAnnotationSheet)) {
			return array("helper" => "annotator", "message" => "annotation_not_found");
		} else {
			$result = Database::query("SELECT * FROM AnnotationSheet WHERE id_annotation_sheet = ? AND id_user = ?", array($idAnnotationSheet, $idUser));
			if(count($result) <= 0) {
				return array("helper" => "annotator", "message" => "bad_user");
			} else {
				Database::exec("DELETE FROM AnnotationSheet WHERE id_annotation_sheet = ?",array($idAnnotationSheet));
				return array("helper" => "annotator", "message" => "deleted");
			}
		}
	}

	/**
	 * Update an annotation on a Table
	 */
	public static function update_annotation_table($id_session, $idAnnotationTable, $x, $y, $height, $width, $number)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("helper" => "annotator", "message" => "user_not_found");
		} else if(!Database::existAnnotationTable($idAnnotationTable)) {
			return array("helper" => "annotator", "message" => "annotation_not_found");
		} else {
			Database::exec("UPDATE AnnotationPageTable SET x = ?, y = ?, width = ?, height = ?, id_number = ? WHERE id_annotation_page_table = ?", array($x, $y, $width, $height, $number, $idAnnotationTable));
			return array("helper" => "annotator", "message" => "updated");
		}
	}


	/**
	 * Update an annotation on a Sheet
	 */
	public static function update_annotation_sheet($id_session, $idAnnotationSheet, $idType, $x, $y, $annotation)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("helper" => "annotator", "message" => "user_not_found");
		} else if (!Database::existAnnotationSheet($idAnnotationSheet)) {
			return array("helper" => "annotator", "message" => "annotation_not_found");
		} else {
			Database::exec("UPDATE AnnotationSheet SET x = ?, y = ?, id_type = ?, text = ? WHERE id_annotation_sheet = ?", array($x, $y, $idType, $annotation, $idAnnotationSheet));
			return array("helper" => "annotator", "message" => "updated");
		}
	}

	public static function add_or_update_annotation_sheet($id_session, $idAnnotationSheet, $idSheet, $idType, $x, $y, $annotation)
	{
		$idUser = Database::getUser($id_session);
		if($idUser == -1) {
			return array("helper" => "annotator", "message" => "user_not_found");
		} else {
			$result = Database::query("SELECT * FROM AnnotationSheet WHERE id_annotation_sheet = ?", array($idAnnotationSheet));
			if($result[0]["id_user"] == $idUser) {
				annotator::update_annotation_sheet($id_session, $idAnnotationSheet, $idType, $x, $y, $annotation);
				return array("helper" => "annotator", "message" => "updated");
			} else {
				annotator::annotate_sheet($id_session, $idSheet, $idType, $x, $y, $annotation);
				return array("helper" => "annotator", "message" => "registered");
			}
		}

	}
	
	public static function get_types()
	{
		$resultTypes = Database::query("SELECT * FROM Type", array());
		
		$data = array();
		
		foreach ($resultTypes as $type)
		{
			$idType = $type['id_type'];
			
			$data[$idType]['label'] = $type['label'];
		}
		
		if(count($data) > 0) {
			return array("helper" => "annotator", "message" => "result_found", "result" => $data);
		} else {
			return array("helper" => "annotator", "message" => "result_not_found");
		}
	}
	
	/**
	 * Get all annotations by sheet
	 */
	public static function get_annotation_sheet($idSession, $idSheet)
	{
		if(!is_numeric($idSheet))
			return array("helper" => "annotator", "message" => "id_sheet_not_numeric");
		
		$data = array();
		
		$idUser = Database::getUser($idSession);
		
		$resultAnnotations = Database::query("SELECT * FROM AnnotationSheet WHERE id_sheet = ?", array($idSheet));
		foreach ($resultAnnotations as $annotation)
		{
			$idAnnotation = $annotation['id_annotation_sheet'];

			$data[$idAnnotation]['type'] = $annotation['id_type'];
			$data[$idAnnotation]['x'] = $annotation['x'];
			$data[$idAnnotation]['y'] = $annotation['y'];
			$data[$idAnnotation]['text'] = $annotation['text'];
			
			if($idUser == $annotation['id_user']) {
				$data[$idAnnotation]['user'] = "-1";
			} else {
				$data[$idAnnotation]['user'] = Database::getUserName($annotation['id_user']);
			}
		}
		
		if(count($data) > 0) {
			return array("helper" => "annotator", "message" => "result_found", "result" => $data);
		} else {
			return array("helper" => "annotator", "message" => "result_not_found");
		}
	}
	
	/**
	 * Get all annotations by table
	 */
	public static function get_annotation_table($idSession, $idTable)
	{
		if(!is_numeric($idTable))
			return array("helper" => "annotator", "message" => "id_table_not_numeric");
		
		$data = array();
		
		$idUser = Database::getUser($idSession);
		
		$resultAnnotations = Database::query("SELECT * FROM AnnotationPageTable WHERE id_page_table = ?", array($idTable));
		foreach ($resultAnnotations as $annotation)
		{
			$idAnnotation = $annotation['id_annotation_page_table'];
			
			$data[$idAnnotation]['x'] = $annotation['x'];
			$data[$idAnnotation]['y'] = $annotation['y'];
			$data[$idAnnotation]['width'] = $annotation['width'];
			$data[$idAnnotation]['height'] = $annotation['height'];
			$data[$idAnnotation]['id_number'] = $annotation['id_number'];
			$data[$idAnnotation]['id_sheet'] = $annotation['id_sheet'];
			
			if($idUser == $annotation['id_user']) {
				$data[$idAnnotation]['user'] = "-1";
			} else {
				$data[$idAnnotation]['user'] = Database::getUserName($annotation['id_user']);
			}
		}
		
		if(count($data) > 0) {
			return array("helper" => "annotator", "message" => "result_found", "result" => $data);
		} else {
			return array("helper" => "annotator", "message" => "result_not_found");
		}
	}
}