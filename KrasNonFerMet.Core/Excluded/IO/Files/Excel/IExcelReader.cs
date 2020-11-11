using System;
using System.IO;
using OfficeOpenXml;

namespace KrasNonFerMet.Core.Interfaces
{
    public interface IExcelReader : IFileReader
    {//TODO: Make Props ExcelPackage And Worksheets
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