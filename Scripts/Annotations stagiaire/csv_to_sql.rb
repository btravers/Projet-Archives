#!/usr/bin/env ruby
require 'csv'

def main
	if(ARGV.count < 1 || !ARGV[0][-4..-1].eql?(".csv"))
		puts 'Usage: ruby csv_to_sql.rb [csvFile]'
	else
		csvFile = ARGV[0]

		idAnnotationSheet, idAnnotationPageTable = 5, 0
		xSheetMargin, ySheetMargin, offsetXSheet = 750, 480, 1700
		xTableMargin, xTableGap, yTableMargin, width, height = 210, 420, 500, 800, 125
		idType = { :matNum => 3, :name => 4 }
		idUser = 1
		lastID = -1

		toBeAnnotatedArray = createToBeAnnotatedArray
		cptColumn, cptLine = 0, 0

		#puts toBeAnnotatedArray.inspect

		first = true
		CSV.foreach(csvFile) do |row|
			unless(first)

				imgName = "Fiches/" + row[0] + ".JPG"
				tableImgName = "Tables/" + row[3] + ".JPG"

				idAnnotationSheet += 1
				idSheet = row[0][-9..-1].tr('_','')
				name = row[2].tr("'", ' ')
				query = "INSERT INTO AnnotationSheet " \
					"VALUES (#{idAnnotationSheet}, #{idSheet}, #{idType[:name]}, #{idUser}, #{xSheetMargin}, #{ySheetMargin}, '#{name}');"
				#puts query

				idAnnotationSheet += 1
				idNumber = row[1]
				query = "INSERT INTO AnnotationSheet " \
					"VALUES (#{idAnnotationSheet}, #{idSheet}, #{idType[:matNum]}, #{idUser}, #{xSheetMargin + offsetXSheet}, #{ySheetMargin}, #{idNumber});"
				#puts query

				unless idNumber == lastID
					while(!toBeAnnotatedArray[row[3][-1..-1].to_i-1][cptColumn][cptLine])
						cptLine = (cptLine+1) % 18
						cptColumn = (cptLine == 0) ? (cptColumn+1)%4 : cptColumn
					end

					idAnnotationPageTable += 1
					idPageTable = row[3][-9..-1].tr('_','')
					xTable = xTableMargin + (cptColumn / 2).to_i * xTableGap + cptColumn * width
					yTable = yTableMargin + cptLine * height
					query = "INSERT INTO AnnotationPageTable " \
						"VALUES (#{idAnnotationPageTable}, #{idPageTable}, #{idUser}, #{xTable}, #{yTable}, #{width}, #{height}, #{idNumber}, #{idSheet});"
					puts query
				end
					
				cptLine = (cptLine+1) % 18
				cptColumn = (cptLine == 0) ? (cptColumn+1)%4 : cptColumn
				lastID = idNumber
			end
			first = false
		end
	end
end

# Initialisation du tableau determinant si une ligne est a annoter, ligne par ligne
def createToBeAnnotatedArray
	# Array of [page, column, line]
	array = Array.new(9) { Array.new(4) { Array.new(18) { true } } }

	# Picture 1
	array[0][0] = Array.new(18) { false }
	array[0][1] = Array.new(18) { false }
	array[0][2][0] = false
	array[0][2][17] = false
	array[0][3][0] = false
	array[0][3][17] = false

	# Picture 2
	array[1][0][17] = false
	array[1][1][17] = false
	array[1][2][4] = false
	array[1][3][17] = false

	# Picture 3
	array[2][0][11] = false
	array[2][0][17] = false
	array[2][1][17] = false
	array[2][2][12] = false
	array[2][2][17] = false
	array[2][3][4] = false
	array[2][3][17] = false

	# Picture 4
	array[3][0][13] = false
	array[3][0][17], array[3][1][17], array[3][2][17], array[3][3][17] = false

	# Picture 5
	array[4][0][0] = false
	array[4][1][7] = false
	array[4][1][9] = false
	array[4][2][3] = false
	array[4][2][6] = false
	array[4][0][17], array[4][1][17], array[4][2][17], array[4][3][17] = false

	# Picture 6
	array[5][3][3] = false
	array[5][0][17], array[5][1][17], array[5][2][17], array[5][3][17] = false

	# Picture 7
	array[6][1][9] = false
	array[6][1][13] = false
	array[6][2][1] = false
	array[6][3][15] = false
	array[6][0][17], array[6][1][17], array[6][2][17], array[6][3][17] = false

	# Picture 8
	array[7][0][2] = false
	array[7][2][7] = false
	array[7][3][3] = false
	array[7][0][17], array[7][1][17], array[7][2][17], array[7][3][17] = false

	# Picture 9
	array[8][0][2] = false
	array[8][0][3] = false
	array[8][0][4] = false
	array[8][0][16] = false
	array[8][0][17] = false
	array[8][1] = Array.new(18) { false }
	array[8][2] = Array.new(18) { false }
	array[8][3] = Array.new(18) { false }

	array
end

main