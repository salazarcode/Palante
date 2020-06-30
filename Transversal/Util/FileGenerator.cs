using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Transversal.Util
{
    public static class FileGenerator
    {
        public static void CSV<type>(List<type> lista, string absolutePath)
        {
            try
            {
                List<PropertyInfo> propiedades = typeof(type).GetProperties().ToList();

                List<string> valComma = new List<string>();

                lista.ForEach(elem => {
                    List<dynamic> valores = new List<dynamic>();

                    propiedades.ForEach(prop => {
                        dynamic val = prop.GetValue(elem);
                        valores.Add(val);
                    });

                    string res = String.Join("", valores);
                    valComma.Add(res);
                });

                File.Delete(absolutePath);
                FileStream fs1 = new FileStream(absolutePath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);

                //writer.WriteLine(String.Join(",", propiedades.Select(x => x.Name).ToList()));
                valComma.ForEach(v => writer.WriteLine(v));

                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void Excel<Type>(List<Type> lista, string nombre, string absolutePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add(nombre);

                worksheet.Cells["A1"].LoadFromCollection(lista, PrintHeaders: true);

                for (var col = 1; col < lista.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: lista.Count + 1, toColumn: typeof(Type).GetProperties().Count()), nombre);
                tabla.ShowHeader = true;
                tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = false;

                libro.SaveAsAsync(new FileInfo(absolutePath));
            }
        }


        public static void ExcelReporteVariasPaginas(Dictionary<string, dynamic> dict, Dictionary<string, Type> dictTypes, string absolutePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var libro = new ExcelPackage())
            {

                foreach (var elem in dict)
                {
                    var nombre = elem.Key;  
                    var lista = elem.Value;

                    var worksheet = libro.Workbook.Worksheets.Add(nombre);
                    worksheet.Cells["A1"].LoadFromCollection(lista, PrintHeaders: true);

                    for (var col = 1; col < lista.Count + 1; col++)
                        worksheet.Column(col).AutoFit();

                    var propiedades = dictTypes[nombre].GetProperties().Count();
                    var addressBase = new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: lista.Count + 1, toColumn: propiedades);
                    var tabla = worksheet.Tables.Add(addressBase, nombre);

                    tabla.ShowHeader = true;
                    tabla.TableStyle = TableStyles.Light6;
                    tabla.ShowTotal = false;
                }

                libro.SaveAsAsync(new FileInfo(absolutePath));
            }
        }



        public static Byte[] ExcelToByteArray<Type>(List<Type> lista, string nombre)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add(nombre);

                worksheet.Cells["A1"].LoadFromCollection(lista, PrintHeaders: true);

                for (var col = 1; col < lista.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: lista.Count + 1, toColumn: typeof(Type).GetProperties().Count()), nombre);
                tabla.ShowHeader = true;
                tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = false;

                return libro.GetAsByteArray();
            }
        }
    }
}
