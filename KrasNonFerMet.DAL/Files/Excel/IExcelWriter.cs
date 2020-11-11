using System.IO;
using OfficeOpenXml;

namespace KrasNonFerMet.DAL.Files.Excel
{
    public interface IExcelWriter : IFileWriter
    {
        public ExcelPackage ExcelPackage { get; set; }
        
        public void Save()
        {
            ExcelPackage.Save();
        }

        public void SaveAs(FileInfo fileInfo)
        {   //TODO: Exception if file blocked or  used by another process
            //TODO: file saving (1.file.Extension == ".xlsx" ? 'continue' : concat('.xlsx')
            ExcelPackage.SaveAs(fileInfo);
        }
    }
}