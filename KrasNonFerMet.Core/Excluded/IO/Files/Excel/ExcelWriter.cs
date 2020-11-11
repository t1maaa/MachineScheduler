using System.Runtime.CompilerServices;
using KrasNonFerMet.Core.Interfaces;
using OfficeOpenXml;

namespace KrasNonFerMet.Core
{
    public class ExcelWriter : IExcelWriter
    {
        public string DirectoryPath { get; set; }
        public string Filename { get; set; }
        
        public ExcelPackage ExcelPackage { get; set; }
        public ExcelWorkbook Workbook { get; set; }

        public ExcelWriter()
        {
            ExcelPackage = new ExcelPackage();
            Workbook = ExcelPackage.Workbook;
        }
    }
}