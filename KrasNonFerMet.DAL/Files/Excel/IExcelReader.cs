using System;
using System.IO;
using OfficeOpenXml;

namespace KrasNonFerMet.DAL.Files.Excel
{
    public interface IExcelReader : IFileReader
    {
        private ExcelPackage GetPackage()
        {
            var fileInfo = GetFileInfo();

            if (fileInfo.Exists)
            {
                return new ExcelPackage(fileInfo);
            }

            throw new FileNotFoundException(String.Concat(new[] {"File ", FullPath, " not found"}));
        }

        public ExcelWorksheets GetWorksheets()
        {
            return GetPackage().Workbook.Worksheets;
        }
    }
}