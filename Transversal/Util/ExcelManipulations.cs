using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace Transversal.Util
{
    public static class ExcelManipulations
    {
        public static string Create(string baseDir) {
            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                if (ExcelApp == null)
                {
                    throw new Exception("Excel no está instalado");
                }

                Excel.Workbook libro = ExcelApp.Workbooks.Add("Libro1");

                Excel.Worksheet pagina = (Excel.Worksheet)libro.Worksheets[0];

                pagina.Cells[1, 1] = "ID";
                pagina.Cells[1, 2] = "Nombres";

                pagina.Cells[2, 1] = "45";
                pagina.Cells[2, 2] = "Andrés";

                string savePath = "miexcel.xlsx";

                libro.SaveAs(savePath);

                return savePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
