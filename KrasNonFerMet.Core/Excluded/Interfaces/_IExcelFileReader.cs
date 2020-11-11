using System;
using System.Collections.Generic;
using System.IO;
using KrasNonFerMet.DAL.IO.Files;
using OfficeOpenXml;

namespace KrasNonFerMet.Core.Interfaces
{
    [Obsolete]
    public interface IExcelFileReader<T> : IFileReader
    { 
        //TODO: check for EndsWith .xls or .xlsx // default method which gets field of `this` in impl classes 
        
        public ExcelPackage GetPackage()
        {
            /*if(String.IsNullOrWhiteSpace(Path))
                throw new ArgumentException("Path is null or empty",Path);
            if(String.IsNullOrWhiteSpace(Filename))
                throw new ArgumentException("Filename is null or empty", Filename);
            */
            var fileInfo = GetFileInfo();

            if (fileInfo.Exists)
            {
                return new ExcelPackage(fileInfo);
            } 
            
            throw new FileNotFoundException(String.Concat(new [] {"File ",FullPath, " not found"}));

        }

        public List<T> Read();

    }
}
