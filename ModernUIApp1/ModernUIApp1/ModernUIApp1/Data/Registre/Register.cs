using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Registre
{
    public class Register
    {
        int id_register;

        String location;
        int year;
        int volume;

        /* Dictionnary contains all page table which refers to the register */
        Dictionary<int, PageTable> pages_table;

        /* Dictionnary contains all sheet which refers to the register */
        Dictionary<int, Sheet> sheets;

        /* Constructors */
        public Register()
        {
            pages_table = new Dictionary<int, PageTable>();
            sheets = new Dictionary<int, Sheet>();
        }

        public Register(int id_register, String location, int year, int volume)
        {
            this.id_register = id_register;
            this.location = location;
            this.year = year;
            this.volume = volume;

            pages_table = new Dictionary<int, PageTable>();
            sheets = new Dictionary<int, Sheet>();
        }

        /* Add a page table to the Dictionnary */
        public void addPageTable(PageTable new_page_table)
        {
            pages_table.Add(new_page_table.id_page_table, new_page_table);
        }

        /* Add a sheet to the Dictionnary */
        public void addSheet(Sheet new_sheet)
        {
            sheets.Add(new_sheet.id_sheet, new_sheet);
        }
    }
}
