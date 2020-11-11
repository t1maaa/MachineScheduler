using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Win32;
using OfficeOpenXml;

namespace KrasNonFerMet.Core.Interfaces
{
    public interface IExcelWriter : IFileWriter
    {
        public ExcelPackage ExcelPackage { get; set; }

        public ExcelWorkbook Workbook { get; set; }

        public void Save()
        {
            ExcelPackage.Save();
        }

        public void SaveAs(FileInfo fileInfo)
        {
            ExcelPackage.SaveAs(fileInfo);
        }
    }
}