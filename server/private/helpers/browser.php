<?php

/**
 * Browser class
 * Browser helper
 */
class Browser
{
	const MAT_NUMBER_TYPE = 0; // TODO Put the right value.

	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		$function = new ReflectionMethod(get_called_class(), $request->function);
		return $function->invokeArgs(NULL, $request->arguments);
	}

	/**
	 * Get a Page of a Table. If idUser isn't null, also return the annotations for this user.
	 */
	public static function get_page_table($idPageTable, $idUser, $page)
	{
		$tables = Database::query("SELECT * FROM PageTable WHERE id_page_table = ? AND page = ?", array($idPageTable, $page));
		if (count($tables) > 0) {
			$authenticated = ($idUser != null);

			$tables = array("helper" => "browser", "message" => "ok");
			foreach ($tables as $table) {
				$annotationsArray = array();

				if ($authenticated) {
					$annotations = Database::query("SELECT * FROM AnnotationPageTable WHERE id_page_table = ? AND id_user = ?", array($idPageTable, $idUser));
					foreach ($annotations as $annotation) {
						$annotationsArray[] = array("x" => $annotation[3],
							"y" => $annotation[4],
							"width" => $annotation[5],
							"height" => $annotation[6],
							"id_number" => $annotation[7]);
					}
				}
				$tables[] = array("id_page_table" => $idPageTable,
					"page" => $page,
					"image_src" => $table[3],
					"size" => $table[4],
					"width" => $table[5],
					"height" => $table[6],
					"annotations" => $annotationsArray);
			}
			return $tables;
		} else {
			return array("helper" => "browser", "message" => "null");
		}
	}

	/**
	 * Get a Table. If idUser isn't null, also return the annotations for this user.
	 */
	public static function get_table($idRegister, $idUser)
	{
		$tables = Database::query("SELECT * FROM PageTable WHERE id_register = ?", array($idRegister));
		if (count($tables) > 0) {
			$authenticated = ($idUser != null);

			$tables = array("helper" => "browser", "message" => "ok");
			foreach ($tables as $table) {
				$annotationsArray = array();

				if ($authenticated) {
					$annotations = Database::query("SELECT * FROM AnnotationPageTable WHERE id_page_table = ? AND id_user = ?", array($table[0], $idUser));
					foreach ($annotations as $annotation) {
						$annotationsArray[] = array("x" => $annotation[3],
							"y" => $annotation[4],
							"width" => $annotation[5],
							"height" => $annotation[6],
							"id_number" => $annotation[7]);
					}
				}

				$tables[] = array("id_page_table" => $table[0],
					"page" => $table[2],
					"image_src" => $table[3],
					"size" => $table[4],
					"width" => $table[5],
					"height" => $table[6],
					"annotations" => $annotationsArray);
			}
			return $tables;
		} else {
			return array("helper" => "browser", "message" => "null");
		}
	}

	/**
	 * Get Sheets between the pages start and end. If idUser isn't null, also return the annotations for this user.
	 */
	public static function get_sheets($idRegister, $idUser, $start, $end)
	{
		$sheets = Database::query("SELECT * FROM Sheet WHERE id_register =  ? AND page >= ? AND page <= ?", array($idRegister, $start, $end));
		if (count($sheets) > 0) {
			$authenticated = ($idUser != null);

			$sheets = array("helper" => "browser", "message" => "ok");
			foreach ($sheets as $sheet) {
				$annotationsArray = array();

				if ($authenticated) {
					$query = "SELECT x, y, label, text FROM AnnotationSheet, Type WHERE id_sheet = ? AND id_user = ?";

					$annotations = Database::query($query, array($sheet[0], $idUser));
					foreach ($annotations as $annotation) {
						$annotationsArray[] = array("x" => $annotation[0],
							"y" => $annotation[1],
							"type" => $annotation[2],
							"text" => $annotation[3]);
					}
				}

				$sheets[] = array("id_sheet" => $sheet[0],
					"page" => $sheet[2],
					"image_src" => $sheet[3],
					"size" => $sheet[4],
					"width" => $sheet[5],
					"height" => $sheet[6],
					"annotations" => $annotationsArray);
			}
			return $sheets;
		} else {
			return array("helper" => "browser", "message" => "null");
		}
	}

	/**
	 * Get the Sheet with the matricule number. If idUser isn't null, also return the annotations for this user.
	 */
	public static function get_sheet($idRegister, $idUser, $matriculeNumber)
	{
		$query = "SELECT Sheet.id_sheet, page, url, size, width, height FROM Sheet, AnnotationSheet "
			. "WHERE id_register =  ? AND id_type = ?";

		$sheets = Database::query($query, array($idRegister, self::MAT_NUMBER_TYPE));
		if (count($sheets) > 0) {
			$authenticated = ($idUser != null);

			$sheets = array("helper" => "browser", "message" => "ok");
			foreach ($sheets as $sheet) {
				$annotationsArray = array();

				if ($authenticated) {
					$query = "SELECT x, y, label, text FROM AnnotationSheet, Type "
						. "WHERE id_sheet = ? AND id_user = ?";

					$annotations = Database::query($query, array($sheet[0], $idUser));
					foreach ($annotations as $annotation) {
						$annotationsArray[] = array("x" => $annotation[0],
							"y" => $annotation[1],
							"type" => $annotation[2],
							"text" => $annotation[3]);
					}
				}

				$sheets[] = array("id_sheet" => $sheet[0],
					"page" => $sheet[1],
					"image_src" => $sheet[2],
					"size" => $sheet[3],
					"width" => $sheet[4],
					"height" => $sheet[5],
					"annotations" => $annotationsArray);
			}
			return $sheets;
		} else {
			return array("helper" => "browser", "message" => "null");
		}
	}
}